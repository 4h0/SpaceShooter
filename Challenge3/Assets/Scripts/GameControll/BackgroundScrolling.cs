using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private Vector3 originalPosition;

    public float scrollSpeed;

    private float backgroundSize;
    private float newPosition;

    private void Awake()
    {
        originalPosition = this.transform.position;

        backgroundSize = this.transform.localScale.y;
    }

    private void Update()
    {
        newPosition = Mathf.Repeat(Time.time * scrollSpeed, backgroundSize);
        transform.position = originalPosition + Vector3.forward * newPosition;
    }
}
