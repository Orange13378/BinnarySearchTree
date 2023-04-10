namespace BinnarySearchTree
{
    internal class Tree<T> where T : IComparable<T>
    {
        public Node<T>? Root { get; private set; }
        public int Count { get; private set; }

        public bool Add(T data)
        {
            if (data == null)
                return false;

            if (Root == null)
            {
                Root = new Node<T>(data);
                Count = 1;
                return true;
            }

            var addResult = Root.Add(data);

            if (addResult)
                Count++;

            return addResult;
        }

        public List<T> PreOrder()
        {
            if (Root == null)
            {
                return new List<T>();
            }

            return PreOrder(Root);
        }

        private List<T> PreOrder(Node<T> node)
        {
            var list = new List<T>();

            if (node != null)
            {
                list.Add(node.Data);

                if (node.Left != null)
                {
                    list.AddRange(PreOrder(node.Left));
                }

                if (node.Right != null)
                {
                    list.AddRange(PreOrder(node.Right));
                }
            }

            return list;
        }

        public List<T> PostOrder()
        {
            if (Root == null)
            {
                return new List<T>();
            }

            return PostOrder(Root);
        }

        private List<T> PostOrder(Node<T> node)
        {
            var list = new List<T>();

            if (node != null)
            {
                if (node.Left != null)
                {
                    list.AddRange(PostOrder(node.Left));
                }

                if (node.Right != null)
                {
                    list.AddRange(PostOrder(node.Right));
                }

                list.Add(node.Data);
            }

            return list;
        }

        public List<T> InOrderASC() // По возрастанию
        {
            if (Root == null)
            {
                return new List<T>();
            }

            return InOrderASC(Root);
        }

        private List<T> InOrderASC(Node<T> node)
        {
            var list = new List<T>();

            if (node != null)
            {
                if (node.Left != null)
                {
                    list.AddRange(InOrderASC(node.Left));
                }

                list.Add(node.Data);

                if (node.Right != null)
                {
                    list.AddRange(InOrderASC(node.Right));
                }
            }

            return list;
        }

        public List<T> InOrderDESC() // По Убыванию
        {
            if (Root == null)
            {
                return new List<T>();
            }

            return InOrderDESC(Root);
        }

        private List<T> InOrderDESC(Node<T> node)
        {
            var list = new List<T>();

            if (node != null)
            {
                if (node.Right != null)
                {
                    list.AddRange(InOrderDESC(node.Right));
                }

                list.Add(node.Data);

                if (node.Left != null)
                {
                    list.AddRange(InOrderDESC(node.Left));
                }
            }

            return list;
        }

        private Node<T>? SearchNode(Node<T> tree, T value)
        {
            if (tree == null)
                return null;

            switch (value.CompareTo(tree.Data))
            {
                case 1: return SearchNode(tree.Right, value);
                case -1: return SearchNode(tree.Left, value);
                case 0: return tree;
                default: return null;
            }
        }

        public Node<T> Search(T val)
        {
            return SearchNode(Root, val);
        }

        public bool Remove(T val)
        {
            //Проверяем, существует ли данный узел
            Node<T> tree = Search(val);

            if (tree == null)
            {
                //Если узла не существует, вернем false
                return false;
            }

            Node<T> curTree;

            //Удаляем корень
            if (tree == Root)
            {
                if (tree.Right != null)
                {
                    curTree = tree.Right;
                }
                else curTree = tree.Left;

                while (curTree.Left != null)
                {
                    curTree = curTree.Left;
                }

                T temp = curTree.Data;
                Remove(temp);
                Console.WriteLine("\nУдаляем корень: " + tree.Data);
                tree.Data = temp;

                return true;
            }

            //Удаление листьев
            if (tree.Left == null && tree.Right == null && tree.Parent != null)
            {
                if (tree == tree.Parent.Left)
                {
                    tree.Parent.Left = null;
                    Console.WriteLine("\nУдаляем левые листья: " + tree.Data);
                }
                else
                {
                    tree.Parent.Right = null;
                    Console.WriteLine("\nУдаляем правые листья: " + tree.Data);
                }
                return true;
            }

            //Удаление узла, имеющего левое поддерево, но не имеющее правого поддерева
            if (tree.Left != null && tree.Right == null)
            {
                //Меняем родителя
                tree.Left.Parent = tree.Parent;

                if (tree == tree.Parent.Left)
                {
                    tree.Parent.Left = tree.Left;
                    Console.WriteLine("\nУдаляем узел(Л) Л && !П: " + tree.Data);
                }
                else if (tree == tree.Parent.Right)
                {
                    tree.Parent.Right = tree.Left;
                    Console.WriteLine("\nУдаляем узел(П) Л && !П: " + tree.Data);
                }

                return true;
            }

            //Удаление узла, имеющего правое поддерево, но не имеющее левого поддерева
            if (tree.Left == null && tree.Right != null)
            {
                //Меняем родителя
                tree.Right.Parent = tree.Parent;

                if (tree == tree.Parent.Left)
                {
                    tree.Parent.Left = tree.Right;
                    Console.WriteLine("\nУдаляем узел(Л) !Л && П: " + tree.Data);
                }
                else if (tree == tree.Parent.Right)
                {
                    tree.Parent.Right = tree.Right;
                    Console.WriteLine("\nУдаляем узел(П) !Л && П: " + tree.Data);
                }

                return true;
            }

            //Удаляем узел, имеющий поддеревья с обеих сторон
            if (tree.Right != null && tree.Left != null)
            {
                curTree = tree.Right;

                while (curTree.Left != null)
                {
                    curTree = curTree.Left;
                }

                //Если самый левый элемент является первым потомком
                if (curTree.Parent == tree)
                {
                    curTree.Left = tree.Left;
                    tree.Left.Parent = curTree;
                    curTree.Parent = tree.Parent;

                    if (tree == tree.Parent.Left)
                    {
                        tree.Parent.Left = curTree;
                        Console.WriteLine("\nУдаляем узел(самый Л Л) Л && П: " + tree.Data);
                    }
                    else if (tree == tree.Parent.Right)
                    {
                        tree.Parent.Right = curTree;
                        Console.WriteLine("\nУдаляем узел(самый Л П) Л && П: " + tree.Data);
                    }

                    return true;
                }

                //Если самый левый элемент НЕ является первым потомком
                else
                {
                    if (curTree.Right != null)
                    {
                        curTree.Right.Parent = curTree.Parent;
                    }

                    curTree.Parent.Left = curTree.Right;
                    curTree.Right = tree.Right;
                    curTree.Left = tree.Left;
                    tree.Left.Parent = curTree;
                    tree.Right.Parent = curTree;
                    curTree.Parent = tree.Parent;

                    if (tree == tree.Parent.Left)
                    {
                        tree.Parent.Left = curTree;
                        Console.WriteLine("\nУдаляем узел(!самый Л Л) Л && П: " + tree.Data);
                    }
                    else if (tree == tree.Parent.Right)
                    {
                        tree.Parent.Right = curTree;
                        Console.WriteLine("\nУдаляем узел(!самый Л П) Л && П: " + tree.Data);
                    }

                    return true;
                }
            }
            return false;
        }
    }
}
