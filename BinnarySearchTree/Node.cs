
namespace BinnarySearchTree
{
    internal class Node<T> 
        where T : IComparable<T>
    {
        public T Data { get;  set; }
        public Node<T>? Left { get;  set; }
        public Node<T>? Right { get; set; }
        public Node<T>? Parent { get; set; }

        public Node(T data) => Data = data;

        public Node(T data, Node<T> parent) => (Data, Parent) = (data, parent);

        public bool Add(T data)
        {
            if (data == null)
            {
                return false;
            }

            var compareResult = data.CompareTo(Data);

            if (compareResult == 0)
            {
                return false;
            }

            if (compareResult < 0)
            {
                if (Left == null)
                {
                    Left = new Node<T>(data, this);
                }
                else
                {
                    return Left.Add(data);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = new Node<T>(data, this);
                }
                else
                {
                    return Right.Add(data);
                }
            }

            return true;
        }

        public override string? ToString() => Data.ToString();
    }
}
