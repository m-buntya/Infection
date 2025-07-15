using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace StatePatteren.State
{
    public class ReadyState : UnitState
    {
        UnitController unitController;
        float time = 0;
        float sortieTime = 3.0f;

        public ReadyState(UnitController unitController)
        {
            this.unitController = unitController;
        }

        public void Enter()
        {

        }

        public async void Update()
        {
            await WaitEndDrag.WaitDragEndAsync();
            time += Time.deltaTime;
        }

        public void Exit()
        {
            
        }

        // TransitionTo‚ðŒÄ‚Ño‚·‚½‚ß‚Ìˆ—
        public void Transition()
        {
            if(time >= sortieTime)
            {
                unitController.StateMachine.TransitionTo(unitController.StateMachine.moveState);
            }
        }
    }
}

