using GameLib;

namespace lab13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyObservableCollection<Game> col1 = new MyObservableCollection<Game>("Первая коллекция", 4); // Создание первой коллекции
            MyObservableCollection<Game> col2 = new MyObservableCollection<Game>("Вторая коллекция", 7); // Создание второй коллекции
            Journal jour1 = new Journal(); // Создание первого журнала записей
            Journal jour2 = new Journal(); // Создание второго журнала записей
            col1.CollectionCountChanged += jour1.WriteRecord; // Подписка на событие для первой коллекции первого журнала
            col1.CollectionReferenceChanged += jour1.WriteRecord; // Подписка на событие для первой коллекции первого журнала
            col1.CollectionReferenceChanged += jour2.WriteRecord; // Подписка на событие для первой коллекции второго журнала
            col2.CollectionReferenceChanged += jour2.WriteRecord; // Подписка на событие для второй коллекции второго журнала
            int answer; // Переменная выбора пункта меню
            do
            {
                UI.PrintMainMenu(); // Печать главного меню
                answer = UI.GetInt(); // Получение корректного пункта главного меню
                switch (answer)
                {
                    case 1:
                        Console.Clear();
                        PrintCollection(col1, col2); // Печать коллекции на экран
                        break;

                    case 2:
                        Console.Clear();
                        AddItem(col1, col2); // Добавление элемента в коллекцию          
                        break;

                    case 3:
                        Console.Clear();
                        RemoveItem(col1, col2); // Удаление элемента из коллекции
                        break;

                    case 4:
                        Console.Clear();
                        EditItem(col1, col2); // Изменение значения элемента коллекции
                        break;

                    case 5:
                        Console.Clear();
                        PrintJournals(jour1, jour2); // Печать журналов на экран
                        break;

                    case 6:
                        Console.Clear();
                        int exitAnswer; /* Переменная, отвечающая за выбор
                                           пользователем пункта выходного меню */
                        do
                        {
                            UI.PrintExitMenu(); // Печать выходного меню
                            exitAnswer = UI.GetInt(); // Проверка корректного ввода пункта меню
                            switch (exitAnswer)
                            {
                                case 1:
                                    Console.Clear();
                                    UI.ExitProgram(); // Печать сообщения о завершении программы
                                    System.Environment.Exit(0); // Принудительное завершение программы
                                    break;

                                case 2:
                                    Console.Clear();
                                    break;

                                default:
                                    Console.Clear();
                                    UI.ChangeColor("Некорректный пункт меню. \nПопробуйте снова.\n\n", ConsoleColor.Red); // Печать сообщения об ошибочном выборе пункта меню
                                    break;
                            }
                        } while (exitAnswer != 2);
                        break;

                    default:
                        Console.Clear();
                        UI.ChangeColor("Некорректный пункт меню. \nПопробуйте снова.\n", ConsoleColor.Red); // Печать сообщения об ошибочном выборе пункта меню
                        break;
                }
            } while (true);
        }

        /// <summary>
        /// Печать коллекции на экран
        /// </summary>
        /// <param name="col1">Первая коллекция</param>
        /// <param name="col2">Вторая коллекция</param>
        static void PrintCollection(MyObservableCollection<Game> col1, MyObservableCollection<Game> col2)
        {
            int answerSelectCol = UI.SelectCollection(); // Выбор номера коллекции
            if (answerSelectCol == 1) // Выбрана первая коллекция
            { 
                Console.Clear(); 
                Console.WriteLine("Печать коллекции 1 по уровням:\n"); 
                col1.Print(); // Печать первой коллекции
                Console.WriteLine(); 
            }
            else if (answerSelectCol == 2) // Выбрана вторая коллекция
            {
                Console.Clear();
                Console.WriteLine("Печать коллекции 2 по уровням:\n");
                col2.Print(); // Печать второй коллекции
                Console.WriteLine();
            }
            else { Console.Clear(); } // Выход из метода
        }

        /// <summary>
        /// Добавление элемента в коллекции
        /// </summary>
        /// <param name="col1">Первая коллекция</param>
        /// <param name="col2">Вторая коллекция</param>
        static void AddItem(MyObservableCollection<Game> col1, MyObservableCollection<Game> col2)
        {
            int answerSelectCol = UI.SelectCollection(); // Выбор номера коллекции
            if (answerSelectCol == 1) // Выбрана первая коллекция
            {
                AddItemToSomeCollection(col1); // Вызов метода добавления элемента в первую коллекцию
            }
            else if (answerSelectCol == 2) // Выбрана вторая коллекция
            {
                AddItemToSomeCollection(col2); // Вызов метода добавления элемента во вторую коллекцию
            }
            else { Console.Clear(); } // Выход из метода
        }
        
        /// <summary>
        /// Добавление элемента в определенную коллекцию
        /// </summary>
        /// <param name="col">Коллекция для добавления элемента</param>
        static void AddItemToSomeCollection(MyObservableCollection<Game> col)
        {
            Console.WriteLine("\nВведите элемент для добавления: ");
            Game newItem = new Game(); // Создание нового элемента
            newItem.Init(); // Заполнение элемента вручную
            bool result = col.Contains(newItem); // Проверка, существует ли уже такой элемент в коллекции
            if (result) { Console.Clear(); Console.WriteLine("Элемент уже есть в коллекции.\n"); } // Ошибка, если элемент уже есть
            else // Если элемент уникальный
            {
                col.Add(newItem); // Добавление элемента в коллекцию
                Console.Clear();
                Console.WriteLine($"Элемент {newItem} добавлен в коллекцию.\n");
            }
        }

        /// <summary>
        /// Удаление элемента из коллекции
        /// </summary>
        /// <param name="col1">Первая коллекция</param>
        /// <param name="col2">Вторая коллекция</param>
        static void RemoveItem(MyObservableCollection<Game> col1, MyObservableCollection<Game> col2)
        {
            int answerSelectCol = UI.SelectCollection(); // Выбор номера коллекции
            if (answerSelectCol == 1) // Выбрана первая коллекция
            {
                RemoveItemFromSomeCollection(col1); // Вызов метода удаления элемента из первой коллекции
            }
            else if (answerSelectCol == 2) // Выбрана вторая коллекция
            {
                RemoveItemFromSomeCollection(col2); // Вызов метода удаления элемента из второй коллекции
            }
            else { Console.Clear(); } // Выход из метода
        }

        /// <summary>
        /// Удаление элемента из определенной коллеции
        /// </summary>
        /// <param name="col">Коллекция для удаления элемента</param>
        static void RemoveItemFromSomeCollection(MyObservableCollection<Game> col)
        {
            if (col.Root == null) { Console.WriteLine("Дерево пусто. Невозможно осуществить удаление.\n"); return; } // Если дерево пусто, выход из метода
            Console.WriteLine("\nВведите элемент для удаления: ");
            Game deleteItem = new Game(); // Создание нового элемента
            deleteItem.Init(); // Заполнение элемента вручную
            bool result = col.Remove(deleteItem); // Удаление элемента из коллекции
            Console.Clear();
            if (result) { Console.WriteLine($"Элемент {deleteItem} удален из коллекции.\n"); } // Если удаление успешно
            else { Console.WriteLine("Элемент не был удален из коллекции.\n"); } // Если удаление неуспешно
        }

        /// <summary>
        /// Изменение значения элемента коллекции
        /// </summary>
        /// <param name="col1">Первая коллекция</param>
        /// <param name="col2">Вторая коллекция</param>
        static void EditItem(MyObservableCollection<Game> col1, MyObservableCollection<Game> col2)
        {
            int answerSelectCol = UI.SelectCollection(); // Выбор номера коллекции
            if (answerSelectCol == 1) // Выбрана первая коллекция
            {
                EditItemInSomeCollection(col1); // Вызов метода изменения элемента из первой коллекции
            }
            else if (answerSelectCol == 2) // Выбрана вторая коллекция
            {
                EditItemInSomeCollection(col2); // Вызов метода изменения элемента из второй коллекции
            }
            else { Console.Clear(); } // Выход из метода
        }

        /// <summary>
        /// Изменение значения элемента в определенной коллекции
        /// </summary>
        /// <param name="col">Коллекция для изменения значения элемента</param>
        static void EditItemInSomeCollection(MyObservableCollection<Game> col)
        {
            if (col.Root == null) { Console.WriteLine("Дерево пусто. Невозможно осуществить изменение элемента.\n"); return; } // Если дерево пусто, выход из метода
            Console.WriteLine("\nВведите элемент для изменения: ");
            Game editItem = new Game(); // Создание нового элемента
            editItem.Init(); // Заполнение элемента вручную
            bool result = col.Contains(editItem); // Проверка, существует ли такой элемент в коллекции
            if (result)
            {
                Console.WriteLine("\nВведите новое информационное поле элемента: ");
                Game newItem = new Game(); // Создание нового элемента
                newItem.Init(); // Заполнение элемента вручную
                col[editItem] = newItem; // Изменение значения элемента
                Console.Clear();
                Console.WriteLine($"Информационное поле элемента было изменено на {newItem}.\n");
            }
            else { Console.Clear();  Console.WriteLine("Элемента с таким информационным полем не найдено в коллекции.\n"); }
        }

        /// <summary>
        /// Печать журналов событий на экран
        /// </summary>
        /// <param name="journal1">Первый журнал событий</param>
        /// <param name="journal2">Второй журнал событий</param>
        static void PrintJournals(Journal journal1, Journal journal2)
        {
            Console.WriteLine("Печать первого журнала: ");
            journal1.PrintJournal(); // Печать первого журнала
            Console.WriteLine();

            Console.WriteLine("Печать второго журнала: ");
            journal2.PrintJournal(); // Печать второго журнала
            Console.WriteLine();
        } 
    }
}