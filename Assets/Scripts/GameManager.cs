using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject titleScreen;
    public Projectile projectile;
    public PlayerController player;
    public Obstacle[] obstaclePrefab;
    private MainUIHandler mainUIHandler;
    
    [SerializeField] float _obstacleSpeed = 5f;
    public float ObstacleSpeed
    {
        get { return _obstacleSpeed; }
        set { if(value <= 20f) _obstacleSpeed = value; }
    }
    public bool isGameActive;
    public string playerName;
    public float score;

    private float spawnPosX = 30;
    private float spawnPosY = 0.78f;
    private float spawnPosZ;
    private float spawnRate;

    private Vector3 playerSpawnPos = new Vector3(1, 0, 4);
    private float _shootRate = 0.5f;
    public float ShootRate
    {
        get { return _shootRate; }
        set { _shootRate = value; }
    }
    private void Awake()
    {
        isGameActive = true;
        StartCoroutine(SpawnObstacle());
        StartCoroutine(SpawnProjectile());
    }
    private void Start()
    {
        mainUIHandler = GameObject.Find("UI").GetComponent<MainUIHandler>();
        Reset();
        if(MenuManager.Instance != null)
        {
            playerName = MenuManager.Instance.PlayerName;
        }
    }
    public void GameOver()
    {
        isGameActive = false;
        mainUIHandler.SetActive();
    }
    public void Restart()
    {
        isGameActive = true;
        mainUIHandler.SetInactive();
        StartCoroutine(SpawnObstacle());
        StartCoroutine(SpawnProjectile());
        Reset();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
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
    
    
    
}
