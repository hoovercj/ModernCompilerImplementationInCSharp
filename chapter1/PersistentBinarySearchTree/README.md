## Chapter 1: Exercise 1

This simple program implements persistent functional binary search trees, so
that if tree2=insert(x,tree1), then tree1 is still available for lookups
even while tree2 can be used.

```java
class Tree {
    Tree left; String key; Tree right;
    Tree(Tree l, String k, Tree r) {left=l; key=k; right=r;}

    Tree insert(String key, Tree t) {
        if (t==null) return new Tree(null, key, null)
        else if (key.compareTo(t.key) < 0)
            return new Tree(insert(key,t.left),t.key,t.right);
        else if (key.compareTo(t.key) > 0)
            return new Tree(t.left,t.key,insert(key,t.right));
        else return new Tree(t.left,key,t.right);
    }
}
```

a. Implement a member function that returns true if the item is found, else
false.

b. Extend the program to include not just membership, but the mapping of
keys to bindings:
Tree insert(String key, Object binding, Tree t);
Object lookup(String key, Tree t);

c. These trees are not balanced; demonstrate the behavior on the following
two sequences of insertions:

    (a) t s p i p f b s t
    (b) a b c d e f g h i

*d. Research balanced search trees in Sedgewick [1997] and recommend
a balanced-tree data structure for functional symbol tables. Hint: To
preserve a functional style, the algorithm should be one that rebalances
on insertion but not on lookup, so a data structure such as splay trees is
not appropriate.

e. Rewrite in an object-oriented (but still “functional”) style, so that insertion
is now t.insert(key) instead of insert(key,t). Hint: You’ll need an
EmptyTree subclass.