namespace lab13
{
    /// <summary>
    /// Класс, реализующий передачу информации о событии
    /// </summary>
    public class CollectionHandlerEventArgs : EventArgs
    {
        #region Свойства
        /// <summary>
        /// Информация о том, какое действие произошло над коллекцией
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Элемент, реализуемый в действии над коллекцией
        /// </summary>
        public object Item { get; set; }
        #endregion

        #region Конструкторы
        /// <summary>
        /// Заполнение информации переданными параметрами
        /// </summary>
        /// <param name="action">Действие, которое произошло над коллекцией</param>
        /// <param name="item">Элемент, реализуемый в действии над коллекцией</param>
        public CollectionHandlerEventArgs(string action, object item)
        {
            Action = action; // Заполнение информации о действии
            Item = item; // Заполнение элемента действия
        }
        #endregion
    }
}
