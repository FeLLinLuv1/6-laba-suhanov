using System;
using NLog;

namespace MathAppWithLogging
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Выберите операцию:");
                    Console.WriteLine("1. Сложение");
                    Console.WriteLine("2. Вычитание");
                    Console.WriteLine("3. Умножение");
                    Console.WriteLine("4. Деление");
                    Console.Write("Введите номер операции: ");
                    string operation = Console.ReadLine();

                    Console.Write("Введите первое число: ");
                    double num1 = double.Parse(Console.ReadLine());

                    Console.Write("Введите второе число: ");
                    double num2 = double.Parse(Console.ReadLine());

                    double result = PerformOperation(operation, num1, num2);
                    Console.WriteLine($"Результат: {result}");
                }
                catch (InvalidInputException ex)
                {
                    Logger.Error(ex, "Некорректная операция.");
                    Console.WriteLine("Ошибка: введена некорректная операция!");
                }
                catch (DivideByZeroException ex)
                {
                    Logger.Error(ex, "Попытка деления на ноль.");
                    Console.WriteLine("Ошибка: деление на ноль!");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Общая ошибка.");
                    Console.WriteLine("Произошла ошибка. Повторите ввод.");
                }
            }
        }

        static double PerformOperation(string operation, double num1, double num2)
        {
            return operation switch
            {
                "1" => num1 + num2,
                "2" => num1 - num2,
                "3" => num1 * num2,
                "4" => num2 == 0 ? throw new DivideByZeroException() : num1 / num2,
                _ => throw new InvalidInputException("Операция не поддерживается.")
            };
        }
    }

    class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message) { }
    }
}
