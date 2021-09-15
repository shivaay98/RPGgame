using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace rpgadventure
{
    public class fixedupdatefollow : MonoBehaviour
    {
        public Transform tofollow;
        void FixedUpdate()
        {
            transform.position = tofollow.position;
            transform.rotation = tofollow.rotation;
        }
    }
}

