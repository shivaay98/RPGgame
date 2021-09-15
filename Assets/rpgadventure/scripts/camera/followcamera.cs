using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followcamera : MonoBehaviour
{
    
    [SerializeField]
    private Transform target;

    
    void LateUpdate()
    {
        if(!target)
        {
            return;
        }

        float currentrotationangle = transform.eulerAngles.y;
        float wantedrotationangle = target.eulerAngles.y;

        currentrotationangle = Mathf.LerpAngle(currentrotationangle, wantedrotationangle, 0.5f);

        transform.position = new Vector3(target.position.x, 5.0f, target.position.z);

        Quaternion currentrotation = Quaternion.Euler(0, currentrotationangle, 0);

        Vector3 roatatedposition = currentrotation * Vector3.forward;

        transform.position -= roatatedposition*10;

        transform.LookAt(target);
    }
}
