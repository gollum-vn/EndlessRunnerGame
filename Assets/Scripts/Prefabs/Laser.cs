using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float speed = 100f;
   
    // Update is called once per frame
    void Update()
    {
        Setup();
    }
    void Setup()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.x > 60)
        {
            Destroy(gameObject);
        }
    }
}
