namespace BinnarySearchTree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tree = new Tree<float>();
            
            //Add elements
            {
                tree.Add(5);
                tree.Add(5);
                tree.Add(3);
                tree.Add(9.05f);
                tree.Add(7);
                tree.Add(1);
                tree.Add(2);
                tree.Add(8);
                tree.Add(6);
                tree.Add(9);
                tree.Add(9.00f);
                tree.Add(9.01f);
                tree.Add(4);
                tree.Add(2.5f);
            }

            Console.WriteLine("Обход удобный для копирования:");

            foreach (var item in tree.PreOrder()) 
            { 
                Console.Write(item + ",  ");
            }
            tree.Remove(3f);
            tree.Remove(4);

            Console.WriteLine("\n\nОбход удобный для удаления:");

            foreach (var item in tree.PostOrder())
            {
                Console.Write(item + ",  ");
            }

            tree.Remove(1);
            tree.Remove(6);

            Console.WriteLine("\n\nСортировка по возрастанию");

            foreach (var item in tree.InOrderASC())
            {
                Console.Write(item + ",  ");
            }
            tree.Remove(2);
            tree.Remove(8);

            Console.WriteLine("\n\nСортировка по уменьшению");

            foreach (var item in tree.InOrderDESC())
            {
                Console.Write(item + ",  ");
            }
            tree.Remove(5);

            Console.ReadLine();
        }
    }
}