using System.Collections.Generic;

namespace RaffleApp;

public class Raffle
{
    public double Pot { get; private set; }
    public double RemainingFunds { get; private set; }
    public bool IsInProgress { get; private set; }
    public List<Ticket> SoldTickets { get; private set; }
    public List<int> WinningNumbers { get; private set; }

    private readonly ITicketNumbersGenerator ticketNumbersGenerator;
    private readonly Dictionary<PriceGroup, double> rewardPercentages = new Dictionary<PriceGroup, double>
    {
        { PriceGroup.Group2, Constants.Group2RewardPercentage },
        { PriceGroup.Group3, Constants.Group3RewardPercentage },
        { PriceGroup.Group4, Constants.Group4RewardPercentage },
        { PriceGroup.Group5, Constants.Group5RewardPercentage }
    };

    public Raffle(ITicketNumbersGenerator numberGenerator)
    {
        SoldTickets = new List<Ticket>();
        WinningNumbers = new List<int>();
        ticketNumbersGenerator = numberGenerator;
        Pot = Constants.POT_SIZE;
    }

    public void StartRaffle()
    {
        IsInProgress = true;
        Pot = Constants.POT_SIZE + RemainingFunds;
        SoldTickets.Clear();
        WinningNumbers.Clear();
    }

    public List<Ticket> SellTickets(string user, int numOfTickets)
    {
        if (!IsInProgress)
            throw new RaffleException(Constants.RAFFLE_HAS_NOT_STARTED);

        if (numOfTickets > Constants.MAX_TICKET_PER_USER)
            throw new RaffleException(string.Format(Constants.EXCEEDS_TICKET_LIMIT, Constants.MAX_TICKET_PER_USER));

        Pot += Constants.TICKET_PRICE * numOfTickets;
        var purchasedTickets = GenerateTickets(numOfTickets, user);

        SoldTickets.AddRange(purchasedTickets);
        return purchasedTickets;
    }

    public void Draw()
    {
        if (!IsInProgress)
            throw new RaffleException(Constants.RAFFLE_HAS_NOT_STARTED);

        if (SoldTickets.Count == 0)
        {
            throw new RaffleException(Constants.NO_TICKET_SOLD);
        }
        WinningNumbers = ticketNumbersGenerator.GenerateTicketNumbers();
        SoldTickets.ForEach(ticket => ticket.SetPriceGroup(WinningNumbers));
        SetRemainingFunds();
        IsInProgress = false;
    }

    public Dictionary<PriceGroup, List<ResultRecord>> GetWinners()
    {
        var groupedWinners = SoldTickets
            .Where(ticket => ticket.PriceGroup != PriceGroup.None)
            .GroupBy(ticket => ticket.PriceGroup)
            .OrderByDescending(group => group.Key)
            .ToDictionary(
                group => group.Key,
                group =>
                {
                    double reward = CalculateReward(group.Key, group.Count());
                    return group.GroupBy(item => item.UserName)
                                .Select(userGroup => new ResultRecord(userGroup.Key, userGroup.Count(), userGroup.Count() * reward))
                                .ToList();
                }
            );

        return groupedWinners;
    }

    private void SetRemainingFunds()
    {
        var winningGroups = SoldTickets
            .Where(ticket => ticket.PriceGroup != PriceGroup.None)
            .Select(ticket => ticket.PriceGroup)
            .Distinct().ToList();

        double totalWinningPercentage = 0;
        foreach (PriceGroup group in winningGroups)
        {
            totalWinningPercentage += rewardPercentages[group];
        }

        RemainingFunds = Pot - Pot * totalWinningPercentage;
    }

    private List<Ticket> GenerateTickets(int numOfTickets, string user)
    {
        var tickets = new List<Ticket>();
        for (int i = 0; i < numOfTickets; i++)
            tickets.Add(new Ticket(ticketNumbersGenerator.GenerateTicketNumbers(), user));

        return tickets;
    }

    private double CalculateReward(PriceGroup PriceGroup, int noOfWinners)
    {
        if (noOfWinners == 0)
            return 0;

        double reward = Pot * rewardPercentages[PriceGroup] / noOfWinners;
        return reward;
    }
}

public record ResultRecord(string Name, int numberOfTickets, double amountWon);

