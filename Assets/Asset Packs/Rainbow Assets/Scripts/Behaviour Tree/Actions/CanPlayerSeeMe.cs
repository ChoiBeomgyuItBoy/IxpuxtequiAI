using UnityEngine;

namespace RainbowAssets.BehaviourTree.Actions
{
    public class CanPlayerSeeMe : ActionNode
    {
        Collider collider;

        protected override void OnEnter()
        {
           collider = controller.GetComponent<Collider>();
        }

        protected override Status OnTick()
        {
            var cameraFrustum = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            
            if(GeometryUtility.TestPlanesAABB(cameraFrustum, collider.bounds))
            {
                Vector3 directionToPlayer = (Camera.main.transform.position - collider.bounds.center).normalized;
                float distanceToPlayer = Vector3.Distance(Camera.main.transform.position, collider.bounds.center);

                if(!Physics.Raycast(collider.bounds.center, directionToPlayer, distanceToPlayer))
                {
                    Debug.Log("Seen by player");
                    return Status.Success;
                }
            }

            return Status.Running;
        }

        protected override void OnExit() { }
    }   
}