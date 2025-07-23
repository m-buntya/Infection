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
            SetDragPreviewAlpha(unitController.gameObject, 0.5f);
        }

        public async void Update()
        {
            if(unitController.GetUnitGroup() == UnitController.UNIT_GROUP.PLAYER)
            {
                await WaitEndDrag.WaitDragEndAsync();
            }

            time += Time.deltaTime;
            if (time >= sortieTime)
            {
                Transition();
            }
        }

        public void Exit()
        {
            SetDragPreviewAlpha(unitController.gameObject, 1.0f);
        }

        // TransitionTo‚ğŒÄ‚Ño‚·‚½‚ß‚Ìˆ—
        public void Transition()
        {
            unitController.StateMachine.TransitionTo(unitController.StateMachine.moveState);            
        }

        private void SetDragPreviewAlpha(GameObject obj, float alpha)
        {
            foreach (var sr in obj.GetComponentsInChildren<SpriteRenderer>())
            {
                Color c = sr.color;
                c.a = alpha;
                sr.color = c;
            }
        }
    }
}

