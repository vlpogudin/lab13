using GameLib;
using lab13;
using System.Text;

namespace UnitTestForCollection { }

/// <summary>
/// Класс тестов класса JournalEntry
/// </summary>
[TestClass]
public class UnitTestJournalEntry
{
    /// <summary>
    /// Тест конструктора с параметрами
    /// </summary>
    [TestMethod]
    public void TestParamConstr()
    {
        string name = "Первая"; // Задаем имя коллекции
        string action = "Удаление"; // Задаем действие над коллекцией
        Game game = new Game(); // Задаем элемент в действии
        string data = game.ToString(); // Сохраняем значение элемента
        JournalEntry item = new JournalEntry(name, action, data); // Создаем новый элемент
        Assert.AreEqual(name, item.Name); // Проверка, что присвоено корректное название
        Assert.AreEqual(action, item.Action); // Проверка, что присвоено корректное действие
        Assert.AreEqual(data, item.Data); // Проверка, что присвоено корректное значение
    }

    /// <summary>
    /// Тест преобразования в строку элемента
    /// </summary>
    [TestMethod]
    public void TestItemToString()
    {
        string name = "Первая"; // Задаем имя коллекции
        string action = "Удаление"; // Задаем действие над коллекцией
        Game game = new Game(); // Задаем элемент в действии
        string data = game.ToString(); // Сохраняем значение элемента
        JournalEntry item = new JournalEntry(name, action, data); // Создаем новый элемент
        string? itemString = item.ToString(); // Преобразуем элемент в строку
        Assert.IsNotNull(itemString); // Проверка, что преобразование в строку корректно
    }
}

/// <summary>
/// Класс тестов класса Journal
/// </summary>
[TestClass]
public class UnitTestJournal
{
    /// <summary>
    /// Тест добавления записи в журнал и его печать на экран
    /// </summary>
    [TestMethod]
    public void TestWriteRecordToJournal_PrintJournal()
    {
        Journal journal = new Journal(); // Создание нового журнала
        Game game = new Game(); // Задаем элемент в действии
        string data = game.ToString(); // Сохраняем значение элемента
        StringWriter sw = new StringWriter();
        Console.SetOut(sw); // Перенаправляем стандартный вывод в StringWriter
        journal.WriteRecord(new MyObservableCollection<Game>("Test", 1), new CollectionHandlerEventArgs("Удаление", game)); // Запись в журнал
        journal.PrintJournal(); // Печать журнала на экран
        string consoleOutput = sw.ToString(); // Сохраняем вывод консоли в строку
        Console.SetOut(Console.Out); // Восстанавливаем стандартный вывод
        Assert.IsTrue(consoleOutput.Contains("Удаление")); // Проверка, что журнал содержит корректное действие
        Assert.IsTrue(consoleOutput.Contains(data)); // Проверка, что журнал содержит корректный элемент 
    }
}