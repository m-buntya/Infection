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

        //// �����̏�����
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

        // �_���[�W����
        public void TakeDamage(float damage)
        {
            if(isGuard)
            {
                Debug.Log("�_���[�W�𖳌���");
            }
            else
            {
                unitStats.hp -= damage;
            }

            Debug.Log($"Squad : {damage}�̃_���[�W���󂯂�");

            if (unitStats.hp <= 0)
            {
                Dead();
            }
        }

        // �񕜏���
        public void CareHp(float hp)
        {
            unitStats.hp += hp;

            Debug.Log($"Squad : {hp}�񕜂���");
        }

        // �K�[�h����
        public void Guard()
        {
            isGuard = true;
        }

        // ��ŏ���
        public void Dead()
        {
            Destroy(gameObject);
        }
    }

}