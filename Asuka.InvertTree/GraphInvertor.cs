using System;
using System.Collections.Generic;
using System.Linq;

namespace Asuka.InvertTree
{
    public class GraphInvertor
    {
        public List<Node> Invert(Node root)
        {
            if (root.Childs.Count == 0) return new List<Node> { root };

            var result = new List<Node>();

            var nodesForInverting = new Queue<Tuple<Node, Node>>();

            nodesForInverting.Enqueue(Tuple.Create(root, (Node)null));

            do
            {
                var currentPair = nodesForInverting.Dequeue();
                var currentNode = currentPair.Item1;
                foreach (var currentNodeChild in currentNode.Childs)
                {
                    nodesForInverting.Enqueue(Tuple.Create(currentNodeChild, currentNode));
                }

                currentNode.Childs = currentPair.Item2 == null ? new List<Node>() : new List <Node> { currentPair.Item2 };

                result.Add(currentNode);
            } while (nodesForInverting.Any());

            return result;
        }
    }
}