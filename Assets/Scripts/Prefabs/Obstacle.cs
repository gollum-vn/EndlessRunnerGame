using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Obstacle : MonoBehaviour
{
    private GameManager gameManager;

    public DamagedText damagedCanvas;
    public TextMeshProUGUI healthText;

    public float health;
    public float currentHealth;
    public int level;
    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    void Start()
    {
        health = 50 * level  + 15 * gameManager.ObstacleSpeed;
        currentHealth = health;
    }

    void Update()
    {
        Setup();
    }
    void Setup()
    {
        transform.Translate(Vector3.left * gameManager.ObstacleSpeed * Time.deltaTime);
        if (transform.position.x < -10) { Destroy(gameObject); }
        healthText.text = "" + (int)currentHealth;
    }

    public void TakeDamage(float damage)
    {
        SpawnDamagedText((int)damage);
        if (currentHealth >= 0)
        {
            currentHealth -= damage; 
        }
        if (currentHealth <= 0) { Die(); }
        
    }
    public void Die()
    {
        Destroy(gameObject);
        gameManager.UpdateProjectile(level);
    }
    void SpawnDamagedText(int damaged)
    {
        damagedCanvas.damaged = damaged;
        Instantiate(damagedCanvas, transform.position, damagedCanvas.transform.rotation);
    }
}
