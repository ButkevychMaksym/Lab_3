using System;
using System.Linq;
using System.Threading.Tasks;

public class PalindromeChecker
{
    /// <summary>
    /// Очищає вхідний рядок, видаляючи неалфавітні символи і перетворюючи його в нижній регістр.
    /// </summary>
    /// <param name="input">Вхідний рядок для очищення.</param>
    /// <returns>Очищена версія вхідного рядка.</returns>
    private string CleanInput(string input)
    {
        // Фільтрує неалфавітні символи і перетворює рядок в нижній регістр
        return new string(input.Where(char.IsLetterOrDigit).ToArray()).ToLower();
    }

    /// <summary>
    /// Перевіряє, чи є даний рядок паліндромом.
    /// </summary>
    /// <param name="input">Рядок для перевірки.</param>
    /// <returns>True, якщо вхідний рядок є паліндромом, інакше false.</returns>
    public bool IsPalindrome(string input)
    {
        // Очищає рядок для ігнорування пробілів, пунктуації та регістру
        string cleanedInput = CleanInput(input);

        // Ініціалізує два вказівники для порівняння символів з обох кінців
        int left = 0;
        int right = cleanedInput.Length - 1;

        // Порівнює символи з початку і кінця, рухаючись до центру
        while (left < right)
        {
            if (cleanedInput[left] != cleanedInput[right])
            {
                // Повертає false, якщо символи не співпадають
                return false;
            }
            left++;
            right--;
        }

        // Повертає true, якщо всі порівняння символів співпали
        return true;
    }

    /// <summary>
    /// Асинхронно перевіряє, чи є даний рядок паліндромом.
    /// </summary>
    /// <param name="input">Рядок для перевірки.</param>
    /// <returns>Задача, яка представляє асинхронну операцію, що містить true, якщо вхідний рядок є паліндромом, інакше false.</returns>
    public async Task<bool> IsPalindromeAsync(string input)
    {
        // Виконує перевірку паліндрому в окремому потоці, щоб уникнути блокування основного потоку
        return await Task.Run(() => IsPalindrome(input));
    }

    /// <summary>
    /// Валідовує вхідний рядок, щоб впевнитися, що він не є null, порожнім або занадто коротким.
    /// </summary>
    /// <param name="input">Рядок для валідовки.</param>
    /// <exception cref="ArgumentException">Викидається, якщо вхідний рядок є недійсним.</exception>
    public void ValidateInput(string input)
    {
        // Перевіряє, чи рядок є null, порожнім або складається тільки з пробілів
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Введений рядок не може бути порожнім або складатися лише з пробілів.");
        }

        // Перевіряє, чи рядок має менше двох символів
        if (input.Length < 2)
        {
            throw new ArgumentException("Рядок повинен містити щонайменше два символи.");
        }
    }
}
