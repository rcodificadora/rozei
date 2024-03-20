using System.Numerics;

class Program
{
    static void Main()
    {
        Board playerOneBoard = new Board();
        Board playerTwoBoard = new Board();


        Player playerOne = new Player(playerOneBoard);
        Player playerTwo = new Player(playerTwoBoard);

        bool playerOneTurn = true;
        bool endOfGame = false;

        Console.WriteLine("Gra w statka, gracz 1 ustaw swoje");

        playerOne.PlaceShips(playerOneBoard);

        Console.WriteLine("Teraz gracz 2");
        playerTwo.PlayerTwoPlaceShips(playerTwoBoard);

        Console.WriteLine("Start gry");

        while (!endOfGame)
        {
            Console.WriteLine("Plansza strzałów gracza 2");
            playerOneBoard.ShowShootBoard();
            Console.WriteLine("Plnasza strzałów gracza 1");
            playerTwoBoard.ShowShootBoard();

            if (playerOneTurn)
            {
                Console.WriteLine("Teraz gracz 1");
                playerOne.PlayerOneShoot(playerTwoBoard, playerOne);
                if(playerOne.endOfGamePlayerOne)
                {
                    endOfGame = true;
                    Console.WriteLine("wygrywa gracz 2");
                }
            }
            else
            {
                Console.WriteLine("Teraz gracz 2");
                playerTwo.PlayerTwoShoot(playerOneBoard, playerTwo);
                if(playerTwo.endOfGamePlayerTwo)
                {
                    endOfGame = true;
                    Console.WriteLine("wygrywa gracz 1");
                }

            }
            Console.Clear();
            playerOneTurn = !playerOneTurn;
        }

        Console.Clear();
        Console.WriteLine("Plansza strzałów gracza 1");
        playerOneBoard.ShowShootBoard();
        Console.WriteLine("Plansza strzałów gracza 2");
        playerTwoBoard.ShowPlayerOneBoard();

        Console.WriteLine("Koniec gry");
    }
}
