using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace rpgadventure
{
    public class playerinput : MonoBehaviour
    {

        private Vector3 m_movement;
        private bool m_attack;
        public Vector3 MoveInput
        {
            get
            {
                return m_movement;
            }
        }

        public bool ismovenput
        {
            get
            {
                return !Mathf.Approximately(MoveInput.magnitude, 0);
            }
        }

        public bool isattack
        {
            get
            {
                return m_attack;
            }
        }
        void Update()
        {
            m_movement.Set(
                Input.GetAxis("Horizontal"),0,
                Input.GetAxis("Vertical"));

            if(Input.GetButtonDown("Fire1")&& !m_attack)
            {
                StartCoroutine(attackandwait());
            }
        }

        private IEnumerator attackandwait()
        {
            m_attack = true;
            yield return new WaitForSeconds(0.03f);
            m_attack = false;
        }
    }


}
