using UnityEngine;

namespace StatePatteren.State
{
    public class UnitController : MonoBehaviour
    {
        UnitController unitController;

        public UnitStats unitStats { get; private set; }

        private SquadStateMachine stateMachine;
        public SquadStateMachine StateMachine => stateMachine;

        public UnitFormation unitFormation;
         
        public bool isGuard { get; private set; } = false;

        public void SetUnitStats(UnitStats stats)
        {
            unitStats = stats;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            unitFormation = GameObject.Find("SquadFormation").GetComponent<UnitFormation>();
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

            Debug.Log($"Unit：{damage}のダメージを受けた");

            if (unitStats.hp <= 0)
            {
                Dead();
            }
        }

        // 感染ダメージ処理
        public void TakeVirusDamage(float virusDamage)
        {
            unitStats.virusHp -= virusDamage;
            Debug.Log($"Unit：{virusDamage}の感染ダメージを受けた");
        }

        // 回復処理
        public void CareHp(float hp)
        {
            unitStats.hp += hp;

            Debug.Log($"Unit：体力が{hp}回復した");
        }

        // 感染回復処理
        public void CareVirusHp(float virusHp)
        {
            unitStats.virusHp += virusHp;

            Debug.Log($"Unit：感染体力が{virusHp}回復した");
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