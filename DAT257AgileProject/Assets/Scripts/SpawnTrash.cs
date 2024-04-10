using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrash : MonoBehaviour // TODO Better name for this script
{
    private float rotationSpeed = 50f;
    private float moveSpeed = 3f;

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
