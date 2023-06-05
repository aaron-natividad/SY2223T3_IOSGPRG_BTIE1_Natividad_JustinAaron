using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public delegate void PointsChangeDelegate(int points);
    public static PointsChangeDelegate OnPointsChange;

    public CameraFollow cam;
    public GameUIManager ui;
    public TileSpawner tileSpawner;
    public EnemySpawner enemySpawner;

    [Space(10)]
    [SerializeField] private Transform playerSpawnPosition;
    [SerializeField] private float gravity;

    [HideInInspector] public Player player;
    [HideInInspector] public int points;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        Physics2D.gravity = new Vector2(gravity, 0f);
    }

    private void Start()
    {
        StartCoroutine(CO_StartGame());
    }

    public void ReceivePoints(GameObject enemyObj, bool isDestroyedByPlayer)
    {
        if (isDestroyedByPlayer)
        {
            points += enemyObj.GetComponent<Enemy>().pointValue;
        }
        OnPointsChange?.Invoke(points);
    }

    public void ReceivePoints(int pointValue)
    {
        points += pointValue;
        OnPointsChange?.Invoke(points);
    }

    public void GameOver()
    {
        Destroy(player.gameObject);
        ui.ActivateUI(1);
        StartCoroutine(tileSpawner.AnimateTiles(1.5f));
        enemySpawner.ClearEnemies();
    }

    public IEnumerator CO_StartGame()
    {
        points = 0;
        yield return new WaitForSeconds(1f);

        StartCoroutine(tileSpawner.AnimateTiles(0));
        yield return new WaitForSeconds(0.5f);

        player = Instantiate(ChoiceManager.instance.GetPlayerPrefab(), playerSpawnPosition.position, Quaternion.identity).GetComponent<Player>();
        enemySpawner.Initialize();
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(CO_Reload(sceneName));
    }

    public IEnumerator CO_Reload(string sceneName)
    {
        StartCoroutine(ui.cover.SetCoverValue(1, 0.5f));
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}
