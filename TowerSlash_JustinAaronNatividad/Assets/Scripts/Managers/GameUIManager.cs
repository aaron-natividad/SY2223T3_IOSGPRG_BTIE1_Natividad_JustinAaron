using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;

    [Header("Panels")]
    public GameObject gameUI;
    public GameObject gameOverScreen;

    [Header("Public UI Elements")]
    public ScreenCover cover;
    public Button dashButton;
    public Image dashCooldown;

    [Header("Private UI Elements")]
    [SerializeField] private GameObject dashBarParent;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI gameOverPointsText;
    [SerializeField] private Image dashBar;
    [SerializeField] private Image flashImage;

    [Header("Parameters")]
    [SerializeField] private float flashSpeed;

    private PlayerDashGauge dashGauge;
    private int internalHealthCounter = 0;

    private void OnEnable()
    {
        GameManager.OnPointsChange += UpdatePointsUI;
    }

    private void OnDisable()
    {
        GameManager.OnPointsChange -= UpdatePointsUI;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(cover.SetCoverValue(0, 0.5f));
        ActivateUI(0);
    }

    void Update()
    {
        if (GameManager.instance.player == null) return;

        if (dashGauge == null)
        {
            dashGauge = GameManager.instance.player.dashGauge;
        }

        switch (dashGauge.state)
        {
            case GaugeState.Inactive:
                dashBarParent.SetActive(true);
                dashButton.gameObject.SetActive(false);
                dashCooldown.gameObject.SetActive(false);
                dashBar.fillAmount = GameManager.instance.player.dashGauge.GetGaugePercentage();
                break;
            case GaugeState.Active:
                dashBarParent.SetActive(false);
                dashButton.gameObject.SetActive(true);
                dashCooldown.gameObject.SetActive(false);
                dashButton.enabled = true;
                break;
            case GaugeState.Cooldown:
                dashBarParent.SetActive(false);
                dashButton.gameObject.SetActive(true);
                dashCooldown.gameObject.SetActive(true);
                dashButton.enabled = false;
                dashCooldown.fillAmount = GameManager.instance.player.dashGauge.GetActivePercentage();
                break;
        }
    }

    public void UpdateHealthUI(int health)
    {
        if (internalHealthCounter > health)
        {
            StartCoroutine(CO_Flash());
        }
        internalHealthCounter = health;
        healthText.text = health.ToString("00");
        StartCoroutine(CO_Pop(healthText.gameObject));
    }

    public void UpdatePointsUI(int points)
    {
        pointsText.text = points.ToString("000");
        gameOverPointsText.text = points.ToString("000");
        StartCoroutine(CO_Pop(pointsText.gameObject));
    }

    public void ActivateUI(int panelID)
    {
        gameUI.SetActive(panelID == 0);
        gameOverScreen.SetActive(panelID == 1);
    }

    public IEnumerator CO_Pop(GameObject obj)
    {
        float scale = 1.1f;

        while(scale > 1)
        {
            obj.transform.localScale = Vector3.one * scale;
            scale -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        obj.transform.localScale = Vector3.one;
    }

    public IEnumerator CO_Flash()
    {
        float timer = 0;

        while (timer < 1)
        {
            flashImage.color = Color.Lerp(Color.red, Color.clear, timer);
            timer += Time.deltaTime * flashSpeed;
            yield return new WaitForFixedUpdate();
        }

        flashImage.color = Color.clear;
    }
}
