using UnityEngine;

namespace StatePatteren.StateEnemy
{
    public interface EnemyState
    {
        public void Enter()
        {
            // このStateに移行したときの処理
        }

        public void Update()
        {
            // このStateのとき毎フレーム呼び出される処理
        }

        public void Exit()
        {
            // このStateを抜けるときの処理
        }

        // TransitionToを呼び出すための処理
        public void Transition()
        {
            // このStateから別のStateへ移行する処理
        }
    }
}