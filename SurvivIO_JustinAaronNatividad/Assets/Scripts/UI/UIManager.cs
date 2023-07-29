using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject unit;

    [Header("Player UI Components")]
    [SerializeField] private Canvas playerUI;
    [SerializeField] private Image healthbar;
    [SerializeField] private TextMeshProUGUI clipText;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private TextMeshProUGUI[] ammoText;
    [SerializeField] private Frame[] gunFrames;

    [Header("Message UI Components")]
    [SerializeField] private Canvas messageUI;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Image cover;

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
            inventory.OnAmmoUpdate += UpdateAmmo;
            inventory.OnGunUpdate += UpdateGunUI;
        }

        EnemySpawner.OnEnemyCountChanged += UpdateEnemyCount;
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.OnHealthChange -= UpdateHealth;
        }

        if (inventory != null)
        {
            inventory.OnAmmoUpdate -= UpdateAmmo;
            inventory.OnGunUpdate -= UpdateGunUI;
        }

        EnemySpawner.OnEnemyCountChanged -= UpdateEnemyCount;
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

    public void UpdateEnemyCount(int remainingEnemies)
    {
        enemyCountText.text = remainingEnemies.ToString() + " left";
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

    public void ActivateMessagePanel(string panelName)
    {
        winPanel.SetActive(winPanel.name == panelName);
        losePanel.SetActive(losePanel.name == panelName);
    }

    public void EnablePlayerUI(bool isEnabled)
    {
        playerUI.enabled = isEnabled;
    }

    public void EnableMessageUI(bool isEnabled)
    {
        messageUI.enabled = isEnabled;
    }

    public void FadeCover(bool fadeIn, float fadeTime)
    {
        Color toColor = fadeIn ? Color.black : Color.clear;
        LeanTween.color(cover.rectTransform, toColor, fadeTime).setIgnoreTimeScale(true);
    }
}
