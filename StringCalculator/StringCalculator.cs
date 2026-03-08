using System;
using System.Text.RegularExpressions;
using StringCalculator.Exceptions;

namespace StringCalculator;

public class Calculator
{
    private string[] _defaultDelemeters = new[] { ",", "\n" };
    private const string _CustomDelimiter = "//";
    private const string _CustomDelimiterEndOfLine = "\n";

    public int Add(string numbers)
    {
        if (string.IsNullOrEmpty(numbers)) return 0;

        if (IsInputValid(numbers))
        {
            if (numbers.StartsWith(_CustomDelimiter))
            {
                int newLineIndex = numbers.IndexOf(_CustomDelimiterEndOfLine);
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
            throw new InvalidInputException($"Invalid input");
        }
    }

    /*
     *  Overloads used for SplitStringWithDelimiter for simplicity
     *  accepts both string and string[] delimiters.
     *  This could also be solved with generics, but overloads
     * are more readable for this use case.
     */
    private static string[] SplitStringWithDelimiter(string[] delimiter, string input)
    {
        string[] nums = input.Split(delimiter, StringSplitOptions.None);
        return nums;
    }

    private static string[] SplitStringWithDelimiter(string delimiter, string input)
    {
        return input.Split(new[] { delimiter }, StringSplitOptions.None);
    }


    /**
     *  Assumption: input size is reasonably small, so iterating all numbers to check
     *  for negatives is acceptable. If the list grows significantly, we can consider an
     *  early-exit approach where we return on the first negative found, and only collect
     *  all negatives if that case is hit.
     */
    private static int SumOfNumbers(string[] numbers)
    {
        var negativeNumbers = numbers.Where(x => int.Parse(x) < 0).ToList();

        if (negativeNumbers.Any())
        {
            throw new NegativeNumberException($"Negatives not allowed {string.Join(",", negativeNumbers)}");
        }

        int sum = numbers.Sum(n => int.Parse(n));
        ;

        return sum;
    }

    private bool IsInputValid(string input)
    {
        var customDelimiterPattern = @"^//(.+)\r?\n-?\d+(?:\1-?\d+)+$";
        var defaultDelimiterPattern = @"^-?\d+(?:(?:,\r?\n|,|\r?\n)-?\d+)+$";

        return Regex.IsMatch(input, defaultDelimiterPattern) || Regex.IsMatch(input, customDelimiterPattern);
    }
}