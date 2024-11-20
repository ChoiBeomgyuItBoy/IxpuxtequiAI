using UnityEditor;
using UnityEngine;

namespace RainbowAssets.BehaviourTree
{
    public abstract class DecoratorNode : Node
    {
        [SerializeField] Node child;
        [SerializeField] BehaviourTree abortTree;
        [SerializeField] Status abortStatus = Status.Success;

        public override Node Clone()
        {
            DecoratorNode clone = Instantiate(this);

            clone.child = child.Clone();

            if (abortTree != null)
            {
                clone.abortTree = abortTree.Clone();
            }
            
            return clone;
        }

        public override void Bind(BehaviourTreeController controller)
        {
            base.Bind(controller);

            if (abortTree != null)
            {
                abortTree.Bind(controller);
            }
        }

        public Node GetChild()
        {
            return child;
        }

        public override void Abort()
        {
            child.Abort();
            base.Abort();
        }

        public override Status Tick()
        {
            if(abortTree != null)
            {
                Status abortTreeStatus = abortTree.Tick();

                if(abortTreeStatus == Status.Failure || abortTreeStatus == Status.Success)
                {
                    Abort();
                    return abortStatus;
                }
            }

            return base.Tick();
        }

#if UNITY_EDITOR
        public void SetChild(Node child)
        {
            Undo.RecordObject(this, "Child Set");
            this.child = child;
            EditorUtility.SetDirty(this);
        }

        public void UnsetChild()
        {
            Undo.RecordObject(this, "Child removed");
            child = null;
            EditorUtility.SetDirty(this);
        }
#endif
    }
}   