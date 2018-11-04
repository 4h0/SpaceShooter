using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private Rigidbody bulletRigidBody;

    public float speed;

    private bool destroy;

    private void Awake()
    {
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
            bulletRigidBody.velocity = transform.forward * speed * Time.deltaTime;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyBeam" || collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "EnemyShip")
        {
            destroy = true;
        }
    }
}