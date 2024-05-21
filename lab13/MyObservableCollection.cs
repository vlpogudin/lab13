using MyCollectionAVLTree;
using GameLib;

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
        public void Add(T item)
        {
            base.Add(item); // Вызов метода добавления элемента из базового класса
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs("Добавление", item)); // Обработка события
        }

        /// <summary>
        /// Удаление элемента из коллекции
        /// </summary>
        /// <param name="item">Элемент для удаления из коллекции</param>
        public bool Remove(T item)
        {
            bool result = base.Remove(item); // Вызов метода удаления элемента из базового класса
            if (result) { OnCollectionCountChanged(this, new CollectionHandlerEventArgs("Удаление", item)); } // Обработка события
            return result;
        }

        /// <summary>
        /// Индексатор изменения элемента коллекции
        /// </summary>
        /// <param name="oldData">Информационное поле для обновления значения</param>
        /// <returns>Обновленное значение элемента</returns>
        public T this[T oldData]
        {
            set
            {
                if (!Contains(oldData)) // Если элемент с переданным значением не найден
                {
                    throw new InvalidOperationException("Элемент с заданным значением не найден.\n"); // Выкидываем исключение
                }
                else // Если элемент с переданным значением найден
                {
                    Remove(oldData); // Удаляем элемент с заданным значением
                    Add(value); // Добавляем элемент с новым значением
                    OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs("Изменение значения", oldData)); // Обработка события
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
        #endregion
    }
}