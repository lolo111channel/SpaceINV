using UnityEngine;

namespace SpaceInv
{
    public class Movement : MonoBehaviour
    {
        public bool IsDashStarted { get; private set; } = false;


        [SerializeField] private float _movementSpeed = 10.0f;
        [SerializeField] private float _acceleration = 0.1f;
        [SerializeField] private float _friction = 0.1f;
        [SerializeField] private Rigidbody2D _rb;

        [SerializeField] private float _rotationSpeed = 1.0f;
        [SerializeField] private bool _rotationDisable = false;

        [SerializeField] private float _dashSpeed = 25.0f;
        [SerializeField] private float _dashTime = 1.0f;

        private float _angle = 0.0f;
        private Vector2 _currentLinear = new();

        private float _currentMovementSpeed = 1.0f;
        private float _dashProgressToStop = 0.0f;

        public void Move(Vector2 dir)
        {
            Vector2 rotatedDir = gameObject.transform.TransformDirection(dir * _currentMovementSpeed);
            rotatedDir = rotatedDir.normalized;
            if (dir != Vector2.zero)
            {
                _currentLinear.x = Mathf.Lerp(_rb.linearVelocity.x, rotatedDir.x * _currentMovementSpeed, _acceleration);
                _currentLinear.y = Mathf.Lerp(_rb.linearVelocity.y, rotatedDir.y * _currentMovementSpeed, _acceleration);
                return;
            }

            _currentLinear.x = Mathf.Lerp(_rb.linearVelocity.x, 0.0f, _friction);
            _currentLinear.y = Mathf.Lerp(_rb.linearVelocity.y, 0.0f, _friction);

        }

        public void Dash(Vector2 dir)
        {
            _currentMovementSpeed = _dashSpeed;
            Move(dir);

            IsDashStarted = true;
        }

        public void Rotation(float angle)
        {
            _angle += angle * _rotationSpeed;
        }

        public void SetMovementSpeed(float val)
        {
            _movementSpeed = val;
            _currentMovementSpeed = _movementSpeed;
        }

        private void Awake()
        {
            _currentMovementSpeed = _movementSpeed;
        }

        private void Update()
        {
            if (IsDashStarted)
            {
                _dashProgressToStop += Time.deltaTime;
                if (_dashProgressToStop >= _dashTime)
                {
                    _dashProgressToStop = 0.0f;
                    IsDashStarted = false;

                    _currentMovementSpeed = _movementSpeed;
                }
                
            }
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
