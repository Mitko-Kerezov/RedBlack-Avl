namespace RedBlackForrest
{
    using System;
    using System.Text;
    using Common;

    public sealed class RedBlackNode : Node
    {
        public RedBlackNode(IComparable key, object value)
            : base(key, value)
        {
            this.Color = Color.RED;
        }

        public Color Color { get; set; }
       
        public new RedBlackNode Left
        {
            get
            {
                return base.Left as RedBlackNode;
            }

            set
            {
                base.Left = value;
            }
        }

        public new RedBlackNode Right
        {
            get
            {
                return base.Right as RedBlackNode;
            }

            set
            {
                base.Right = value;
            }
        }

        public new RedBlackNode Parent
        {
            get
            {
                return base.Parent as RedBlackNode;
            }

            set
            {
                base.Parent = value;
            }
        }
    }
}
