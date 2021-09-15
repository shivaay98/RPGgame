using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgadventure
{
    public class playercontroller : MonoBehaviour
    {
        public static playercontroller Instance
        {
            get
            {
                return s_instance;
            }
        }
        public meleeweapon mmelee;
        const float k_acctime = 20.0f;
        const float k_deacctime = 535.0f;
        public float maxforwardspeed=8.0f;
        public float rotationspeed;
        public float gravity = 20.0f;
        private float m_verticalspeed;
        private CharacterController m_chcontroller;
        private cameracontroller m_cameracontroller;
        private playerinput m_playerinput;
        private Animator m_anim;
        private float m_desiredforwardspeed;
        private float m_forwardspeed;
        private Quaternion m_targetrotation;
        private static playercontroller s_instance;
        private readonly int m_hashforwardspeed = Animator.StringToHash("ForwardSpeed");
        private readonly int m_hashmeleeattack = Animator.StringToHash("meleeattack");
        private void Awake()
        {
            m_chcontroller = GetComponent<CharacterController>();
            m_cameracontroller = Camera.main.GetComponent<cameracontroller>();
            m_playerinput =GetComponent<playerinput>();
            m_anim = GetComponent<Animator>();

            s_instance = this;

        }
        private void FixedUpdate()
        {
            ComputeForwardMovement();
            ComputeVerticalMovement();
            ComputeRotation();

            if(m_playerinput.ismovenput)
            {
                m_targetrotation = Quaternion.RotateTowards(transform.rotation, m_targetrotation, 600 * Time.fixedDeltaTime);
                transform.rotation = m_targetrotation;
            }

            m_anim.ResetTrigger(m_hashmeleeattack);
            if(m_playerinput.isattack)
            {
                m_anim.SetTrigger(m_hashmeleeattack);
                mmelee.BeginAttack();
            }
        }

        private void OnAnimatorMove()
        {
            Vector3 movement = m_anim.deltaPosition;
            movement += m_verticalspeed * Vector3.up * Time.fixedDeltaTime;
            m_chcontroller.Move(movement);
        }
        private void ComputeVerticalMovement()
        {
            m_verticalspeed = -gravity;
        }

        private void ComputeForwardMovement()
        {
            Vector3 moveinput = m_playerinput.MoveInput.normalized;
            m_desiredforwardspeed = moveinput.magnitude * maxforwardspeed;

            float acceleration = m_playerinput.ismovenput ? k_acctime : k_deacctime;

            m_forwardspeed = Mathf.MoveTowards(m_forwardspeed, m_desiredforwardspeed, Time.fixedDeltaTime*acceleration);

            m_anim.SetFloat(m_hashforwardspeed, m_forwardspeed);
        }
    
        private void ComputeRotation()
        {
            Vector3 moveinput = m_playerinput.MoveInput.normalized;
            Vector3 cameradirection = Quaternion.Euler(
                0,
                m_cameracontroller.playercam.m_XAxis.Value,
                0) * Vector3.forward;

            Quaternion targetrotation;
            if (Mathf.Approximately(Vector3.Dot(moveinput, Vector3.forward),-1.0f))
            {

                targetrotation = Quaternion.LookRotation(-cameradirection);

            }else
            {
                Quaternion movementrotation = Quaternion.FromToRotation(Vector3.forward, moveinput);
                targetrotation = Quaternion.LookRotation(movementrotation * cameradirection);

            }


            m_targetrotation = targetrotation;
        }
    }

}


