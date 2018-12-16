using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public float DampTime = 0.15f;
    public Transform Target;

    private Camera mainCam;
    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        mainCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // If there is a target, smoothly follow this
        if (Target != null)
        {
            Vector3 point = mainCam.WorldToViewportPoint(Target.position);
            Vector3 delta = Target.position - mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.6f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, DampTime);
        }
    }
}