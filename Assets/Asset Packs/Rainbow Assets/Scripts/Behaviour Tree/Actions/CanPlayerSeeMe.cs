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
                Vector3[] colliderPoints = new Vector3[]
                {
                    collider.bounds.center,
                    collider.bounds.min, 
                    collider.bounds.max, 
                    new Vector3(collider.bounds.min.x, collider.bounds.max.y, collider.bounds.min.z), 
                    new Vector3(collider.bounds.max.x, collider.bounds.min.y, collider.bounds.max.z)  
                };

                foreach(var point in colliderPoints)
                {
                    Vector3 directionToPlayer = (Camera.main.transform.position - point).normalized;
                    float distanceToPlayer = Vector3.Distance(Camera.main.transform.position, point);

                    if(!Physics.Raycast(point, directionToPlayer, distanceToPlayer))
                    {
                        return Status.Success;
                    }
                }
            }

            return Status.Running;
        }

        protected override void OnExit() { }
    }
}