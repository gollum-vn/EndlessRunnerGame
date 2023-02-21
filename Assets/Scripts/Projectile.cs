using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Projectile : MonoBehaviour
{
    public float shootSpeed = 10f;
    public float damage = 10f;

    public DamagedText damagedCanvas;
    void Update()
    {
        transform.Translate(Vector3.up * shootSpeed *Time.deltaTime);
        if(transform.position.x > 30)
        {
            Destroy(gameObject);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            var target = collision.gameObject.GetComponent<Obstacle>();
            target.TakeDamage(damage);
            SpawnDamagedText();
            Destroy(gameObject);
        }
    }
    void SpawnDamagedText()
    {
        damagedCanvas.damaged = (int)damage;
        damagedCanvas = Instantiate(damagedCanvas, transform.position, damagedCanvas.transform.rotation);
    }
}
