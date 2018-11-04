using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public Boundary playerBoundary;
    public GameObject shotObject;
    public Transform shotSpawn;

    private Rigidbody playerRigidBody;

    private Vector3 movement;

    public float speed;
    public float tilt;

    private bool canShoot;
    private float moveHorizontal;
    private float moveVertical;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();

        canShoot = true;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && canShoot)
        {
            StartCoroutine(ShootingCooldown());

            Instantiate(shotObject, shotSpawn.position, Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        movement = new Vector3(moveHorizontal, 0f, moveVertical);

        playerRigidBody.velocity = movement * speed * Time.deltaTime;
        playerRigidBody.position = new Vector3(Mathf.Clamp(playerRigidBody.position.x, playerBoundary.xMin, playerBoundary.xMax), 0f, Mathf.Clamp(playerRigidBody.position.z, playerBoundary.zMin, playerBoundary.zMax));

        playerRigidBody.rotation = Quaternion.Euler(0f, 0f, playerRigidBody.velocity.x * -tilt);
    }

    IEnumerator ShootingCooldown()
    {
        canShoot = false;

        yield return new WaitForSeconds(.3f);

        canShoot = true;
    }
}