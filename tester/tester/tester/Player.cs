using System;
using System.Numerics;

public class Player
{
    public bool endOfGamePlayerOne { get; set; }

    public bool endOfGamePlayerTwo { get; set; }


    Board playerOneBoard;

    Board playerTwoBoard;

    Board shootBoard;

    int shipNumber;
    const int MaxShipNumber = 10;

    public Ship[] playerOneShips;

    public Ship[] playerTwoShips;

    public Player(Board playerOneBoard)
    {
        this.playerOneBoard = playerOneBoard;
        shootBoard = new Board();
        
        playerOneShips = new Ship[MaxShipNumber];
        
        playerTwoShips = new Ship[MaxShipNumber];
        shipNumber = 0;

    }
    public bool CheckDirection(int direction)
    {
        return direction == 1 || direction == 2 || direction == 3 || direction == 4;
    }
    public bool CheckShot(int row, int column)
    {
        return row >= 0 && row < 11 && column >= 0 && column < 10 && shootBoard.shootBoard[row, column] == ' ';
    }
    public void PlaceShips(Board targetBoard)
    {
        int[] shipSizes = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };

        for (int i = 0; i < 11; i++)
        {
            Console.WriteLine($"Ustaw statek o rozmiarze {shipSizes[i]}.");
            bool goodPlacement = false;

            while (!goodPlacement)
            {
                Console.Write("Podaj kolumne (A-J)");
                string columnStr = Console.ReadLine().ToUpper();
                Console.Write("Podaj wiersz (0-10)");
                int row = GetInt();

                int column = "ABCDEFGHIJ".IndexOf(columnStr);

                Console.Write("Podaj kierunek 1.Góra  2.Dół  3.Prawo  4.Lewo ");
                int direction = GetInt();
                Console.WriteLine();

                if (targetBoard.CheckPosition(row, column) && CheckDirection(direction))
                {
                    if (targetBoard.CanPlaceShip(row, column, shipSizes[i], direction))
                    {
                        targetBoard.OnePlaceShip(row, column, shipSizes[i], direction);
                        goodPlacement = true;

                        playerOneShips[shipNumber] = new Ship(shipSizes[i]);
                        shipNumber++;
                    }
                    else
                    {
                        Console.WriteLine("Zła pozycja");
                    }
                }
                else
                {
                    Console.WriteLine("Złe dane");
                }
            }
            Console.Clear();
            Console.WriteLine("Plansza gracza1");
            playerOneBoard.ShowPlayerOneBoard();
            Console.WriteLine("Plansza do ataku:");
            shootBoard.ShowShootBoard();

            if (shipNumber == 10)
            {
                Console.WriteLine("Postawiłeś wszystkie statki");
                break;
            }
        }
    }

    public void PlayerTwoPlaceShips(Board targetBoard)
    {
        int[] shipSizes = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };

        for (int i = 0; i < 11; i++)
        {
            Console.WriteLine($"Ustaw statek o rozmiarze {shipSizes[i]}.");
            bool goodPlacement = false;

            while (!goodPlacement)
            {
                Console.Write("Podaj kolumne (A-J)");
                string columnStr = Console.ReadLine().ToUpper();
                Console.Write("Podaj wiersz (0-10)");
                int row = GetInt();

                int column = "ABCDEFGHIJ".IndexOf(columnStr);

                Console.Write("Podaj kierunek 1.Góra  2.Dół  3.Prawo  4.Lewo ");
                int direction = GetInt();
                Console.WriteLine();

                if (targetBoard.CheckPosition(row, column) && CheckDirection(direction))
                {
                    if (targetBoard.CanPlaceShip(row, column, shipSizes[i], direction))
                    {
                        targetBoard.TwoPlaceShip(row, column, shipSizes[i], direction);
                        goodPlacement = true;

                        playerTwoShips[shipNumber] = new Ship(shipSizes[i]);
                        shipNumber++;
                    }
                    else
                    {
                        Console.WriteLine("Zła pozycja");
                    }
                }
                else
                {
                    Console.WriteLine("Złe dane");
                }
            }
            Console.Clear();
            Console.WriteLine("Plansza gracza2");
            playerTwoBoard.ShowPlayerTwoBoard();
            Console.WriteLine("Plansza do ataku:");
            shootBoard.ShowShootBoard();

            if (shipNumber == 10)
            {
                Console.WriteLine("Postawiłeś wszystkie statki");
                break;
            }
        }
    }
    
    public void PlayerOneShoot(Board targetBoard, Player playerOne)
    {
        Console.Write("Podaj kolumne strzału (A-J)");
        string columnStr = Console.ReadLine().ToUpper();

        Console.Write("Podaj wiersz strzału (0-10)");
        int row = GetInt();

        int column = "ABCDEFGHIJ".IndexOf(columnStr);

        if (targetBoard.CheckPosition(row, column))
        {
            if (CheckShot(row, column))
            {
                char result = targetBoard.takeHitPlayerTwo(targetBoard, row, column, playerOne);

                if (result == 'X')
                {
                    if (targetBoard.ArePlayerTwoShipsWrecked(playerOne))
                    {
                        Console.WriteLine("Gracz 1 wygrywa");
                    }
                    else
                    {
                        Console.WriteLine("Pudło");
                    }
                }
                else
                {
                    Console.WriteLine("Zła pozycja");
                }
            }
            else
            {
                Console.WriteLine("Złe dane");
            }
        }
    }
    public bool[,] memoryOne = new bool[10, 10];
    
    public void PlayerTwoShoot(Board targetBoard, Player playerOne)
    {
        Console.Write("Podaj kolumne strzału (A-J)");
        string columnStr = Console.ReadLine().ToUpper();

        Console.Write("Podaj wiersz strzału (0-10)");
        int row = GetInt();

        int column = "ABCDEFGHIJ".IndexOf(columnStr);

        if (targetBoard.CheckPosition(row, column))
        {
            if (CheckShot(row, column))
            {
                char result = targetBoard.takeHitPlayerTwo(targetBoard, row, column, playerOne);

                if (result == 'X')
                {
                    if (targetBoard.ArePlayerOneShipsWrecked(playerOne))
                    {
                        Console.WriteLine("Gracz 2 wygrywa");
                    }
                    else
                    {
                        Console.WriteLine("Pudło");
                    }
                }
                else
                {
                    Console.WriteLine("Zła pozycja");
                }
            }
            else
            {
                Console.WriteLine("Złe dane");
            }
        } }
    public bool[,] memoryTwo = new bool[10, 10];
    public int GetInt()
    {
        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int value))
            {
                return value;
            }
            Console.WriteLine("Złą wartość");
        }
    }


}
