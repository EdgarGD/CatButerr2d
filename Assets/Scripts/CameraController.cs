using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform trackedObject;
    private float updateSpeed = 20;
    public Vector2 trackingOffset;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        //offset = (Vector3)trackingOffset;
        offset.z = transform.position.z - trackedObject.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(0, trackedObject.position.y, offset.z);
    }
}
