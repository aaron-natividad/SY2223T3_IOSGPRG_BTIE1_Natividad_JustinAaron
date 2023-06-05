using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform playerSpawnPosition;

    public GameObject playerPrefab;

    public Player player;
    public CameraFollow cam;
    public UIManager ui;
    public TileSpawner tileSpawner;
    public EnemySpawner enemySpawner;
    public int points;
    public float gravity;

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

    public void GameOver()
    {
        Destroy(player.gameObject);
        ui.ActivateUI(1);
        StartCoroutine(tileSpawner.AnimateTiles(1.5f));
        enemySpawner.ClearEnemies();
    }

    public IEnumerator CO_StartGame()
    {
        yield return new WaitForSeconds(1f);
        ui.ActivateUI(0);
        StartCoroutine(tileSpawner.AnimateTiles(0));
        yield return new WaitForSeconds(0.5f);
        player = Instantiate(playerPrefab, playerSpawnPosition.position, Quaternion.identity).GetComponent<Player>();
        enemySpawner.Initialize();
    }

    public void ReloadGame()
    {
        StartCoroutine(CO_Reload());
    }

    public IEnumerator CO_Reload()
    {
        StartCoroutine(ui.cover.SetCoverValue(1, 0.5f));
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("GameScene");
    }
}
