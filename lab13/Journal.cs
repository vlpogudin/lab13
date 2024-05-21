using GameLib;

namespace lab13
{
    /// <summary>
    /// Журнал записей действий, произошедших над коллекцией
    /// </summary>
    public class Journal
    {
        #region Поля
        /// <summary>
        /// Список, в котором находится журнал событий
        /// </summary>
        List<JournalEntry> journal = new List<JournalEntry>();
        #endregion

        #region Методы
        /// <summary>
        /// Добавление записи о событии в журнал
        /// </summary>
        /// <param name="entry"></param>
        public void WriteRecord(object source, CollectionHandlerEventArgs args)
        {
            journal.Add(new JournalEntry(((MyObservableCollection<Game>)source).Name,args.Action, args.Item.ToString())); // Добавление записи в журнал
        }

        /// <summary>
        /// Печать журнала на экран
        /// </summary>
        public void PrintJournal()
        {
            if (journal.Count == 0) { Console.WriteLine("Журнал событий пуст."); return; } // Если журнал пуст, выход из метода 
            foreach (JournalEntry entry in journal) // Цикл перебора записей в журнале
            {
                Console.WriteLine(entry.ToString()); // Печать записи на экран
            }
        }
        #endregion
    }
}
