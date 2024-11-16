using UnityEngine;
using UnityEngine.AI;

namespace RainbowAssets.BehaviourTree.Actions
{
    public class Teleport : ActionNode
    {
        [SerializeField] float teleportRadius = 20;
        GameObject player;
        NavMeshAgent agent;
        
        protected override void OnEnter()
        {
            player = GameObject.FindWithTag("Player");
            agent = controller.GetComponent<NavMeshAgent>();
        }

        protected override Status OnTick()
        {
            Vector3 randomDirection = Random.onUnitSphere * teleportRadius;
            randomDirection += player.transform.position;

            if(NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, teleportRadius, NavMesh.AllAreas))
            {
                Vector3 worldViewPoint = Camera.main.WorldToViewportPoint(randomDirection);

                if(worldViewPoint.z < 0)
                {
                    agent.Warp(hit.position);
                    
                    return Status.Success;
                }

                return Status.Running;
            }
            
            return Status.Failure;
        }

        protected override void OnExit() { }
    }
}