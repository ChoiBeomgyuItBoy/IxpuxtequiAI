using UnityEngine;

namespace RainbowAssets.BehaviourTree.Actions
{
    public class SetVisibility : ActionNode
    {
        [SerializeField] bool visible = false;

        protected override void OnEnter()
        {
            controller.GetComponentInChildren<MeshRenderer>().enabled = visible;
            controller.GetComponent<Collider>().enabled = visible;
        }

        protected override Status OnTick()
        {
            return Status.Success;
        }

        protected override void OnExit() { }
    }
}