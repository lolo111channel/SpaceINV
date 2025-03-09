using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceInv
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference _moveInput;
        [SerializeField] private InputActionReference _rotationInput;
        [SerializeField] private InputActionReference _shootInput;

        private Movement _movement;
        private Shooting _shooting;

        private Vector2 _dir = new();

        private void OnEnable()
        {
            _movement = GetComponent<Movement>();
            _shooting = GetComponent<Shooting>();

            _moveInput.action.started += Move;
            _moveInput.action.canceled += Move;

            _shootInput.action.performed += Shoot;
        }

        private void Shoot(InputAction.CallbackContext context)
        {
            _shooting.Shoot();
        }

        private void Move(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _dir = Vector2.up;
            }
            else if (context.canceled)
            {
                _dir = Vector2.zero;
            }
        }

        private void FixedUpdate()
        {
            _movement.Move(_dir);

            float rotationInputVal = _rotationInput.action.ReadValue<float>();
            _movement.Rotation(rotationInputVal);
        }

    }

}
