namespace RaffleApp
{
    public static class Constants
    {
        #region raffle configuaration
        public const double POT_SIZE = 100;
        public const double TICKET_PRICE = 5;
        public const int NUMBER_RANGE_START = 1;
        public const int NUMBER_RANGE_END = 15;
        public const int NUMBERS_IN_TICKET = 5;
        public const int MAX_TICKET_PER_USER = 5;
        public const double Group2RewardPercentage = 0.1;
        public const double Group3RewardPercentage = 0.15;
        public const double Group4RewardPercentage = 0.25;
        public const double Group5RewardPercentage = 0.5;
        #endregion

        #region output messages
        public const string WELCOME_TO_RAFFLE = "Welcome to My Raffle App";
        public const string DRAW_NOT_STARTED = "Status: Draw has not started";
        public const string DRAW_ONGOING = "Status: Draw is ongoing. Raffle pot size is ${0}";
        public const string NEW_DRAW_STARTED = "\nNew Raffle draw has been started. Initial pot size: ${0}";
        public const string START_NEW_DRAW = "\n[1] Start a New Draw";
        public const string BUY_TICKETS = "[2] Buy Tickets";
        public const string RUN_RAFFLE = "[3] Run Raffle";
        public const string PRESS_ANY_KEY_TO_RETURN = "Press any key to return to main menu";
        public const string ENTER_NAME_NO_OF_TICKETS = "Enter your name, no of tickets to purchase";
        public const string USER_PURCHASED_TICKETS = "\nHi {0}, you have purchased {1} ticket(s)";
        public const string TICKETS_DETAILS = "Ticket {0}: {1}";
        public const string RUNING_RAFFLE = "Running Raffle..";
        public const string WINNING_TICKET = "\nWinning Ticket is {0}";
        public const string GROUP_WINNERS = "\nGroup {0} Winners:";
        public const string USER_WINNING_TICKETS = "{0} with {1} winning ticket(s)- ${2}";
        public const string INVALID_INPUT = "Invalid input";
        public const string NIL = "Nil";
        public const string NUMBER_SEPARATOR = " ";
        public const string UNEPECTED_ERROR = "An unexpected error occurred: ";
        #endregion

        #region error messages
        public const string RAFFLE_HAS_NOT_STARTED = "The raffle has not started yet.";
        public const string NO_TICKET_SOLD = "No tickets have been sold yet.";
        public const string EXCEEDS_TICKET_LIMIT = "Users can purchase up to a maximum of {0} tickets per draw.";
        #endregion
    }
}

