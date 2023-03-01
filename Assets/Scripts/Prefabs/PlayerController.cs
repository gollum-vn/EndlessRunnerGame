using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 4f;
    private float horizontalInput;
    private float minPosZ = 0;
    private float maxPosZ = 8;
    private Animator playerAnimator;
    private GameManager gameManager;

    public ParticleSystem explosionParticle;
    public Laser laser;
    public Projectile projectile;
    public PlayerController player;
    //public TrailRenderer laser;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        StartCoroutine(SpawnProjectile());
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
        if(gameManager.isGameActive)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }
        if(gameManager.isGameActive && gameManager.IsPower())
        {
            Debug.Log("You can use your power");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LaserFire();
            }
        }
    }

    private void Movement()
    {
        //calculate movement direction
        if (gameManager.isGameActive)
        {
            transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);
        }
        if (transform.position.z <= minPosZ)
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
            Destroy(gameObject, 5f);
        }
    }
    IEnumerator SpawnProjectile()
    {
        Vector3 playerSpawnPos = new Vector3(1, 0, 4);
        //PlayerController currentPlayer = Instantiate(player, playerSpawnPos, player.transform.rotation);
        while (gameManager.isGameActive)
        {
            Vector3 projectileSpawnPos = transform.position + new Vector3(1, 0.5f, 0);
            Instantiate(projectile, projectileSpawnPos, projectile.transform.rotation);
            yield return new WaitForSeconds(gameManager.ShootRate);
        }
    }
    private void LaserFire()
    {
        Vector3 position = transform.position + new Vector3(0, 0.8f, 0);
        Instantiate(laser, position, transform.rotation);
        
        Vector3 origin = transform.position + new Vector3(0, 0.8f, 0);
        Vector3 direction = transform.forward;
        RaycastHit[] hits = Physics.RaycastAll(origin, direction * 30f);
        Debug.DrawRay(origin, direction * 30f);
        if(hits.Length > 0)
        {
            foreach(var hit in hits)
            {
                hit.collider.gameObject.GetComponent<Obstacle>().TakeDamage(gameManager.damage * 10);
            }
        }
        gameManager.powerPoint = 0;
        
    }
}