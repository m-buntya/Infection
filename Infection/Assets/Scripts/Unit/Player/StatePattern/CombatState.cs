using StatePatteren.StateEnemy;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace StatePatteren.State
{
    public class CombatState : SquadState
    {
        SquadController squadController;
        EnemyController enemyController;

        float atkSpd = 0;

        float time = 0;

        public CombatState(SquadController squadController)
        {
            this.squadController = squadController;
        }

        public void Enter()
        {
            atkSpd = squadController.squadStats.leaderUnit.atkSpd;
            time = 0;
        }

        public void Update()
        {
          
            //ターゲットが射程外またはいないなら移動に戻る
            if(squadController.currentTarget == null || !squadController.IsTargetInRange(squadController.currentTarget))
            {
                squadController.StateMachine.TransitionTo(squadController.StateMachine.moveState);
                return;
            }
             ActionTimer();
        }

        public void Exit()
        {

        }

        public void Transition()
        {
            // デバッグ用
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    squadController.StateMachine.TransitionTo(squadController.StateMachine.moveState);
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    squadController.StateMachine.TransitionTo(squadController.StateMachine.deadState);
                }
            }
        }

        // 行動速度
        void ActionTimer()
        {
            time += Time.deltaTime;

            if(time > atkSpd)
            {
                RoleAction(squadController.squadStats);
                time = 0;
            }
        }

        public void RoleAction(SquadStats stats)
        {
            switch (stats.leaderUnit.role)
            {
                case UnitStats.ROLE.Attacker:
                    Attack();
                    break;
                case UnitStats.ROLE.Healer:
                    Heal();
                    break;
                case UnitStats.ROLE.Tank:
                    Cover();
                    break;
            }
        }

        // アタッカー
        void Attack()
        {
            GameObject targetObj = squadController.GetTarget("Enemy");
            if(targetObj == null)
            {
                Debug.LogError("攻撃対象がいません");
            }
            else
            {
                EnemyController target = targetObj.GetComponent<EnemyController>();
                Debug.Log($"攻撃対象：{target}");
                target.TakeDamage(squadController.squadStats.leaderUnit.atk);
            }
        }

        // ヒーラー
        void Heal()
        {
            GameObject targetObj = squadController.GetTarget("Squad");
            if (targetObj == null)
            {
                Debug.LogError("支援対象がいません");
            }
            else
            {
                SquadController target = targetObj.GetComponent<SquadController>();
                Debug.Log($"支援対象：{target}");
                target.CareHp(squadController.squadStats.leaderUnit.atk);
            }
        }

        // タンク
        void Cover()
        {

        }
    }
}