using System;
using BankAccountWithDelegates.BA;
using BankAccountWithDelegates.Calculator;

namespace BankAccountWithDelegates
{
	public class Program
	{
        public delegate void ProgramExecutor();

        public static void Main()
		{
            ProgramExecutor executor = null;

            // Логика выбора программы
            char choice = ' ';
            bool flag = false;

            do
            {
                try
                {
                    Console.WriteLine("Выберите программу: 1 - BankAccount Program, 2 - Calculator Program");
                    choice = char.ToLower(char.Parse(Console.ReadLine()));
                    flag = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                }
            } while (!flag);

            // В зависимости от выбора пользователя устанавливаем делегат
            switch (choice)
            {
                case '1':
                    executor = ProgramBA.Execute;
                    break;
                case '2':
                    executor = CalculatorProgram.Execute;
                    break;
                default:
                    Console.WriteLine("Неправильный выбор. Программа завершает работу.");
                    return;
            }

            // Запуск выбранной программы через делегат
            executor.Invoke();
        }
	}
}
