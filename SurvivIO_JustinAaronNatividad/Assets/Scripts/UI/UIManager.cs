using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject unit;

    [Header("UI Components")]
    [SerializeField] private Canvas playerUI;
    [SerializeField] private Image healthbar;
    [SerializeField] private TextMeshProUGUI clipText;
    [SerializeField] private TextMeshProUGUI[] ammoText;
    [SerializeField] private Frame[] gunFrames;

    private HealthComponent health;
    private InventoryComponent inventory;

    private void OnEnable()
    {
        GetUnitComponents();
        if (health != null)
        {
            health.OnHealthChange += UpdateHealth;
            health.OnDeath += DisableUI;
        }

        if (inventory != null)
        {
            inventory.OnAmmoUpdate += UpdateAmmo;
            inventory.OnGunUpdate += UpdateGunUI;
        }
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.OnHealthChange -= UpdateHealth;
            health.OnDeath -= DisableUI;
        }

        if (inventory != null)
        {
            inventory.OnAmmoUpdate -= UpdateAmmo;
            inventory.OnGunUpdate -= UpdateGunUI;
        }
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

    public void UpdateAmmo(int[] ammo)
    {
        for(int i = 0; i < 3; i++)
        {
            ammoText[i].text = ammo[i].ToString("000");
        }
        UpdateClipUI();
    }

    public void UpdateGunUI(Gun[] guns, GunType equippedType)
    {
        for(int i = 0; i < 2; i++)
        {
            gunFrames[i].SetFrameInfo(guns[i], guns[(i + 1) % 2]);
            gunFrames[i].gameObject.SetActive(i == (int)equippedType);
        }

        if (inventory.GetEquippedGun() != null)
        {
            inventory.GetEquippedGun().OnClipChange = UpdateClipUI;
        }
        UpdateClipUI();
    }

    public void UpdateClipUI()
    {
        if (inventory.GetEquippedGun() != null)
        {
            clipText.text = inventory.GetEquippedGun().GetClipInfo();
        }
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
        UpdateAmmo(inventory.GetAmmoArray());
        UpdateClipUI();
    }

    public void GetUnitComponents()
    {
        health = unit.GetComponent<HealthComponent>();
        inventory = unit.GetComponent<InventoryComponent>();
    }

    public void DisableUI(GameObject unit)
    {
        playerUI.enabled = false;
    }
}
