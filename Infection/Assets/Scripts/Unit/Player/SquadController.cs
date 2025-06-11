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

        public void Dead()
        {
            Destroy(gameObject);
        }
    }

}