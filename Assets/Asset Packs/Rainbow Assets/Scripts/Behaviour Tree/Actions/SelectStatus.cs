using UnityEngine;

namespace RainbowAssets.BehaviourTree.Actions
{
    public class SelectStatus : ActionNode
    {
        [SerializeField] Status statusToSelect;

        protected override void OnEnter() { }

        protected override Status OnTick()
        {
            return statusToSelect;
        }

        protected override void OnExit() { }
    }
}