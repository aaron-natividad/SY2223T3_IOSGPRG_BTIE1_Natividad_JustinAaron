using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public delegate void ClipChangeDelegate(string message);
    public ClipChangeDelegate OnClipChange;

    public InventoryComponent inventory;
    public Transform bulletSpawn;

    [Header("Gun Parameters")]
    public string gunName;
    public Sprite gunIcon;
    [Space(10)]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AmmoType bulletType;
    [SerializeField] private int bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletSpread;
    [SerializeField] private int bulletCount;
    [Space(10)]
    [SerializeField] private int clipCapacity;
    [SerializeField] private float reloadTime;
    [Space(10)]
    [SerializeField] private float fireRate;
    [SerializeField] private bool isAutomatic;

    protected int currentClip = 0;
    protected float fireCooldown = 0;
    protected bool semiAutoFlag = false;
    protected bool isReloading = false;
    protected Coroutine reloadCoroutine;

    private void OnDisable()
    {
        OnClipChange = null;
    }

    public void Fire(bool isFiring)
    {
        if (isReloading)
        {
            return;
        }

        if (isAutomatic)
        {
            if (fireCooldown > 0)
            {
                fireCooldown -= Time.deltaTime;
            }
            else if (isFiring)
            {
                fireCooldown = fireRate;
                TrySpawnBullet();
            }
        }
        else
        {
            if (semiAutoFlag == false && isFiring)
            {
                semiAutoFlag = true;
                TrySpawnBullet();
            }
            else if (!isFiring)
            {
                semiAutoFlag = false;
            }
        }
    }

    public string GetClipInfo()
    {
        return "Clip: " + currentClip + "/" + inventory.GetAmmo(bulletType);
    }

    private void TrySpawnBullet()
    {
        TryReload();

        if(currentClip > 0)
        {
            currentClip--;
            OnClipChange?.Invoke(GetClipInfo());

            for (int i = 0; i < bulletCount; i++)
            {
                float randomSpread = Random.Range(-bulletSpread, bulletSpread);
                Projectile p = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation).GetComponent<Projectile>();
                p.transform.Rotate(new Vector3(0, 0, randomSpread));
                p.Initialize(bulletSpeed);
            }
        }
    }

    private void TryReload()
    {
        if(currentClip <= 0 && inventory.GetAmmo(bulletType) > 0)
        {
            int reloadAmount = inventory.GetAmmo(bulletType) >= clipCapacity ? clipCapacity : inventory.GetAmmo(bulletType);
            reloadCoroutine = StartCoroutine(CO_Reload(reloadAmount));
        }
    }

    private IEnumerator CO_Reload(int amount)
    {
        OnClipChange?.Invoke("Reloading...");
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        inventory.ModifyAmmo(bulletType, -amount);
        currentClip = amount;
        OnClipChange?.Invoke(GetClipInfo());
        reloadCoroutine = null;
    }
}
