using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceInv
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference _moveInput;
        [SerializeField] private InputActionReference _shootInput;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private CameraShake _cameraShake;

        [SerializeField] private float _cameraShakePower = 1.5f;

        private Movement _movement;
        private Shooting _shooting;

        private Vector2 _dir = new();

        private float _currentAngle = 0.0f;

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
            if (_cameraShake != null)
            {
                _cameraShake.RunShake(0.25f, _cameraShakePower, 1.0f);
            }
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

            Vector3 mousePos = _mainCamera.ScreenToWorldPoint(new(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
            Vector3 mousePosDirRelativeToPlayerPos = (mousePos - transform.position).normalized;
            float angle = Mathf.Atan2(mousePosDirRelativeToPlayerPos.y, mousePosDirRelativeToPlayerPos.x) * Mathf.Rad2Deg - 90.0f;

            _currentAngle = Mathf.LerpAngle(_currentAngle, angle, 0.1f);
            _movement.SetRotation(_currentAngle);
        }

    }

}
