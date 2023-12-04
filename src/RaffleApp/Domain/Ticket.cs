namespace RaffleApp;

public class Ticket
{
    public List<int> Numbers { get; }
    public string UserName { get; }
    public PriceGroup PriceGroup { get; private set; }

    public Ticket(List<int> numbers, string userName)
    {
        if (numbers == null)
            throw new ArgumentNullException(nameof(numbers));
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentNullException(nameof(userName));
        if (numbers.Count != Constants.NUMBERS_IN_TICKET)
            throw new RaffleException(Constants.INVALID_TICKET_NUM_COUNT);

        Numbers = numbers;
        UserName = userName;
    }

    public void SetPriceGroup(List<int> winningNumbers)
    {
        int matchingCount = Numbers.Intersect(winningNumbers).Count();
        PriceGroup = matchingCount > 1 ? (PriceGroup)matchingCount : PriceGroup.None;
    }
}

