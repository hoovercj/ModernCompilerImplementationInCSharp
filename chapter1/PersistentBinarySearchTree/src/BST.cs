namespace ModernCompilerImplementation.Chapter1.BST.Lib
{
    using System;
    using System.Text;

    public class EmptyTree : Tree
    {
        public EmptyTree() : base(null, null, null, null)
        {

        }

        public override Tree Insert(String key, Object binding)
        {
            return new Tree(new EmptyTree(), key, binding, new EmptyTree());

        }
    }

    public class Tree
    {
        public Tree Left { get; set; }
        public string Key { get; set; }
        public Tree Right { get; set; }
        public Object Binding { get; set; }

        public Tree(Tree l, String k, Object b, Tree r)
        {
            Left = l;
            Key = k;
            Right = r;
            Binding = b;
        }

        public virtual Tree Insert(String key, Object binding)
        {
            if (key.CompareTo(this.Key) < 0)
            {
                Tree newLeft;
                if (this.Left == null)
                {
                    newLeft = new Tree(null, key, binding, null);
                }
                else
                {
                    newLeft = this.Left.Insert(key, binding);
                }
                return new Tree(newLeft, this.Key, this.Binding, this.Right);
            }
            else if (key.CompareTo(this.Key) > 0)
            {
                Tree newRight;
                if (this.Right == null)
                {
                    newRight = new Tree(null, key, binding, null);
                }
                else
                {
                    newRight = this.Right.Insert(key, binding);
                }
                return new Tree(this.Left, this.Key, this.Binding, newRight);
            }
            else
            {
                return new Tree(this.Left, this.Key, this.Binding, this.Right);
            }
        }

        public static Tree Insert(String key, Object binding, Tree t)
        {
            if (t == null)
            {
                return new Tree(null, key, binding, null);
            }
            else if (key.CompareTo(t.Key) < 0)
            {
                return new Tree(Insert(key, binding, t.Left), t.Key, t.Binding, t.Right);
            }
            else if (key.CompareTo(t.Key) > 0)
            {
                return new Tree(t.Left, t.Key, t.Binding, Insert(key, binding, t.Right));
            }
            else
            {
                return new Tree(t.Left, key, binding, t.Right);
            }
        }

        public bool Member(string key)
        {
            if (key.CompareTo(this.Key) < 0)
            {
                return this.Left == null ? false : this.Left.Member(key);
            }
            else if (key.CompareTo(this.Key) > 0)
            {
                return this.Right == null ? false : this.Right.Member(key);
            }
            else
            {
                return true;
            }
        }

        public static Object Lookup(string key, Tree t)
        {
            if (key == null) { throw new ArgumentNullException(nameof(key)); }

            if (t == null)
            {
                return null;
            }

            if (key.CompareTo(t.Key) < 0)
            {
                return Tree.Lookup(key, t.Left);
            }
            else if (key.CompareTo(t.Key) > 0)
            {
                return Tree.Lookup(key, t.Right);
            }
            else
            {
                return t.Binding;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            InOrderTreeTraverser.Traverse(this, (string key, Object binding) => {
                
                sb.Append(key + " ");
            });

            return sb.ToString().Trim();
        }
    }

    public class InOrderTreeTraverser
    {
        public static void Traverse(Tree t, Action<string, object> a)
        {
            if (t == null)
            {
                return;
            }

            if (t.Left != null)
            {
                InOrderTreeTraverser.Traverse(t.Left, a);
            }
            
            a.Invoke(t.Key, t.Binding);
            
            if (t.Right != null)
            {
                InOrderTreeTraverser.Traverse(t.Right, a);
            }
        }
    }
}
