using UnityEngine;

namespace SpaceInv
{
    public class Movement : MonoBehaviour
    {
        public bool IsDashStarted { get; private set; } = false;


        public delegate void ObjectIsMoving();
        public event ObjectIsMoving OnObjectIsMoving;

        [SerializeField] private float _movementSpeed = 10.0f;
        [SerializeField] private float _maxMovementSpeed = 100.0f;
        [SerializeField] private float _acceleration = 0.1f;
        [SerializeField] private float _friction = 0.1f;
        [SerializeField] private Rigidbody2D _rb;

        [SerializeField] private float _rotationSpeed = 1.0f;
        [SerializeField] private bool _rotationDisable = false;

        [SerializeField] private float _dashSpeed = 25.0f;
        [SerializeField] private float _dashTime = 1.0f;

        private float _angle = 0.0f;
        [SerializeField] private Vector2 _currentLinear = new();

        private float _currentMovementSpeed = 1.0f;
        private float _dashProgressToStop = 0.0f;


        public void Move(Vector2 dir)
        {
           
            Vector2 rotatedDir = gameObject.transform.TransformDirection(dir);
            rotatedDir = rotatedDir.normalized;

            if (dir != Vector2.zero)
            {
                OnObjectIsMoving?.Invoke();
                _currentLinear.x = Mathf.Lerp(_currentLinear.x, rotatedDir.x * _currentMovementSpeed, _acceleration);
                _currentLinear.y = Mathf.Lerp(_currentLinear.y, rotatedDir.y * _currentMovementSpeed, _acceleration);
                return;
            }

            _currentLinear.x = Mathf.Lerp(_currentLinear.x, 0.0f, _friction);
            _currentLinear.y = Mathf.Lerp(_currentLinear.y, 0.0f, _friction);

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

        public void SetRotation(float angle)
        {
            _angle = angle;
        }

        public void SetMovementSpeed(float val)
        {
            _movementSpeed = val;
            _currentMovementSpeed = _movementSpeed;
        }

        public void ResetMovemnet()
        {
            _rb.linearVelocity = Vector2.zero;
        }

        public Vector2 GetCurrentDir(Vector2 dir)
        {
            return gameObject.transform.TransformDirection(dir);
        }

        public void SetCurrentLinear(Vector2 newVec)
        {
            _currentLinear = newVec;
        }

        public Vector2 GetCurrentLinear() => _currentLinear;

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
            Vector2 linearVel = _currentLinear;
            if ((Mathf.Abs(linearVel.x) + Mathf.Abs(linearVel.y)) > _maxMovementSpeed)
            {
                Vector2 normalizedDir = _rb.linearVelocity.normalized;
                
                linearVel = normalizedDir * _maxMovementSpeed;
            }

            _rb.linearVelocity = linearVel;

            if (!_rotationDisable)
            {
                _rb.MoveRotation(_angle);
            }
        }
    }

}
