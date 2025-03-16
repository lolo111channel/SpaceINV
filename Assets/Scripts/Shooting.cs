using UnityEngine;

namespace SpaceInv
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _bulletSpeed = 5.0f;
        [SerializeField] private float _shootingSpeed = 1.0f;
        [SerializeField] private float _repulse = 1.5f;

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawn;
        [SerializeField] private Rigidbody2D _rb;

        [SerializeField] private bool _setManuallyCanShoot = false; 
        
        private bool _canShoot = true;
        private float _canShootProgress = 0.0f;
        private float _canShootProgressMax = 100.0f;

        public void Shoot()
        {
            if (_setManuallyCanShoot)
            {
                _canShoot = true;
            }

            if (_canShoot)
            {
            
                GameObject go = Instantiate(_bulletPrefab, _bulletSpawn.position, gameObject.transform.rotation, null);

                Bullet bullet = go.GetComponent<Bullet>();
                bullet.BulletSpeed = _bulletSpeed;
                bullet.Damage = _damage;

                Vector2 rotatedDir = gameObject.transform.TransformDirection(Vector2.up);

                float randomDirX = Random.Range(-0.5f, 0.5f);
                float randomDirY = Random.Range(-0.5f, 0.5f);

                rotatedDir = new Vector2(rotatedDir.x + randomDirX, rotatedDir.y + randomDirY);

                _rb.linearVelocity = (rotatedDir * -1) * _repulse;

                _canShoot = false;
            }
        }

        private void Update()
        {
            if (!_canShoot && _canShootProgress <= _canShootProgressMax)
            {
                _canShootProgress += _shootingSpeed * Time.deltaTime;
                if (_canShootProgress >= _canShootProgressMax)
                {
                    _canShoot = true;
                    _canShootProgress = 0.0f;
                }
            }
        }
    }

}
