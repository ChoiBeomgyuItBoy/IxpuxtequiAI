using UnityEngine;
using UnityEngine.AI;

namespace RainbowAssets.BehaviourTree.Actions
{
    public class ChasePlayer : ActionNode
    {
        [SerializeField] float chaseSpeed = 10;
        [SerializeField] float catchDistance = 10;
        GameObject player;
        NavMeshAgent agent;

        protected override void OnEnter()
        {
            player = GameObject.FindWithTag("Player");
            agent = controller.GetComponent<NavMeshAgent>();
            agent.speed = chaseSpeed;
        }
        
        protected override Status OnTick()
        {
            if(agent.pathStatus != NavMeshPathStatus.PathComplete)
            {
                return Status.Failure; 
            }

            agent.SetDestination(player.transform.position);

            if(Vector3.Distance(player.transform.position, controller.transform.position) <= catchDistance)
            {
                return Status.Success;
            }

            return Status.Running;
        }

        protected override void OnExit() { }
    }
}