using UnityEngine;

namespace SpaceInv
{
    public class EnemyDashAttack : EnemyAttackAbract
    {
        public override void Attack()
        {
            if (_canAttack)
            {
                ai.Movement.Dash(Vector2.up);
                _canAttack = false;
            }
        }


        private void OnEnable()
        {
            InitAttack();
        }

        private void Update()
        {
            if (ai.Movement.IsDashStarted)
            {
                ai.Movement.Move(Vector2.up);
            }
            else
            {
                ai.Movement.Move(Vector2.zero);
            }


            AttackRegeneration();
        }
    }

}
