namespace lab13
{
    /// <summary>
    /// Пользовательскиий интерфейс
    /// </summary>
    internal class UI
    {
        #region Списки для меню
        /// <summary>
        /// Список для главного меню
        /// </summary>
        static List<string> MainMenu = new List<string>() {
            "=== ГЛАВНОЕ МЕНЮ ===",
            "1. Распечатать коллекцию.\n",
            "2. Добавить элемент в коллекцию.\n",
            "3. Удалить элемент из коллекции.\n",
            "4. Изменить значение элемента.\n",
            "5. Очистить коллекцию.\n",
            "6. Распечатать журнал событий.\n",
            "7. Выход.\n",
            "\nВыберите пункт меню: " };

        /// <summary>
        /// Список для выходного меню
        /// </summary>
        static List<string> ExitMenu = new List<string>() { 
            "Вы уверены, что хотите завершить работу?",
            "1. Да.\n", 
            "2. Нет.\n", 
            "\nВыберите пункт меню: " };

        /// <summary>
        /// Список для меню выбора коллекции
        /// </summary>
        static List<string> SelectionCollectionMenu = new List<string>() { 
            "=== ВЫБОР КОЛЛЕКЦИИ ===",
            "1. Коллекция 1.\n",
            "2. Коллекция 2.\n",
            "3. Вернуться назад.\n",
            "\nВыберите пункт меню: " };
        #endregion

        #region Методы работы с цветом
        /// <summary>
        /// Изменение цвета переданной строки
        /// </summary>
        /// <param name="str"></param>
        /// <param name="color"></param>
        public static void ChangeColor(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color; // Изменение цвета консоли
            Console.WriteLine(str); // Печать строки
            Console.ResetColor(); // Сброс цвета консоли
        }
        #endregion

        #region Методы печати меню
        /// <summary>
        /// Печать главного меню
        /// </summary>
        static public void PrintMainMenu()
        {
            ChangeColor(MainMenu[0], ConsoleColor.Blue); // Печать заголовка
            for (int i = 1; i < MainMenu.Count; i++) // Перебор пунктов меню
            {
                Console.Write(MainMenu[i]); // Печать пункта меню
            }
        }

        /// <summary>
        /// Печать меню выбора коллекции
        /// </summary>
        static public void PrintSelectionCollectionMenu()
        {
            ChangeColor(SelectionCollectionMenu[0], ConsoleColor.Blue); // Печать заголовка
            for (int i = 1; i < SelectionCollectionMenu.Count; i++) // Перебор пунктов меню
            {
                Console.Write(SelectionCollectionMenu[i]); // Печать пункта меню
            }
        }

        /// <summary>
        /// Печать меню выхода
        /// </summary>
        static public void PrintExitMenu()
        {
            ChangeColor(ExitMenu[0], ConsoleColor.Blue); // Печать заголовка
            for (int i = 1; i < ExitMenu.Count; i++) // Перебор пунктов меню
            {
                Console.Write(ExitMenu[i]); // Печать пункта меню
            }
        }

        /// <summary>
        /// Печать сообщения о завершении программы
        /// </summary>
        static public void ExitProgram()
        {
            ChangeColor("Программа завершена!", ConsoleColor.Red);
        }
        #endregion

        #region Вспомогательные методы для пользователя
        /// <summary>
        /// Получение целого числа от пользователя
        /// </summary>
        /// <returns>Корректное введеное целое число</returns>
        public static int GetInt()
        {
            bool isConvert; // Результат конвертирования
            int result; // Результат преобразования строки
            do // Пока не будет введено корректное значение
            {
                isConvert = int.TryParse(Console.ReadLine(), out result); // Проверка конвертирования
                if (!isConvert) // Если введено некорректное число
                {
                    ChangeColor("\nНекорректный ввод или число выходит за пределы. \nПовторите ввод: ", ConsoleColor.Red); // Вывод ошибки
                }
            }
            while (!isConvert);
            return result; // Возврат корректного числа
        }

        /// <summary>
        /// Выбор коллекции
        /// </summary>
        /// <returns>Номер выбранной коллекции</returns>
        static public int SelectCollection()
        {
            int answer; // Переменная выбора пункта меню
            do // Пока не введено корректное значение
            {
                UI.PrintSelectionCollectionMenu(); // Печать меню выбора коллекции
                answer = UI.GetInt(); // Получение корректного пункта меню выбора коллекции
                switch (answer)
                {
                    case 1: return 1; // Выбрана первая коллекция

                    case 2: return 2; // Выбрана вторая коллекция

                    case 3: return 3; // Выбрано вернуться назад

                    default:
                        Console.Clear();
                        UI.ChangeColor("Некорректный пункт меню. \nПопробуйте снова.\n", ConsoleColor.Red); // Печать сообщения об ошибочном выборе пункта меню
                        break;
                }
            } while (answer != 3);
            return 3;
        }
        #endregion
    }
}