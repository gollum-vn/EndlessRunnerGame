using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Projectile projectile;
    public PlayerController player;
    public Obstacle[] obstaclePrefab;
    private MainUIHandler mainUIHandler;
    
    [SerializeField] float _obstacleSpeed = 5f;
    public float ObstacleSpeed
    {
        get { return _obstacleSpeed; }
        set { if(value <= 25f) _obstacleSpeed = value; }
    }
    public bool isGameActive;
    public string playerName;
    public float score;
    public float damage = 15f;
    public float powerPoint;
    public float maxPowerPoint = 1000f;

    private float spawnRate;
    private float _shootRate = 0.4f;
    public float ShootRate
    {
        get { return _shootRate; }
        set { _shootRate = value; }
    }
   
    private void Start()
    {
        isGameActive = true;
        Instantiate(player, new Vector3(1, 0, 4), player.transform.rotation);
        StartCoroutine(SpawnObstacle());
        mainUIHandler = GameObject.Find("UI").GetComponent<MainUIHandler>();
        if(MenuManager.Instance != null)
        {
            playerName = MenuManager.Instance.PlayerName;
        }
    }
    private void Update()
    {
        ScoreUpdate();
        IsPower();
    }
    public void GameOver()
    {
        isGameActive = false;
        mainUIHandler.SetActive();
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    IEnumerator SpawnObstacle()
    {
        while (isGameActive)
        {
            List<int> posZ = new List<int> { 0, 2, 4, 6, 8 };
            List<int> levelSpawn = new List<int> { 0, 1, 2, 3, 4 };
            for (int i = 4; i >= 0; i--)
            {
                spawnRate = 6 / ObstacleSpeed;
                int a = Random.Range(0, i + 1);
                int b = Random.Range(0, i + 1);
                Vector3 spawnPosition = new Vector3(30, 0.78f, posZ[a]);
                int level = levelSpawn[b];
                if (isGameActive)
                {
                    Instantiate(obstaclePrefab[level], spawnPosition, obstaclePrefab[level].transform.rotation);
                    ObstacleSpeed += 0.1f;
                    yield return new WaitForSeconds(spawnRate);
                }
                posZ.RemoveAt(a);
                levelSpawn.RemoveAt(b);
            }
        }
    }
    public void UpdateProjectile(int level)
    {
        damage += level;
    }
    void ScoreUpdate()
    {
        score += ObstacleSpeed;
    }
    public bool IsPower()
    {
        if (powerPoint >= maxPowerPoint)
        {
            return true;
        }
        else return false;
    }
}
