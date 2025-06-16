using UnityEngine;

namespace StatePatteren.State
{
    public class SquadController : MonoBehaviour
    {
        public SquadStats squadStats;

        private SquadStateMachine stateMachine;
        public SquadStateMachine StateMachine => stateMachine;

        public SquadFormation squadFormation;
         
        float maxDistance = 50f;        // ���m����ő勗��
        public bool isGuard { get; private set; } = false;

        public void SetUnitStats(SquadStats stats)
        {
            squadStats = stats;
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
                squadStats.leaderUnit.hp -= damage;
            }

            Debug.Log($"Squad : {damage}�̃_���[�W���󂯂�");

            if (squadStats.leaderUnit.hp <= 0)
            {
                Dead();
            }
        }

        // �񕜏���
        public void CareHp(float hp)
        {
            squadStats.leaderUnit.hp += hp;

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

        // �ł��߂��Ώۂ�Ԃ�
        public GameObject GetTarget(string targetTag)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
            GameObject nearest = null;
            float minDistance = maxDistance;

            foreach (var target in targets)
            {
                float dist = Vector2.Distance(transform.position, target.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    nearest = target;
                }
            }

            return nearest;
        }
    }

}