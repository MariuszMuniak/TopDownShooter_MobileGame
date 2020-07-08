using System.Collections;
using System.Collections.Generic;
using TDS_MG.Movement;
using UnityEngine;

namespace TDS_MG.Combat
{
    public class PlayerFighter : MonoBehaviour
    {
        [SerializeField] Transform weaponPlace = null;
        [SerializeField] Weapon gun = null;
        WeaponType weaponType;
        Animator animator;
        Transform characterModel;
        Weapon currentWeapon;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            AttachWeapon();
            characterModel = GetComponent<PlayerMover>().GetCharacterModel();
        }

        private void Update()
        {
            UpdateAnimator();
            FaceWeaponPointForward();
            Fire();
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

        private void FaceWeaponPointForward()
        {
            Vector3 point = new Vector3
            {
                x = 0f,
                y = characterModel.InverseTransformPoint(weaponPlace.position).y,
                z = currentWeapon.GetRange()
            };

            point = characterModel.TransformPoint(point);
            weaponPlace.LookAt(point);
        }

        private void Fire()
        {
            if (currentWeapon == null) { return; }

            if (currentWeapon.CanAttack())
            {
                currentWeapon.Fire();
            }
        }

        private void AttachWeapon()
        {
            currentWeapon = Instantiate(gun, weaponPlace);
            weaponType = currentWeapon.GetWeaponType();
        }
    }
}
