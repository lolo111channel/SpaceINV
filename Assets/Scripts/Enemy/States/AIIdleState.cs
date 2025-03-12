using System;
using UnityEngine;


namespace SpaceInv
{
    public class AIIdleState : IAIState
    {
        public void FixedProcess(AI ai)
        {
            ai.Movement.Move(Vector2.zero);
        }

        public void Process(AI ai)
        {
            if (ai.IsAISeeTarget())
            {
                ai.ChangeState(new AIChasingState());
            }
        }

        public void Start(AI ai)
        {
            Debug.Log("Start Idle State");
        }

        public void Stop(AI ai)
        {
            Debug.Log("End Idle State");
        }
    }
}
