namespace Tests.Avl
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RussianForrest;

    [TestClass]
    public class SanityTests
    {
        private AvlTree avlTree;

        [TestInitialize]
        public void AvlInit()
        {
            this.avlTree = new AvlTree();
        }

        [TestMethod]
        public void AvlWithOnlyRootShouldHaveSizeOne()
        {
            this.avlTree.Add(1, 1);
            Assert.AreEqual(1, this.avlTree.Size());
        }

        [TestMethod]
        public void AvlNoElementsShouldHaveSizeZero()
        {
            Assert.AreEqual(0, this.avlTree.Size());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AvlWhenAddingDuplicateKeyShouldThrow()
        {
            this.avlTree.Add(1, 1);
            this.avlTree.Add(1, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(AvlException))]
        public void AvlWhenAddingNullKeyShouldThrow()
        {
            this.avlTree.Add(null, 1);
        }

        [TestMethod]
        public void AvlRemoveShouldRemoveElement()
        {
            this.avlTree.Add(1, 1);
            this.avlTree.Remove(1);
            Assert.AreEqual(0, this.avlTree.Size());
        }

        [TestMethod]
        public void AvlRemoveInvalidKeyShouldDoNothing()
        {
            this.avlTree.Remove(1);
            Assert.AreEqual(0, this.avlTree.Size());
        }

        

        [TestMethod]
        public void AvlGetDataShouldReturnData()
        {
            this.avlTree.Add(1, 1);

            var result = this.avlTree.GetData(1);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        [ExpectedException(typeof(AvlException))]
        public void AvlWhenDeletingWithNullKeyShouldThrow()
        {
            this.avlTree.Remove((IComparable)null);
        }
    }
}