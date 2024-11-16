using UnityEngine;

namespace RainbowAssets.BehaviourTree.Actions
{
    public class SpawnPrefab : ActionNode
    {
        [SerializeField] GameObject prefab;

        protected override void OnEnter()
        {
            Instantiate(prefab, controller.transform.position, Quaternion.identity);
        }

        protected override Status OnTick()
        {
            return Status.Success;
        }

        protected override void OnExit() { }
    }
}