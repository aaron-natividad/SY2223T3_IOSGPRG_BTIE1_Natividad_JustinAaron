using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    public event Action<int[]> OnAmmoUpdate;
    public event Action<Gun[], GunType> OnGunUpdate;

    [SerializeField] private Transform gunPosition;
    [Space(10)]
    [SerializeField] private int[] maxAmmo;
    [Space(10)]
    public float reloadTimeMultiplier;
    public bool infiniteAmmo;
    public bool canLoot;

    private Gun[] gunSlot = new Gun[2] { null, null };
    private int[] ammo = new int[4] { 0, 0, 0, 0 };

    private GunType equippedType = GunType.Primary;

    #region Public Methods
    public void ModifyAmmo(AmmoType type, int amount)
    {
        ammo[(int)type] = Mathf.Clamp(ammo[(int)type] + amount, 0, maxAmmo[(int)type]);
        OnAmmoUpdate?.Invoke(ammo);
    }

    public void AddGun(GameObject gunPrefab)
    {
        Gun newGun = Instantiate(gunPrefab, gunPosition).GetComponent<Gun>();
        newGun.inventory = this;

        if (gunSlot[(int)newGun.gunType] != null)
        {
            Destroy(gunSlot[(int)newGun.gunType].gameObject);
        }
        gunSlot[(int)newGun.gunType] = newGun;

        OnGunUpdate?.Invoke(gunSlot, equippedType);
        SetEquippedGun(equippedType);
    }

    public void SwapWeapon()
    {
        if (GetEquippedGun() != null)
        {
            if (GetEquippedGun().isReloading)
            {
                return;
            }
        }

        if (equippedType == GunType.Primary)
        {
            if (gunSlot[1] != null)
            {
                SetEquippedGun(GunType.Secondary);
            }
        }
        else
        {
            if (gunSlot[0] != null)
            {
                SetEquippedGun(GunType.Primary);
            }
        }
    }

    public void SetEquippedGun(GunType type)
    {
        foreach (Gun gun in gunSlot)
        {
            if (gun != null)
            {
                gun.gameObject.SetActive(false);
            }
        }

        if (gunSlot[(int)type] != null)
        {
            equippedType = type;
            gunSlot[(int)type].gameObject.SetActive(true);
        }

        OnGunUpdate?.Invoke(gunSlot, equippedType);
    }

    public bool IsAmmoFull(AmmoType type)
    {
        return ammo[(int)type] >= maxAmmo[(int)type];
    }
    #endregion

    #region Getters
    public int[] GetAmmoArray()
    {
        return ammo;
    }

    public int GetAmmo(AmmoType type)
    {
        return ammo[(int)type];
    }

    public Gun GetGun(GunType type)
    {
        return gunSlot[(int)type];
    }

    public Gun GetEquippedGun()
    {
        return gunSlot[(int)equippedType];
    }
    #endregion
}
