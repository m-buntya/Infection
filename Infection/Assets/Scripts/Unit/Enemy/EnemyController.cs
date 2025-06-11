using StatePatteren.State;
using UnityEngine;

namespace StatePatteren.StateEnemy
{
    public class EnemyController : MonoBehaviour
    {
        private EnemyStateMachine stateMachine;
        public EnemyStateMachine EnemyStateMachine => stateMachine;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}