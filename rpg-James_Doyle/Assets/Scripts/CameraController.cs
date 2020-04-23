using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //ref to obj camera focuses on
    public Transform target;

    void Start()
    {
        //gets the transform positions of the player
        target = PlayerController.instance.transform;
    }

    //LateUpdate calls AFTER other Update scripts
    void LateUpdate()
    {
        //update the camera every frame
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
