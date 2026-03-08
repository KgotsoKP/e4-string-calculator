# e4-string-calculator

![Build](https://img.shields.io/github/actions/workflow/status/KgotsoKP/e4-string-calculator/dotnet.yml?branch=main&label=build)
![Tests](https://img.shields.io/github/actions/workflow/status/KgotsoKP/e4-string-calculator/dotnet.yml?branch=main&label=tests)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)

Test 2 : A simple String Calculator built in .NET 8. It exposes an Add(string numbers) method that parses a string of delimited numbers and returns their sum, with support for custom delimiters, mixed separators, and negative number validation.

## Demo
![e4_string_calculator_demo](https://github.com/user-attachments/assets/8ed17571-ce7e-4045-827a-13edd3c7756f)

## How It Works

The `Add` method accepts a string of numbers and returns their sum.

- An **empty string** returns `0`
- A string with **one or two numbers** returns their sum (e.g. `""` → `0`, `"1"` → `1`, `"1,2"` → `3`)
- An **unknown amount of numbers** is supported
- **New lines between numbers** are allowed in addition to commas (e.g. `"1\n2,3"` returns `6`)
- **Different delimiters** are supported — to change the delimiter, the beginning of the string should contain a separate line in the format `//[delimiter]\n[numbers...]` (e.g. `"//;\n1;2"` returns `3`). The first line is optional; all existing scenarios are still supported.
- Calling `Add` with a **negative number** throws an exception with the message `"negatives not allowed"` followed by the negative number that was passed. If there are multiple negatives, all of them are shown in the exception message.


## Getting Started

```bash
git clone https://github.com/KgotsoKP/e4-string-calculator.git
cd e4-string-calculator
dotnet restore
dotnet build
dotnet run
```

## Running Tests

```bash
dotnet test
```

## Observations

- Test 2 has a deterministic set of inputs and outputs with clearly defined rules, so TDD was a natural fit. It was easy to write tests first and build up from there.
- One interesting challenge was how \n behaves differently depending on context. In the test methods, "1\n2,3" contains an actual newline character, so the tests passed fine. But when the same input came through Console.ReadLine, the \n was read as a literal backslash and the letter n. So the tests were green, but the console had an edge case. It was a revealing insight.

