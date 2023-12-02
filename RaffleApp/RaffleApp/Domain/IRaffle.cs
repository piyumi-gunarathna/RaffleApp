namespace RaffleApp
{
    public interface IRaffle
    {
        double Pot { get; }
        bool IsInProgress { get; }
        List<int> WinningNumbers { get; }
        List<Ticket> SoldTickets { get; }

        void StartRaffle();
        List<Ticket> SellTickets(string user, int numOfTickets);
        void Draw();
        Dictionary<PriceGroup, List<ResultRecord>> GetWinners();
    }
}

