using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemycontroller : MonoBehaviour
{
    private NavMeshAgent m_navmeshagent;
    private Animator m_animator;
    private float m_speedmodifier = 0.7f;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_navmeshagent = GetComponent<NavMeshAgent>();
    }

    private void OnAnimatorMove()
    {
        if(m_navmeshagent.enabled)
        {
            m_navmeshagent.speed = (m_animator.deltaPosition / Time.fixedDeltaTime).magnitude * m_speedmodifier;
        }
       
    }

    public bool followtarget(Vector3 position)
    {
        if(!m_navmeshagent.enabled)
        {
            m_navmeshagent.enabled = true;
        }
       return m_navmeshagent.SetDestination(position);
    }

    public void stopfollowtarget()
    {
        m_navmeshagent.enabled = false;
    }


}
