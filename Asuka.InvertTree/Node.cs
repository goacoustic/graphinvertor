namespace Asuka.InvertTree
{
    public class Node
    {
        public Node() { }
        public Node(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public Node[] Childs { get; set; }

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