using Unity.VisualScripting;
using UnityEngine;

namespace SpaceInv
{
    public abstract class EnemyAttackAbract : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] protected int _damage = 5;
        [SerializeField] protected float _speedAttack = 0.5f;

        protected bool _canAttack = false;
        protected AI ai;
        
        private float _attackCooldownProgress = 0.0f;

        public virtual void Attack() { }


        protected void AttackRegeneration()
        {
            if (!_canAttack)
            {
                Debug.Log(_attackCooldownProgress);
                _attackCooldownProgress += Time.deltaTime * _speedAttack;
                if (_attackCooldownProgress >= 25.0f)
                {
                    _attackCooldownProgress = 0.0f;
                    _canAttack = true;
                }
            }
        }

        protected void InitAttack()
        {
            ai = gameObject.GetComponent<AI>();
        }


    
    }

}
