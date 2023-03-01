using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private GameManager gameManager;

    private float repeatWidth;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        SetUp();
    }
    void SetUp()
    {
        transform.Translate(Vector3.left * gameManager.ObstacleSpeed * Time.deltaTime);
        if(transform.position.x < repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
