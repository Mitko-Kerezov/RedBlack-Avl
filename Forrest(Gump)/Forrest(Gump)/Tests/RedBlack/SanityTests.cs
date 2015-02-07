namespace Tests.RedBlackTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RedBlackForrest;

    [TestClass]
    public class SanityTests
    {
        private RedBlack redBlack;

        [TestInitialize]
        public void RedBlackInit()
        {
            this.redBlack = new RedBlack();
        }

        [TestMethod]
        public void RedBlackRootShouldBeBlack()
        {
            this.redBlack.Add(1, 1);
            Assert.AreEqual(Color.BLACK, ((RedBlackNode)this.redBlack.Root).Color);
        }

        [TestMethod]
        public void RedBlackWithOnlyRootShouldHaveSizeOne()
        {
            this.redBlack.Add(1, 1);
            Assert.AreEqual(1, this.redBlack.Size());
        }

        [TestMethod]
        public void RedBlackNoElementsShouldHaveSizeZero()
        {
            Assert.AreEqual(0, this.redBlack.Size());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RedBlackWhenAddingDuplicateKeyShouldThrow()
        {
            this.redBlack.Add(1, 1);
            this.redBlack.Add(1, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(RedBlackException))]
        public void RedBlackWhenAddingNullKeyShouldThrow()
        {
            this.redBlack.Add(null, 1);
        }

        [TestMethod]
        public void RedBlackRemoveShouldRemoveElement()
        {
            this.redBlack.Add(1, 1);
            this.redBlack.Remove(1);
            Assert.AreEqual(0, this.redBlack.Size());
        }

        [TestMethod]
        public void RedBlackRemoveInvalidKeyShouldDoNothing()
        {
            this.redBlack.Remove(1);
            Assert.AreEqual(0, this.redBlack.Size());
        }

        [TestMethod]
        public void RedBlackGetDataShouldReturnData()
        {
            this.redBlack.Add(1, 1);

            var result = this.redBlack.GetData(1);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RedBlackWhenDeletingWithNullKeyShouldThrow()
        {
            this.redBlack.Remove((IComparable)null);
        }
    }
}