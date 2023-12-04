namespace RaffleApp;

public class Ticket
{
    public List<int> Numbers { get; }
    public string UserName { get; }
    public PriceGroup PriceGroup { get; private set; }

    public Ticket(List<int> numbers, string userName)
    {// TODO: validate numbers.length = 5, username isNullOrEmptyString
        Numbers = numbers ?? throw new ArgumentNullException(nameof(numbers));
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
    }

    public void SetPriceGroup(List<int> winningNumbers)
    {
        int matchingCount = Numbers.Intersect(winningNumbers).Count();
        PriceGroup = matchingCount > 1 ? (PriceGroup)matchingCount : PriceGroup.None;
    }
}

