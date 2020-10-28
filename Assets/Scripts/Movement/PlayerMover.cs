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

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            Move();
            Rotate();
            UpdateAnimatorParameters();
        }

        private void Move()
        {
            if (moveJoystick == null)
            {
                return;
            }

            Vector3 direction = new Vector3
            {
                x = moveJoystick.Horizontal,
                y = IsGrounded() ? 0f : gravity,
                z = moveJoystick.Vertical
            };

            characterController.Move(direction * speed * Time.deltaTime);
        }

        private bool IsGrounded() => characterController.isGrounded;

        private void Rotate()
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

        private void UpdateAnimatorParameters()
        {
            float velocity = characterController.velocity.magnitude;
            int moveJoystickValue = CoordinateSystemQuarter(moveJoystick.Horizontal, moveJoystick.Vertical);
            int characterJoystickValue = CoordinateSystemQuarter(characterModel.forward.x, characterModel.forward.z);
            bool moveBckward = IsMovingBckward(moveJoystickValue, characterJoystickValue);
            bool moveRight = IsMovingRight(moveJoystickValue, characterJoystickValue);
            bool moveLeft = IsMovingLeft(moveJoystickValue, characterJoystickValue);

            Debug.Log($"Move: {moveJoystick.Horizontal}/{moveJoystick.Vertical} | Rotation: {characterModel.forward.x}/{characterModel.forward.z}");

            animator.SetBool("Static_b", true);
            animator.SetFloat("Speed_f", velocity);
            animator.SetBool("RunBckward_b", moveBckward);
            animator.SetBool("RunRight_b", moveRight);
            animator.SetBool("RunLeft_b", moveLeft);
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

        bool IsMovingBckward(int moveJoystickValue, int characterJoystickValue)
        {
            if (moveJoystickValue == 1 && characterJoystickValue == 3)
            {
                return true;
            }
            else if (moveJoystickValue == 2 && characterJoystickValue == 4)
            {
                return true;
            }
            else if (moveJoystickValue == 3 && characterJoystickValue == 1)
            {
                return true;
            }
            else if (moveJoystickValue == 4 && characterJoystickValue == 2)
            {
                return true;
            }

            return false;
        }

        bool IsMovingRight(int moveJoystickValue, int characterJoystickValue)
        {
            if (moveJoystickValue == 1 && characterJoystickValue == 2)
            {
                return true;
            }
            else if (moveJoystickValue == 2 && characterJoystickValue == 3)
            {
                return true;
            }
            else if (moveJoystickValue == 3 && characterJoystickValue == 4)
            {
                return true;
            }
            else if (moveJoystickValue == 4 && characterJoystickValue == 1)
            {
                return true;
            }

            return false;
        }

        bool IsMovingLeft(int moveJoystickValue, int characterJoystickValue)
        {
            if (moveJoystickValue == 1 && characterJoystickValue == 4)
            {
                return true;
            }
            else if (moveJoystickValue == 2 && characterJoystickValue == 1)
            {
                return true;
            }
            else if (moveJoystickValue == 3 && characterJoystickValue == 2)
            {
                return true;
            }
            else if (moveJoystickValue == 4 && characterJoystickValue == 3)
            {
                return true;
            }

            return false;
        }

        public bool IsRunning() => moveJoystick.Direction != Vector2.zero;

        public Transform GetCharacterModel() => characterModel;
    }
}
