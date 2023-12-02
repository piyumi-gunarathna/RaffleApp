using Microsoft.Extensions.DependencyInjection;
using RaffleApp;

IServiceCollection services = new ServiceCollection();
services.AddSingleton<ITicketNumbersGenerator, TicketNumbersGenerator>();
services.AddSingleton<IRaffle, Raffle>();
services.AddSingleton< RaffleConsoleManager>();

IServiceProvider serviceProvider = services.BuildServiceProvider();
RaffleConsoleManager raffleConsoleManager = serviceProvider.GetRequiredService<RaffleConsoleManager>();


while (true)
{
    try
    {
        raffleConsoleManager.Run();
    }
    catch (Exception ex)
    {
        Console.WriteLine(Constants.UNEPECTED_ERROR + ex.Message);
    }
}