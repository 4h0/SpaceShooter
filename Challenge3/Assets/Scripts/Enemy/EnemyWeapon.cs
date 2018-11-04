using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject explosion;

    private GameController gameControllerReference;
    private Rigidbody bulletRigidBody;

    public float speed;

    private bool destroy;

    private void Awake()
    {
        gameControllerReference = FindObjectOfType<GameController>();

        bulletRigidBody = GetComponent<Rigidbody>();

        destroy = false;
    }

    private void Start()
    {
        StartCoroutine(DestroyAmmo());
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
            bulletRigidBody.velocity = transform.forward * -speed * Time.deltaTime;
        }
    }

    IEnumerator DestroyAmmo()
    {
        for (int counter = 0; counter <= 30; counter++)
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
        }
        if (other.gameObject.tag == "Player")
        {
            destroy = true;

            Instantiate(explosion, this.transform.position, Quaternion.identity);

            gameControllerReference.gameEnd = true;

            Destroy(other.gameObject);
        }
    }
}
