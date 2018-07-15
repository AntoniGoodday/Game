using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

    public Transform playerTransform;
    public float cameraSmoothness = 0.25f;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = playerTransform.position;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSmoothness);

        gameObject.transform.position = smoothPosition;

    }
    
}
