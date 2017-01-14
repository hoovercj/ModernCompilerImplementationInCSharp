namespace ModernCompilerImplementation.Chapter1.BST.Test
{
    using Xunit;
    using ModernCompilerImplementation.Chapter1.BST.Lib;

    public class BSTTests
    {
        [Fact]
        public void TestFunctionalInsert() 
        {
            Tree tree = new Tree(null, null, null, null);
            tree = Tree.Insert("t", "t", tree);
            tree = Tree.Insert("s", "s", tree);
            tree = Tree.Insert("p", "p", tree);
            tree = Tree.Insert("i", "i", tree);
            tree = Tree.Insert("p", "p", tree);
            tree = Tree.Insert("f", "f", tree);
            tree = Tree.Insert("b", "b", tree);
            tree = Tree.Insert("s", "s", tree);
            tree = Tree.Insert("t", "t", tree);

            var expected = "b f i p s t";
            Assert.Equal(expected, tree.ToString());

            tree = new Tree(null, null, null, null);
            tree = Tree.Insert("a", "a", tree);
            tree = Tree.Insert("b", "b", tree);
            tree = Tree.Insert("c", "c", tree);
            tree = Tree.Insert("d", "d", tree);
            tree = Tree.Insert("e", "e", tree);
            tree = Tree.Insert("f", "f", tree);
            tree = Tree.Insert("g", "g", tree);
            tree = Tree.Insert("h", "h", tree);
            tree = Tree.Insert("i", "i", tree);

            expected = "a b c d e f g h i";
            Assert.Equal(expected, tree.ToString());
        }

        [Fact]
        public void TestInsert() 
        {
            Tree tree = new Tree(null, null, null, null);
            tree = tree.Insert("t", "t");
            tree = tree.Insert("s", "s");
            tree = tree.Insert("p", "p");
            tree = tree.Insert("i", "i");
            tree = tree.Insert("p", "p");
            tree = tree.Insert("f", "f");
            tree = tree.Insert("b", "b");
            tree = tree.Insert("s", "s");
            tree = tree.Insert("t", "t");

            var expected = "b f i p s t";
            Assert.Equal(expected, tree.ToString());

            var tree1 = new Tree(null, null, null, null);
            var tree2 = Tree.Insert("a", "a", tree1);
            var tree3 = Tree.Insert("b", "b", tree2);
            var tree4 = Tree.Insert("c", "c", tree3);
            var tree5 = Tree.Insert("d", "d", tree4);
            var tree6 = Tree.Insert("e", "e", tree5);
            var tree7 = Tree.Insert("f", "f", tree6);
            var tree8 = Tree.Insert("g", "g", tree7);
            var tree9 = Tree.Insert("h", "h", tree8);
            var tree10 = Tree.Insert("i", "i", tree9);

            var expected5 = "a b c d";
            Assert.Equal(expected5, tree5.ToString());

            var expected10 = "a b c d e f g h i";
            Assert.Equal(expected10, tree10.ToString());
        }
    }
}
