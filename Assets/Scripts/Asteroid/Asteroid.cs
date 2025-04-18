using UnityEngine;


namespace SpaceInv
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private bool _canMove = true;
        [SerializeField] private Vector2 _dir;

        private Movement _movement;
        private Rigidbody2D _rb;
    

        private void OnEnable()
        {
            _movement = GetComponent<Movement>();
            _rb = GetComponent<Rigidbody2D>();

            

        }

        private void Start()
        {
            float randomRot = Random.Range(-360f, 360f);
            transform.Rotate(new(0,0,randomRot));

            if (!_canMove)
            {
                _rb.bodyType = RigidbodyType2D.Kinematic;
            }

        }

        private void FixedUpdate()
        {
            if (_canMove && _movement != null)
            {
                _movement.Move(_dir);
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Asteroid")
            {
                float newX = Random.Range(-1.0f, 1.0f);
                float newY = Random.Range(-1.0f, 1.0f);
                Vector2 newDir = new Vector2(newX, newY);

                newDir = newDir.normalized;
                _dir = newDir;
            }


            if (collision.gameObject.tag == "Player")
            {
                HealthComponent health = collision.gameObject.GetComponent<HealthComponent>();
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (health != null && rb != null)
                {
                    Debug.Log(new Vector2(rb.linearVelocity.x, rb.linearVelocity.y));
                    int damage = Mathf.RoundToInt(Mathf.Abs(rb.linearVelocity.x) + Mathf.Abs(rb.linearVelocity.y)) / 2;
                    if (damage <= 0)
                    {
                        damage = 1;
                    }
                    Debug.Log(damage);
                    health.TakeDamage(damage);
                }
            }
        }
    }
}

