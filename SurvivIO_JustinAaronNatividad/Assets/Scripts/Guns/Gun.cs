using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public delegate void ClipChangeDelegate(string message);
    public ClipChangeDelegate OnClipChange;

    [Header("Components")]
    public InventoryComponent inventory;
    [SerializeField] private Transform bulletSpawn;

    [Header("Gun Data")]
    public string gunName;
    public Sprite gunIcon;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AmmoType bulletType;
    [SerializeField] private int bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletSpread;
    [SerializeField] private int bulletCount;

    [Header("Reload Parameters")]
    [SerializeField] private int clipCapacity;
    [SerializeField] private float reloadTime;

    [Header("Fire Rate")]
    [SerializeField] private float fireRate;
    public bool isAutomatic;

    protected int currentClip = 0;
    protected float fireCooldown = 0;
    protected bool semiAutoFlag = false;
    protected bool isReloading = false;
    protected Coroutine reloadCoroutine;

    private void OnDisable()
    {
        OnClipChange = null;
    }

    // Function to be hooked to aim component
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
        int reloadAmount;
        if(currentClip <= 0 && (inventory.GetAmmo(bulletType) > 0 ||  inventory.infiniteAmmo))
        {
            if (inventory.infiniteAmmo || inventory.GetAmmo(bulletType) >= clipCapacity)
            {
                reloadAmount = clipCapacity;
            }
            else
            {
                reloadAmount = inventory.GetAmmo(bulletType);
            }
            
            reloadCoroutine = StartCoroutine(CO_Reload(reloadAmount));
        }
    }

    private IEnumerator CO_Reload(int amount)
    {
        OnClipChange?.Invoke("Reloading...");
        isReloading = true;

        yield return new WaitForSeconds(reloadTime * inventory.reloadTimeMultiplier);
        isReloading = false;
        inventory.ModifyAmmo(bulletType, -amount);
        currentClip = amount;
        OnClipChange?.Invoke(GetClipInfo());
        reloadCoroutine = null;
    }
}
