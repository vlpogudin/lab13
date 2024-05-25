using GameLib;
using lab13;

namespace UnitTestForCollection { }

/// <summary>
/// ����� ������ ������ MyObservableCollection
/// </summary>
[TestClass]
public class UnitTestMyObservableCollection
{
    MyObservableCollection<Game> col1 = new MyObservableCollection<Game>("������ ���������", 4); // �������� ������ ���������

    #region ����� ���������� �������� � ���������
    /// <summary>
    /// ���� ���������� �������� � ��������� � ��������� ������� ��������� ���������� ��������� � ���������
    /// </summary>
    [TestMethod]
    public void TestAddItem_CountChangedEvent()
    {
        bool eventRaised = false; // ���� �������� �������
        col1.CollectionCountChanged += (source, args) => eventRaised = true; // �������� �� �������
        Game game = new Game(); // �������� ������ ��������
        col1.Add(game); // ���������� ������ �������� � ���������
        Assert.IsTrue(eventRaised); // ��������, ��� ������� ���� ��������
    }
    #endregion

    #region ����� �������� �������� � ���������
    /// <summary>
    /// ���� �������� �������� �� ��������� � ��������� ������� ��������� ���������� ��������� � ���������
    /// </summary>
    [TestMethod]
    public void TestRemoveItem_CountChangedEvent()
    {
        Game game = new Game(); // �������� ������ ��������
        col1.Add(game); // ���������� ������ �������� � ���������
        bool eventRaised = false; // ���� �������� �������
        col1.CollectionCountChanged += (source, args) => eventRaised = true; // �������� �� �������
        col1.Remove(game); // �������� �������� �� ���������
        Assert.IsTrue(eventRaised); // ��������, ��� ������� ���� ��������
    }

    /// <summary>
    /// ���� �������� ��������������� �������� �� ��������� � ��������� ������� ��������� ���������� ��������� � ���������
    /// </summary>
    [TestMethod]
    public void TestRemoveNoExistItem_CountChangedEvent()
    {
        Game game = new Game(); // �������� ������ ��������
        bool eventRaised = false; // ���� �������� �������
        col1.CollectionCountChanged += (source, args) => eventRaised = true; // �������� �� �������
        col1.Remove(game); // �������� �������� �� ���������
        Assert.IsFalse(eventRaised); // ��������, ��� ������� �� ���� ��������
    }
    #endregion

    #region ����� ��������� �������� �������� � ���������
    /// <summary>
    /// ���� ��������� �������� �������� � ��������� �� ��� ������������ �������� ���������
    /// � ��������� ������� ��������� ������ �������� � ���������
    /// </summary>
    [TestMethod]
    public void TestEditItemOnExistItem_ReferenceChangedEvent()
    {
        Game game = new Game(); // �������� ������ ��������
        col1.Add(game); // ���������� ������ �������� � ���������
        bool eventRaised = false; // ���� �������� �������
        col1.CollectionReferenceChanged += (source, args) => eventRaised = true; // �������� �� �������
        Game newGame = new Game(); // �������� ������ ��������
        Assert.ThrowsException<InvalidOperationException>(() => col1[game] = newGame); // ��������, ��� �������� ����������
        Assert.IsFalse(eventRaised); // ��������, ��� ������� �� ���� ��������
    }

    /// <summary>
    /// ���� ��������� �������� �������� � ��������� � ��������� ������� ��������� ������ �������� � ���������
    /// </summary>
    [TestMethod]
    public void TestEditNoExistItem_ReferenceChangedEvent()
    {
        Game game = new Game(); // �������� ������ ��������
        bool eventRaised = false; // ���� �������� �������
        col1.CollectionReferenceChanged += (source, args) => eventRaised = true; // �������� �� �������
        Game newGame = new Game(); // �������� ������ ��������
        Assert.ThrowsException<InvalidOperationException>(() => col1[game] = newGame); // ��������, ��� �������� ����������
        Assert.IsFalse(eventRaised); // ��������, ��� ������� �� ���� ��������
    }

    /// <summary>
    /// ���� ��������� �������� �������� � ��������� �� ���������� � ��������� ������� ��������� ������ �������� � ���������
    /// </summary>
    [TestMethod]
    public void TestEditCorrectItem_ReferenceChangedEvent()
    {
        Game game = new Game(); // �������� ������ ��������
        col1.Add(game); // ���������� ������ �������� � ���������
        bool eventRaised = false; // ���� �������� �������
        col1.CollectionReferenceChanged += (source, args) => eventRaised = true; // �������� �� �������
        Game newGame = new Game("����", 1, 2, 1); // �������� ������ ��������
        col1[game] = newGame; // ��������� �������� �������� �� �����
        Assert.IsTrue(eventRaised); // ��������, ��� ������� �� ���� ��������
    }
    #endregion

    #region ����� ��������� �������� �������� � ���������
    /// <summary>
    /// ���� ��������� ����������� �������� ������������� �������� � ���������
    /// </summary>
    [TestMethod]
    public void TestGetExistItem()
    {
        Game game = new Game(); // �������� ������ ��������
        col1.Add(game); // ���������� ������ �������� � ���������
        Game existGame = col1[game]; // ��������� �������� �������� �� ���������
        Assert.AreEqual(game, existGame); // ��������, ��� ���������� �������� ���������
    }

    /// <summary>
    /// ���� ��������� �������� ��������������� �������� � ���������
    /// </summary>
    [TestMethod]
    public void TestGetNoExistItem()
    {
        Game game = new Game(); // �������� ������ ��������
        Game noExistGame = new Game(); // �������� ������ ��������
        Assert.ThrowsException<InvalidOperationException>(() => noExistGame = col1[game]); // ��������, ��� �������� ����������
    }
    #endregion

    #region ����� ������� ���������
    /// <summary>
    /// ���� ������� ��������� � ��������� ������� ��������� ���������� ��������� � ���������
    /// </summary>
    [TestMethod]
    public void TestClearCollection()
    {
        bool eventRaised = false; // ���� �������� �������
        col1.CollectionCountChanged += (source, args) => eventRaised = true; // �������� �� �������
        col1.Clear(); // ������� ���������
        Assert.IsTrue(eventRaised); // ��������, ��� ������� ���� ��������
        Assert.IsNull(col1.Root); // ��������, ��� ������ ������ ����
    }
    #endregion
}