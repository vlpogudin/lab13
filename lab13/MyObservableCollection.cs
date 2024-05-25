using MyCollectionAVLTree;
using GameLib;
using BalancedTree;

namespace lab13
{
    /// <summary>
    /// Делегат, информирующий о событиях коллекции 
    /// </summary>
    /// <param name="source">Объект, генерирующий событие</param>
    /// <param name="args">Информация для обработчика</param>
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    /// <summary>
    /// Класс коллекции с обработкой событий
    /// </summary>
    /// <typeparam name="T">Тип элементов, содержащихся в коллекции</typeparam>
    public class MyObservableCollection<T> : MyAVLTree<T> where T: IComparable, ICloneable, IInit, new()
    {
        #region Поля
        /// <summary>
        /// Событие, происходящее при изменении количества элементов в коллекции
        /// </summary>
        public event CollectionHandler CollectionCountChanged;

        /// <summary>
        /// Событие, которое происходящее при изменении значений одной из ссылок коллекции
        /// </summary>
        public event CollectionHandler CollectionReferenceChanged;
        #endregion

        #region Свойства
        /// <summary>
        /// Название коллекции
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество элементов коллекции
        /// </summary>
        private int Length => base.Count; // Вычисление количества элементов коллекции
        #endregion

        #region Конструкторы
        /// <summary>
        /// Заполнение информации о коллекции
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size"></param>
        public MyObservableCollection(string name, int size) : base(size)
        {
            Name = name; // Заполнение названия коллекции
        }
        #endregion

        #region Методы
        /// <summary>
        /// Добавление элемента в коллекцию
        /// </summary>
        /// <param name="item">Элемент для добавления в коллекцию</param>
        public new void Add(T item)
        {
            base.Add(item); // Вызов метода добавления элемента из базового класса
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs("Добавление", item)); // Обработка события
        }

        /// <summary>
        /// Удаление элемента из коллекции
        /// </summary>
        /// <param name="item">Элемент для удаления из коллекции</param>
        public new bool Remove(T item)
        {
            bool result = base.Remove(item); // Вызов метода удаления элемента из базового класса
            if (result) { OnCollectionCountChanged(this, new CollectionHandlerEventArgs("Удаление", item)); } // Обработка события
            return result;
        }

        /// <summary>
        /// Очистка коллекции
        /// </summary>
        public new void Clear()
        {
            ClearNode(root); // Вызов метода очистки дерева из базового класса
            root = null; // Очистка корня дерева
        }

        /// <summary>
        /// Индексатор изменения элемента коллекции
        /// </summary>
        /// <param name="key">Информационное поле для обновления значения</param>
        /// <returns>Обновленное значение элемента</returns>
        public T this[T key]
        {
            get
            {
                if (!Contains(key))
                {
                    throw new InvalidOperationException("Элемент с заданным значением не найден."); // Выкидываем исключение
                }
                else
                {
                    return key; // Получение переданного элемента
                }
            }

            set
            {
                if (!Contains(key)) // Если элемент с переданным значением не найден
                {
                    throw new InvalidOperationException("Элемент с заданным значением не найден."); // Выкидываем исключение
                }
                else // Если элемент с переданным значением найден
                {
                    if (Contains(value)) // Если новое значение элемента уже существует в коллекции
                    {
                        throw new InvalidOperationException("Элемент с заданным значением уже существует в коллекции."); // Выкидываем исключение
                    }
                    else
                    {
                        Remove(key); // Удаляем элемент с заданным значением
                        Add(value); // Добавляем элемент с новым значением
                        OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs("Изменение значения", key)); // Обработка события
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик события CollectionCountChanged
        /// </summary>
        /// <param name="source">Объект, генерирующий событие</param>
        /// <param name="args">Информация для обработчика</param>
        public virtual void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            CollectionCountChanged?.Invoke(source, args); // Выполнение делегата
        }

        /// <summary>
        /// Обработчик события OnCollectionReferenceChanged
        /// </summary>
        /// <param name="source">Объект, генерирующий событие</param>
        /// <param name="args">Информация для обработчика</param>
        public virtual void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            CollectionReferenceChanged?.Invoke(source, args); // Выполнение делегата
        }

        /// <summary>
        /// Рекурсивная очистка корней коллекции
        /// </summary>
        /// <param name="node">Узел для очистки элементов</param>
        private void ClearNode(AVLTreeItem<T>? node)
        {
            if (node != null) // Если узел не пуст
            {
                OnCollectionCountChanged(this, new CollectionHandlerEventArgs("Удаление", node.Data)); // Обработка события
                ClearNode(node.Left); // Рекурсивная очистка из левого поддерева
                node.Data = default(T); // Обнуление информационного поля узла
                ClearNode(node.Right); // Рекурсивная очистка из правого поддерева
                node.Left = null; // Обнуление ссылки на левое поддерево
                node.Right = null; // Обнуление ссылки на правое поддерево
            }
        }
        #endregion
    }
}