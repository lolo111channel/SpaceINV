using UnityEngine;

namespace SpaceInv
{
    public class Bullet : MonoBehaviour
    {
        public int Damage = 1;
        public float BulletSpeed;
        private Movement _movement;

        private void OnEnable()
        {
            _movement = GetComponent<Movement>();
            _movement.SetMovementSpeed(BulletSpeed);
        }

        private void FixedUpdate()
        {
            _movement.Move(Vector2.up);
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
