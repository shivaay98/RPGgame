using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace rpgadventure
{
    public class meleeweapon : MonoBehaviour
    {
        [System.Serializable]
        public class attackpoint
        {
            public float radius;
            public Vector3 offset;
            public Transform roottransform;
        }
        private bool m_isattack = false;
        private Vector3[] m_originattackpos;

        public int damage = 10;
        public attackpoint[] attackpoints = new attackpoint[0];
        public void BeginAttack()
        {
            m_isattack = true;
            m_originattackpos = new Vector3[attackpoints.Length];
            for(int i=0;i<attackpoints.Length;i++)
            {
                attackpoint ap = attackpoints[i];
                m_originattackpos[i] = ap.roottransform.position + ap.roottransform.TransformDirection(ap.offset);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            for(int i=0;i<attackpoints.Length;i++)
            {

            }
            foreach(attackpoint attackPoint in attackpoints)
            {
                if(attackPoint.roottransform!=null)
                {

                    Vector3 worldposition = attackPoint.roottransform.TransformVector(attackPoint.offset);
                    Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.6f);
                    Gizmos.DrawSphere(attackPoint.roottransform.position+ worldposition, attackPoint.radius);
                }
            }
        }
#endif
    }
 
}
