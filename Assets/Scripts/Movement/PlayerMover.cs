using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Movement
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] float speed = 10f;
        [SerializeField] float gravity = -1f;
        [SerializeField] FixedJoystick moveJoystick = null;
        [SerializeField] FixedJoystick rotationJoystick = null;
        [SerializeField] Transform characterModel = null;

        CharacterController characterController;
        Animator animator;
        bool isGrounded = false;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            isGrounded = characterController.isGrounded;

            Move();
            Rotation();
            UpdateAnimator();
        }

        public bool IsRunning() => moveJoystick.Direction != Vector2.zero;

        private void Move()
        {
            if (moveJoystick == null) { return; }

            Vector3 direction = new Vector3
            {
                x = moveJoystick.Horizontal,
                y = isGrounded ? 0f : gravity,
                z = moveJoystick.Vertical
            };

            characterController.Move(direction * speed * Time.deltaTime);
        }

        private void Rotation()
        {
            if (rotationJoystick == null) { return; }

            Vector3 direction = new Vector3
            {
                x = characterModel.position.x + rotationJoystick.Horizontal,
                y = characterModel.position.y,
                z = characterModel.position.z + rotationJoystick.Vertical
            };

            characterModel.LookAt(direction);
        }

        private void UpdateAnimator()
        {
            float velocity = characterController.velocity.magnitude;
            animator.SetBool("Static_b", true);
            animator.SetFloat("Speed_f", velocity);

            bool moveBckward = false;
            int joystickValue = CoordinateSystemQuarter(moveJoystick.Horizontal, moveJoystick.Vertical);
            int characterValue = CoordinateSystemQuarter(characterModel.forward.x, characterModel.forward.z);

            if (joystickValue == 1 && characterValue == 3)
            {
                moveBckward = true;
            }
            else if (joystickValue == 2 && characterValue == 4)
            {
                moveBckward = true;
            }
            else if (joystickValue == 3 && characterValue == 1)
            {
                moveBckward = true;
            }
            else if (joystickValue == 4 && characterValue == 1)
            {
                moveBckward = true;
            }

            animator.SetBool("RunBckward_b", moveBckward);
        }

        private int CoordinateSystemQuarter(float x, float y)
        {
            if (x == 0 && y == 0) { return 0; }

            if (x >= 0f && y >= 0f)
            {
                return 1;
            }
            else if (x < 0f && y >= 0f)
            {
                return 2;
            }
            else if (x <= 0f && y <= 0f)
            {
                return 3;
            }
            else if (x > 0f && y < 0f)
            {
                return 4;
            }
            else
            {
                return 0;
            }
        }
    }
}
