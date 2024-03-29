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
    [SerializeField] private GameObject[] messagePanels;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI countdownText;
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

    #region Public Methods
    public void ActivateMessagePanel(string panelName)
    {
        playerUI.enabled = false;
        messageUI.enabled = true;
        foreach (GameObject panel in messagePanels)
        {
            panel.SetActive(panel.name == panelName);
        }
    }

    public void EnablePlayerUI()
    {
        messageUI.enabled = false;
        playerUI.enabled = true;
    }

    public void FadeCover(bool fadeIn, float fadeTime)
    {
        Color toColor = fadeIn ? Color.black : Color.clear;
        LeanTween.color(cover.rectTransform, toColor, fadeTime).setIgnoreTimeScale(true);
    }

    public void SetCountdown(string countMessage)
    {
        countdownText.text = countMessage;
    }
    #endregion

    #region Private Event Updates
    private void UpdateHealth(float healthPercentage)
    {
        healthbar.fillAmount = healthPercentage;
    }

    private void UpdateAmmo(int[] ammo)
    {
        for(int i = 0; i < 4; i++)
        {
            ammoText[i].text = ammo[i].ToString("000");
        }
        UpdateClipUI();
    }

    private void UpdateGunUI(Gun[] guns, GunType equippedType)
    {
        for(int i = 0; i < 2; i++)
        {
            gunFrames[i].SetFrameInfo(guns[i], guns[(i + 1) % 2]);
            gunFrames[i].gameObject.SetActive(i == (int)equippedType);
        }

        if (inventory.GetEquippedGun() != null)
        {
            inventory.GetEquippedGun().OnClipChange += UpdateClipUI;
        }
        UpdateClipUI();
    }

    private void UpdateClipUI()
    {
        if (inventory.GetEquippedGun() != null)
        {
            clipText.text = inventory.GetEquippedGun().GetClipInfo();
        }
    }

    private void UpdateClipUI(string message)
    {
        clipText.text = message;
    }

    private void UpdateEnemyCount(int remainingEnemies)
    {
        int rank = remainingEnemies + 1;
        enemyCountText.text = remainingEnemies.ToString() + " left";
        rankText.text = "Rank #" + rank.ToString();
    }
    #endregion

    #region Miscellaneous Private Methods
    private void ForceUpdateUI()
    {
        if (unit == null)
        {
            return;
        }

        UpdateHealth(health.GetHealthPercentage());
        UpdateAmmo(inventory.GetAmmoArray());
        UpdateClipUI();
    }

    private void GetUnitComponents()
    {
        health = unit.GetComponent<HealthComponent>();
        inventory = unit.GetComponent<InventoryComponent>();
    }
    #endregion
}
