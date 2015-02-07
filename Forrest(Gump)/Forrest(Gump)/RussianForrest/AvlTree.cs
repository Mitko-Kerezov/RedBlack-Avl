namespace RussianForrest
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using Common;
    
    public partial class AvlTree : Tree
    {
        // I'll try using sentinelNode here too for consistency
        public static AvlNode SentinelNode = new AvlNode(null, null)
        {
            Left = null,
            Right = null,
            Parent = null
        };

        public new AvlNode Root
        {
            get
            {
                return base.Root as AvlNode;
            }

            set
            {
                base.Root = value;
            }
        }

        public AvlTree()
        {
            // initially the tree doesn't contain any elements
            this.Root = SentinelNode;
        }

        public void Add(IComparable key, object data)
        {
            if (key == null || data == null)
            {
                throw new AvlException("AvlNode key and value must not be null");
            }

            AvlNode node = new AvlNode(key, data);
            AvlNode returnedNode = (AvlNode)base.Add(node);
            returnedNode.Left = SentinelNode;
            returnedNode.Right = SentinelNode;
            
            // if we haven't just added the root then we may need to rebalance
            if (node.Parent != null)
            {
                if (node == node.Parent.Left)
                {
                    this.InsertBalance(node.Parent, 1);
                }
                else
                {
                    this.InsertBalance(node.Parent, -1);
                }
            }
        }

        private void InsertBalance(AvlNode node, int balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);

                if (balance == 0)
                {
                    return;
                }
                else if (balance == 2)
                {
                    if (node.Left.Balance == 1)
                    {
                        this.RotateRight(node);
                    }
                    else
                    {
                        node = this.RotateLeftRight(node);
                    }

                    return;
                }
                else if (balance == -2)
                {
                    if (node.Right.Balance == -1)
                    {
                        this.RotateLeft(node);
                    }
                    else
                    {
                        node = this.RotateRightLeft(node);
                    }

                    return;
                }

                AvlNode parent = node.Parent;

                if (parent != null)
                {
                    balance = parent.Left == node ? 1 : -1;
                }

                node = parent;
            }
        }

        private void DeleteBalance(AvlNode node, int balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);

                if (balance == 2)
                {
                    if (node.Left.Balance >= 0)
                    {
                        this.RotateRight(node);
                        node = node.Parent;
                        if (node.Balance == -1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = RotateLeftRight(node);
                    }
                }
                else if (balance == -2)
                {
                    if (node.Right.Balance <= 0)
                    {
                        this.RotateLeft(node);
                        node = node.Parent;
                        if (node.Balance == 1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = this.RotateRightLeft(node);
                    }
                }
                else if (balance != 0)
                {
                    return;
                }

                AvlNode parent = node.Parent;

                if (parent != null)
                {
                    balance = parent.Left == node ? -1 : 1;
                }

                node = parent;
            }
        }

        private AvlNode RotateLeftRight(AvlNode node)
        {
            node = node.Left;
            this.RotateLeft(node);
            node = node.Parent.Parent;
            this.RotateRight(node);
            node = node.Parent;
            if (node.Balance == -1)
            {
                node.Right.Balance = 0;
                node.Left.Balance = 1;
            }
            else if (node.Balance == 1)
            {
                node.Right.Balance = -1;
                node.Left.Balance = 0;
            }
            else
            {
                node.Right.Balance = 0;
                node.Left.Balance = 0;
            }

            node.Balance = 0;
            return node;
        }

        private AvlNode RotateRightLeft(AvlNode node)
        {
            node = node.Right;
            this.RotateRight(node);
            node = node.Parent.Parent;
            this.RotateLeft(node);
            node = node.Parent;
            if (node.Balance == -1)
            {
                node.Right.Balance = 1;
                node.Left.Balance = 0;
            }
            else if (node.Balance == 1)
            {
                node.Right.Balance = 0;
                node.Left.Balance = -1;
            }
            else
            {
                node.Right.Balance = 0;
                node.Left.Balance = 0;
            }

            node.Balance = 0;
            return node;
        }
        
        protected override void RotateLeft(Node node)
        {
            base.RotateLeft(node);
            var avlNode = node as AvlNode;
            avlNode.Parent.Balance++;
            avlNode.Balance = -avlNode.Parent.Balance;
        }

        protected override void RotateRight(Node node)
        {
            base.RotateRight(node);
            var avlNode = node as AvlNode;
            avlNode.Parent.Balance--;
            avlNode.Balance = -avlNode.Parent.Balance;
        }
        private void Replace(AvlNode target, AvlNode source)
        {
            AvlNode left = source.Left;
            AvlNode right = source.Right;

            target.Balance = source.Balance;
            target.Key = source.Key;
            target.Value = source.Value;
            target.Left = left;
            target.Right = right;

            if (left != null)
            {
                left.Parent = target;
            }

            if (right != null)
            {
                right.Parent = target;
            }
        }

        public override void Remove(IComparable key)
        {
            if (key == null)
            {
                throw new AvlException("Key is null!");
            }

            var node = (AvlNode)this.Search(key);

            if (node == null)
            {
                // key not found
                return;
            }

            this.BalanceBuffer = node.Balance;
            base.Remove(node);
        }
        
        protected override void RebalanceAfterDeletionIfNeeded(Node deletedNode, Node successorNode)
        {
            ((AvlNode)deletedNode).Balance = this.BalanceBuffer;
            if (deletedNode.Left == SentinelNode) 
            {
                if (deletedNode.Right == SentinelNode)
                {
                    if (deletedNode.Parent != SentinelNode)
                    {
                        if (deletedNode.Key.CompareTo(deletedNode.Parent.Key) < 0)
                        {
                            this.DeleteBalance((AvlNode)deletedNode.Parent, -1);
                        }
                        else
                        {
                            this.DeleteBalance((AvlNode)deletedNode.Parent, 1);
                        }
                    }
                    else
                    {
                        this.Root = SentinelNode;
                    }
                }
                else
                {
                    this.Replace((AvlNode)deletedNode, ((AvlNode)deletedNode).Right);
                    this.DeleteBalance((AvlNode)deletedNode, 0);
                }
            }
            else if (deletedNode.Right == SentinelNode)
            {
                this.Replace((AvlNode)deletedNode, ((AvlNode)deletedNode).Left);
                this.DeleteBalance((AvlNode)deletedNode, 0);
            }
            else
            {
                if (successorNode == SentinelNode)
                {
                    this.DeleteBalance((AvlNode)successorNode, 1);
                }
                else
                {
                    this.DeleteBalance(((AvlNode)successorNode.Parent), -1);
                }
            }
        }

        public int BalanceBuffer { get; set; }
    }
}