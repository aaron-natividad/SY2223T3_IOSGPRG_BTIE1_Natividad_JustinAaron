using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject unit;

    private UIManager ui;
    private HealthComponent unitHealth;

    private void OnEnable()
    {
        unitHealth = unit.GetComponent<HealthComponent>();
        if (unitHealth != null)
        {
            unitHealth.OnDeath += Lose;
        }

        EnemySpawner.OnEnemiesDepleted += Win;
    }

    private void OnDisable()
    {
        if (unitHealth != null)
        {
            unitHealth.OnDeath -= Lose;
        }

        EnemySpawner.OnEnemiesDepleted -= Win;
    }

    private void Awake()
    {
        instance = this;
        ui = GetComponent<UIManager>();
    }

    private void Start()
    {
        StartCoroutine(CO_StartGame());
    }

    public void Win()
    {
        Time.timeScale = 0;
        ui.EnablePlayerUI(false);
        ui.EnableMessageUI(true);
        ui.ActivateMessagePanel("Win Panel");
    }

    public void Lose(GameObject unit)
    {
        Time.timeScale = 0;
        ui.EnablePlayerUI(false);
        ui.EnableMessageUI(true);
        ui.ActivateMessagePanel("Lose Panel");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(CO_LoadScene(sceneName));
    }

    private IEnumerator CO_StartGame()
    {
        Time.timeScale = 0;
        ui.EnablePlayerUI(false);
        yield return new WaitForSecondsRealtime(0.5f);

        ui.FadeCover(false, 0.5f);
        yield return new WaitForSecondsRealtime(0.5f);

        for(int i = 3; i > 0; i--)
        {
            ui.SetCountdown(i.ToString());
            yield return new WaitForSecondsRealtime(1f);
        }
        
        Time.timeScale = 1;
        ui.EnableMessageUI(false);
        ui.EnablePlayerUI(true);
    }

    private IEnumerator CO_LoadScene(string sceneName)
    {
        Time.timeScale = 0;
        ui.FadeCover(true, 0.5f);
        yield return new WaitForSecondsRealtime(0.6f);
        SceneManager.LoadScene(sceneName);
    }
}
