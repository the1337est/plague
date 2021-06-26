using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform target;

    private float xOffset;
    private float zOffset;
    private float yOffset;

    private void Start()
    {
        xOffset = transform.position.x - target.position.x;
        zOffset = transform.position.z;
        yOffset = transform.position.y;
    }
    private void FixedUpdate()
    {
        if (target)
        {
            transform.position = new Vector3(target.position.x + xOffset, yOffset, zOffset);
        }

    }
}
