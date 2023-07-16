using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject unit;

    [Header("UI Components")]
    [SerializeField] private Image healthbar;
    [SerializeField] private TextMeshProUGUI clipText;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI pistolAmmoText;
    [SerializeField] private TextMeshProUGUI machinegunAmmoText;
    [SerializeField] private TextMeshProUGUI shotgunAmmoText;
    [Space(10)]
    [SerializeField] private Frame primaryFrame;
    [SerializeField] private Frame secondaryFrame;

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
            inventory.OnAmmoChange += UpdateClipUI;
            inventory.OnChangeGun += UpdateGunUI;
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
            inventory.OnAmmoChange -= UpdateClipUI;
            inventory.OnChangeGun -= UpdateGunUI;
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
        inventory.GetEquippedGun().OnClipChange = UpdateClipUI;
        clipText.text = inventory.GetEquippedGun().GetClipInfo();
    }

    public void UpdateClipUI(string message)
    {
        clipText.text = message;
    }

    public void UpdateClipUI(int pistolAmmo = 0, int machinegunAmmo = 0, int shotgunAmmo = 0)
    {
        if(inventory.GetEquippedGun() != null)
        {
            clipText.text = inventory.GetEquippedGun().GetClipInfo();
        }
    }

    public void ForceUpdateUI()
    {
        if (unit == null)
        {
            return;
        }

        UpdateHealth(health.GetHealthPercentage());
        UpdateAmmo(inventory.GetAmmo(AmmoType.PistolAmmo), inventory.GetAmmo(AmmoType.MachineGunAmmo), inventory.GetAmmo(AmmoType.ShotgunAmmo));
        UpdateClipUI();
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
