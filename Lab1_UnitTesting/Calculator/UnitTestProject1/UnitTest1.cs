using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnalaizerClassLibrary;
using System.Collections.Generic;
using System.Data.SQLite;


namespace UnitTestProject1
{
    [TestClass]
    public class AnalaizerFormatTests
    {
        // Метод-постачальник даних
        public static IEnumerable<object[]> GetTestCases()
        {
            const string connectionString = "Data Source=CalculatorTestsDB.db";
            using (var connection = new SQLiteConnection(connectionString))
            {
                // 1. Відкриваємо з'єднання 
                connection.Open();

                // 2. Створюємо об'єкт команди
                var command = connection.CreateCommand();
                command.CommandText = "SELECT InputExpression, ExpectedResult FROM FormatTestCases";

                // 3. Виконуємо команду і отримуємо "читач" даних
                using (var reader = command.ExecuteReader())
                {
                    // 4. Проходимо по кожному рядку в результаті
                    while (reader.Read())
                    {
                        // 5. Отримуємо дані з колонок по їх індексу (0 і 1)
                        var inputExpression = reader.GetString(0);
                        var expectedResult = reader.GetString(1);

                        // 6. "Віддаємо" дані для одного запуску тесту
                        yield return new object[] { inputExpression, expectedResult };
                    }
                }
            } // З'єднання закриється тут автоматично завдяки 'using'
        }

       
        [TestMethod]
        [DynamicData(nameof(GetTestCases), DynamicDataSourceType.Method)]
        public void Format_WithDataFromSQLite_ShouldReturnExpectedResult(string inputExpression, string expectedResult)
        {
            AnalaizerClass.expression = inputExpression;
            string actualResult = AnalaizerClass.Format();
            Assert.AreEqual(expectedResult, actualResult, $"Неправильний результат для виразу: '{inputExpression}'");
        }

        
        [TestMethod]
        public void Format_WithTooLongExpression_ShouldReturnError07()
        {
            string longExpression = new string('1', 70000);
            AnalaizerClass.expression = longExpression;
            string expectedResult = "&Error 07 — Дуже довгий вираз. Максмальная довжина — 65536 символів.";
            string actualResult = AnalaizerClass.Format();
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}