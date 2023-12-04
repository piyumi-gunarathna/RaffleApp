using Moq;

namespace RaffleApp.Tests;

public class RaffleAppIntegrationTest : IDisposable
{
    private StringWriter consoleOutput;
    private StringReader consoleInput;
    private readonly RaffleConsoleManager raffleConsoleManager;
    private readonly Raffle raffle;

    public RaffleAppIntegrationTest()
    {
        consoleOutput = new StringWriter();
        consoleInput = new StringReader("");
        Console.SetOut(consoleOutput);

        var ticketGeneratorMock = new Mock<ITicketNumbersGenerator>();
        ticketGeneratorMock.SetupSequence(ng => ng.GenerateUniqueNumbers())
                            .Returns(new List<int> { 4, 7, 8, 13, 14 })
                            .Returns(new List<int> { 3, 6, 9, 11, 13 })
                            .Returns(new List<int> { 3, 7, 8, 11, 14 })
                            .Returns(new List<int> { 3, 7, 9, 14, 15 })
                            .Returns(new List<int> { 4, 5, 10, 12, 15 })
                            .Returns(new List<int> { 1, 2, 7, 12, 13 })
                            .Returns(new List<int> { 3, 7, 8, 11, 12 });
        raffle = new Raffle(ticketGeneratorMock.Object);
        raffleConsoleManager = new RaffleConsoleManager(raffle);
    }

    [Fact]
    public void RaffleConsoleManager_Run_Should_PerformRaffleOptionsSuccessfully()
    {
        // Option 1: Start Draw
        consoleInput = new StringReader("1");
        Console.SetIn(consoleInput);
        raffleConsoleManager.Run();

        // Assert for Option 1
        Assert.True(raffle.IsInProgress);
        Assert.Equal(Constants.POT_SIZE, raffle.Pot);
        Assert.Empty(raffle.SoldTickets);
        Assert.Empty(raffle.WinningNumbers);
        Assert.Contains(string.Format(Constants.NEW_DRAW_STARTED, Constants.POT_SIZE), consoleOutput.ToString());

        // Option 2: Buy Tickets user1 => James,1
        consoleInput = new StringReader("2\nJames,1");
        Console.SetIn(consoleInput);
        raffleConsoleManager.Run();

        Assert.Contains(Constants.ENTER_NAME_NO_OF_TICKETS, consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.USER_PURCHASED_TICKETS, "James", 1), consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.TICKETS_DETAILS, 1, "4 7 8 13 14"), consoleOutput.ToString());


        // Option 2: Buy Tickets user2 => Ben,2
        consoleInput = new StringReader("2\nBen,2");
        Console.SetIn(consoleInput);
        raffleConsoleManager.Run();
        Assert.Contains(string.Format(Constants.USER_PURCHASED_TICKETS, "Ben", 2), consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.TICKETS_DETAILS, 1, "3 6 9 11 13"), consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.TICKETS_DETAILS, 2, "3 7 8 11 14"), consoleOutput.ToString());

        // Option 2: Buy Tickets user3 => Romeo,3
        consoleInput = new StringReader("2\nRomeo,3");
        Console.SetIn(consoleInput);
        raffleConsoleManager.Run();

        Assert.Contains(string.Format(Constants.USER_PURCHASED_TICKETS, "Romeo", 3), consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.TICKETS_DETAILS, 1, "3 7 9 14 15"), consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.TICKETS_DETAILS, 2, "4 5 10 12 15"), consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.TICKETS_DETAILS, 3, "1 2 7 12 13"), consoleOutput.ToString());

        // Option 3: Run Draw
        consoleOutput.GetStringBuilder().Clear();
        consoleInput = new StringReader("3");
        Console.SetIn(consoleInput);
        raffleConsoleManager.Run();

        // Assert for Option 3
        Assert.Contains(string.Format(Constants.WINNING_TICKET, "3 7 8 11 12"), consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.GROUP_WINNERS, 2), consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.GROUP_WINNERS, 3), consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.GROUP_WINNERS, 4), consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.GROUP_WINNERS, 5), consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.USER_WINNING_TICKETS, "James", 1, 3.25), consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.USER_WINNING_TICKETS, "Ben", 1, 3.25), consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.USER_WINNING_TICKETS, "Romeo", 2, 6.5), consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.USER_WINNING_TICKETS, "Ben", 1, 32.5), consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.USER_WINNING_TICKETS, "James", 1, 3.25), consoleOutput.ToString());
        Assert.Contains(string.Format(Constants.USER_WINNING_TICKETS, "James", 1, 3.25), consoleOutput.ToString());
    }

    public void Dispose()
    {
        consoleOutput.Dispose();
        consoleInput.Dispose();
    }
}
