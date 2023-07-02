using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    public delegate void AmmoChangeDelegate(int pistolAmmo, int machinegunAmmo, int shotgunAmmo);
    public AmmoChangeDelegate OnAmmoChange;

    public int maxPistolAmmo;
    public int maxMachineGunAmmo;
    public int maxShotgunAmmo;

    public int pistolAmmo
    {
        get;
        private set;
    }

    public int machinegunAmmo
    {
        get;
        private set;
    }

    public int shotgunAmmo
    {
        get;
        private set;
    }

    private void Start()
    {
        pistolAmmo = 0;
        machinegunAmmo = 0;
        shotgunAmmo = 0;
    }

    public void AddAmmo(AmmoType type, int amount)
    {
        switch (type)
        {
            case AmmoType.PistolAmmo:
                {
                    pistolAmmo = Mathf.Clamp(pistolAmmo + amount, 0, maxPistolAmmo);
                }
                break;
            case AmmoType.MachineGunAmmo:
                {
                    machinegunAmmo = Mathf.Clamp(machinegunAmmo + amount, 0, maxMachineGunAmmo);
                }
                break;
            case AmmoType.ShotgunAmmo:
                {
                    shotgunAmmo = Mathf.Clamp(shotgunAmmo + amount, 0, maxShotgunAmmo);
                }
                break;
        }

        OnAmmoChange?.Invoke(pistolAmmo, machinegunAmmo, shotgunAmmo);
    }

    public bool IsAmmoFull(AmmoType type)
    {
        switch (type)
        {
            case AmmoType.PistolAmmo:
                {
                    return pistolAmmo >= maxPistolAmmo;
                }
            case AmmoType.MachineGunAmmo:
                {
                    return machinegunAmmo >= maxMachineGunAmmo;
                }
            case AmmoType.ShotgunAmmo:
                {
                    return shotgunAmmo >= maxShotgunAmmo;
                }
        }
        return false;
    }
}
