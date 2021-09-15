using rpgadventure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class playerscanner 
{
    public float meleedetectionradius = 2.0f;
    public float detectionradius = 10.0f;
    public float detectionangle = 180.0f;
    public playercontroller detect(Transform detector)
    {
        if (playercontroller.Instance == null)
        {
            return null;
        }
        
        Vector3 toplayer = playercontroller.Instance.transform.position - detector.position;
        toplayer.y = 0;
        if (toplayer.magnitude <= detectionradius)
        {
            if ((Vector3.Dot(toplayer.normalized, detector.forward) > Mathf.Cos(detectionangle * 0.5f * Mathf.Deg2Rad)) ||
                toplayer.magnitude <= meleedetectionradius)
            {
                return playercontroller.Instance;
            }
        }

        return null;
    }
}
