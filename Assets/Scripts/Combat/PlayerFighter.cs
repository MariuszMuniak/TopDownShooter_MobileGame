using System.Collections;
using System.Collections.Generic;
using TDS_MG.Movement;
using UnityEngine;

namespace TDS_MG.Combat
{
    public class PlayerFighter : MonoBehaviour
    {
        WeaponType weaponType;
        Animator animator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            weaponType = WeaponType.Rifle;
        }

        private void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            animator.SetInteger("WeaponType_int", (int)weaponType);

            float bodyHorizontal;
            float bodyVertical;

            bool isRunning = GetComponent<PlayerMover>().IsRunning();

            if (weaponType == WeaponType.NoWeapon)
            {
                bodyHorizontal = 0f;
                bodyVertical = 0f;
            }
            else if (weaponType == WeaponType.Pistol)
            {
                if (isRunning)
                {
                    bodyHorizontal = 0f;
                    bodyVertical = 0.2f;
                }
                else
                {
                    bodyHorizontal = 0f;
                    bodyVertical = 0f;
                }
            }
            else
            {
                if (isRunning)
                {
                    bodyHorizontal = 0.3f;
                    bodyVertical = 0.6f;
                }
                else
                {
                    bodyHorizontal = 0f;
                    bodyVertical = 0.6f;
                }
            }

            animator.SetFloat("Body_Horizontal_f", bodyHorizontal);
            animator.SetFloat("Body_Vertical_f", bodyVertical);
        }
    }
}
