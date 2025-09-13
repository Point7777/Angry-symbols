using GameClass;

class Program
{
    static void Main()
    {
        Game NewGame = new Game();

        Console.SetWindowSize(NewGame.GetCoord('X') + 1, NewGame.GetCoord('Y') + 3); // Устанавливаем размер окна

        Console.CursorVisible = false;

        NewGame.CreateMap();
        NewGame.FlyAndDrawLines();
        NewGame.Rendering();

        while (true)
        {
            Thread.Sleep(100);

            NewGame.ClearBuffer();
            if (NewGame.Management() == 0) return;
            NewGame.CreateMap();
            NewGame.FlyAndDrawLines();
        }
    }
}