using Assets.Scripts.MiscUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MarkovChain
{
    // TODO: Find way to convert to data class. Actual functionality should be offloaded elsewhere.
    public class MarkovNode<TNodeType>
    {
        public readonly TNodeType NodeValue;
        private readonly List<MarkovConnection<TNodeType>> _connections;

        public MarkovNode(TNodeType nodeValue)
        {
            NodeValue = nodeValue;
            _connections = new List<MarkovConnection<TNodeType>>();
        }

        public void AddConnection(MarkovNode<TNodeType> connectionTarget, double connectionWeight)
        {
            var newConnection = new MarkovConnection<TNodeType>
            {
                Target = connectionTarget,
                Weight = connectionWeight
            };

            _connections.Add(newConnection);
        }

        public MarkovNode<TNodeType> GetNextNode()
        {
            double randomNumber = RandomNumberSource.GetRandomNumber();
            double connectionSum = 0;

            foreach (var connection in _connections)
            {
                connectionSum += connection.Weight;

                if (connectionSum >= randomNumber)
                {
                    return connection.Target;
                }
            }

            return this;
        }
    }

    internal  class MarkovConnection<TNodeType>
    {
        public MarkovNode<TNodeType> Target { get; set; }
        public double Weight { get; set; }
    }
}
