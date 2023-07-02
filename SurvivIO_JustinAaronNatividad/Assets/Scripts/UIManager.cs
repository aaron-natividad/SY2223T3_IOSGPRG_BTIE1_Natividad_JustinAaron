using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject unit;

    public Image healthbar;
    public TextMeshProUGUI pistolAmmoText;
    public TextMeshProUGUI machinegunAmmoText;
    public TextMeshProUGUI shotgunAmmoText;

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
        }
    }

    private void Start()
    {
        GetUnitComponents();
        ForceUpdateUI();
    }

    public void GetUnitComponents()
    {
        health = unit.GetComponent<HealthComponent>();
        inventory = unit.GetComponent<InventoryComponent>();
    }

    public void ForceUpdateUI()
    {
        if (unit == null)
        {
            return;
        }

        UpdateHealth(health.GetHealthPercentage());
        UpdateAmmo(inventory.pistolAmmo, inventory.machinegunAmmo, inventory.shotgunAmmo);
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

}
