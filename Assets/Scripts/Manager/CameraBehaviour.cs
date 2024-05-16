using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Tooltip("Camera looking at")]
    public Transform target;

    public Vector3 offset = new Vector3(0,3,-6);

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            //change rotation to face target
            transform.LookAt(target.position);
        }    
    }
}
