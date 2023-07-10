using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject unit;

    [Header("UI Components")]
    public Image healthbar;
    public TextMeshProUGUI clipText;
    [Space(10)]
    public TextMeshProUGUI pistolAmmoText;
    public TextMeshProUGUI machinegunAmmoText;
    public TextMeshProUGUI shotgunAmmoText;
    [Space(10)]
    public Frame primaryFrame;
    public Frame secondaryFrame;

    private HealthComponent health;
    private InventoryComponent inventory;

    private void OnEnable()
    {
        GetUnitComponents();
        if (health != null)
        {
            health.OnHealthChange += UpdateHealth;
        }

        if (inventory != null)
        {
            inventory.OnAmmoChange += UpdateAmmo;
            inventory.OnAddGun += UpdateGunUI;
        }
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.OnHealthChange -= UpdateHealth;
        }

        if (inventory != null)
        {
            inventory.OnAmmoChange -= UpdateAmmo;
            inventory.OnAddGun -= UpdateGunUI;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GetUnitComponents();
        ForceUpdateUI();
    }

    public void UpdateHealth(float healthPercentage)
    {
        healthbar.fillAmount = healthPercentage;
    }

    public void UpdateAmmo(int pistolAmmo, int machinegunAmmo, int shotgunAmmo)
    {
        pistolAmmoText.text = pistolAmmo.ToString("000");
        machinegunAmmoText.text = machinegunAmmo.ToString("000");
        shotgunAmmoText.text = shotgunAmmo.ToString("000");
    }

    public void UpdateGunUI(Gun primaryGun, Gun secondaryGun)
    {
        primaryFrame.SetFrameInfo(primaryGun, secondaryGun);
        secondaryFrame.SetFrameInfo(secondaryGun, primaryGun);
        clipText.text = inventory.GetEquippedGun().GetClipInfo();
    }

    public void UpdateClipUI(string message)
    {
        clipText.text = message;
    }

    public void ForceUpdateUI()
    {
        if (unit == null)
        {
            return;
        }

        UpdateHealth(health.GetHealthPercentage());
        UpdateAmmo(inventory.GetAmmo(AmmoType.PistolAmmo), inventory.GetAmmo(AmmoType.MachineGunAmmo), inventory.GetAmmo(AmmoType.ShotgunAmmo));
    }

    public void SwapWeaponUI()
    {
        primaryFrame.gameObject.SetActive(!primaryFrame.gameObject.activeSelf);
        secondaryFrame.gameObject.SetActive(!secondaryFrame.gameObject.activeSelf);
        clipText.text = inventory.GetEquippedGun().GetClipInfo();
        inventory.GetEquippedGun().OnClipChange = UpdateClipUI;
    }

    public void GetUnitComponents()
    {
        health = unit.GetComponent<HealthComponent>();
        inventory = unit.GetComponent<InventoryComponent>();
    }
}
