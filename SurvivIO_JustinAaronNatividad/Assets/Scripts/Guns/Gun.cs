using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour
{
    public event Action<string> OnClipChange;

    [SerializeField] private Transform bulletSpawn;

    [Header("Gun Data")]
    public string gunName;
    public Sprite gunIcon;
    public GunType gunType;
    public AmmoType ammoType;

    [Header("Fire Rate")]
    public float fireRate;
    public bool isAutomatic;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletDamage;
    [SerializeField] private float bulletSpread;
    [SerializeField] private int bulletCount;

    [Header("Reload Parameters")]
    [SerializeField] private int clipCapacity;
    [SerializeField] private float reloadTime;

    [HideInInspector] public InventoryComponent inventory;
    [HideInInspector] public bool isReloading = false;
    protected int currentClip = 0;
    protected float fireCooldown = 0;
    protected bool hasFired = false;

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
            if (hasFired == false && isFiring)
            {
                hasFired = true;
                TrySpawnBullet();
            }
            else if (!isFiring)
            {
                hasFired = false;
            }
        }
    }

    public string GetClipInfo()
    {
        return "Clip: " + currentClip + "/" + inventory.GetAmmo(ammoType);
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
                float randomSpread = Random.Range(-bulletSpread/2, bulletSpread/2);
                Projectile spawnedProjectile = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation).GetComponent<Projectile>();
                spawnedProjectile.transform.Rotate(new Vector3(0, 0, randomSpread));
                spawnedProjectile.Initialize(bulletDamage);
            }
        }
    }

    private void TryReload()
    {
        int reloadAmount;
        if(currentClip <= 0 && (inventory.GetAmmo(ammoType) > 0 ||  inventory.infiniteAmmo))
        {
            if (inventory.infiniteAmmo || inventory.GetAmmo(ammoType) >= clipCapacity)
            {
                reloadAmount = clipCapacity;
            }
            else
            {
                reloadAmount = inventory.GetAmmo(ammoType);
            }
            
            StartCoroutine(CO_Reload(reloadAmount));
        }
    }

    private IEnumerator CO_Reload(int amount)
    {
        OnClipChange?.Invoke("Reloading...");
        isReloading = true;

        yield return new WaitForSeconds(reloadTime * inventory.reloadTimeMultiplier);
        isReloading = false;
        inventory.ModifyAmmo(ammoType, -amount);
        currentClip = amount;
        OnClipChange?.Invoke(GetClipInfo());
    }
}
