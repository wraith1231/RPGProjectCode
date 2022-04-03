using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

namespace BehaviorTree
{
    public class Selector : HeroNode
    {
        public Selector(EnemyHeroController controller) : base(controller)
        {

        }

        public Selector(EnemyHeroController controller, List<HeroNode> children) : base(controller,children)
        {

        }

        public override NodeState Evaluate()
        {

            int size = _children.Count;
            for(int i = 0; i < size; i++)
            {
                switch (_children[i].Evaluate())
                {
                    case NodeState.Running:
                        _state = NodeState.Running;
                        return _state;
                    case NodeState.Success:
                        _state = NodeState.Success;
                        return _state;
                    case NodeState.Failed:
                        continue;
                    default:
                        continue;
                }
            }

            _state = NodeState.Failed;
            return _state;
        }
    }
}