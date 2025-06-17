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

            Debug.Log($"Unit�F{damage}�̃_���[�W���󂯂�");

            if (unitStats.hp <= 0)
            {
                Dead();
            }
        }

        // �����_���[�W����
        public void TakeVirusDamage(float virusDamage)
        {
            unitStats.virusHp -= virusDamage;
            Debug.Log($"Unit�F{virusDamage}�̊����_���[�W���󂯂�");
        }

        // �񕜏���
        public void CareHp(float hp)
        {
            unitStats.hp += hp;

            Debug.Log($"Unit�F�̗͂�{hp}�񕜂���");
        }

        // �����񕜏���
        public void CareVirusHp(float virusHp)
        {
            unitStats.virusHp += virusHp;

            Debug.Log($"Unit�F�����̗͂�{virusHp}�񕜂���");
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