using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Az_elsőjáték_a_világon_2_0
{
    class Program
    {
        static int EgyesjatekosPadSize = 20;
        static int KettesjatekosPadSize = 10;
        static int LabdaPositionX = 0;
        static int LabdaPositionY = 0;
        static bool LabdaDirectionUp = true; // megvizsgálja azt hogy a labda fent van-e
        static bool LabdaDirectionRight = false;
        static int EgyesjatekosPosition = 0;
        static int KettesjatekosPosition = 0;
        static int EgyesjatekosResult = 0;
        static int KettesjatekosResult = 0;
        static Random randomGenerator = new Random();

        static void RemoveScrollBars()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }

        static void Elsojatekosmegalkotasa()
        {
            for (int y = EgyesjatekosPosition; y < EgyesjatekosPosition + EgyesjatekosPadSize; y++)
            {
                PrintAtPosition(0, y, '|');
                PrintAtPosition(1, y, '|');
            }
        }

        static void PrintAtPosition(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        static void Kettesjatekosmegalkotasa()
        {
            for (int y = KettesjatekosPosition; y < KettesjatekosPosition + KettesjatekosPadSize; y++)
            {
                PrintAtPosition(Console.WindowWidth - 1, y, '|');
                PrintAtPosition(Console.WindowWidth - 2, y, '|');
            }
        }

        static void alapPositions()
        {
            EgyesjatekosPosition = Console.WindowHeight / 2 - EgyesjatekosPadSize / 2;
            KettesjatekosPosition = Console.WindowHeight / 2 - KettesjatekosPadSize / 2;
            Labdalegyenközépen();
        }

        static void Labdalegyenközépen()
        {
            LabdaPositionX = Console.WindowWidth / 2;
            LabdaPositionY = Console.WindowHeight / 2;
        }

        static void Labdamegalkotása()
        {
            PrintAtPosition(LabdaPositionX, LabdaPositionY, '@');
        }

        static void eredmeny()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 1, 0);
            Console.Write("{0}-{1}", EgyesjatekosResult, KettesjatekosResult);
        }

        static void MoveElsojatekosLE()
        {
            if (EgyesjatekosPosition < Console.WindowHeight - EgyesjatekosPadSize)
            {
                EgyesjatekosPosition++;
            }
        }

        static void MoveElsojatekosFEL()
        {
            if (EgyesjatekosPosition > 0)
            {
                EgyesjatekosPosition--;
            }
        }

        static void MoveKettesjatekosLE()
        {
            if (KettesjatekosPosition < Console.WindowHeight - KettesjatekosPadSize)
            {
                KettesjatekosPosition++;
            }
        }

        static void MoveKettesjatekosFEL()
        {
            if (KettesjatekosPosition > 0)
            {
                KettesjatekosPosition--;
            }
        }

        static void SecondPlayerAIMove()
        {
            int randomNumber = randomGenerator.Next(1, 101);

            if (randomNumber <= 70)
            {
                if (LabdaDirectionUp == true)
                {
                    MoveKettesjatekosFEL();
                }
                else
                {
                    MoveKettesjatekosLE();
                }
            }
        }

        private static void Labdamozgatas()
        {
            if (LabdaPositionY == 0)
            {
                LabdaDirectionUp = false;
            }
            if (LabdaPositionY == Console.WindowHeight - 1)
            {
                LabdaDirectionUp = true;
            }
            if (LabdaPositionX == Console.WindowWidth - 1)
            {
                Labdalegyenközépen();
                LabdaDirectionRight = false;
                LabdaDirectionUp = true;
                EgyesjatekosResult++;
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.WriteLine("Első játékos nyert!");
                Console.ReadKey();
            }
            if (LabdaPositionX == 0)
            {
                Labdalegyenközépen();
                LabdaDirectionRight = true;
                LabdaDirectionUp = true;
                KettesjatekosResult++;
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.WriteLine("Második játékos nyert!");
                Console.ReadKey();
            }

            if (LabdaPositionX < 3)
            {
                if (LabdaPositionY >= EgyesjatekosPosition
                    && LabdaPositionY < EgyesjatekosPosition + EgyesjatekosPadSize)
                {
                    LabdaDirectionRight = true;
                }
            }

            if (LabdaPositionX >= Console.WindowWidth - 3 - 1)
            {
                if (LabdaPositionY >= KettesjatekosPosition
                    && LabdaPositionY < KettesjatekosPosition + KettesjatekosPadSize)
                {
                    LabdaDirectionRight = false;
                }
            }

            if (LabdaDirectionUp)
            {
                LabdaPositionY--;
            }
            else
            {
                LabdaPositionY++;
            }


            if (LabdaDirectionRight)
            {
                LabdaPositionX++;
            }
            else
            {
                LabdaPositionX--;
            }
        }

        static void Main(string[] args)
        {
            RemoveScrollBars();
            alapPositions();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        MoveElsojatekosFEL();
                    }
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        MoveElsojatekosLE();
                    }
                }
                SecondPlayerAIMove();
                Labdamozgatas();
                Console.Clear();
                Elsojatekosmegalkotasa();
                Kettesjatekosmegalkotasa();
                Labdamegalkotása();
                eredmeny();
                Thread.Sleep(60);
            }
        }
    }
}

