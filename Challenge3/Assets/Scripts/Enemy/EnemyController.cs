using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject[] explosion;
    public GameObject bullet;
    public Transform shotSpawn;

    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;

    private GameController gameControllerReference;
    private PlayerController playerControllerReference;
    private Rigidbody enemyShipRigidBody;

    public float dodge;
        
    private float speed;
    private float tilt;
    private float movingCountDown;
    private float targetManeuver;
    private float newManeuver;
    private bool destroy;

    private void Awake()
    {
        gameControllerReference = FindObjectOfType<GameController>();
        playerControllerReference = FindObjectOfType<PlayerController>();

        enemyShipRigidBody = GetComponent<Rigidbody>();
        
        destroy = false;
        movingCountDown = 3;
        speed = -3f;
        tilt = 7;
    }

    private void Start()
    {
        InvokeRepeating("Shooting", 1f, 3f);

        StartCoroutine(Evade());
        StartCoroutine(DestroyCountDown());
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
            newManeuver = Mathf.MoveTowards(enemyShipRigidBody.position.x, targetManeuver, Time.deltaTime);
            enemyShipRigidBody.velocity = new Vector3(newManeuver, 0f, speed);

            enemyShipRigidBody.position = new Vector3(Mathf.Clamp(enemyShipRigidBody.position.x, playerControllerReference.playerBoundary.xMin, playerControllerReference.playerBoundary.xMax), 0f, Mathf.Clamp(enemyShipRigidBody.position.z, -13f, 9f));

            enemyShipRigidBody.rotation = Quaternion.Euler(0f, 0f, enemyShipRigidBody.velocity.x * -tilt);
        }
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (!destroy)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);

            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));

            targetManeuver = 0;

            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    IEnumerator DestroyCountDown()
    {
        for(int counter = 0; counter <= 420; counter++)
        {
            if (!destroy)
            {
                yield return new WaitForSeconds(.1f);
            }
            else
            {
                destroy = true;

                yield break;
            }
        }

        destroy = true;
    }

    private void Shooting()
    {
        Instantiate(bullet, shotSpawn.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            destroy = true;

            gameControllerReference.score += 40;

            Instantiate(explosion[0], this.transform.position, Quaternion.identity);
        }

        if (other.gameObject.tag == "Player")
        {
            destroy = true;

            for (int counter = 0; counter <= 1; counter++)
            {
                Instantiate(explosion[counter], this.transform.position, Quaternion.identity);
            }

            gameControllerReference.gameEnd = true;
            gameControllerReference.score += 40;

            Destroy(other.gameObject);
        }    
    }
}
