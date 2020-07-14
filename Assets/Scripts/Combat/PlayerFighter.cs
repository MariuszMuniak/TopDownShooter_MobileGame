﻿using System.Collections;
using System.Collections.Generic;
using TDS_MG.Movement;
using UnityEngine;

namespace TDS_MG.Combat
{
    public class PlayerFighter : MonoBehaviour
    {
        [SerializeField] Transform weaponPlace = null;

        WeaponType weaponType;
        Animator animator;
        WeaponCollection weaponCollection;
        Transform characterModel;
        Weapon currentWeapon;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            weaponCollection = GetComponent<WeaponCollection>();
        }

        private void Start()
        {
            characterModel = GetComponent<PlayerMover>().GetCharacterModel();
            AttachDefaultWeapon();
        }

        private void Update()
        {
            UpdateAnimator();
            FaceWeaponPointForward();
            Fire();
        }

        public Sprite NextWeapon()
        {
            Weapon weapon = weaponCollection.NextWeapon();
            AttachWeapon(weapon);

            return weaponCollection.GetWeaponIcon(weapon.GetWeaponType());
        }

        public Sprite PreviousWeapon()
        {
            Weapon weapon = weaponCollection.PreviousWeapon();
            AttachWeapon(weapon);

            return weaponCollection.GetWeaponIcon(weapon.GetWeaponType());
        }

        private void UpdateAnimator()
        {
            animator.SetInteger("WeaponType_int", (int)weaponType);

            float bodyHorizontal;
            float bodyVertical;
            float headHorizontal = 0f;
            float headVertical = 0f;

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
                    bodyVertical = 0.0f;
                    headHorizontal = -0.4f;
                }
                else
                {
                    bodyHorizontal = 0.55f;
                    bodyVertical = 0f;
                    headHorizontal = -0.9f;
                }
            }

            animator.SetFloat("Body_Horizontal_f", bodyHorizontal);
            animator.SetFloat("Body_Vertical_f", bodyVertical);
            animator.SetFloat("Head_Horizontal_f", headHorizontal);
            animator.SetFloat("Head_Vertical_f", headVertical);
        }

        private void FaceWeaponPointForward()
        {
            if (currentWeapon == null) { return; }

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

        private void AttachDefaultWeapon()
        {
            Weapon weapon = weaponCollection.ShowDefaultWeapon();

            AttachWeapon(weapon);
        }

        private void AttachWeapon(Weapon weapon)
        {
            weaponCollection.HideAllWeapons();

            currentWeapon = weaponCollection.ShowWeapon(weapon);

            currentWeapon.transform.parent = weaponPlace;
            currentWeapon.gameObject.transform.localPosition = Vector3.zero;
            currentWeapon.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            currentWeapon.gameObject.transform.localScale = Vector3.one;

            this.weaponType = currentWeapon.GetWeaponType();
        }
    }
}