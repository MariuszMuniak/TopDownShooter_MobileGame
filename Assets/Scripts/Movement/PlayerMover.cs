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
        [SerializeField] Transform rootModel = null;

        CharacterController characterController;
        bool isGrounded = false;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            isGrounded = characterController.isGrounded;

            Move();
            Rotation();
        }

        private void Rotation()
        {
            if (rotationJoystick == null) { return; }

            Vector3 direction = new Vector3
            {
                x = rootModel.position.x + rotationJoystick.Horizontal,
                y = rootModel.position.y,
                z = rootModel.position.z + rotationJoystick.Vertical
            };

            rootModel.LookAt(direction);
        }

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
    }
}
