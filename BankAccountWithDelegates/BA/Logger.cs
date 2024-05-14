namespace BankAccountWithDelegates;

/// <summary>
/// класс логгер 
/// </summary>
public class Logger
{
    /// <summary>
    /// лог информации
    /// </summary>
    /// <param name="message"> сообщение </param>
    public static void LogInfo(string message)
    {
        Log("[INFO] " + message);
    }

    /// <summary>
    /// лог ошибки
    /// </summary>
    /// <param name="message"> сообщение </param>
    public static void LogError(string message)
    {
        Log("[ERROR] " + message);
    }

    /// <summary>
    /// приватный метод
    /// для записи логов (сообщений) в файл 'log.txt'
    /// </summary>
    /// <param name="message"> сообщение </param>
    private static void Log(string message)
    {
        try
        {
            string filePath = "log.txt";
            using (StreamWriter writer = new StreamWriter(filePath, append:true))
            {
                writer.WriteLine($"{DateTime.Now} - {message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"ошибка записи в лог {e.Message}");
        }
    }
    
}