using System;
using UnityEngine;


namespace SpaceInv
{
    public class AIChasingState : IAIState
    {
        public void FixedProcess(AI ai)
        {
            ai.Movement.Move(Vector2.up);
        }

        public void Process(AI ai)
        {
            if (ai.CanAIAttack())
            {
                ai.ChangeState(new AIAttackState());
                return;
            }

            if (!ai.IsAISeeTarget())
            {
                ai.ChangeState(new AIIdleState());
                return;
            }

        
        }

        public void Start(AI ai)
        {
            Debug.Log("Start Chasing State");
        }

        public void Stop(AI ai)
        {
            Debug.Log("End Chasing State");
        }
    }
}
