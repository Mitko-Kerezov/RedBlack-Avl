namespace Test
{
    using System;
    using System.Collections.Generic;

    using RedBlackForrest;
    using RussianForrest;
using Common;

    public class TestRedBlack
    {
        private static RedBlack redBlack = new RedBlack();
        private static AvlTree avlTree = new AvlTree();

        public static void Main()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Red");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Black");
            Console.ResetColor();
            try
            {
                Console.WriteLine("Adding items to Red-Black");
                redBlack.Add(1, "One");
                redBlack.Add(2, "Two");
                redBlack.Add(3, "Three");
                redBlack.Add(4, "Four");
                redBlack.Add(5, "Five");
                redBlack.Add(6, "Six");
                redBlack.Add(7, "Seven");
                redBlack.Add(8, "Eight");
                redBlack.Add(9, "Nine");
                redBlack.Add(10, "Ten");
                redBlack.Add(11, "Eleven");
                redBlack.Add(12, "Twelve");
                redBlack.Add(13, "Thirteen");
                Console.WriteLine("Traversing tree..");
                TraverseRedBlack(redBlack);
                Console.WriteLine("Removing 9");
                redBlack.Remove(9);
                Console.WriteLine("Traversing tree..");
                TraverseRedBlack(redBlack);
                Console.WriteLine("Removing 13");
                redBlack.Remove(13);
                Console.WriteLine("Traversing tree..");
                TraverseRedBlack(redBlack);
                Console.WriteLine("Removing 12");
                redBlack.Remove(12);
                Console.WriteLine("Traversing tree..");
                TraverseRedBlack(redBlack);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press enter to terminate...");
                Console.ReadLine();
            }
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("AVL");
            Console.ResetColor();
            try
            {
                Console.WriteLine("Adding items to AVL");
                avlTree.Add(1, "One");
                avlTree.Add(2, "Two");
                avlTree.Add(3, "Three");
                avlTree.Add(4, "Four");
                avlTree.Add(5, "Five");
                avlTree.Add(6, "Six");
                avlTree.Add(7, "Seven");
                avlTree.Add(8, "Eight");
                avlTree.Add(9, "Nine");
                avlTree.Add(10, "Ten");
                avlTree.Add(11, "Eleven");
                avlTree.Add(12, "Twelve");
                avlTree.Add(13, "Thirteen");
                Console.WriteLine("Traversing tree..");
                TraverseAvl(avlTree);
                Console.WriteLine("Removing 9");
                avlTree.Remove(9);
                Console.WriteLine("Traversing tree..");
                TraverseAvl(avlTree);
                Console.WriteLine("Removing 13");
                avlTree.Remove(13);
                Console.WriteLine("Traversing tree..");
                TraverseAvl(avlTree);
                Console.WriteLine("Removing 12");
                avlTree.Remove(12);
                TraverseAvl(avlTree);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press enter to terminate...");
                Console.ReadLine();
            }
        }

        private static void PrintSize(Tree tree)
        {
            Console.WriteLine("The tree's size is: {0}", tree.Size());
        }

        private static void TraverseAvl(AvlTree avlTree)
        {
            // Depth-first traversal  
            PrintSize(avlTree);
            var stack = new Stack<AvlNode>();
            stack.Push(avlTree.Root);
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                IComparable parentKey = null;
                if (node.Left != null)
                {
                    stack.Push(node.Left);
                }

                if (node.Right != null)
                {
                    stack.Push(node.Right);
                }

                if (node.Parent != null)
                {
                    parentKey = node.Parent.Key;
                }

                PrintAvlNode(node, parentKey);
            }
        }

        private static void TraverseRedBlack(RedBlack redBlack)
        {
            // Depth-first traversal
            PrintSize(redBlack);
            var stack = new Stack<RedBlackNode>();
            stack.Push((RedBlackNode)redBlack.Root);
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                IComparable parentKey = null;
                if (node.Left != RedBlack.SentinelNode)
                {
                    stack.Push(node.Left);
                }

                if (node.Right != RedBlack.SentinelNode)
                {
                    stack.Push(node.Right);
                }

                if (node.Parent != null)
                {
                    parentKey = node.Parent.Key;
                }

                PrintRedBlackNode(node, parentKey);
            }
        }

        private static void PrintRedBlackNode(RedBlackNode node, IComparable parentKey)
        {
            Console.Write(
                "Key:{0}\t" + "  Data:{1}\t",
                node.Key,
                node.Value);
            Console.Write(" Color:");
            if (node.Color == Color.BLACK)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(node.Color);
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(node.Color);
                Console.ResetColor();
            }

            Console.WriteLine("\t Parent Key:{0}", parentKey);
        }

        private static void PrintAvlNode(AvlNode node, IComparable parentKey)
        {
            Console.WriteLine(
            "Key:{0}\t" + "Data:{1}\t" + "Parent Key:{2}\t" + "Balance:{3}",
            node.Key,
            node.Value,
            parentKey,
            node.Balance);
        }
    }
}
