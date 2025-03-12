using System;
using UnityEngine;

namespace SpaceInv
{
    public class AIAttackState : IAIState
    {
        public void FixedProcess(AI ai)
        {
            ai.EnemyAttack.Attack();
        }

        public void Process(AI ai)
        {
            if (!ai.IsAISeeTarget())
            {
                ai.ChangeState(new AIIdleState());
                return;
            }

            if (!ai.CanAIAttack())
            {
                ai.ChangeState(new AIChasingState());
                return;
            }

        }

        public void Start(AI ai)
        {
            Debug.Log("Start Attack State");
        }

        public void Stop(AI ai)
        {
            Debug.Log("End Attack State");
        }
    }
}
