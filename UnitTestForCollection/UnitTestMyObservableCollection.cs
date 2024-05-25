using GameLib;
using lab13;

namespace UnitTestForCollection { }

/// <summary>
/// Класс тестов класса MyObservableCollection
/// </summary>
[TestClass]
public class UnitTestMyObservableCollection
{
    MyObservableCollection<Game> col1 = new MyObservableCollection<Game>("Первая коллекция", 4); // Создание первой коллекции

    #region Тесты добавления элемента в коллекцию
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
    #endregion

    #region Тесты удаления элемента в коллекцию
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
    #endregion

    #region Тесты изменения значения элемента в коллекции
    /// <summary>
    /// Тест изменения значения элемента в коллекции на уже существующее значение коллекции
    /// и обработка события изменения ссылки элемента в коллекции
    /// </summary>
    [TestMethod]
    public void TestEditItemOnExistItem_ReferenceChangedEvent()
    {
        Game game = new Game(); // Создание нового элемента
        col1.Add(game); // Добавление нового элемента в коллекцию
        bool eventRaised = false; // Флаг действия события
        col1.CollectionReferenceChanged += (source, args) => eventRaised = true; // Подписка на событие
        Game newGame = new Game(); // Создание нового элемента
        Assert.ThrowsException<InvalidOperationException>(() => col1[game] = newGame); // Проверка, что выкинуло исключение
        Assert.IsFalse(eventRaised); // Проверка, что событие не было запущено
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

    /// <summary>
    /// Тест изменения значения элемента в коллекции на корректное и обработка события изменения ссылки элемента в коллекции
    /// </summary>
    [TestMethod]
    public void TestEditCorrectItem_ReferenceChangedEvent()
    {
        Game game = new Game(); // Создание нового элемента
        col1.Add(game); // Добавление нового элемента в коллекцию
        bool eventRaised = false; // Флаг действия события
        col1.CollectionReferenceChanged += (source, args) => eventRaised = true; // Подписка на событие
        Game newGame = new Game("Игра", 1, 2, 1); // Создание нового элемента
        col1[game] = newGame; // Изменение значения элемента на новое
        Assert.IsTrue(eventRaised); // Проверка, что событие не было запущено
    }
    #endregion

    #region Тесты получения значения элемента в коллекции
    /// <summary>
    /// Тест получения корректного значения существующего элемента в коллекции
    /// </summary>
    [TestMethod]
    public void TestGetExistItem()
    {
        Game game = new Game(); // Создание нового элемента
        col1.Add(game); // Добавление нового элемента в коллекцию
        Game existGame = col1[game]; // Получение значения элемента из коллекции
        Assert.AreEqual(game, existGame); // Проверка, что полученное значение корректно
    }

    /// <summary>
    /// Тест получения значения несуществующего элемента в коллекции
    /// </summary>
    [TestMethod]
    public void TestGetNoExistItem()
    {
        Game game = new Game(); // Создание нового элемента
        Game noExistGame = new Game(); // Создание нового элемента
        Assert.ThrowsException<InvalidOperationException>(() => noExistGame = col1[game]); // Проверка, что выкинуло исключение
    }
    #endregion

    #region Тесты очистки коллекции
    /// <summary>
    /// Тест очистки коллекции и обработка события изменения количества элементов в коллекции
    /// </summary>
    [TestMethod]
    public void TestClearCollection()
    {
        bool eventRaised = false; // Флаг действия события
        col1.CollectionCountChanged += (source, args) => eventRaised = true; // Подписка на событие
        col1.Clear(); // Очистка коллекции
        Assert.IsTrue(eventRaised); // Проверка, что событие было запущено
        Assert.IsNull(col1.Root); // Проверка, что корень дерева пуст
    }
    #endregion
}