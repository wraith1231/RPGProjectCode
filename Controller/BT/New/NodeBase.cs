using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public enum BTResult
    {
        SUCCESS,
        RUNNING,
        FAILURE,
    }

    public abstract class NodeBase
    {
        protected NodeBase _parent = null;
        public void SetParent(NodeBase parent) { _parent = parent; }

        public abstract BTResult Evaluate();


    }
}