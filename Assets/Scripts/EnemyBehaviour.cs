using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public ColorType color;

        NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            agent.SetDestination(new Vector3(0, 0, 100));
        }
    }
}