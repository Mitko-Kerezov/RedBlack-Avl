/// <summary>
/// A red-black tree must satisfy these properties:
///
/// 1. The root is black. 
/// 2. All leaves are black. 
/// 3. Red nodes can only have black children. 
/// 4. All paths from a node to its leaves contain the same number of black nodes.
/// </summary>
namespace RedBlackForrest
{
    using System;
    using Common;

    public class RedBlack : Tree
    {
        // sentinelNode is convenient way of indicating a leaf node - nil
        public static RedBlackNode SentinelNode = new RedBlackNode(null, null)
        {
            Color = Color.BLACK,
            Left = null,
            Right = null,
            Parent = null
        };

        public RedBlack()
        {
            // initially the tree doesn't contain any elements
            this.Root = SentinelNode;
        }

        // the actual tree
        public new RedBlackNode Root
        {
            get
            {
                return base.Root as RedBlackNode;
            }

            set
            {
                base.Root = value;
            }
        }

        public void Add(IComparable key, object data)
        {
            if (key == null || data == null)
            {
                throw new RedBlackException("RedBlackNode key and value must not be null");
            }

            RedBlackNode node = new RedBlackNode(key, data);
            RedBlackNode returnedNode = (RedBlackNode)base.Add(node);
            returnedNode.Left = SentinelNode;
            returnedNode.Right = SentinelNode;
            if (returnedNode.Parent == null)
            {
                returnedNode.Color = Color.BLACK;
            }
            this.RestoreAfterInsert(returnedNode);
        }

        protected override void RebalanceAfterDeletionIfNeeded(Node deletedNode, Node successorNode)
        {
            // If the deleted node is red, the red black properties still hold.
            // If the deleted node is black, the tree needs rebalancing
            if (((RedBlackNode)deletedNode).Color == Color.BLACK)
            {
                this.RestoreAfterDelete((RedBlackNode)successorNode);
            }
        }

        private void RestoreAfterDelete(RedBlackNode node)
        {
            RedBlackNode workingNode;
            while (node != this.Root && node.Color == Color.BLACK)
            {
                // determine from parent which sub tree we're examining
                if (node == node.Parent.Left)
                {
                    // on the left sub tree
                    workingNode = node.Parent.Right;    // workingNode is node's sibling 
                    if (workingNode.Color == Color.RED)
                    {
                        // node is black, workingNode is red - make both black and rotate
                        workingNode.Color = Color.BLACK;
                        node.Parent.Color = Color.RED;
                        this.RotateLeft(node.Parent);
                        workingNode = node.Parent.Right;
                    }

                    if (workingNode.Left.Color == Color.BLACK &&
                        workingNode.Right.Color == Color.BLACK)
                    {
                        // node's sibling's children are both black
                        workingNode.Color = Color.RED;    // change sibling to red
                        node = node.Parent;               // move up the tree
                    }
                    else
                    {
                        // node's sibling has at least one red child
                        if (workingNode.Right.Color == Color.BLACK)
                        {
                            workingNode.Left.Color = Color.BLACK;
                            workingNode.Color = Color.RED;
                            this.RotateRight(workingNode);
                            workingNode = node.Parent.Right;
                        }

                        workingNode.Color = node.Parent.Color;
                        node.Parent.Color = Color.BLACK;
                        workingNode.Right.Color = Color.BLACK;
                        this.RotateLeft(node.Parent);
                        node = this.Root;
                    }
                }
                else
                {
                    // on the right sub tree
                    // mirror left case
                    workingNode = node.Parent.Left;
                    if (workingNode.Color == Color.RED)
                    {
                        workingNode.Color = Color.BLACK;
                        node.Parent.Color = Color.RED;
                        this.RotateRight(node.Parent);
                        workingNode = node.Parent.Left;
                    }

                    if (workingNode.Right.Color == Color.BLACK &&
                        workingNode.Left.Color == Color.BLACK)
                    {
                        workingNode.Color = Color.RED;
                        node = node.Parent;
                    }
                    else
                    {
                        if (workingNode.Left.Color == Color.BLACK)
                        {
                            workingNode.Right.Color = Color.BLACK;
                            workingNode.Color = Color.RED;
                            this.RotateLeft(workingNode);
                            workingNode = node.Parent.Left;
                        }

                        workingNode.Color = node.Parent.Color;
                        node.Parent.Color = Color.BLACK;
                        workingNode.Left.Color = Color.BLACK;
                        this.RotateRight(node.Parent);
                        node = this.Root;
                    }
                }
            }

            node.Color = Color.BLACK;
        }

        private void RestoreAfterInsert(RedBlackNode insertedNode)
        {
            RedBlackNode uncle;
            while (insertedNode != this.Root && insertedNode.Parent.Color == Color.RED)
            {
                // Parent is RED; 
                if (insertedNode.Parent == insertedNode.Parent.Parent.Left)
                {
                    // traversing Left subtree
                    uncle = insertedNode.Parent.Parent.Right;    // get uncle
                    if (uncle != null && uncle.Color == Color.RED)
                    {
                        // uncle is RED - repaint
                        insertedNode.Parent.Color = Color.BLACK;
                        uncle.Color = Color.BLACK;
                        insertedNode.Parent.Parent.Color = Color.RED;
                        insertedNode = insertedNode.Parent.Parent;  // continue loop with grandparent
                    }
                    else
                    {
                        // uncle is BLACK (don't be racist)
                        if (insertedNode == insertedNode.Parent.Right)
                        {
                            // insertedNode is on the right of it's parent
                            insertedNode = insertedNode.Parent;
                            this.RotateLeft(insertedNode);
                        }

                        // insertedNode is on the left of it's parent
                        // repaint
                        insertedNode.Parent.Color = Color.BLACK;
                        insertedNode.Parent.Parent.Color = Color.RED;

                        // rotate right
                        this.RotateRight(insertedNode.Parent.Parent);
                    }
                }
                else
                {
                    // traversing Right subtree
                    // mirror to traversing Left subtree
                    uncle = insertedNode.Parent.Parent.Left;
                    if (uncle != null && uncle.Color == Color.RED)
                    {
                        insertedNode.Parent.Color = Color.BLACK;
                        uncle.Color = Color.BLACK;
                        insertedNode.Parent.Parent.Color = Color.RED;
                        insertedNode = insertedNode.Parent.Parent;
                    }
                    else
                    {
                        if (insertedNode == insertedNode.Parent.Left)
                        {
                            insertedNode = insertedNode.Parent;
                            this.RotateRight(insertedNode);
                        }

                        insertedNode.Parent.Color = Color.BLACK;
                        insertedNode.Parent.Parent.Color = Color.RED;
                        this.RotateLeft(insertedNode.Parent.Parent);
                    }
                }
            }

            // root is always black
            this.Root.Color = Color.BLACK;
        }
    }
}
