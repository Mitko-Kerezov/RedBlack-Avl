namespace Common
{
    using System;

    public abstract class Tree
    {
        public static Node EmptyNode = new Node(null, null)
        {
            Left = null,
            Right = null,
            Parent = null
        };

        // the number of nodes contained in the tree 
        protected int intCount;

        public Node Root { get; set; }
        
        public Node Search(IComparable key)
        {
            if (key == null)
            {
                throw new ArgumentException("Key is null");
            }

            int result;
            Node node;

            node = this.Root;
            while (node != EmptyNode)
            {
                result = key.CompareTo(node.Key);
                if (result == 0)
                {
                    break;
                }

                if (result < 0)
                {
                    node = node.Left;
                }
                else
                {
                    node = node.Right;
                }
            }

            if (node == EmptyNode)
            {
                // key not found
                return null;
            }

            return node;
        }
        
        protected Node Add(Node node)
        {
            int result = 0;
            
            // start traversing the tree
            Node temp = this.Root;

            while (temp != EmptyNode)
            {
                node.Parent = temp;
                result = node.Key.CompareTo(temp.Key);
                if (result == 0)
                {
                    throw new Exception("A Node with the same key already exists");
                }

                if (result > 0)
                {
                    temp = temp.Right;
                }
                else
                {
                    temp = temp.Left;
                }
            }

            // insert node at the right place according to it's parent
            if (node.Parent != null)
            {
                result = node.Key.CompareTo(node.Parent.Key);
                if (result > 0)
                {
                    node.Parent.Right = node;
                }
                else
                {
                    node.Parent.Left = node;
                }
            }
            else
            {
                // first node added
                this.Root = node;
            }

            ++this.intCount;
            return node;  
        }
        
        public virtual void Remove(IComparable key)
        {
            if (key == null)
            {
                throw new ArgumentException("Key is null!");
            }

            var node = this.Search(key);
            if (node == null)
            {
                // key not found
                return;
            }

            this.Delete(node);
            --this.intCount;
        }

        public virtual void Remove(Node node)
        {
            if (node == null)
            {
                // key not found
                return;
            }

            this.Delete(node);
            --this.intCount;
        }

        public void Clear()
        {
            this.Root = EmptyNode;
            this.intCount = 0;
        }

        public int Size()
        {
            return this.intCount;
        }

        public bool IsEmpty()
        {
            return this.Root == EmptyNode;
        }

        protected virtual void RotateLeft(Node node)
        {
            Node nodeRight = node.Right;
            node.Right = nodeRight.Left;
            if (nodeRight.Left != EmptyNode)
            {
                nodeRight.Left.Parent = node;
            }

            if (nodeRight != EmptyNode)
            {
                nodeRight.Parent = node.Parent;
            }

            if (node.Parent != null)
            {
                if (node == node.Parent.Left)
                {
                    node.Parent.Left = nodeRight;
                }
                else
                {
                    node.Parent.Right = nodeRight;
                }
            }
            else
            {
                this.Root = nodeRight;
            }

            nodeRight.Left = node;
            if (node != EmptyNode)
            {
                node.Parent = nodeRight;
            }
        }

        public object GetData(IComparable key)
        {
            Node treeNode = this.Search(key);
            if (treeNode == null)
            {
                throw new Exception("Key was not found");
            }

            return treeNode.Value;
        }

        protected virtual void RotateRight(Node node)
        {
            Node nodeLeft = node.Left;
            node.Left = nodeLeft.Right;
            if (nodeLeft.Right != EmptyNode)
            {
                nodeLeft.Right.Parent = node;
            }

            if (nodeLeft != EmptyNode)
            {
                nodeLeft.Parent = node.Parent;
            }

            if (node.Parent != null)
            {
                if (node == node.Parent.Right)
                {
                    node.Parent.Right = nodeLeft;
                }
                else
                {
                    node.Parent.Left = nodeLeft;
                }
            }
            else
            {
                this.Root = nodeLeft;
            }

            nodeLeft.Right = node;
            if (node != EmptyNode)
            {
                node.Parent = nodeLeft;
            }
        }

        protected virtual void Delete(Node node)
        {
            // A node to be deleted will be: 
            //     1. a leaf with no children
            //     2. have one child
            //     3. have two children
            Node successorNode;  // the node's successor
            Node successorChild = new Node(null, null); // the successor's child (or empty node)

            // find suitable successor
            // a node with at most one child
            if (node.Left == EmptyNode || node.Right == EmptyNode)
            {
                // current node is a suitable successor
                successorNode = node;
            }
            else
            {
                // node has two children, find the most suitable successor
                // it be the leftmost node, greater than the current
                successorNode = node.Right;     // traverse right subtree
                while (successorNode.Left != EmptyNode)
                {
                    successorNode = successorNode.Left;
                }
            }

            // at this point, replacementNode contains the replacement node. it's content will be copied 
            // to the valules in the node to be deleted

            // successorChild is the node that will be linked to replacementNode's old parent. 
            // Note: in case the original node doesn't have two children
            //       the successorChild will replace the actual node instead of the successor   
            if (successorNode.Left != EmptyNode)
            {
                successorChild = successorNode.Left;
            }
            else
            {
                successorChild = successorNode.Right;
            }

            // replace successorChild's parent with successor's parent and
            // link successorChild to proper subtree in parent
            // this removes replacementNode from the chain
            successorChild.Parent = successorNode.Parent;
            if (successorNode.Parent != null)
            {
                if (successorNode == successorNode.Parent.Left)
                {
                    successorNode.Parent.Left = successorChild;
                }
                else
                {
                    successorNode.Parent.Right = successorChild;
                }
            }
            else
            {
                // if we got this far that means that we're deleting the root
                this.Root = successorChild;
            }

            // copy the values from replacementNode to the node being deleted.
            // this is the actual deletion of the node 
            if (successorNode != node)
            {
                node.Key = successorNode.Key;
                node.Value = successorNode.Value;
            }

            this.RebalanceAfterDeletionIfNeeded(successorNode, successorChild);
        }

        protected abstract void RebalanceAfterDeletionIfNeeded(Node deletedNode, Node successorNode);
    }
}
