using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.MarkovChain
{
    public class MarkovChain<TNodeType>
    {
        private MarkovNode<TNodeType> _currentNode;
        private List<MarkovNode<TNodeType>> _possibleNodes;

        public MarkovChain(List<MarkovNode<TNodeType>> possibleNodes, MarkovNode<TNodeType> startNode)
        {
            _possibleNodes = possibleNodes;
            _currentNode = startNode;
        }

        public MarkovChain(List<MarkovNode<TNodeType>> possibleNodes)
        {
            _currentNode = PickRandomNode(possibleNodes);
        }

        private MarkovNode<TNodeType> PickRandomNode(List<MarkovNode<TNodeType>> possibleNodes)
        {
            double result = RandomNumberSource.GetRandomNumber();
            double nodeValue = 1.0 / possibleNodes.Count;

            double nodeValueTotal = 0;
            foreach (var node in possibleNodes)
            {
                nodeValueTotal += nodeValue;

                if (nodeValueTotal >= result)
                {
                    return node;
                }
            }

            return possibleNodes[0];
        }

        public TNodeType NextValue()
        {
            _currentNode = _currentNode.GetNextNode();
            return _currentNode.NodeValue;
        }

        public TNodeType NextValue(TNodeType nodeSource)
        {
            _currentNode = _possibleNodes.FirstOrDefault(node => node.NodeValue.Equals(nodeSource));
            return NextValue();
        }
    }
}
