using JetBrains.Annotations;
using StringCalculator.Exceptions;

namespace StringCalculator.Tests;

[TestSubject(typeof(Calculator))]
public class StringCalculatorTest
{

    private static readonly Calculator calculator = new();
    
    [Fact]
    public void Should_Return_Zero_On_Empty_String()
    {
        Assert.Equal(0, calculator.Add(string.Empty));
    }
    
    [Fact]
    public void Should_Return_Sum_Using_Comma_Separator()
    {
        Assert.Equal(6, calculator.Add("1,2,3"));
    }
    
    [Fact]
    public void Should_Return_Sum_Using_Newline_Separator()
    {
        Assert.Equal(10, calculator.Add("1\n2\n3\n4"));
    }
    
    [Fact]
    public void Should_Return_Sum_Using_Comma_And_Newline_Separator()
    {
        Assert.Equal(6, calculator.Add("1\n2,3"));
    }
    
    [Fact]
    public void Should_Return_Sum_Using_Colounm_Delemeter()
    {
        string input = "//;\n1;2;3";
        
        int result = calculator.Add(input);
        
        Assert.Equal(6, result);
    }
    
    [Fact]
    public void Should_Throw_Negative_Exception_When_Input_Contains_Negative_Numbers()
    {
        string input = "1,-4,-7";
        
        var ex = Assert.Throws<NegativeNumberException>(() => calculator.Add(input));
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
        var ex = Assert.Throws<InvalidInputException>(() => calculator.Add(input));

        Assert.Equal("Invalid input", ex.Message);
    }
}