using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{ 
    public class Sequence : HeroNode
    {
        public Sequence(EnemyHeroController controller) : base(controller)
        {

        }

        public Sequence(EnemyHeroController controller, List<HeroNode> children) : base(controller, children)
        {

        }

        public override NodeState Evaluate()
        {
            int nodeLen = _children.Count;
            for(int nodeCount = 0; nodeCount < nodeLen; nodeCount++)
            {
                switch(_children[nodeCount].Evaluate())
                {
                    case NodeState.Failed:
                        _state = NodeState.Failed;
                        return _state;
                    case NodeState.Success:
                        continue;
                    case NodeState.Running:
                        _state = NodeState.Running;
                        return _state;
                    default:
                        continue;
                }
            }

            _state = NodeState.Success;
            return _state;
        }
    }
}