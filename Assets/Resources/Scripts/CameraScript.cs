using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform target;

	void LateUpdate()
    {
        // To follow the target, set position of the camera equal to target position
        transform.position = new Vector3(target.position.x, transform.position.y,transform.position.z);
    }
}
