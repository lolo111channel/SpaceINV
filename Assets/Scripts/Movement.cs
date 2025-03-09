using UnityEngine;

namespace SpaceInv
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 10.0f;
        [SerializeField] private float _acceleration = 0.1f;
        [SerializeField] private float _friction = 0.1f;
        [SerializeField] private Rigidbody2D _rb;

        [SerializeField] private float _rotationSpeed = 1.0f;
        [SerializeField] private bool _rotationDisable = false;

        private float _angle = 0.0f;
        private Vector2 _currentLinear = new();

        public void Move(Vector2 dir)
        {
            Vector2 rotatedDir = gameObject.transform.TransformDirection(dir * _movementSpeed);
            rotatedDir = rotatedDir.normalized;
            if (dir != Vector2.zero)
            {
                _currentLinear.x = Mathf.Lerp(_rb.linearVelocity.x, rotatedDir.x * _movementSpeed, _acceleration);
                _currentLinear.y = Mathf.Lerp(_rb.linearVelocity.y, rotatedDir.y * _movementSpeed, _acceleration);
                return;
            }

            _currentLinear.x = Mathf.Lerp(_rb.linearVelocity.x, 0.0f, _friction);
            _currentLinear.y = Mathf.Lerp(_rb.linearVelocity.y, 0.0f, _friction);

        }

        public void Rotation(float angle)
        {
            _angle += angle * _rotationSpeed;
        }

        public void SetMovementSpeed(float val)
        {
            _movementSpeed = val;
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = _currentLinear;

            if (!_rotationDisable)
            {
                _rb.MoveRotation(_angle);
            }
        }
    }

}
