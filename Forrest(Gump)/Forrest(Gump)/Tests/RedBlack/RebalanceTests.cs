namespace Tests.RedBlack
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RedBlackForrest;

    [TestClass]
    public class RebalanceTests
    {
        private RedBlack redBlack;

        [TestInitialize]
        public void RedBlackInit()
        {
            this.redBlack = new RedBlack();
        }

        [TestMethod]
        public void RedBlackWhenAddingThreeConsequentItemsShouldRebalanceCorrectly()
        {
            this.redBlack.Add(1, 1);
            this.redBlack.Add(2, 2);
            this.redBlack.Add(3, 3);
            Assert.AreEqual(2, this.redBlack.Root.Key);
        }

        [TestMethod]
        public void RedBlackWhenDeletingLeftBlackShouldRebalanceCorrectly()
        {
            this.redBlack.Add(1, 1);
            this.redBlack.Add(2, 2);
            this.redBlack.Add(3, 3);
            this.redBlack.Add(4, 4);
            this.redBlack.Remove(1);

            Assert.AreEqual(3, this.redBlack.Root.Key);
            Assert.AreEqual(Color.BLACK, this.redBlack.Root.Color);

            Assert.AreEqual(2, this.redBlack.Root.Left.Key);
            Assert.AreEqual(Color.BLACK, this.redBlack.Root.Left.Color);

            Assert.AreEqual(4, this.redBlack.Root.Right.Key);
            Assert.AreEqual(Color.BLACK, this.redBlack.Root.Left.Color);


        }

        [TestMethod]
        public void RedBlackWhenDeletingRightBlackShouldRebalanceCorrectly()
        {
            this.redBlack.Add(2, 2);
            this.redBlack.Add(3, 3);
            this.redBlack.Add(4, 4);
            this.redBlack.Add(1, 1);
            this.redBlack.Remove(4);

            Assert.AreEqual(2, this.redBlack.Root.Key);
            Assert.AreEqual(Color.BLACK, this.redBlack.Root.Color);

            Assert.AreEqual(1, this.redBlack.Root.Left.Key);
            Assert.AreEqual(Color.BLACK, this.redBlack.Root.Left.Color);

            Assert.AreEqual(3, this.redBlack.Root.Right.Key);
            Assert.AreEqual(Color.BLACK, this.redBlack.Root.Left.Color);
        }

        [TestMethod]
        public void RedBlackWhenDeletingLeftAndHisSiblingIsBlackAndHasTwoRedChildrenShouldRebalanceCorrectly()
        {
            this.redBlack.Add(1, 1);
            this.redBlack.Add(2, 2);
            this.redBlack.Add(4, 4);
            this.redBlack.Add(3, 3);
            this.redBlack.Add(5, 5);

            this.redBlack.Remove(1);

            Assert.AreEqual(4, this.redBlack.Root.Key);
            Assert.AreEqual(Color.BLACK, this.redBlack.Root.Color);

            Assert.AreEqual(2, this.redBlack.Root.Left.Key);
            Assert.AreEqual(Color.BLACK, this.redBlack.Root.Left.Color);

            Assert.AreEqual(3, this.redBlack.Root.Left.Right.Key);
            Assert.AreEqual(Color.RED, this.redBlack.Root.Left.Right.Color);

            Assert.AreEqual(5, this.redBlack.Root.Right.Key);
            Assert.AreEqual(Color.BLACK, this.redBlack.Root.Right.Color);
        }

        [TestMethod]
        public void RedBlackWhenDeletingRightAndHisSiblingIsBlackAndHasTwoRedChildrenShouldRebalanceCorrectly()
        {
            this.redBlack.Add(5, 5);
            this.redBlack.Add(2, 2);
            this.redBlack.Add(4, 4);
            this.redBlack.Add(1, 1);
            this.redBlack.Add(3, 3);

            this.redBlack.Remove(5);

            Assert.AreEqual(2, this.redBlack.Root.Key);
            Assert.AreEqual(Color.BLACK, this.redBlack.Root.Color);

            Assert.AreEqual(1, this.redBlack.Root.Left.Key);
            Assert.AreEqual(Color.BLACK, this.redBlack.Root.Left.Color);

            Assert.AreEqual(4, this.redBlack.Root.Right.Key);
            Assert.AreEqual(Color.BLACK, this.redBlack.Root.Right.Color);

            Assert.AreEqual(3, this.redBlack.Root.Right.Left.Key);
            Assert.AreEqual(Color.RED, this.redBlack.Root.Right.Left.Color);

        }
    }
}
