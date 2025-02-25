using UnityEngine;

public class UpDownCamera : MonoBehaviour
{
    public float RotationSpeed;
    public Vector3 lookDirection;
    private float xRotation = 0f; 

    void Update()
    {
        xRotation -= Input.mousePositionDelta.y * RotationSpeed;

        xRotation = Mathf.Clamp(xRotation, -50f, 70f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

        lookDirection = hit.point;
        Debug.Log(hit.transform);
    }
}

