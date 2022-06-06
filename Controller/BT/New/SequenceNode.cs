using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class SequenceNode : NodeBase
    {
        protected List<NodeBase> _nodes = new List<NodeBase>();
        protected int _runningNode = -1;

        public SequenceNode()
        {

        }
        public SequenceNode(NodeBase node)
        {
            Attach(node);
        }
        public SequenceNode(List<NodeBase> nodes)
        {
            Attach(nodes);
        }
        public void Attach(NodeBase node)
        {
            node.SetParent(this);
            _nodes.Add(node);
        }
        public void Attach(List<NodeBase> nodes)
        {
            int size = nodes.Count;
            for (int i = 0; i < size; i++)
            {
                nodes[i].SetParent(this);
                _nodes.Add(nodes[i]);
            }
        }
        public override BTResult Evaluate()
        {
            int start = 0;
            int size = _nodes.Count;
            if (_runningNode >= 0)
                start = _runningNode;
            for(int i = start; i < size; i++)
            {
                switch (_nodes[i].Evaluate())
                {
                    case BTResult.SUCCESS:
                        break;
                    case BTResult.RUNNING:
                        _runningNode = i;
                        return BTResult.RUNNING;
                    case BTResult.FAILURE:
                        return BTResult.FAILURE;
                }
            }
            _runningNode = -1;
            return BTResult.SUCCESS;
        }
    }
}