namespace RaffleApp.Tests;

public class TicketNumbersGeneratorTest
{
    [Fact]
    public void GenerateTicketNumbers_Should_Return_Correct_Number_Of_Numbers()
    {
        // Arrange
        var ticketNumbersGenerator = new TicketNumbersGenerator();

        // Act
        List<int> ticketNumbers = ticketNumbersGenerator.GenerateUniqueNumbers();

        // Assert
        Assert.NotNull(ticketNumbers);
        Assert.Equal(Constants.NUMBERS_IN_TICKET, ticketNumbers.Count());
    }

    [Fact]
    public void GenerateTicketNumbers_Should_Return_Different_Numbers_Each_Time()
    {
        // Arrange
        var ticketNumbersGenerator = new TicketNumbersGenerator();

        // Act
        List<int> ticketNumbers1 = ticketNumbersGenerator.GenerateUniqueNumbers();
        List<int> ticketNumbers2 = ticketNumbersGenerator.GenerateUniqueNumbers();

        // Assert
        Assert.NotEqual(ticketNumbers1, ticketNumbers2);
    }

    [Fact]
    public void GenerateTicketNumbers_Should_Return_Unique_Numbers_Each_Time()
    {
        // Arrange
        var ticketNumbersGenerator = new TicketNumbersGenerator();

        // Act
        List<int> ticketNumbers = ticketNumbersGenerator.GenerateUniqueNumbers();

        // Assert
        Assert.NotNull(ticketNumbers);
        Assert.Equal(Constants.NUMBERS_IN_TICKET, ticketNumbers.Count);
        Assert.True(ticketNumbers.Distinct().Count() == ticketNumbers.Count);
    }
}
