using UnityEngine;

namespace SpaceInv
{
    public class EnemyShootAttack : EnemyAttackAbract
    {
        [SerializeField] private Shooting _shooting;
        public override void Attack()
        {
            if (_canAttack)
            {
                _shooting.Shoot();
                _canAttack = false;
            }
        }

        private void OnEnable()
        {
            InitAttack();
        }

        private void Update()
        {

            ai.Movement.Move(Vector2.zero);
           
            if (ai.State is AIAttackState)
            {
                AttackRegeneration();

            }
            else
            {
                ResetAttackCooldownProgress();
            }
        }
    }

}
