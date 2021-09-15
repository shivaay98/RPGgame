using UnityEngine;
using UnityEngine.AI;
using System.Collections;
namespace rpgadventure
{
    public class Bandit : MonoBehaviour
    {
        private Animator m_animator;
        public float attackdistance=1.1f;
        public playerscanner playerscan;
        public float TimeToStopPursuit = 2.0f;
        public float timetowait = 2.0f;
        private Vector3 m_OriginPos;
        private Quaternion m_originrotation;
        private playercontroller m_FollowTarget;
        private enemycontroller m_enemycontroller;
        private float m_TimeSinceLostTarget = 0;
        public bool hasfollowingtarget
        {
            get
            {
                return m_FollowTarget != null;
            }
        }
        private readonly int m_hashinpursuit = Animator.StringToHash("inpursuit");
        private readonly int m_hashnearbase = Animator.StringToHash("nearbase");
        private readonly int m_hashattack = Animator.StringToHash("attack");
        private void Awake()
        {
            m_enemycontroller = GetComponent<enemycontroller>();
            m_OriginPos = transform.position;
            m_animator = GetComponent<Animator>();
            m_originrotation = transform.rotation;
        }
        private void Update()
        {
            guardposition();
        }

        private void guardposition()
        {
            var detectedtarget = playerscan.detect(transform);
            bool hasdetectedtarget = detectedtarget != null;

            if (hasdetectedtarget)
            {
                m_FollowTarget = detectedtarget;
            }

            if (hasfollowingtarget)
            {
                attackorfollowtarget();

                if (hasdetectedtarget)
                {
                    m_TimeSinceLostTarget = 0;
                }
                else
                {
                    stoppursuit();
                }
            }

            checkifnearbase();
        }

        private void attackorfollowtarget()
        {
            Vector3 totarget = m_FollowTarget.transform.position - transform.position;
            if (totarget.magnitude <= attackdistance)
            {
                attacktarget(totarget);
            }
            else
            {
                followtarget();

            }
        }

        private void attacktarget(Vector3 totarget)
        {
            var totargetrotation = Quaternion.LookRotation(totarget);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, totargetrotation, 360 * Time.deltaTime);
            m_enemycontroller.stopfollowtarget();
            m_animator.SetTrigger(m_hashattack);
        }

        private void followtarget()
        {
            m_animator.SetBool(m_hashinpursuit, true);
            m_enemycontroller.followtarget(m_FollowTarget.transform.position);
        }
        private void stoppursuit()
        {
            m_TimeSinceLostTarget += Time.deltaTime;
            if (m_TimeSinceLostTarget >= TimeToStopPursuit)
            {
                m_FollowTarget = null;

                m_animator.SetBool(m_hashinpursuit, false);
                StartCoroutine(WaitonPursuit());
            }
        }

        private void checkifnearbase()
        {
            Vector3 tobase = m_OriginPos - transform.position;
            tobase.y = 0;

            var nearbase = tobase.magnitude < 0.01f;
            m_animator.SetBool(m_hashnearbase, nearbase);

            if (nearbase)
            {
                Quaternion targetrotation = Quaternion.RotateTowards(transform.rotation, m_originrotation, 360 * Time.deltaTime);
                transform.rotation = targetrotation;
            }
        }
        private IEnumerator WaitonPursuit()
        {
            yield return new WaitForSeconds(timetowait);

            m_enemycontroller.followtarget(m_OriginPos);
        }

      
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Color c = new Color(0, 0, 0.7f, 0.4f);
            UnityEditor.Handles.color = c;

            Vector3 rotatedforward = Quaternion.Euler(0, -playerscan.detectionangle * 0.5f, 0)*transform.forward;
            UnityEditor.Handles.DrawSolidArc(transform.position,Vector3.up,rotatedforward,
                playerscan.detectionangle, playerscan.detectionradius);

            UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedforward,
                360, playerscan.meleedetectionradius);


        }
#endif
    }
}
