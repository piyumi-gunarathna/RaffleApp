using System;
using Microsoft.VisualBasic;

namespace RaffleApp
{
    public class TicketNumbersGenerator : ITicketNumbersGenerator
    {
        private readonly Random random = new Random();

        public List<int> GenerateTicketNumbers()
        {
            HashSet<int> uniqueNumbers = new HashSet<int>();
            while (uniqueNumbers.Count < Constants.NUMBERS_IN_TICKET)
                uniqueNumbers.Add(GetRandomNumber());
            return uniqueNumbers.ToList();
        }

        private int GetRandomNumber()
        {
            return random.Next(Constants.NUMBER_RANGE_START, Constants.NUMBER_RANGE_END + 1);
        }
    }
}

