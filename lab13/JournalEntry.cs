namespace lab13
{
    /// <summary>
    /// Класс общей информации о действии над коллекцией
    /// </summary>
    public class JournalEntry
    {
        #region Свойства
        /// <summary>
        /// Название коллекции, над которой произошло действие
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Информация о действии, которое произошло над коллекцией
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Информационное поле элемента, задействованного в действии над коллекцией
        /// </summary>
        public string? Data { get; set; }
        #endregion

        #region Конструкторы
        /// <summary>
        /// Инициализация полей коллекции
        /// </summary>
        /// <param name="name">Название коллекции, над которой произошло действие</param>
        /// <param name="action">Информация о действии, которое произошло над коллекцией</param>
        /// <param name="data">Информационное поле элемента, задействованного в действии над коллекцией</param>
        public JournalEntry(string name, string action, string? data)
        {
            Name = name; // Заполнение названия коллекции
            Action = action; // Заполнение информации о действии
            Data = data; // Заполнение информационного поля
        }
        #endregion

        #region Методы
        /// <summary>
        /// Преобразование переданной информации в строку
        /// </summary>
        /// <returns>Преобразованная из переданной информации строка</returns>
        public override string? ToString()
        {
            return $"Действие над коллекцией \"{Name}\": {Action}. " +
                $"Элемент в действии: \"{Data}\".";
        }
        #endregion
    }
}