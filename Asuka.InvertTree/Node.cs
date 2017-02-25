using System.Collections.Generic;

namespace Asuka.InvertTree
{
    public class Node
    {
        public Node()
        {
            Childs = new List<Node>();
        }

        public Node(string value) : this()
        {
            Value = value;
        }

        public string Value { get; set; }
        public List<Node> Childs { get; set; }

        public override bool Equals(object obj)
        {
            var o = obj as Node;
            return o != null && o.Value == Value;
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
    }
}