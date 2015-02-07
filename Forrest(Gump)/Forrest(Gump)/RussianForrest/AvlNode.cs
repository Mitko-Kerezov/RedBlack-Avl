namespace RussianForrest
{
    using System;
    using Common;

    public sealed class AvlNode : Node
    {
        public AvlNode(IComparable key, object value)
            : base (key, value)
        {
            this.Balance = 0;
        }

        public new AvlNode Left
        {
            get
            {
                return base.Left as AvlNode;
            }

            set
            {
                base.Left = value;
            }
        }

        public new AvlNode Right
        {
            get
            {
                return base.Right as AvlNode;
            }

            set
            {
                base.Right = value;
            }
        }

        public new AvlNode Parent
        {
            get
            {
                return base.Parent as AvlNode;
            }

            set
            {
                base.Parent = value;
            }
        }

        public int Balance { get; set; }
    }
}
