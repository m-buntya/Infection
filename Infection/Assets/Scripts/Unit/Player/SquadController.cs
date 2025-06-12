using UnityEngine;

namespace StatePatteren.State
{
    public class SquadController : MonoBehaviour
    {
        private SquadStateMachine stateMachine;
        public SquadStateMachine StateMachine => stateMachine;

        public SquadFormation squadFormation;

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

        // É_ÉÅÅ[ÉWèàóù
        public void TakeDamage(float damage)
        {
            squadFormation.squadStats.leaderUnit.hp -= damage;
            if (squadFormation.squadStats.leaderUnit.hp <= 0)
            {
                Dead();
            }
        }

        // âÛñ≈èàóù
        public void Dead()
        {
            Destroy(gameObject);
        }
    }

}