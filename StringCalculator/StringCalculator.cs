using System;
using System.Text.RegularExpressions;

namespace StringCalculator;
public class Calculator
{
    private string[] _defaultDelemeters = new[] { ",", "\n" };
    private const string _CustomDelimiter = "//";
    
    public int Add(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        if (IsInputValid(numbers))
        {
            if (numbers.StartsWith(_CustomDelimiter))
            {
                int newLineIndex = numbers.IndexOf("\n");
                var delimiter = numbers[_CustomDelimiter.Length..newLineIndex];
                string numbersSubstring = numbers.Substring(newLineIndex + 1);

                string[] result = SplitStringWithDelimiter(delimiter, numbersSubstring);
                return SumOfNumbers(result);
            }
            
            string[] nums = SplitStringWithDelimiter(_defaultDelemeters, numbers);
            return SumOfNumbers(nums);
        }
        else
        {
            throw new Exception($"Invalid input");
        }
    }

    private static string[] SplitStringWithDelimiter(string[] delimiter, string input)
    {
        string[] nums = input.Split(delimiter, StringSplitOptions.None);
        return nums;
    }
    
    private static string[] SplitStringWithDelimiter(string delimiter, string input)
    {

        string[] delimiters = new string[] { delimiter };
        string[] nums = input.Split(delimiters, StringSplitOptions.None);

        return nums;
    }

    private static int SumOfNumbers(string[] nums)
    {

        // simple app this will do.
        // if we had big number we sould move 
        var negativeNumbers = nums.Where(x => int.Parse(x) < 0).ToList();

        if (negativeNumbers.Any())
        {
            throw new Exception($"Negatives not allowed {string.Join(",", negativeNumbers)}");
        }

        int sum = 0;
        foreach (string num in nums)
        {
            sum += int.Parse(num);
        }

        return sum;
    }

    private bool IsInputValid(string input)
    {
        var customDelimiterPattern = @"^//(.+)\r?\n-?\d+(?:\1-?\d+)+$";
        var defaultDelimiterPattern = @"^-?\d+(?:(?:,\r?\n|,|\r?\n)-?\d+)+$";

        return Regex.IsMatch(input, defaultDelimiterPattern) || Regex.IsMatch(input, customDelimiterPattern);
    }
}