using System.Collections;
using System.Net.Http.Headers;

class Program
{
    /// <summary>
    /// узел дерева
    /// </summary>
    /// <typeparam name="TNode">Значение</typeparam>
    class BinaryTreeNode<TNode> : IComparable<TNode> where TNode : IComparable<TNode>
    {
        /// <summary>
        /// значение узла типа TNode, реализующее интерфейс IComparable<TNode>
        /// </summary>
        public TNode Value { get; private set; }
        /// <summary>
        /// ссылка на левый узел
        /// </summary>
        public BinaryTreeNode<TNode>? Left { get; set; }
        /// <summary>
        /// ссылка на правый узел
        /// </summary>
        public BinaryTreeNode<TNode>? Right { get; set; }
        /// <summary>
        /// конструктор, принимающий значение узла
        /// </summary>
        /// <param name="value"></param>
        public BinaryTreeNode(TNode value)
        {
            Value = value;
        }
        /// <summary>
        /// метод сравнения значений 
        /// </summary>
        /// <param name="other">второе значение</param>
        /// <returns>-1 0 1</returns>
        public int CompareTo(TNode? other)
        {
            return Value.CompareTo(other);          // сравниваем значения
        }
        /// <summary>
        /// метод сравнения значений узлов
        /// </summary>
        /// <param name="other">второй узел</param>
        /// <returns>-1 0 1</returns>
        public int CompareTo(BinaryTreeNode<TNode> other)
        {
            return Value.CompareTo(other.Value);    // сравниваем значения
        }
    }
    class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        /// <summary>
        /// корень дерева
        /// </summary>
        private BinaryTreeNode<T>? root;
        /// <summary>
        /// количество узлов
        /// </summary>
        private int count;
        /// <summary>
        /// аксессор количества узлов
        /// </summary>
        public int Count { get { return count; } }
        /// <summary>
        /// метод добавления узла
        /// </summary>
        /// <param name="Value">значение</param>
        public void Add(T Value)
        {
            // если дерево пустое, первое значение помещается в корень
            if (root == null) { root = new BinaryTreeNode<T>(Value); }

            // если дерево не пустое, применяем рекурсивный алгоритм для поиска места добавления узла
            else
            {
                AddTo(root, Value);
            }
            count++;                    // инкрементируем количество узлов
        }
        /// <summary>
        /// метод добавления значения к узлу
        /// </summary>
        /// <param name="node">узел, к которому добавляется значение</param>
        /// <param name="Value">добавляемое значение</param>
        public void AddTo(BinaryTreeNode<T> node, T Value)
        {

            if (Value.CompareTo(node.Value) < 0)                                         // если значение добавляемого узла меньше,
                                                                                         // чем значение текущего, идем в левое поддерево
            {
                if (node.Left == null) node.Left = new BinaryTreeNode<T>(Value);         // если левого поддерева нет - добавляем его
                else AddTo(node.Left, Value);                                            // иначе запускаем рекурсивный алгоритм к левому поддереву
            }
            else                                                                         //иначе идем в правое поддерево
            {
                if (node.Right == null) node.Right = new BinaryTreeNode<T>(Value);        // если правого поддерева нет - добавляем его
                else AddTo(node.Right, Value);                                           // иначе запускаем рекурсивный алгоритм к правому поддереву   
            }
        }
        /// <summary>
        /// метод, возвращающий первый найденый узел и его родителя
        /// если значение не найдено, метод возвращает null
        /// </summary>
        /// <param name="parent">родительский узел</param>
        /// <returns></returns>
        public BinaryTreeNode<T> FindWithParent(T Value, out BinaryTreeNode<T> parent)
        {
            BinaryTreeNode<T> current = root;                           // текущий обследуемый узел, первый будет корневым
            parent = null;                                              // к корня нет родителя, потому parent = null

            while (current != null)
            {
                int result = current.CompareTo(Value);
                if (result > 0)                          // если искомое меньше значения текущего - переходим в левый поток
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)                     // если искомое больше значения текущего - переходим в правый поток
                {
                    parent = current;
                    current = parent.Right;
                }
                else
                {
                    break;
                }
            }
            return current;
        }
        /// <summary>
        /// метод поиска значения в дереве
        /// </summary>
        /// <param name="Value">искомое значение</param>
        /// <returns>true/false</returns>
        public bool Contains(T Value)
        {
            BinaryTreeNode<T> parent;
            return FindWithParent(Value, out parent) != null;
        }
        /// <summary>
        /// обертка для вызова метода симметричного обхода дерева
        /// </summary>
        public void InOrderTraversal()
        {
            InOrderTraversal(root);
            Console.WriteLine();
        }
        /// <summary>
        /// Симметричный обход дерева
        /// </summary>
        /// <param name="node">узел</param>
        private void InOrderTraversal(BinaryTreeNode<T> node)
        {
            if (node.Left != null) InOrderTraversal(node.Left);
            Console.Write($"{node.Value} ");
            if (node.Right != null) InOrderTraversal(node.Right);
        }
        /// <summary>
        /// метод обертка для вызова метода обратного обхода дерева
        /// </summary>
        public void PostOrderTraversal()
        {
            PostOrderTraversal(root);
            Console.WriteLine();
        }
        /// <summary>
        /// обратный обход дерева
        /// </summary>
        /// <param name="node">узел</param>
        private void PostOrderTraversal(BinaryTreeNode<T> node)
        {
            if (node.Left != null) PostOrderTraversal(node.Left);
            if (node.Right != null) PostOrderTraversal(node.Right);
            Console.Write($"{node.Value} ");
        }
        /// <summary>
        /// метод обертка для вызова метода прямого обхода дерева
        /// </summary>
        public void PreOrderTraversal()
        {
            PreOrderTraversal(root);
            Console.WriteLine();
        }
        /// <summary>
        /// прямой обход дерева
        /// </summary>
        /// <param name="node">узел</param>
        private void PreOrderTraversal(BinaryTreeNode<T> node)
        {
            Console.Write($"{node.Value} ");
            if (node.Left != null) PreOrderTraversal(node.Left);
            if (node.Right != null) PreOrderTraversal(node.Right);
        }
        /// <summary>
        /// метод удаления узла
        /// </summary>
        /// <param name="value">значения узла к удалению</param>
        /// <returns>true/false</returns>
        public bool Remove(T value)
        {
            BinaryTreeNode<T>? current;
            BinaryTreeNode<T>? parent;

            current = FindWithParent(value, out parent);    //находим узел и его родителя по значению

            if (current == null) { return false; }          //если узла нет, возвращаем false

            count--;                                        //декрементируем счетчик

            if (current.Right == null)                      //Вар.1 Удаляемый узел не имеет правого потомка
            {
                if (parent == null)                         //проверка на корень, у корня нет родителя
                {
                    root = current.Left;                    //если требуется удалить корень, заменяем его на 
                }                                           //корень левого поддерева

                else                                        //если удаляемый элемент не корень
                {
                    int result = parent.CompareTo(current.Value);

                    //если значение узла родителя больше значения удаляемого узла - сделать
                    //левого потомка текущего узла - левым потомком родительского узла
                    if (result > 0) { parent.Left = current.Left; }
                    //если значение узла родителя меньше значения удаляемого узла - сделать
                    //левого потомка текущего узла - правым потомком родительского узла
                    else if (result < 0) { parent.Right = current.Left; }
                    //
                    // 8>5, 8.left = 5.left = 3 
                    // 8<12 8.right = 12
                    //             8                 
                    //           /   \
                    //         3>5   10>12 (r<1)         
                    //         /     /  \            
                    //(r>1)  1>3   >10  15
                    //      /  \  
                    //    >1    2
                }
            }
            else if (current.Right.Left == null)            //Вар2. Удаляемый узел имеет правого потомка,
            {                                               //у которого нет левого потомка.

                current.Right.Left = current.Left;          //левый потомок удаляемого узла становится
                                                            //на место отсутствующего левого потомка
                                                            //правого потомка удаляемого узла

                if (parent == null)                         //проверка на корень, у корня нет родителя
                {
                    root = current.Right;                   //перемещение корня правого поддерева в основной
                }

                else
                {
                    int result = parent.CompareTo(current.Value);

                    //если значение узла родителя больше значения удаляемого узла - сделать
                    //правого потомка текущего узла - левым потомком родительского узла
                    if (result > 0)
                    {
                        parent.Left = current.Right;
                    }

                    //если значение узла родителя меньше значения удаляемого узла - сделать
                    //правого потомка текущего узла - правым потомком родительского узла
                    else if (result < 0)
                    {
                        parent.Right = current.Right;
                    }

                    //                  8  
                    //            /           \      
                    //(r>1)     6>5          11>10 (r<0)
                    //          / \          /  \           current.Right.Left = current.Left;  
                    //        >3   7>6      >9  12>11 
                    //            / \          /   \
                    //           3>  >7       9>   >12
                }
            }
            else                                            //Вар3. удаляемый узел имеет правого потомка,                                    
            {                                               //у которго есть левый потомок
                BinaryTreeNode<T> leftMost = current.Right.Left;
                BinaryTreeNode<T> leftMostParent = current.Right;

                while (leftMost.Left != null)                //поиск крайнего левого потомка от правого
                {                                            //потомка удаляемого потомка
                    leftMostParent = leftMost;
                    leftMost = leftMost.Left;
                }

                leftMostParent.Left = leftMost.Right;       //чтобы не потерять правое поддерево последнего  
                                                            //левого узла, мы помещаем его в правое   
                                                            //поддерево родителя последнего левого узла
                                                            //на место крайнего левого потомка 
                                                            //значения правого поддерева крайнего левого узла
                                                            //гарантированно меньше родителя крайнего левого узла

                leftMost.Left = current.Left;               // присваиваем крайнему левому потомку левое поддерево удаляемого
                leftMost.Right = current.Right;             // присваиваем крайнему левому потомку правое поддерево удаляемого

                if (parent == null) { root = leftMost; }    //проверка на корень, обмен значениями с крайним левым узлом
                else
                {
                    int result = parent.CompareTo(current.Value);

                    if (result > 0)                     //если удаляемый узел в левом поддереве родителя
                    {
                        parent.Left = leftMost;         //подставить крайний левый потомок удаляемого узла
                                                        //на место удаляемого
                    }
                    else if (result < 0)                //если удаляемый узел в правом поддереве родителя
                    {
                        parent.Right = leftMost;        //подставить крайний левый потомок удаляемого узла
                                                        //на место удаляемого
                    }
                    //
                    //                             20 r>1
                    //                             /
                    //                        lm11>10 currentъ
                    //                          /   \
                    //                         9    14 c.r
                    //                              /  \
                    //                        lmp  13  15
                    //                            /  
                    //                         12>11 lm 
                    //                             \
                    //                             >12  lm.r     
                    //
                    //                          8 r<1
                    //                           \
                    //                        lm11>10 current
                    //                          /   \
                    //                         9    14 c.r
                    //                              /  \
                    //                        lmp  13  15
                    //                            /  
                    //                         12>11 lm 
                    //                             \
                    //                             >12  lm.r     
                }
            }
            return true;
        }
        /// <summary>
        /// обертка для метода извлечения значений в очередь
        /// </summary>
        private Queue<T> GetValuesToQueue()
        {
            Queue<T> nodesQueue = new Queue<T>();
            GetEachValuePostOrder(root, ref nodesQueue);
            return nodesQueue;
        }
        /// <summary>
        /// извлечение в ходе обратного прохода дерева  
        /// всех его узлов и помещение их в очередь
        /// </summary>
        /// <param name="node">узел</param>
        /// <param name="nodesQueue">стек</param>
        private void GetEachValuePostOrder(BinaryTreeNode<T>? node, ref Queue<T> nodesQueue)
        {
            if (node.Left != null) { GetEachValuePostOrder(node.Left, ref nodesQueue); }
            if (node.Right != null) { GetEachValuePostOrder(node.Right, ref nodesQueue); }
            nodesQueue.Enqueue(node.Value);
        }
        /// <summary>
        /// итератор
        /// </summary>
        /// <returns>возвращает итератор очереди</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return GetValuesToQueue().GetEnumerator();
        }
        /// <summary>
        /// итератор
        /// </summary>
        /// <returns>GetEnumerator()</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    static void Main(string[] args)
    {
        BinaryTree<int> tree = new BinaryTree<int>();

        //добавление элементов
        Console.WriteLine("Добавляем элементы");
        tree.Add(8);
        tree.Add(5);
        tree.Add(3);
        tree.Add(6);
        tree.Add(7);
        tree.Add(12);
        tree.Add(10);
        tree.Add(15);
        tree.Add(20);
        tree.Add(17);

        //вспомогательные методы
        Console.WriteLine($"\nКоличество узлов: {tree.Count}");   //количество узлов
        Console.WriteLine($"Содержит 10: {tree.Contains(10)}");   //содержит узел со значением 10 true 
        Console.WriteLine($"Содержит 12: {tree.Contains(12)}");   //содержит узел со значением 12 true 
        Console.WriteLine($"Содержит 15: {tree.Contains(15)}");   //содержит узел со значением 15 true

        //проход по дереву
        Console.WriteLine("\nСимметричный проход по дереву:");
        tree.InOrderTraversal();
        Console.WriteLine("\nОбратный проход по дереву:");
        tree.PostOrderTraversal();
        Console.WriteLine("\nПрямой проход по дереву:");
        tree.PreOrderTraversal();                                 //прямой проход узлов

        //удаление элементов
        Console.WriteLine("\nУдаление элементов:");
        Console.WriteLine($"Удален узел со значением 10: {tree.Remove(10)}");     //Удаляемый узел не имеет правого потомка
        tree.PreOrderTraversal();

        Console.WriteLine($"Удален узел со значением 12: {tree.Remove(12)}");     //Удаляемый узел имеет правого потомка,
        tree.PreOrderTraversal();                                                 //у которого нет левого потомка.

        Console.WriteLine($"Удален узел со значением 15: {tree.Remove(15)}");     //Удаляемый узел имеет правого потомка,                                    
        tree.PreOrderTraversal();                                                 //у которого есть левый потомок

        //вспомогательные методы
        Console.WriteLine($"\nКоличество узлов: {tree.Count}");   //количество узлов
        Console.WriteLine($"Содержит 10: {tree.Contains(10)}");   //содержит узел со значением 10 false
        Console.WriteLine($"Содержит 12: {tree.Contains(12)}");   //содержит узел со значением 12 false
        Console.WriteLine($"Содержит 15: {tree.Contains(15)}");   //содержит узел со значением 15 false

        Console.WriteLine("\nОбратный проход по дереву:");
        tree.PostOrderTraversal();                                //3 7 6 5 20 17 8

        Console.WriteLine("Итератор:");
        foreach (var item in tree) Console.Write($"{item} ");     //3 7 6 5 20 17 8

        #region СХЕМА
        /*
          Схема дерева:
        
                  8
                /   \
               5     12
              / \   /  \
             3   6 10   15
                  \      \
                   7     20
                         /
                        17
        
          Удаляем 10: Удаляемый узел не имеет правого потомка
          
                  8
                /   \
               5     12
              / \     \
             3   6     15
                  \     \
                   7    20
                        /
                       17
        
          Удаляем 12: Удаляемый узел имеет правого потомка, у которого нет левого потомка
        
                  8
                /   \
               5     15
              / \     \
             3   6     20
                  \    /
                   7  17
        
          Удаляем 15: Удаляемый узел имеет правого потомка, у которого есть левый потомок
        
                  8
                /   \
               5     17
              / \     \
             3   6     20
                  \      
                   7      
        */
        #endregion
    }
}