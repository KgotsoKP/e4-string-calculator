using JetBrains.Annotations;
using StringCalculator.Exceptions;

namespace StringCalculator.Tests;

/*
 * No Arrange-Act-Assert pattern used since the calculator method has a single method, simple inputs, no dependencies.
 */

[TestSubject(typeof(Calculator))]
public class StringCalculatorTest
{
    private readonly Calculator _calculator = new();
    
    [Fact]
    public void Should_Return_Zero_On_Empty_String() =>  Assert.Equal(0, _calculator.Add(string.Empty));

    [Fact]
    public void Should_Return_Sum_Using_Comma_Separator() => Assert.Equal(6, _calculator.Add("1,2,3"));

    [Fact]
    public void Should_Return_Sum_Using_Newline_Separator() => Assert.Equal(10, _calculator.Add("1\n2\n3\n4"));

    [Fact]
    public void Should_Return_Sum_Using_Comma_And_Newline_Separator() => Assert.Equal(6, _calculator.Add("1\n2,3"));
 
    [Fact]
    public void Should_Return_Sum_Using_Colounm_Delemeter() => Assert.Equal(6, _calculator.Add("//;\n1;2;3"));
    
    [Fact]
    public void Should_Throw_Negative_Exception_When_Input_Contains_Negative_Numbers()
    {
        string input = "1,-4,-7";
        
        var ex = Assert.Throws<NegativeNumberException>(() => _calculator.Add(input));
        Assert.Contains("-4", ex.Message);
        Assert.Contains("-7", ex.Message);
    }
    
    [Theory]
    [InlineData("abc")]              
    [InlineData("1;2")]              
    [InlineData("1, 2")]
    [InlineData("//;\n1")]
    public void Should_Throw_Invalid_Input_Exception_On_Invlid_Input(string input)
    {
        var ex = Assert.Throws<InvalidInputException>(() => _calculator.Add(input));
        Assert.Equal("Invalid input", ex.Message);
    }
}