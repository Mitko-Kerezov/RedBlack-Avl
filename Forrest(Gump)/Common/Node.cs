namespace Common
{
    using System;

    public class Node
    {
        public Node(IComparable key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

        // key provided by the calling class
        public IComparable Key { get; set; }

        // the value associated with the key
        public object Value { get; set; }

        // left node 
        public Node Left { get; set; }

        // right node 
        public Node Right { get; set; }

        // parent node 
        public Node Parent { get; set; }

        public static bool operator ==(Node node1, Node node2)
        {
            if (object.ReferenceEquals(node1, null))
            {
                return object.ReferenceEquals(node2, null) || (node2.Key == null && node2.Value == null);
            }

            if (object.ReferenceEquals(node2, null))
            {
                return object.ReferenceEquals(node1, null) || (node1.Key == null && node1.Value == null);
            }

            return node1.Equals(node2);
        }

        public static bool operator !=(Node node1, Node node2)
        {
            return !(node1 == node2);
        }

        public override bool Equals(object obj)
        {
            Node node = (Node)obj;
            if (object.ReferenceEquals(this.Key, null) && object.ReferenceEquals(this.Value, null))
            {
                return object.ReferenceEquals(node.Key, null) && object.ReferenceEquals(node.Value, null);
            }

            return this.Key.Equals(node.Key) && this.Value.Equals(node.Value);
        }

        public override int GetHashCode()
        {
            return this.Key.GetHashCode() ^ this.Value.GetHashCode();
        }
    }
}
