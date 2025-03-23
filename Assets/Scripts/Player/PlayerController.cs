using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        private Fuel _fuel;

        private Vector2 _dir = new();

        private float _currentAngle = 0.0f;
        private bool _canMove = true;

        private void OnEnable()
        {
            _movement = GetComponent<Movement>();
            _shooting = GetComponent<Shooting>();
            _fuel = GetComponent<Fuel>();

        }

        private void Shoot(InputAction.CallbackContext context)
        {
            if (_cameraShake != null)
            {
                _cameraShake.RunShake(0.25f, _cameraShakePower, 1.0f);
            }
            _shooting.Shoot();
        }

        private void Move(float inputVal)
        {
            if (_fuel == null)
            {
                return;
            }

            if (_fuel.IsFuelFull())
            {
                _canMove = true;
            }

            if (_fuel.IsFuelEquelZero())
            {
                _canMove = false;
            }



            if (inputVal > 0.0f && _canMove)
            {
                _dir = Vector2.up;
                return;
            }    
             


            _dir = Vector2.zero;
            if (!_fuel.IsFuelFull())
            {
                _canMove = false;
            }
        }

        private void Update()
        {
            float _moveInputVal = _moveInput.action.ReadValue<float>();
            Move(_moveInputVal);
            

            float shootInputVal = _shootInput.action.ReadValue<float>();
            if (shootInputVal > 0.0f)
            {
                _shooting.Shoot();
            }

            //Debug Stuff
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }

        private void FixedUpdate()
        {
            _movement.Move(_dir);

            Vector3 mousePos = _mainCamera.ScreenToWorldPoint(new(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
            Vector3 mousePosDirRelativeToPlayerPos = (mousePos - transform.position).normalized;
            float angle = Mathf.Atan2(mousePosDirRelativeToPlayerPos.y, mousePosDirRelativeToPlayerPos.x) * Mathf.Rad2Deg - 90.0f;

            _currentAngle = Mathf.LerpAngle(_currentAngle, angle, 1.0f);
            _movement.SetRotation(_currentAngle);
        }

    }

}
