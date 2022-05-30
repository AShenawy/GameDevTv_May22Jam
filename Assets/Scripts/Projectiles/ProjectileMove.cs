using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float moveSpeed = 10f;

    void Update()
    {
        transform.Translate(Vector3.forward * GameManager.worldSpeed * moveSpeed * Time.deltaTime);
    }
}
