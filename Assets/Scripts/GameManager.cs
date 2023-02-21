using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public GameObject titleScreen;
    public Projectile projectile;
    public PlayerController player;
    public Obstacle[] obstaclePrefab;

    private Vector3 playerSpawnPos = new Vector3(1, 0, 4);
    private float _shootRate = 0.5f;
    public float ShootRate
    {
        get { return _shootRate; }
        set { _shootRate = value; }
    }
    [SerializeField] float _obstacleSpeed = 5f;
    public float ObstacleSpeed
    {
        get { return _obstacleSpeed; }
        set { if(value <= 15f) _obstacleSpeed = value; }
    }
    public bool isGameActive;
    
    private float spawnPosX = 30;
    private float spawnPosY = 0.78f;
    private float spawnPosZ;
    private float spawnRate;

    private float score;
    public string playerName;

    public Button startButton;
    public Button restartButton;
    public Button exitButton;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI yourScoreText;

    private void Start()
    {
        Reset();
    }
    private void Update()
    {
        ScoreUpdate();
    }
    public void StartGame()
    {
        isGameActive = true;
        titleScreen.SetActive(false);
        scoreText.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(false);
        StartCoroutine(SpawnObstacle());
        StartCoroutine(SpawnProjectile());
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        yourScoreText.gameObject.SetActive(true);
        Debug.Log("Game Over!");
        isGameActive = false;
        Invoke("ActiveRestartButton", 5f);
    }
    void ActiveRestartButton()
    {
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
    }
    public void Restart()
    {
        isGameActive = true;
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        yourScoreText.gameObject.SetActive(false);
        StartCoroutine(SpawnObstacle());
        StartCoroutine(SpawnProjectile());
        Reset();
    }
    IEnumerator SpawnProjectile()
    {
        PlayerController currentPlayer = Instantiate(player, playerSpawnPos, player.transform.rotation);
        while (isGameActive)
        {
            Vector3 projectileSpawnPos = currentPlayer.transform.position + new Vector3(1, 0.5f, 0);
            Instantiate(projectile, projectileSpawnPos, projectile.transform.rotation);
            yield return new WaitForSeconds(_shootRate);
        }
    }
    IEnumerator SpawnObstacle()
    {
        while (isGameActive)
        {
            List<int> posZ = new List<int> { 0, 1, 2, 3, 4 };
            List<int> levelSpawn = new List<int> { 0, 1, 2, 3, 4 };
            for (int i = 4; i >= 0; i--)
            {
                int a = Random.Range(0, i + 1);
                int b = Random.Range(0, i + 1);
                spawnPosZ = posZ[a] * 2;
                posZ.RemoveAt(a);
                int level = levelSpawn[b];
                levelSpawn.RemoveAt(b);
                Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, spawnPosZ);
                if (isGameActive)
                {
                    Instantiate(obstaclePrefab[level], spawnPosition, obstaclePrefab[level].transform.rotation);
                    ObstacleSpeed += 0.1f;
                    yield return new WaitForSeconds(spawnRate);
                }
                spawnRate = 5 / ObstacleSpeed;
            }
        }
    }
    public void UpdateProjectile(int level)
    {
        projectile.damage += level;
        //shootRate -= 0.005f * level;
    }
    void Reset()
    {
        ShootRate = 0.25f;
        ObstacleSpeed = 5f;
        spawnRate = 0.5f;
        projectile.damage = 10f;
        score = 0;
    }
    void ScoreUpdate()
    {
        if (isGameActive)
        {
            score += Time.time * ObstacleSpeed/600;
            scoreText.text = "Score:" + (int)score;
            yourScoreText.text = playerName +": "+ (int)score;
        }
    }
    public void ReadInputField(string input)
    {
        playerName = input;
        Debug.Log(playerName);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
