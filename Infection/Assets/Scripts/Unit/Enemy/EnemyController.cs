using StatePatteren.State;
using UnityEngine;

namespace StatePatteren.StateEnemy
{
    public class EnemyController : MonoBehaviour
    {
        public EnemyFormation enemyFormation;

        private EnemyStateMachine stateMachine;
        public EnemyStateMachine EnemyStateMachine => stateMachine;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            enemyFormation = GameObject.Find("EnemyFormation").GetComponent<EnemyFormation>();
            stateMachine = new EnemyStateMachine(this);

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
            enemyFormation.enemyStats.enemyUnit.hp -= damage;

            Debug.Log($"Enemy : {damage}�̃_���[�W���󂯂�");

            if (enemyFormation.enemyStats.enemyUnit.hp <= 0)
            {
                Dead();
            }
        }

        // ��ŏ���
        public void Dead()
        {
            Destroy(gameObject);
        }
    }

}