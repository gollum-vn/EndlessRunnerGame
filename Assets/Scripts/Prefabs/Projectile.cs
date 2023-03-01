using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Projectile : MonoBehaviour
{
    private float speed = 10f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    void Update()
    {
        SetUp();
    }
    void SetUp()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (transform.position.x > 30)
        {
            Destroy(gameObject);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            var target = collision.gameObject.GetComponent<Obstacle>();
            target.TakeDamage(gameManager.damage);
            gameManager.powerPoint += gameManager.damage;
            Destroy(gameObject);
        }
    }
    
}
