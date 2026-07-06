internal class Program
{
    private static void Main(string[] args)
    {
        using var game = new Debugger.Application.Debugger();
        game.Run();
    }
}