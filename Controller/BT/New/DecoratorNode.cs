using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class DecoratorNode : NodeBase
    {
        protected NodeBase _nodes = null;

        public DecoratorNode(NodeBase node)
        {
            if (node == null)
                return;

            SetChild(node);
        }

        public void SetChild(NodeBase node)
        {
            if (_nodes != null)
                _nodes.SetParent(null);

            node.SetParent(this);
            _nodes = node;
        }

        public override BTResult Evaluate()
        {
            if (Condition() == true)
                return _nodes.Evaluate();

            else
                return BTResult.FAILURE;
        }

        protected abstract bool Condition();
    }
}