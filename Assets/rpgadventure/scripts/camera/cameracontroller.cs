using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class cameracontroller : MonoBehaviour
{
    [SerializeField]
    CinemachineFreeLook freelookcamera;
    public CinemachineFreeLook playercam
    {
        get
        {
            return freelookcamera;
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            freelookcamera.m_XAxis.m_MaxSpeed = 400;
            freelookcamera.m_YAxis.m_MaxSpeed = 10;
        }
        if(Input.GetMouseButtonUp(1))
        {
            freelookcamera.m_XAxis.m_MaxSpeed = 0;
            freelookcamera.m_YAxis.m_MaxSpeed = 0;
        }
    }
}
