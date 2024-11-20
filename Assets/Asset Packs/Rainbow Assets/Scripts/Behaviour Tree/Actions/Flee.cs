using UnityEngine;
using UnityEngine.AI;

namespace RainbowAssets.BehaviourTree.Actions
{
    public class Flee : ActionNode
    {
        [SerializeField] float fleeDistance = 5f;
        [SerializeField] float speed = 5f;       
        NavMeshAgent agent;

        protected override void OnEnter()
        {
            agent = controller.GetComponent<NavMeshAgent>();
            agent.speed = speed;

            Vector3 directionToPlayer = (controller.transform.position - Camera.main.transform.position).normalized;
            Vector3 fleePosition = controller.transform.position + directionToPlayer * fleeDistance;

            if (NavMesh.SamplePosition(fleePosition, out NavMeshHit hit, fleeDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
            else
            {
                agent.SetDestination(controller.transform.position + directionToPlayer * fleeDistance * 0.5f);
            }
        }

        protected override Status OnTick()
        {
            if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                return Status.Success; 
            }

            return Status.Running;
        }

        protected override void OnExit()
        {
            agent.ResetPath();
        }
    }
}
