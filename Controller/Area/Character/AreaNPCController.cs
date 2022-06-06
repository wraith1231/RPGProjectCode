using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaNPCController : AreaGroupController
{
    class AStarNode
    {
        //total
        public float t;
        //todest
        public float d;
        //fromstart
        public float a;

        public bool isAlive = true;
        public AStarNode parent;
        public AreaNode currentNode;
    }
    
    protected override void FixedUpdate()
    {

    }

    protected override void Initialize()
    {
        int size = Managers.Map.VillageLists.Count;
        int start = Random.Range(0, size);

        //_currentNode = Managers.Map.VillageLists[start].BaseAreaNode;
        //_transform.position = _currentNode.transform.position;

    }

    protected override void DayChangeUpdate(int day)
    {
        base.DayChangeUpdate(day);

        if (_moveToDest == false)
            TestRandomMove();
    }

    protected void TestRandomMove()
    {
        int size = Managers.Map.VillageLists.Count;
        int num = Random.Range(0, size);
        
        string villName = Managers.Map.VillageLists[num].Data.VillageName;
        Debug.Log($"{gameObject.name} testrandommove");
        float dest = Vector3.Distance(_transform.position, Managers.Map.VillageLists[num].transform.position);

        AStarNode temp = new AStarNode();
        temp.t = dest;
        temp.a = 0;
        temp.d = dest;
        temp.currentNode = _currentNode;
        temp.parent = null;

        List<AStarNode> queue = new List<AStarNode>();
        size = _currentNode.ConnectedNode.Count;
        for(int i = 0; i < size; i++)
        {
            AStarNode qTemp = new AStarNode();
            qTemp.parent = temp;
            qTemp.currentNode = qTemp.parent.currentNode.ConnectedNode[i];
            qTemp.a = qTemp.parent.a + Vector3.Distance(qTemp.parent.currentNode.transform.position, qTemp.currentNode.transform.position);
            qTemp.d = Vector3.Distance(qTemp.currentNode.transform.position, Managers.Map.Villages[villName].transform.position);
            qTemp.t = qTemp.a + qTemp.d;

            queue.Add(qTemp);
        }

        int listSize = 0;
        int number = 0;
        bool isComputed = false;
        AStarNode result = null;
        int queueCount = 0;
        while(true)
        {
            queueCount = queue.Count;
            if (number >= queueCount)
                return;
            size = queue[number].currentNode.ConnectedNode.Count;

            for (int i = 0; i < size; i++)
            {
                AStarNode qTemp = new AStarNode();
                qTemp.parent = queue[number];
                qTemp.currentNode = qTemp.parent.currentNode.ConnectedNode[i];
                listSize = queue.Count;
                isComputed = false;
                for(int j = 0; j < listSize; j++)
                {
                    if (qTemp.currentNode == queue[j].currentNode)
                    {
                        queue[j].isAlive = false;
                        isComputed = true;
                        break;
                    }
                }
                if (isComputed == true)
                    continue;

                qTemp.a = qTemp.parent.a + Vector3.Distance(qTemp.parent.currentNode.transform.position, qTemp.currentNode.transform.position);
                qTemp.d = Vector3.Distance(qTemp.currentNode.transform.position, Managers.Map.Villages[villName].transform.position);
                qTemp.t = qTemp.a + qTemp.d;

                if(qTemp.currentNode.Village != null)
                {
                    if(qTemp.currentNode.Village.Data.VillageName == villName)
                    {
                        result = qTemp;
                        break;
                    }
                }

                queue.Add(qTemp);
            }
            
            if(result != null)
            {
                Stack<AStarNode> stack = new Stack<AStarNode>();

                while(result.parent != null)
                {
                    stack.Push(result);
                    result = result.parent;
                }

                while(stack.Count > 0)
                {
                    _destination.Enqueue(stack.Pop().currentNode.transform.position);
                }

                Status = Define.AreaStatus.Move;
                
                break;
            }
            number++;
        }

        StartCoroutine(MoveToTarget());
    }
}
