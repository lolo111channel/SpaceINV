using Unity.VisualScripting;
using UnityEngine;

namespace SpaceInv
{
   
    public class AI : MonoBehaviour
    {

        [HideInInspector] public Movement Movement;
        public Transform Target = null;

        [SerializeField] private float _distanceNeedToChasing = 25.0f;
        [SerializeField] private float _distaceNeedToAttack = 5.0f;

        private IAIState _state = new AIIdleState();

        public void ChangeState(IAIState newState)
        {
            _state.Stop(this);
            _state = newState;
            _state.Start(this);
        }

        public bool IsAISeeTarget()
        {
            return Vector2.Distance(transform.position, Target.position) <= _distanceNeedToChasing;
        }

        public bool CanAIAttack()
        {
            return Vector2.Distance(transform.position, Target.position) <= _distaceNeedToAttack;
        }

        private void OnEnable()
        {
            Movement = GetComponent<Movement>();
        }

        private void Update()
        {
            _state.Process(this);
        }

        private void FixedUpdate()
        {
            if (Target != null)
            {
                Vector3 look = transform.InverseTransformPoint(Target.position);
                float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;

                transform.Rotate(0, 0, angle);
            }


            _state.FixedProcess(this);
        }

    }

}
