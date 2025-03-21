using UnityEngine;

namespace SpaceInv
{
    public class Bullet : MonoBehaviour
    {
        public int Damage = 1;
        public float BulletSpeed;
        public Vector2 Dir = new();
        private Rigidbody2D _rb;

        private void OnEnable()
        {

            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            //_movement.Move(Vector2.up);
            _rb.linearVelocity = Dir * BulletSpeed;
           
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            HealthComponent healthCom =  collision.gameObject.GetComponent<HealthComponent>();
            if (healthCom != null)
            {
                healthCom.TakeDamage(Damage);
            }

            Destroy(gameObject);
        }
    }

}
