using UnityEngine;

namespace StatePatteren.State
{
    public class SquadController : MonoBehaviour
    {
        SquadController squadController;

        public UnitStats unitStats;

        private SquadStateMachine stateMachine;
        public SquadStateMachine StateMachine => stateMachine;

        public SquadFormation squadFormation;
         
        public bool isGuard { get; private set; } = false;

        //// 部隊の初期化
        //public SquadController(SquadController controller)
        //{
        //    this.squadController = controller;
        //}

        public void SetUnitStats(UnitStats stats)
        {
            unitStats = stats;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            squadFormation = GameObject.Find("SquadFormation").GetComponent<SquadFormation>();
            stateMachine = new SquadStateMachine(this);

            stateMachine.Initialize(stateMachine.moveState);
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.Update();
            stateMachine.Transition();
        }

        // ダメージ処理
        public void TakeDamage(float damage)
        {
            if(isGuard)
            {
                Debug.Log("ダメージを無効化");
            }
            else
            {
                unitStats.hp -= damage;
            }

            Debug.Log($"Squad : {damage}のダメージを受けた");

            if (unitStats.hp <= 0)
            {
                Dead();
            }
        }

        // 回復処理
        public void CareHp(float hp)
        {
            unitStats.hp += hp;

            Debug.Log($"Squad : {hp}回復した");
        }

        // ガード処理
        public void Guard()
        {
            isGuard = true;
        }

        // 壊滅処理
        public void Dead()
        {
            Destroy(gameObject);
        }
    }

}