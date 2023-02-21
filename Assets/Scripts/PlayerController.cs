using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private float horizontalInput;
    private float minPosZ = 0;
    private float maxPosZ = 8;
    public Animator playerAnimator;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;
    public ParticleSystem runParticle;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        GetInput();
    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void Movement()
    {
        //calculate movement direction
        if (gameManager.isGameActive)
        {
            transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);
            runParticle.Play();
        }
        if(transform.position.z <= minPosZ)
        {
            transform.position = new Vector3(1, 0, minPosZ);
        }
        if (transform.position.z >= maxPosZ)
        {
            transform.position = new Vector3(1, 0, maxPosZ);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && gameManager.isGameActive)
        {
            explosionParticle.Play();
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);
            gameManager.GameOver();
            Destroy(gameObject,5f);
        }
    }
}
