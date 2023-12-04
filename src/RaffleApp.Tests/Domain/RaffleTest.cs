using Moq;

namespace RaffleApp.Tests;

public class RaffleTest
{
    [Fact]
    public void StartRaffle_Should_SetIsInProgressAndInitializePot()
    {
        // Arrange
        var numberGeneratorMock = new Mock<ITicketNumbersGenerator>();
        var raffle = new Raffle(numberGeneratorMock.Object);

        // Act
        raffle.StartRaffle();

        // Assert
        Assert.True(raffle.IsInProgress);
        Assert.Equal(Constants.POT_SIZE, raffle.Pot);
        Assert.Empty(raffle.SoldTickets);
        Assert.Empty(raffle.WinningNumbers);
    }

    [Fact]
    public void SellTickets_Should_IncreasePotAndReturnPurchasedTickets_When_RaffleInProgress()
    {
        // Arrange
        var numberGeneratorMock = new Mock<ITicketNumbersGenerator>();
        numberGeneratorMock.SetupSequence(ng => ng.GenerateTicketNumbers())
                           .Returns(new List<int> { 4, 7, 8, 13, 14 })
                           .Returns(new List<int> { 3, 6, 9, 11, 13 })
                           .Returns(new List<int> { 2, 5, 10, 9, 14 });
        var raffle = new Raffle(numberGeneratorMock.Object);
        raffle.StartRaffle();

        const int numOfTicketsToSell = 3;
        const string userName = "John";
        const double expectedPot = Constants.POT_SIZE + (Constants.TICKET_PRICE * numOfTicketsToSell);

        // Act
        var purchasedTickets = raffle.SellTickets(userName, numOfTicketsToSell);

        // Assert
        Assert.Equal(expectedPot, raffle.Pot);
        Assert.Equal(numOfTicketsToSell, purchasedTickets.Count);
        foreach (var ticket in purchasedTickets)
        {
            Assert.Equal(userName, ticket.UserName);
            Assert.Equal(Constants.NUMBERS_IN_TICKET, ticket.Numbers.Count());
        }
    }

    [Fact]
    public void Draw_Should_GenerateWinningNumbers_When_RaffleInProgressAndTicketsSold()
    {
        // Arrange
        var numberGeneratorMock = new Mock<ITicketNumbersGenerator>();
        numberGeneratorMock.SetupSequence(ng => ng.GenerateTicketNumbers())
                           .Returns(new List<int> { 4, 7, 8, 13, 14 })
                           .Returns(new List<int> { 3, 6, 9, 11, 13 });

        var raffle = new Raffle(numberGeneratorMock.Object);
        raffle.StartRaffle();
        raffle.SellTickets("James", 1);

        // Act
        raffle.Draw();

        // Assert
        Assert.False(raffle.IsInProgress);
        Assert.Equal(5, raffle.WinningNumbers.Count);
    }

    [Fact]
    public void Draw_Should_GenerateWinnersWithReward_When_DrawCompleted()
    {
        // Arrange
        var numberGeneratorMock = new Mock<ITicketNumbersGenerator>();
        numberGeneratorMock.SetupSequence(ng => ng.GenerateTicketNumbers())
                           .Returns(new List<int> { 4, 7, 8, 13, 14 })
                           .Returns(new List<int> { 3, 6, 9, 11, 13 })
                           .Returns(new List<int> { 3, 7, 8, 11, 14 })
                           .Returns(new List<int> { 3, 7, 9, 14, 15 })
                           .Returns(new List<int> { 4, 5, 10, 12, 15 })
                           .Returns(new List<int> { 1, 2, 7, 12, 13 })
                           .Returns(new List<int> { 3, 7, 8, 11, 12 });

        string user1 = "James";
        string user2 = "Ben";
        string user3 = "Romeo";

        var raffle = new Raffle(numberGeneratorMock.Object);
        raffle.StartRaffle();
        raffle.SellTickets(user1, 1);
        raffle.SellTickets(user2, 2);
        raffle.SellTickets(user3, 3);
        raffle.Draw();

        // Act
        var winners = raffle.GetWinners();

        // Assert
        Assert.True(winners.Count == 2);
        Assert.True(winners[PriceGroup.Group2].Count == 3);
        Assert.True(winners[PriceGroup.Group4].Count == 1);
        Assert.Equal(1, winners[PriceGroup.Group2].First(w => w.Name == user1).numberOfTickets);
        Assert.Equal(1, winners[PriceGroup.Group2].First(w => w.Name == user2).numberOfTickets);
        Assert.Equal(2, winners[PriceGroup.Group2].First(w => w.Name == user3).numberOfTickets);
        Assert.Equal(1, winners[PriceGroup.Group4].First(w => w.Name == user2).numberOfTickets);
        Assert.Equal(3.25, winners[PriceGroup.Group2].First(w => w.Name == user1).amountWon);
        Assert.Equal(3.25, winners[PriceGroup.Group2].First(w => w.Name == user2).amountWon);
        Assert.Equal(6.5, winners[PriceGroup.Group2].First(w => w.Name == user3).amountWon);
        Assert.Equal(32.5, winners[PriceGroup.Group4].First(w => w.Name == user2).amountWon);
    }

    [Fact]
    public void Draw_Should_SetRemainingFundsToUseInNextDraw_When_DrawCompleted()
    {
        // Arrange
        var numberGeneratorMock = new Mock<ITicketNumbersGenerator>();
        numberGeneratorMock.SetupSequence(ng => ng.GenerateTicketNumbers())
                           .Returns(new List<int> { 4, 7, 8, 13, 14 })
                           .Returns(new List<int> { 1, 9, 2, 15, 5 })
                           .Returns(new List<int> { 4, 7, 8, 13, 14 });

        var raffle = new Raffle(numberGeneratorMock.Object);
        raffle.StartRaffle();
        raffle.SellTickets("James", 2);

        // Act
        raffle.Draw();

        // Assert
        Assert.True(raffle.RemainingFunds == 55);

    }
}
