namespace RaffleApp
{
    public class RaffleConsoleManager
    {
        private readonly IRaffle raffle;

        public RaffleConsoleManager(IRaffle raffleDraw)
        {
            raffle = raffleDraw;
        }

        public void Run()
        {
            int option;
            DisplayMenu();
            int.TryParse(GetUserInput(), out option);
            ProcessOption(option);
        }

        public void ProcessOption(int option)
        {
            switch (option)
            {
                case 1:
                    StartNewDraw();
                    break;
                case 2:
                    BuyTickets();
                    break;
                case 3:
                    RunRaffle();
                    break;
                default:
                    Console.WriteLine(Constants.INVALID_INPUT);
                    break;
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine(Constants.WELCOME_TO_RAFFLE);
            Console.WriteLine(!raffle.IsInProgress ? Constants.DRAW_NOT_STARTED : Constants.DRAW_ONGOING, raffle.Pot);
            Console.WriteLine(Constants.START_NEW_DRAW);
            Console.WriteLine(Constants.BUY_TICKETS);
            Console.WriteLine(Constants.RUN_RAFFLE);
            Console.WriteLine();
        }

        private string GetUserInput()
        {
            string input = Console.ReadLine();
            return input;
        }

        private void StartNewDraw()
        {
            raffle.StartRaffle();
            Console.WriteLine(Constants.NEW_DRAW_STARTED, raffle.Pot);
            PressToContinue();
        }

        private void PressToContinue()
        {
            Console.WriteLine(Constants.PRESS_ANY_KEY_TO_RETURN);
            Console.ReadLine();
        }

        private void BuyTickets()
        {
            if (!raffle.IsInProgress)
            {
                Console.WriteLine(Constants.RAFFLE_HAS_NOT_STARTED);
                return;
            }

            Console.WriteLine(Constants.ENTER_NAME_NO_OF_TICKETS);
            string input = GetUserInput();
            string[] data = input.Split(',');

            if (data.Length != 2 || !int.TryParse(data[1], out int numOfTickets))
            {
                Console.WriteLine(Constants.INVALID_INPUT);
                return;
            }

            numOfTickets = Math.Min(numOfTickets, Constants.MAX_TICKET_PER_USER);
            string name = data[0];
            List<Ticket> tickets = raffle.SellTickets(name, numOfTickets);
            Console.WriteLine(Constants.USER_PURCHASED_TICKETS, name, numOfTickets);
            DisplayTickets(tickets);
            PressToContinue();
        } 

        private void DisplayTickets(List<Ticket> tickets)
        {
            for (int i = 0; i < tickets.Count; i++)
            {
                Console.WriteLine(Constants.TICKETS_DETAILS, i + 1, string.Join(Constants.NUMBER_SEPARATOR, tickets[i].Numbers));
            }
        }

        private void RunRaffle()
        {
            if (!raffle.IsInProgress)
            {
                Console.WriteLine(Constants.RAFFLE_HAS_NOT_STARTED);
                return;
            }
            else if (raffle.SoldTickets.Count == 0)
            {
                Console.WriteLine(Constants.RAFFLE_HAS_NOT_STARTED);
                return;
            }

            raffle.Draw();
            DisplayWinningNumbers(raffle.WinningNumbers);
            var winners = raffle.GetWinners();
            DisplayGroupWinningInformation(winners);
            PressToContinue();
        }

        private void DisplayWinningNumbers(List<int> winningNumbers)
        {
            Console.WriteLine(Constants.WINNING_TICKET, string.Join(Constants.NUMBER_SEPARATOR, winningNumbers));
        }

        private void DisplayGroupWinningInformation(Dictionary<PriceGroup, List<ResultRecord>> groupWinners)
        {
            foreach (PriceGroup PriceGroup in Enum.GetValues(typeof(PriceGroup)))
            {
                if (PriceGroup == PriceGroup.None)
                    continue;

                Console.WriteLine(Constants.GROUP_WINNERS, PriceGroup);
                if (groupWinners.ContainsKey(PriceGroup))
                    DisplayWinningUsers(groupWinners[PriceGroup]);
                else
                    Console.WriteLine(Constants.NIL);
            }

        }

        private void DisplayWinningUsers(List<ResultRecord> winners)
        {
            foreach (var winner in winners)
                Console.WriteLine(Constants.USER_WINNING_TICKETS, winner.Name, winner.numberOfTickets, winner.amountWon);
        }
    }

}

