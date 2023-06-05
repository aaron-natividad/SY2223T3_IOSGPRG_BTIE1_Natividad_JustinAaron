using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Panels")]
    public GameObject gameUI;
    public GameObject gameOverScreen;

    [Header("Public UI Elements")]
    public ScreenCover cover;
    public Button dashButton;

    [Header("Private UI Elements")]
    [SerializeField] private GameObject dashBarParent;
    [SerializeField] private Image dashBar;
    [Space(10)]
    [SerializeField] private Image flashImage;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Parameters")]
    [SerializeField] private float flashSpeed;

    private int internalHealthCounter = 0;

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

        switch (GameManager.instance.player.dashGauge.state)
        {
            case GaugeState.Inactive:
                dashButton.gameObject.SetActive(false);
                dashBarParent.gameObject.SetActive(true);
                dashBar.fillAmount = GameManager.instance.player.dashGauge.GetGaugePercentage();
                break;
            case GaugeState.Active:
                dashButton.gameObject.SetActive(true);
                dashBarParent.gameObject.SetActive(false);
                break;
            case GaugeState.Cooldown:
                dashButton.gameObject.SetActive(false);
                dashBarParent.gameObject.SetActive(false);
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
        Color invis = new Color(0, 0, 0, 0);
        float timer = 0;

        while (timer < 1)
        {
            flashImage.color = Color.Lerp(Color.red, invis, timer);
            timer += Time.deltaTime * flashSpeed;
            yield return new WaitForFixedUpdate();
        }

        flashImage.color = invis;
    }
}
