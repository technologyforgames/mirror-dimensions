using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public float DampTime = 0.15f;
    public Transform Target;

    private Camera camera;
    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // If there is a target, smoothly follow this
        if (Target != null)
        {
            Vector3 point = camera.WorldToViewportPoint(Target.position);
            Vector3 delta = Target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, DampTime);
        }

    }
}