using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PalindromeCheckerApp
{
    /// <summary>
    /// The main window of the Palindrome Checker application.
    /// </summary>
    public partial class MainWindow : Window
    {
        private PalindromeChecker _palindromeChecker;

        /// <summary>
        /// Initializes a new instance of the MainWindow class and creates an instance of PalindromeChecker.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _palindromeChecker = new PalindromeChecker();
        }

        /// <summary>
        /// Asynchronously checks if the input text is a palindrome when the "Check" button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data associated with the click event.</param>
        private async void CheckPalindrome(object sender, RoutedEventArgs e)
        {
            string input = InputTextBox.Text;

            try
            {
                // Validates the input string to ensure it meets basic constraints
                _palindromeChecker.ValidateInput(input);

                // Shows the progress bar while the palindrome check is being performed
                ProgressBar.Visibility = Visibility.Visible;

                // Asynchronously checks if the input is a palindrome
                bool isPalindrome = await _palindromeChecker.IsPalindromeAsync(input);

                // Displays the result in the UI based on the palindrome check
                ResultLabel.Content = isPalindrome ? "Це паліндром!" : "Це не паліндром!";
            }
            catch (ArgumentException ex)
            {
                // Displays an error message if the input is invalid
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Hides the progress bar once the palindrome check is complete
                ProgressBar.Visibility = Visibility.Collapsed;
            }
        }
    }

    /// <summary>
    /// A utility class for checking if a string is a palindrome.
    /// </summary>
    public class PalindromeChecker
    {
        /// <summary>
        /// Cleans the input string by removing non-alphanumeric characters and converting it to lowercase.
        /// </summary>
        /// <param name="input">The input string to clean.</param>
        /// <returns>A cleaned version of the input string.</returns>
        private string CleanInput(string input)
        {
            return new string(input.Where(char.IsLetterOrDigit).ToArray()).ToLower();
        }

        /// <summary>
        /// Checks if the given string is a palindrome.
        /// </summary>
        /// <param name="input">The string to check.</param>
        /// <returns>True if the input string is a palindrome, false otherwise.</returns>
        public bool IsPalindrome(string input)
        {
            string cleanedInput = CleanInput(input);

            int left = 0;
            int right = cleanedInput.Length - 1;

            while (left < right)
            {
                if (cleanedInput[left] != cleanedInput[right])
                {
                    return false;
                }
                left++;
                right--;
            }
            return true;
        }

        /// <summary>
        /// Asynchronously checks if the given string is a palindrome.
        /// </summary>
        /// <param name="input">The string to check.</param>
        /// <returns>A task that represents the asynchronous operation, containing true if the input string is a palindrome, false otherwise.</returns>
        public async Task<bool> IsPalindromeAsync(string input)
        {
            return await Task.Run(() => IsPalindrome(input));
        }

        /// <summary>
        /// Validates the input string to ensure it is not null, empty, or too short.
        /// </summary>
        /// <param name="input">The input string to validate.</param>
        /// <exception cref="ArgumentException">Thrown if the input string is invalid.</exception>
        public void ValidateInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Введений рядок не може бути порожнім або складатися лише з пробілів.");
            }

            if (input.Length < 2)
            {
                throw new ArgumentException("Рядок повинен містити щонайменше два символи.");
            }
        }
    }
}
