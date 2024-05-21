using GameLib;
using lab13;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace UnitTestForCollection
{
    /// <summary>
    /// ����� ������ ������ MyObservableCollection
    /// </summary>
    [TestClass]
    public class UnitTestMyObservableCollection
    {
        MyObservableCollection<Game> col1 = new MyObservableCollection<Game>("������ ���������", 4); // �������� ������ ���������

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

        /// <summary>
        /// ���� ��������� �������� �������� � ��������� � ��������� ������� ��������� ������ �������� � ���������
        /// </summary>
        [TestMethod]
        public void TestEditItem_ReferenceChangedEvent()
        {
            Game game = new Game(); // �������� ������ ��������
            col1.Add(game); // ���������� ������ �������� � ���������
            bool eventRaised = false; // ���� �������� �������
            col1.CollectionReferenceChanged += (source, args) => eventRaised = true; // �������� �� �������
            Game newGame = new Game(); // �������� ������ ��������
            col1[game] = newGame; // ��������� �������� �������� �� �����
            Assert.IsTrue(eventRaised); // ��������, ��� ������� ���� ��������
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
    }
}