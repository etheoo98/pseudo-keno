namespace PseudoKeno;

public static class PseudoKeno
{
    // Constants
    // Number of balls to be entered
    private const int NumberOfBalls = 10;

    // Number of random numbers to be generated
    private const int NumberOfRandomNumbers = 5;

    // Minimum value for valid input
    private const int MinValue = 1;

    // Maximum value for valid input
    private const int MaxValue = 25;

    private static void Main()
    {
        do
        {
            // Clear any previous output
            Console.Clear();

            // Array to store entered numbers
            var numbers = new int[NumberOfBalls];

            // Welcome message and instructions
            Console.WriteLine(
                $"Welcome to Pseudo Keno! Before the game begins, you have to enter {NumberOfBalls} integers.");

            // Loop to get valid input for all balls
            for (var i = 0; i < NumberOfBalls;)
            {
                // Current ball number
                var ballNumber = i + 1;
                Console.Write($"Enter an integer for ball nr {ballNumber}: ");
                var input = Console.ReadLine();

                // Check if input is valid and within the specified range
                if (!string.IsNullOrWhiteSpace(input) && IsValidInput(input, out var number))
                {
                    // Store the valid number
                    numbers[i] = number;
                    // Move to the next ball
                    i++;
                }
                else
                {
                    Console.WriteLine(
                        $"Error: Invalid input. Please enter a valid integer between {MinValue} and {MaxValue}!");
                }
            }

            // Generate random numbers
            var randomNumbers = GenerateRandomNumbers(NumberOfRandomNumbers, MinValue, MaxValue);

            // Display the generated random numbers
            Console.Write("The random numbers are: ");
            Console.WriteLine(string.Join(", ", randomNumbers));

            // Find the matching numbers
            var matchingNumbers = FindMatchingNumbers(numbers, randomNumbers);

            // Check the count of matching numbers and display the result
            switch (matchingNumbers.Count)
            {
                case 1:
                {
                    // Find the ball number of the winning number
                    var ballNumber = Array.IndexOf(randomNumbers, matchingNumbers[0]) + 1;
                    Console.WriteLine(
                        $"You won! Your winning number {matchingNumbers[0]} was found at ball number {ballNumber}.");
                    break;
                }
                case > 1:
                    Console.Write("You won! Your winning numbers were found at ball numbers: ");
                    // Find the ball numbers of the matching numbers
                    Console.WriteLine(string.Join(", ",
                        matchingNumbers.Select(num => Array.IndexOf(randomNumbers, num) + 1)));
                    break;
                default:
                    Console.WriteLine("Unlucky! You didn't win this time.");
                    break;
            }

            // Ask the user if they want to play again
            Console.WriteLine("Do you want to play again? (Y/n)");

            // Keep looping until the user enters 'n'
        } while (char.ToLower(Console.ReadKey().KeyChar) != 'n');
    }

    // Check if the input is a valid integer within the specified range
    private static bool IsValidInput(string? input, out int number)
    {
        return int.TryParse(input, out number) && number is >= MinValue and <= MaxValue;
    }

    // Generate an array of random numbers within the specified range
    private static int[] GenerateRandomNumbers(int count, int minValue, int maxValue)
    {
        var random = new Random();
        return Enumerable.Range(0, count)
            .Select(_ => random.Next(minValue, maxValue + 1))
            .ToArray();
    }

    // Find the numbers that match between the entered numbers and the generated random numbers
    private static List<int> FindMatchingNumbers(int[] numbers, int[] randomNumbers)
    {
        return numbers.Intersect(randomNumbers).ToList();
    }
}