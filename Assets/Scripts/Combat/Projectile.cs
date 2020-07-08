using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 10f;
        
        void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    } 
}
