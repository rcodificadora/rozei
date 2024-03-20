using System.Data;
using System.Data.Common;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

public class Board
{
    public char[,] playerOneBoard;

    public char[,] playerTwoBoard;

    public char[,] shootBoard;

    public Board()
    {
        playerOneBoard = new char[10, 10];
        playerTwoBoard = new char[10, 10];
        shootBoard = new char[10, 10];
        CreateBoards();
    }
    public void CreateBoards()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                playerOneBoard[i, j] = ' ';
                playerTwoBoard[i, j] = ' ';

                shootBoard[i, j] = ' ';
            }
        }
    }
    public void ShowPlayerOneBoard()
    {
        Console.WriteLine("  A B C D E F G H I J");
        for (int i = 0; i < 10; i++)
        {
            Console.Write(i + " ");
            for (int j = 0; j < 10; j++)
            {
                Console.Write(playerOneBoard[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public void ShowPlayerTwoBoard()
    {
        Console.WriteLine("  A B C D E F G H I J");
        for (int i = 0; i < 10; i++)
        {
            Console.Write(i + " ");
            for (int j = 0; j < 10; j++)
            {
                Console.Write(playerTwoBoard[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
    public void ShowShootBoard()
    {
        Console.WriteLine("  A B C D E F G H I J");
        for (int i = 0; i < 10; i++)
        {
            Console.Write(i + " ");
            for (int j = 0; j < 10; j++)
            {
                Console.Write(shootBoard[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
    
    public bool ArePlayerOneShipsWrecked(Player playerOne)
    {
        foreach (Ship ship in playerOne.playerOneShips)
        {
            if (ship != null)
            {
                if (!ship.IsWrecked)
                {
                    return false;
                }
            }
        }
        return true;

    }
    
    public bool ArePlayerTwoShipsWrecked(Player playerOne)
    {
        foreach (Ship ship in playerOne.playerTwoShips)
        {
            if (ship != null)
            {
                if (!ship.IsWrecked)
                {
                    return false;
                }
            }
        }
        return true;

    }
    public bool CheckPosition(int row, int column)
    {
        if (row < 0 || row > 9 || column < 0 || column > 9)
            return false;

        return row >= 0 && row < 11 && column >= 0 && column < 10;
    }
    public bool CanPlaceShip(int row, int column, int size, int direction)
    {
        int lastRow = row;
        int lastColumn = column;

        if (direction == 1)
        {
            lastRow -= size + 1;
        }
        if (direction == 2)
        {
            lastRow += size - 1;
        }
        if (direction == 3)
        {
            lastColumn += size - 1;
        }
        if (direction == 4)
        {
            lastColumn -= size + 1;
        }


        if (lastRow >= 11 || lastColumn >= 10)
        {
            return false;
        }

        for (int i = row; i <= lastRow; i++)
        {
            for (int j = column; j <= lastColumn; j++)
            {
                if (i < 0 || i >= 10 || j < 0 || j >= 10 || playerOneBoard[i, j] != ' ' || !IsSurrounded(i, j))
                {
                    return false;
                }
            }
        }
        return true;
    }
    public bool IsSurrounded(int row, int column)
    {
        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = column - 1; j <= column + 1; j++)
            {
                if (i >= 0 && i < 10 && j >= 0 && j < 10)
                {
                    if (playerOneBoard[i, j] == 'X')
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public void OnePlaceShip(int row, int column, int size, int direction)
    {
        for (int i = 0; i < size; i++)
        {
            if (direction == 1)
            {
                if (CheckPosition(row - i, column))
                {
                    playerOneBoard[row - i, column] = 'X';
                }
            }
            else if (direction == 2)
            {
                if (CheckPosition(row + i, column))
                {
                    playerOneBoard[row - i, column] = 'X';
                }
            }
            else if (direction == 3)
            {
                if (CheckPosition(row, column + i))
                {
                    playerOneBoard[row - i, column] = 'X';
                }
            }
            else if (direction == 4)
            {
                if (CheckPosition(row, column - i))
                {
                    playerOneBoard[row - i, column] = 'X';
                }
            }
        }
    }
    public void TwoPlaceShip(int row, int column, int size, int direction)
    {
        for (int i = 0; i < size; i++)
        {
            if (direction == 1)
            {
                if (CheckPosition(row - i, column))
                {
                    playerTwoBoard[row - i, column] = 'X';
                }
            }
            else if (direction == 2)
            {
                if (CheckPosition(row + i, column))
                {
                    playerTwoBoard[row + i, column] = 'X';
                }
            }
            else if (direction == 3)
            {
                if (CheckPosition(row, column + i))
                {
                    playerTwoBoard[row, column + i] = 'X';
                }
            }
            else if (direction == 4)
            {
                if (CheckPosition(row, column - i))
                {
                    playerTwoBoard[row, column - i] = 'X';
                }
            }
        }
    }
    
    public char takeHitPlayerOne(Board targetBoard, int row, int column, Player playerOne)
    {
        char target = targetBoard.playerOneBoard[row, column];

        if (target == 'X')
        {
            playerOneBoard[row, column] = 'O';
            shootBoard[row, column] = 'O';

            foreach (Ship ship in playerOne.playerOneShips)
            {
                if (ship != null)
                {
                    if (!ship.IsWrecked)
                    {
                        if (ship.shipSize > 0)
                        {
                            ship.Hit();
                            break;
                        }
                    }
                    else
                    {
                        if (ArePlayerOneShipsWrecked(playerOne))
                        {
                            playerOne.endOfGamePlayerOne = true;
                        }
                    }
                }

            }
            return 'O';
        }
        else
        {
            shootBoard[row, column] = 'P';
            return ' ';
        }
    }
    public char takeHitPlayerTwo(Board targetBoard, int row, int column, Player playerTwo)
    {
        char target = targetBoard.playerOneBoard[row, column];

        if (target == 'X')
        {
            playerOneBoard[row, column] = 'O';
            shootBoard[row, column] = 'O';

            foreach (Ship ship in playerTwo.playerTwoShips)
            {
                if (ship != null)
                {
                    if (!ship.IsWrecked)
                    {
                        if (ship.shipSize > 0)
                        {
                            ship.Hit();
                            break;
                        }
                    }
                    else
                    {
                        if (ArePlayerTwoShipsWrecked(playerTwo))
                        {
                            playerTwo.endOfGamePlayerTwo = true;
                        }
                    }
                }

            }
            return 'O';
        }
        else
        {
            shootBoard[row, column] = 'P';
            return ' ';
        }
    }
}
