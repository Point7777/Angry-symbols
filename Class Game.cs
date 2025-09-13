namespace GameClass
{
    class Game
    {
        private const int mY = 24, mX = 120; // Ширина и длина карты
        private char[,] map = new char[mY, mX]; // Карта

        private int pY = 15, pX = 13; // Координаты игрока

        private bool flyKey = false;
        private float Degree = 0;
        private const int MaxLine = 43; // Максимальный размер линии

        private int NumberBlock = new Random().Next(1, 4); // Номер висячей груши которая должна быть правильной
        private int score = 0;
        private int MaxScore = 0;

        public Game() { }

        public int GetCoord(char a)
        {
            switch (a)
            {
                case 'Y': return mY;
                case 'X': return mX;
            }
            return 0;
        }

        public void Rendering()
        {
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < mY; i++)
            {
                string str = "";

                for (int c = 0; c < mX; c++)
                {
                    if (i == pY && c == pX) str += 'O';
                    else str += map[i, c];
                }
                Console.WriteLine(str);
            }

            Console.WriteLine("SCORE: " + score + "                                     ");
            Console.Write("MAX SCORE: " + MaxScore + "                                     ");
        }

        public void FlyAndDrawLines()
        {
            int key = 0;
            int delay = 13; // Задержка

            while (flyKey) // Полёт
            {
                Thread.Sleep(delay);

                if (Degree > 0) // Y вверх
                {
                    if (key == 0)
                    {
                        pY--;
                        key++;
                    }
                    else
                    {
                        if (key == 2) key = 0;
                        else key++;
                    }
                }
                else if (Degree < 0) // Y вниз
                {
                    if (key == 0)
                    {
                        pY++;
                        key++;
                    }
                    else
                    {
                        if (key == 2) key = 0;
                        else key++;
                    }
                }

                if ((Degree < 5 && Degree > 0) || (Degree > -3 && Degree < 0)) // 4 блока полёта по прямой
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (map[pY, pX + 1] == ' ' || map[pY, pX + 1] == '/' || map[pY, pX + 1] == '-') pX++;
                        else
                        {
                            if (map[pY, pX + 1] == '$')
                            {
                                score++;

                                if (score > MaxScore) MaxScore = score;
                            }
                            else score = 0;

                            flyKey = false;
                            pY = 15; pX = 13;
                            Degree = 0.5f;

                            NumberBlock = new Random().Next(1, 4);
                            CreateMap();

                            break;
                        }

                        Rendering();
                        Thread.Sleep(delay);
                    }
                }
                else if ((Degree < 10 && Degree > 0) || (Degree > -7 && Degree < 0)) // 2 блока полёта по прямой
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (map[pY, pX + 1] == ' ' || map[pY, pX + 1] == '/' || map[pY, pX + 1] == '-') pX++;
                        else
                        {
                            if (map[pY, pX + 1] == '$')
                            {
                                score++;

                                if (score > MaxScore) MaxScore = score;
                            }
                            else score = 0;

                            flyKey = false;
                            pY = 15; pX = 13;
                            Degree = 0.5f;

                            NumberBlock = new Random().Next(1, 4);
                            CreateMap();

                            break;
                        }

                        Rendering();
                        Thread.Sleep(delay);
                    }
                }
                else
                {
                    if (map[pY, pX + 1] == ' ' || map[pY, pX + 1] == '/' || map[pY, pX + 1] == '-') pX++;
                    else
                    {
                        if (map[pY, pX + 1] == '$')
                        {
                            score++;

                            if (score > MaxScore) MaxScore = score;
                        }
                        else score = 0;

                        flyKey = false;
                        pY = 15; pX = 13;
                        Degree = 0.5f;

                        NumberBlock = new Random().Next(1, 4);
                        CreateMap();
                    }

                    Rendering();
                }

                Degree -= 0.5f;
            }

            float dDegree = Degree;
            int dpX = pX, dpY = pY;

            int draw = 1;
            int drawKey = 0;
            key = 0;

            int NewMaxLine = 0;
            bool lineStop = false;

            while (!flyKey && lineStop == false) // Линия
            {
                if (dDegree > 0) // Y
                {
                    if (key == 0)
                    {
                        dpY--;
                        key++;
                    }
                    else
                    {
                        if (key == 2) key = 0;
                        else key++;
                    }
                }
                else if (dDegree < 0) // Y
                {
                    if (key == 0)
                    {
                        dpY++;
                        key++;
                    }
                    else
                    {
                        if (key == 2) key = 0;
                        else key++;
                    }
                }

                if ((dDegree < 5 && dDegree > 0) || (dDegree > -3 && dDegree < 0)) // 4 блока линии по прямой
                {
                    for (int i = 0; i < 4; i++)
                    {
                        dpX++;

                        if (dpX == 30) drawKey = 1;
                        else if (dpX == 60) drawKey = 2;
                        else if (dpX == 80) drawKey = 3;

                        if (dpX < mX && dpY < mY)
                        {
                            if (map[dpY, dpX] == ' ')
                            {
                                switch (drawKey)
                                {
                                    case 0:
                                        if ((NewMaxLine <= MaxLine - score) || (MaxLine - score <= 14 && NewMaxLine < 14))
                                        {
                                            map[dpY, dpX] = '.';
                                            NewMaxLine++;
                                        }
                                        else lineStop = true;

                                        break;

                                    case 1:
                                        if (draw == 1)
                                        {
                                            if ((NewMaxLine <= MaxLine - score) || (MaxLine - score <= 14 && NewMaxLine < 14))
                                            {
                                                map[dpY, dpX] = '.';
                                                NewMaxLine++;
                                            }
                                            else lineStop = true;

                                            draw = 0;
                                        }
                                        else draw = 1;

                                        break;

                                    case 2:
                                        if (draw == 1)
                                        {
                                            if ((NewMaxLine <= MaxLine - score) || (MaxLine - score <= 14 && NewMaxLine < 14))
                                            {
                                                map[dpY, dpX] = '.';
                                                NewMaxLine++;
                                            }
                                            else lineStop = true;

                                            draw++;
                                        }
                                        else
                                        {
                                            if (draw == 3) draw = 1;
                                            else draw++;
                                        }
                                        break;

                                    case 3:
                                        if (draw == 1)
                                        {
                                            if ((NewMaxLine <= MaxLine - score) || (MaxLine - score <= 14 && NewMaxLine < 14))
                                            {
                                                map[dpY, dpX] = '.';
                                                NewMaxLine++;
                                            }
                                            else lineStop = true;

                                            draw++;
                                        }
                                        else
                                        {
                                            if (draw == 4) draw = 1;
                                            else draw++;
                                        }
                                        break;
                                }
                            }
                        }
                        else break;
                    }
                }
                else if ((dDegree < 10 && dDegree > 0) || (dDegree > -7 && dDegree < 0)) // 2 блока линии по прямой
                {
                    for (int i = 0; i < 2; i++)
                    {
                        dpX++;

                        if (dpX == 30) drawKey = 1;
                        else if (dpX == 60) drawKey = 2;
                        else if (dpX == 80) drawKey = 3;

                        if (dpX < mX && dpY < mY)
                        {
                            if (map[dpY, dpX] == ' ')
                            {
                                switch (drawKey)
                                {
                                    case 0:
                                        if ((NewMaxLine <= MaxLine - score) || (MaxLine - score <= 14 && NewMaxLine < 14))
                                        {
                                            map[dpY, dpX] = '.';
                                            NewMaxLine++;
                                        }
                                        else lineStop = true;

                                        break;

                                    case 1:
                                        if (draw == 1)
                                        {
                                            if ((NewMaxLine <= MaxLine - score) || (MaxLine - score <= 14 && NewMaxLine < 14))
                                            {
                                                map[dpY, dpX] = '.';
                                                NewMaxLine++;
                                            }
                                            else lineStop = true;

                                            draw = 0;
                                        }
                                        else draw = 1;
                                        break;

                                    case 2:
                                        if (draw == 1)
                                        {
                                            if ((NewMaxLine <= MaxLine - score) || (MaxLine - score <= 14 && NewMaxLine < 14))
                                            {
                                                map[dpY, dpX] = '.';
                                                NewMaxLine++;
                                            }
                                            else lineStop = true;

                                            draw++;
                                        }
                                        else
                                        {
                                            if (draw == 3) draw = 1;
                                            else draw++;
                                        }
                                        break;

                                    case 3:
                                        if (draw == 1)
                                        {
                                            if ((NewMaxLine <= MaxLine - score) || (MaxLine - score <= 14 && NewMaxLine < 14))
                                            {
                                                map[dpY, dpX] = '.';
                                                NewMaxLine++;
                                            }
                                            else lineStop = true;

                                            draw++;
                                        }
                                        else
                                        {
                                            if (draw == 4) draw = 1;
                                            else draw++;
                                        }
                                        break;
                                }
                            }
                        }
                        else break;
                    }
                }
                else
                {
                    dpX++;

                    if (dpX == 30) drawKey = 1;
                    else if (dpX == 60) drawKey = 2;
                    else if (dpX == 80) drawKey = 3;

                    if (dpX < mX && dpY < mY)
                    {
                        if (map[dpY, dpX] == ' ')
                        {
                            switch (drawKey)
                            {
                                case 0:
                                    if ((NewMaxLine <= MaxLine - score) || (MaxLine - score <= 14 && NewMaxLine < 14))
                                    {
                                        map[dpY, dpX] = '.';
                                        NewMaxLine++;
                                    }
                                    else lineStop = true;

                                    break;

                                case 1:
                                    if (draw == 1)
                                    {
                                        if ((NewMaxLine <= MaxLine - score) || (MaxLine - score <= 14 && NewMaxLine < 14))
                                        {
                                            map[dpY, dpX] = '.';
                                            NewMaxLine++;
                                        }
                                        else lineStop = true;

                                        draw = 0;
                                    }
                                    else draw = 1;
                                    break;

                                case 2:
                                    if (draw == 1)
                                    {
                                        if ((NewMaxLine <= MaxLine - score) || (MaxLine - score <= 14 && NewMaxLine < 14))
                                        {
                                            map[dpY, dpX] = '.';
                                            NewMaxLine++;
                                        }
                                        else lineStop = true;

                                        draw++;
                                    }
                                    else
                                    {
                                        if (draw == 3) draw = 1;
                                        else draw++;
                                    }
                                    break;

                                case 3:
                                    if (draw == 1)
                                    {
                                        if ((NewMaxLine <= MaxLine - score) || (MaxLine - score <= 14 && NewMaxLine < 14))
                                        {
                                            map[dpY, dpX] = '.';
                                            NewMaxLine++;
                                        }
                                        else lineStop = true;

                                        draw++;
                                    }
                                    else
                                    {
                                        if (draw == 4) draw = 1;
                                        else draw++;
                                    }
                                    break;
                            }
                        }
                    }
                    else break;
                }

                dDegree -= 0.5f;
            }

            Rendering();
        }

        public void CreateMap()
        {
            string[] lines;

            try
            {
                lines = File.ReadAllLines("Map.txt");
            }
            catch(FormatException ex)
            {
                Console.Clear();
                Console.WriteLine($"ERROR READ MAP: {ex}");
                Thread.Sleep(5000);

                return;
            }

            for (int i = 0; i < mY; i++)
            {
                if (i < lines.Length)
                {
                    for (int c = 0; c < mX; c++)
                    {
                        if (c < lines[i].Length)
                        {
                            map[i, c] = lines[i][c];
                        }
                        else
                        {
                            map[i, c] = ' '; // Заполняем пробелами, если строка короче
                        }
                    }
                }
                else
                {
                    for (int c = 0; c < mX; c++)
                    {
                        map[i, c] = ' '; // Заполняем пробелами, если строка короче
                    }
                }
            }

            int n = 0;

            switch (NumberBlock)
            {
                case 1:
                    n = 2;

                    break;

                case 2:
                    n = 8;

                    break;

                case 3:
                    n = 14;

                    break;
            }

            for (int i = n; i < n + 3; i++)
            {
                for (int c = 106; c < 111; c++)
                {
                    map[i, c] = '$';
                }
            }
        }

        public int Management()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow: // Вверх
                case ConsoleKey.W:
                    if (Degree < 21) Degree++;

                    break;

                case ConsoleKey.DownArrow: // Вниз
                case ConsoleKey.S:
                    if (Degree > -7) Degree--;

                    break;

                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                    flyKey = true;

                    break;

                case ConsoleKey.Escape:
                    return 0;
            }

            return 1;
        }

        public void ClearBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }
    }
}
