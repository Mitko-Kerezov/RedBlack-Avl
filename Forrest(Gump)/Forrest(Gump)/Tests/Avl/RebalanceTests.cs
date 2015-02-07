namespace Tests.Avl
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RussianForrest;
    
    [TestClass]
    public class RebalanceTests
    {
        private AvlTree avlTree;

        [TestInitialize]
        public void AvlInit()
        {
            this.avlTree = new AvlTree();
        }

        [TestMethod]
        public void AvlWhenAddingThreeConsequentItemsShouldRebalanceCorrectly()
        {
            this.avlTree.Add(1, 1);
            this.avlTree.Add(2, 2);
            this.avlTree.Add(3, 3);
            Assert.AreEqual(2, this.avlTree.Root.Key);
        }

        [TestMethod]
        public void AvlWhenDeletingAndParentBalanceMinusTwoShouldRebalanceCorrectly()
        {
            this.avlTree.Add(1, 1);
            this.avlTree.Add(2, 2);
            this.avlTree.Add(3, 3);
            this.avlTree.Add(4, 4);

            this.avlTree.Remove(1);

            Assert.AreEqual(3, this.avlTree.Root.Key);
            Assert.AreEqual(0, this.avlTree.Root.Balance);

            Assert.AreEqual(2, this.avlTree.Root.Left.Key);
            Assert.AreEqual(0, this.avlTree.Root.Left.Balance);
            
            Assert.AreEqual(4, this.avlTree.Root.Right.Key);
            Assert.AreEqual(0, this.avlTree.Root.Right.Balance);
        }

        [TestMethod]
        public void AvlWhenDeletingAndParentBalancePlusTwoShouldRebalanceCorrectly()
        {
            this.avlTree.Add(2, 2);
            this.avlTree.Add(3, 3);
            this.avlTree.Add(4, 4);
            this.avlTree.Add(1, 1);

            this.avlTree.Remove(4);

            Assert.AreEqual(2, this.avlTree.Root.Key);
            Assert.AreEqual(0, this.avlTree.Root.Balance);

            Assert.AreEqual(1, this.avlTree.Root.Left.Key);
            Assert.AreEqual(0, this.avlTree.Root.Left.Balance);

            Assert.AreEqual(3, this.avlTree.Root.Right.Key);
            Assert.AreEqual(0, this.avlTree.Root.Right.Balance);
        }

        [TestMethod]
        public void AvlWhenDeletingAndParentBalanceMinusTwoAndRightHasTwoChildrenShouldRebalanceCorrectly()
        {
            this.avlTree.Add(1, 1);
            this.avlTree.Add(2, 2);
            this.avlTree.Add(4, 4);
            this.avlTree.Add(3, 3);
            this.avlTree.Add(5, 5);

            this.avlTree.Remove(1);

            Assert.AreEqual(4, this.avlTree.Root.Key);
            Assert.AreEqual(1, this.avlTree.Root.Balance);

            Assert.AreEqual(2, this.avlTree.Root.Left.Key);
            Assert.AreEqual(-1, this.avlTree.Root.Left.Balance);

            Assert.AreEqual(3, this.avlTree.Root.Left.Right.Key);
            Assert.AreEqual(0, this.avlTree.Root.Left.Right.Balance);

            Assert.AreEqual(5, this.avlTree.Root.Right.Key);
            Assert.AreEqual(0, this.avlTree.Root.Right.Balance);
        }

        [TestMethod]
        public void AvlWhenDeletingAndParentBalancePlusTwoAndLeftHasTwoChildrenShouldRebalanceCorrectly()
        {
            this.avlTree.Add(5, 5);
            this.avlTree.Add(2, 2);
            this.avlTree.Add(4, 4);
            this.avlTree.Add(1, 1);
            this.avlTree.Add(3, 3);

            this.avlTree.Remove(5);

            Assert.AreEqual(2, this.avlTree.Root.Key);
            Assert.AreEqual(-1, this.avlTree.Root.Balance);

            Assert.AreEqual(1, this.avlTree.Root.Left.Key);
            Assert.AreEqual(0, this.avlTree.Root.Left.Balance);
            
            Assert.AreEqual(4, this.avlTree.Root.Right.Key);
            Assert.AreEqual(1, this.avlTree.Root.Right.Balance);

            Assert.AreEqual(3, this.avlTree.Root.Right.Left.Key);
            Assert.AreEqual(0, this.avlTree.Root.Right.Left.Balance);

        }
    }
}
