using GameLib;
using lab13;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace UnitTestForCollection
{
    /// <summary>
    /// Класс тестов класса MyObservableCollection
    /// </summary>
    [TestClass]
    public class UnitTestMyObservableCollection
    {
        MyObservableCollection<Game> col1 = new MyObservableCollection<Game>("Первая коллекция", 4); // Создание первой коллекции

        /// <summary>
        /// Тест добавления элемента в коллекцию и обработка события изменения количества элементов в коллекции
        /// </summary>
        [TestMethod]
        public void TestAddItem_CountChangedEvent()
        {
            bool eventRaised = false; // Флаг действия события
            col1.CollectionCountChanged += (source, args) => eventRaised = true; // Подписка на событие
            Game game = new Game(); // Создание нового элемента
            col1.Add(game); // Добавление нового элемента в коллекцию
            Assert.IsTrue(eventRaised); // Проверка, что событие было запущено
        }

        /// <summary>
        /// Тест удаления элемента из коллекции и обработка события изменения количества элементов в коллекции
        /// </summary>
        [TestMethod]
        public void TestRemoveItem_CountChangedEvent()
        {
            Game game = new Game(); // Создание нового элемента
            col1.Add(game); // Добавление нового элемента в коллекцию
            bool eventRaised = false; // Флаг действия события
            col1.CollectionCountChanged += (source, args) => eventRaised = true; // Подписка на событие
            col1.Remove(game); // Удаление элемента из коллекции
            Assert.IsTrue(eventRaised); // Проверка, что событие было запущено
        }

        /// <summary>
        /// Тест удаления несуществующего элемента из коллекции и обработка события изменения количества элементов в коллекции
        /// </summary>
        [TestMethod]
        public void TestRemoveNoExistItem_CountChangedEvent()
        {
            Game game = new Game(); // Создание нового элемента
            bool eventRaised = false; // Флаг действия события
            col1.CollectionCountChanged += (source, args) => eventRaised = true; // Подписка на событие
            col1.Remove(game); // Удаление элемента из коллекции
            Assert.IsFalse(eventRaised); // Проверка, что событие не было запущено
        }

        /// <summary>
        /// Тест изменения значения элемента в коллекции и обработка события изменения ссылки элемента в коллекции
        /// </summary>
        [TestMethod]
        public void TestEditItem_ReferenceChangedEvent()
        {
            Game game = new Game(); // Создание нового элемента
            col1.Add(game); // Добавление нового элемента в коллекцию
            bool eventRaised = false; // Флаг действия события
            col1.CollectionReferenceChanged += (source, args) => eventRaised = true; // Подписка на событие
            Game newGame = new Game(); // Создание нового элемента
            col1[game] = newGame; // Изменение значения элемента на новое
            Assert.IsTrue(eventRaised); // Проверка, что событие было запущено
        }

        /// <summary>
        /// Тест изменения значения элемента в коллекции и обработка события изменения ссылки элемента в коллекции
        /// </summary>
        [TestMethod]
        public void TestEditNoExistItem_ReferenceChangedEvent()
        {
            Game game = new Game(); // Создание нового элемента
            bool eventRaised = false; // Флаг действия события
            col1.CollectionReferenceChanged += (source, args) => eventRaised = true; // Подписка на событие
            Game newGame = new Game(); // Создание нового элемента
            Assert.ThrowsException<InvalidOperationException>(() => col1[game] = newGame); // Проверка, что выкинуло исключение
            Assert.IsFalse(eventRaised); // Проверка, что событие не было запущено
        }
    }
}