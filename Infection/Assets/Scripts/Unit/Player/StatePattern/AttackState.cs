using StatePatteren.StateEnemy;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace StatePatteren.State
{
    public class AttackState : SquadState
    {
        SquadController squadController;
        EnemyController enemyController;

        GameObject squadObj;
        GameObject targetObj;

        float atk = 0;
        float atkSpd = 0;

        float time = 0;

        private string targetTag = "Enemy";     // ���m�Ώۂ�tag
        public float maxDistance = 3.0f;        // ���m����ő勗��

        public AttackState(SquadController squadController)
        {
            this.squadController = squadController;
        }

        public void Enter()
        {
            squadObj = squadController.gameObject;

            atk = squadController.squadFormation.squadStats.leaderUnit.atk;
            atkSpd = squadController.squadFormation.squadStats.leaderUnit.atkSpd;
            time = 0;

            targetObj = GetTarget();
            enemyController = targetObj.GetComponent<EnemyController>();
        }

        public void Update()
        {
            AttackTimer();
        }

        public void Exit()
        {

        }

        public void Transition()
        {
            // �f�o�b�O�p
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

        // �U��
        void Attack()
        {
            enemyController.TakeDamage(atk);
        }

        // �U�����x
        void AttackTimer()
        {
            time += Time.deltaTime;

            if(time > atkSpd)
            {
                Attack();

                time = 0;
            }
        }

        // �G�����Ƃ̋����𑪂�
        public GameObject GetTarget()
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
            GameObject nearestObject = null;
            float nearestDistance = Mathf.Infinity;

            foreach (GameObject target in targets)
            {
                Vector3 direction = target.transform.position - squadObj.transform.position;
                float distance = direction.magnitude;

                // �������͈͓����`�F�b�N
                if (distance <= maxDistance)
                {
                    // Raycast�𔭎˂��ď�Q�����Ȃ����m�F
                    Ray ray = new Ray(squadObj.transform.position, direction.normalized);
                    if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
                    {
                        if (hit.collider.gameObject == target)
                        {
                            // �ŒZ�������X�V
                            if (distance < nearestDistance)
                            {
                                nearestDistance = distance;
                                nearestObject = target;
                            }
                        }
                    }
                }
            }

            return nearestObject;
        }
    }
}