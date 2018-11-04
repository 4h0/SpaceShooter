using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotating : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(Random.Range(45f, 180f), Random.Range(45f, 180f), Random.Range(45f, 180f)) * Time.deltaTime);
    }
}