using Unity.VisualScripting;
using UnityEngine;

namespace SpaceInv
{
    public abstract class EnemyAttackAbract : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] protected int _damage = 5;
        [SerializeField] protected float _speedAttack = 0.5f;
        [SerializeField] protected float _cooldownBeforeNextAttack = 1.0f;

        protected bool _canAttack = false;
        protected AI ai;
        
        private float _attackCooldownProgress = 0.0f;
        private float _cooldownBeforeNextAttackTime = 0.0f;

        public virtual void Attack() { }

        protected void TakeDamage(GameObject obj)
        {
            HealthComponent healthComponent = obj.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(_damage);
            }
        }


        protected void AttackRegeneration()
        {
            if (!_canAttack)
            {
                _cooldownBeforeNextAttackTime += Time.deltaTime;
                if (_cooldownBeforeNextAttackTime >= _cooldownBeforeNextAttack)
                {
                    _attackCooldownProgress += Time.deltaTime * _speedAttack;
                    if (_attackCooldownProgress >= 25.0f)
                    {
                        _attackCooldownProgress = 0.0f;
                        _cooldownBeforeNextAttack = 0.0f;
                        _canAttack = true;
                    }
                }
            }
        }

        protected void InitAttack()
        {
            ai = gameObject.GetComponent<AI>();
        }


        protected void ResetAttackCooldownProgress()
        {
            _attackCooldownProgress = 0.0f;
        }

    
    }

}
