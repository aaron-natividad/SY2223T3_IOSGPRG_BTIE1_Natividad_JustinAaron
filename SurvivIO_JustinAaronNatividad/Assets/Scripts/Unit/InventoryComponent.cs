using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    public delegate void AmmoChangeDelegate(int pistolAmmo, int machinegunAmmo, int shotgunAmmo);
    public AmmoChangeDelegate OnAmmoChange;

    [SerializeField] private Transform gunPosition;
    [Space(10)]
    [SerializeField] private int maxPistolAmmo;
    [SerializeField] private int maxMachineGunAmmo;
    [SerializeField] private int maxShotgunAmmo;

    private Gun primaryGun;
    private Gun secondaryGun;

    private int pistolAmmo = 0;
    private int machinegunAmmo = 0;
    private int shotgunAmmo = 0;

    private bool equippedPrimary = true;

    public void ModifyAmmo(AmmoType type, int amount)
    {
        if (type == AmmoType.PistolAmmo)
        {
            pistolAmmo = Mathf.Clamp(pistolAmmo + amount, 0, maxPistolAmmo);
        }
        else if (type == AmmoType.MachineGunAmmo)
        {
            machinegunAmmo = Mathf.Clamp(machinegunAmmo + amount, 0, maxMachineGunAmmo);
        }
        else if (type == AmmoType.ShotgunAmmo)
        {
            shotgunAmmo = Mathf.Clamp(shotgunAmmo + amount, 0, maxShotgunAmmo);
        }

        OnAmmoChange?.Invoke(pistolAmmo, machinegunAmmo, shotgunAmmo);
    }

    public int GetAmmo(AmmoType type)
    {
        if (type == AmmoType.PistolAmmo)
        {
            return pistolAmmo;
        }
        else if (type == AmmoType.MachineGunAmmo)
        {
            return machinegunAmmo;
        }
        else if (type == AmmoType.ShotgunAmmo)
        {
            return shotgunAmmo;
        }
        else
        {
            return -1;
        }
    }

    public void AddGun(GameObject gunPrefab)
    {
        Gun newGun = Instantiate(gunPrefab, gunPosition).GetComponent<Gun>();
        newGun.inventory = this;

        if (primaryGun == null)
        {
            primaryGun = newGun;
        }
        else if (secondaryGun == null)
        {
            secondaryGun = newGun;
        }

        UIManager.instance.primaryFrame.SetFrameInfo(primaryGun, secondaryGun);
        UIManager.instance.secondaryFrame.SetFrameInfo(secondaryGun, primaryGun);
        SetEquippedGun(equippedPrimary);
    }

    public Gun GetEquippedGun()
    {
        if (equippedPrimary)
        {
            return primaryGun;
        }
        else
        {
            return secondaryGun;
        }
    }

    public void SwapWeapon()
    {
        if (equippedPrimary)
        {
            if (secondaryGun != null)
            {
                SetEquippedGun(false);
                UIManager.instance.SwapWeaponUI();
            }
        }
        else
        {
            if (primaryGun != null)
            {
                SetEquippedGun(true);
                UIManager.instance.SwapWeaponUI();
            }
        }
    }

    public bool IsAmmoFull(AmmoType type)
    {
        if (type == AmmoType.PistolAmmo)
        {
            return pistolAmmo >= maxPistolAmmo;
        }
        else if (type == AmmoType.MachineGunAmmo)
        {
            return machinegunAmmo >= maxMachineGunAmmo;
        }
        else if (type == AmmoType.ShotgunAmmo)
        {
            return shotgunAmmo >= maxShotgunAmmo;
        }
        else
        {
            return false;
        }
    }

    public bool IsValidGun(string gunName)
    {
        if (primaryGun != null && secondaryGun != null)
        {
            return false;
        }

        if (primaryGun != null)
        {
            return primaryGun.gunName != gunName;
        }

        return true;
    }

    private void SetEquippedGun(bool equippedPrimary)
    {
        this.equippedPrimary = equippedPrimary;

        if (primaryGun != null)
        {
            primaryGun.gameObject.SetActive(equippedPrimary);
        }
        
        if (secondaryGun != null)
        {
            secondaryGun.gameObject.SetActive(!equippedPrimary);
        }
    }
}
