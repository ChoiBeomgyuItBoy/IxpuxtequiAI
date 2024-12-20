using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RainbowAssets.BehaviourTree
{
    public abstract class CompositeNode : Node
    {
        [SerializeField] List<Node> children = new();
        [SerializeField] BehaviourTree abortTree;
        [SerializeField] Status abortStatus = Status.Success;

        public override Node Clone()
        {
            CompositeNode clone = Instantiate(this);

            clone.children.Clear();

            foreach (var child in children)
            {
                clone.children.Add(child.Clone());
            }

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

        public IEnumerable<Node> GetChildren()
        {
            return children;
        }

        public Node GetChild(int index)
        {
            return children[index];
        }

        public void SortChildrenByPosition()
        {
            children.Sort(ComparePosition);
        }

        public void SortChildrenByPriority()
        {
            children.Sort(ComparePriority);
        }

        public void ShuffleChildren()
        {
            int currentChild = children.Count;

            while (currentChild > 1)
            {
                currentChild--;

                int randomIndex = new System.Random().Next(currentChild + 1);
                Node randomNode = children[randomIndex];

                children[randomIndex] = children[currentChild];
                children[currentChild] = randomNode;
            }
        }

        public override void Abort()
        {
            foreach (var child in children)
            {
                child.Abort();
            }

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
        public void AddChild(Node child)
        {
            Undo.RecordObject(this, "Child added");
            children.Add(child);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(Node childToRemove)
        {
            Undo.RecordObject(this, "Child removed");
            children.Remove(childToRemove);
            EditorUtility.SetDirty(this);
        }
#endif

        int ComparePosition(Node leftNode, Node rightNode)
        {
            if (leftNode.GetPosition().x < rightNode.GetPosition().x)
            {
                return -1;
            }

            return 1;
        }

        int ComparePriority(Node leftNode, Node rightNode)
        {   
            if (leftNode.GetPriority() < rightNode.GetPriority())
            {
                return 1;
            }

            return -1;
        }
    }
}