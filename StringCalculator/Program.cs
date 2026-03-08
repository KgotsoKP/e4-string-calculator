using StringCalculator;

Console.WriteLine(@"
e4 Strategic | String Calculator
==================================
Instructions:
----------------------------------
- Enter a string of numbers to add them up.
- Use commas to separate: 1,2,3
- New lines also work: 1\n2,3
- Custom delimiter: //[delimiter]\n[numbers]
  Example: //;\n1;2 returns 3
- Enter q to quit.
==================================
");

Calculator calc = new Calculator();

while (true)
{
    Console.Write("\nInput: ");
    string userInput = Console.ReadLine();

    if (userInput.Trim().ToLower() == "q")
        break;

    // Console.ReadLine interprets \n as literal characters, not as a newline.
    // We need to convert the literal strings to actual escape characters
    // so the calculator processes them the same way the test inputs do.
    var normalisedInput = userInput.Replace("\\r\\n", "\r\n").Replace("\\n", "\n");

    try
    {
        var result = calc.Add(normalisedInput);
        Console.WriteLine($"Result: {result}");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}