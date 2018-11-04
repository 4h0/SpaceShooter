using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject[] explosion;

    private GameController gameControllerReference;
    private Rigidbody asteroidRigidBody;

    public float tumble;

    private float speed;
    private bool destroy;

    private void Awake()
    {
        gameControllerReference = FindObjectOfType<GameController>();

        asteroidRigidBody = GetComponent<Rigidbody>();

        destroy = false;
        speed = Random.Range(120f, 600f);
    }

    private void Start()
    {
        StartCoroutine(DestroyAsteroid());
    }

    private void Update()
    {
        if (destroy)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!destroy)
        {
            asteroidRigidBody.velocity = transform.forward * -speed * Time.deltaTime;
        }
    }

    IEnumerator DestroyAsteroid()
    {
        for (int counter = 0; counter <= 60; counter++)
        {
            if (!destroy)
            {
                yield return new WaitForSeconds(.1f);
            }
            else
            {
                yield break;
            }
        }

        destroy = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            destroy = true;

            Instantiate(explosion[0], this.transform.position, Quaternion.identity);

            gameControllerReference.score += 15;
        }

        if (other.gameObject.tag == "Player")
        {
            destroy = true;

            for (int counter = 0; counter <= 1; counter++)
            {
                Instantiate(explosion[counter], this.transform.position, Quaternion.identity);
            }

            gameControllerReference.gameEnd = true;
            gameControllerReference.score += 15;

            Destroy(other.gameObject);
        }
    }
}