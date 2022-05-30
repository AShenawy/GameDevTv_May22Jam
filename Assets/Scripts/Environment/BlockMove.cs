using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    public Vector3 point1, point2;
    public float moveSpeed;

    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        destination = point1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, point1) <= 0.2f)
        {
            destination = point2;
        }
        else if (Vector3.Distance(transform.position, point2) <= 0.2f)
        {
            destination = point1;
        }

        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
    }
}
