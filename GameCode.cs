

using System;

namespace Pig
{
    // Class Die represents a single die
    class Die
    {
        // Number of sides that the die has; can be between 4 and 20
        private int _sides;
        // The current value of the die after it has been rolled; can be 1 to _sides
        private int _currentVal;
        // Variable to hold the random number generator 
        private Random _rand;

        public Die(int _sides)
        {
            // If an illegal value is passed for _sides, exception thrown
            if (_sides < 4 || _sides > 20)
            {
                throw new ArgumentException("Error: number of sides set to invalid state.");
            }

            this._sides = _sides;
            _currentVal = 1;
            _rand = new Random();
        }

        public void Roll()
        {
            _currentVal = _rand.Next(1, _sides);

        }

        public int GetCurrentValue()
        {
            { return _currentVal; }
        }

    }


    // Class Player represents a single player in the game
    class Player
    {

        // Name of player: can't be null or empty. 1+ characters long.
        private string _name;
        // Player's current score: can't be < 0
        private int _score;
        // If the player is the game's winner or not
        private bool _isWinner;

        public Player(string _name)
        {
            if (_name == null)
            {
                throw new ArgumentException("Name cannot be null");
            }

            if (_name.Length == 0)
            {
                throw new ArgumentException("Name cannot be empty");
            }
            this._name = _name;
            _score = 0;
            _isWinner = false;
        }

        public string GetName()
        {
            { return _name; }
        }

        public int GetScore()
        {
            { return _score; }
        }

        public void SetScore(int score)
        {
            // If the score goes below 0, the score stays at 0 instead of becoming negative/error
            if (score < 0)
            {
                _score = 0;
            }

            _score = score;
        }

        public void SetWinner(bool winner)
        {
            _isWinner = winner;
        }

        public bool IsWinner()
        {
            { return _isWinner; }
        }

        public override string ToString()
        {
            return _name;
        }

    }


    // Class Game manages the die & the players of the game, & contains the logic for the game
    class Game
    {

        // The die used in the game
        private Die _gameDie;
        // Two players
        private Player _player1;
        private Player _player2;
        // Number of points needed to win the game 
        private int _maxPoints;

        public Game()
        {
            _gameDie = null;
            _player1 = null;
            _player2 = null;
            _maxPoints = 0;
        }

        public void SetupDie()
        {
            Console.Write("How many sides on the die (4 - 20): ");
            int dieInput = Convert.ToInt32(Console.ReadLine());

            while (dieInput < 4 || dieInput > 20)
            {
                Console.WriteLine("Whoops, " + dieInput + " is not a valid value. Try again.");
                Console.Write("How many sides on the die (4 - 20): ");
                dieInput = Convert.ToInt32(Console.ReadLine());
            }

            // Uses input die side value to create a Die object gameDie
            _gameDie = new Die(dieInput);

        }

        public void SetupMaxPoints()
        {
            Console.WriteLine(" ");
            Console.Write("What are the max points for the game: ");
            int pointsInput = Convert.ToInt32(Console.ReadLine());
            while (pointsInput <= 0)
            {
                Console.WriteLine("Enter a value greater than 0.");
                Console.Write("What are the max points for the game: ");
                pointsInput = Convert.ToInt32(Console.ReadLine());
            }
            _maxPoints = pointsInput;
        }

        // Prompts for player's names and creates Player objects assigned to _player1 and _player2
        public void SetPlayer1()
        {
            Console.WriteLine(" ");
            Console.Write("What is player 1's name: ");
            String name1 = Console.ReadLine();
            while (name1 == null || name1.Trim() == "")
            {
                Console.WriteLine("Name cannot be null or empty.");
                Console.Write("What is player 1's name: ");
                name1 = Console.ReadLine();
            }
            _player1 = new Player(name1);
        }

        public void SetPlayer2()
        {
            Console.WriteLine(" ");
            Console.Write("What is player 2's name: ");
            string name2 = Console.ReadLine();

            while (name2 == null || name2.Trim() == "")
            {
                Console.WriteLine("Name cannot be null or empty.");
                Console.Write("What is player 2's name: ");
                name2 = Console.ReadLine();
            }
            _player2 = new Player(name2);
        }

        public void PlayGame()
        {
            Console.WriteLine(" ");
            Console.WriteLine("===== Let's Play! =====");
            Console.WriteLine(" ");

            // Player 1 reference that can flip after a turn round
            Player currentPlayer = _player1;

            // Output the name up here once for the very start of the game
            Console.WriteLine($"Current player: {currentPlayer.GetName()}");
            Console.WriteLine($"Current score: {currentPlayer.GetScore()}");
            Console.WriteLine(" ");

            int rollAgain = 1;
            bool gameRunning = true;
            int playAgain = 0;

            // scoreCount keeps a seperate count of a player's round score in case
            // the player rolls a 1 & all points for that round's total need
            // to be taken away from their score.
            int scoreCount = 0;

            do
            {
                // Auto roll die once at the start of every turn & show value
                _gameDie.Roll();
                Console.WriteLine($"You rolled a {_gameDie.GetCurrentValue()}.");

                // If the roll would make the player the winner:
                // update score, set the player as the winner, the game is now over.
                if (currentPlayer.GetScore() + _gameDie.GetCurrentValue() >= _maxPoints)
                {
                    currentPlayer.SetScore(_gameDie.GetCurrentValue() + currentPlayer.GetScore());
                    Console.WriteLine($"{ currentPlayer}'s score is now { currentPlayer.GetScore() }.");
                    Console.WriteLine(" ");

                    currentPlayer.SetWinner(true);

                    gameRunning = false;
                }

                // A 1 is rolled:
                // their turn ends, their score is updated, switch players
                else if (_gameDie.GetCurrentValue() == 1 && !currentPlayer.IsWinner())
                {
                    Console.WriteLine("Sorry you rolled a 1. Your turn is over :((");
                    Console.WriteLine(" ");

                    currentPlayer.SetScore(currentPlayer.GetScore() - scoreCount);
                    Console.WriteLine($"{ currentPlayer}'s score is now { currentPlayer.GetScore()}.");
                    Console.WriteLine(" ");
                    Console.WriteLine("----- Next player's turn ----");
                    Console.WriteLine(" ");
                    scoreCount = 0;

                    currentPlayer = (currentPlayer == _player1) ? _player2 : _player1;

                    Console.WriteLine($"Current player: {currentPlayer.GetName()}");
                    Console.WriteLine($"Current score: {currentPlayer.GetScore()}");
                    Console.WriteLine(" ");
                }

                // A 1 is NOT rolled:
                // player's score updates, scoreCount updates, prompted to roll/hold if no winner
                else
                {
                    currentPlayer.SetScore(_gameDie.GetCurrentValue() + currentPlayer.GetScore());

                    scoreCount += _gameDie.GetCurrentValue();
                    if (currentPlayer.GetScore() < _maxPoints)
                    {
                        Console.Write($"Do you want to roll (1) or hold (2)? [Points you could keep: {scoreCount}] ");
                        Console.WriteLine(" ");

                        rollAgain = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(" ");
                    }

                    // If they don't want to roll again & there's no winner:
                    // output updated score, switch players
                    if (rollAgain == 2 && !currentPlayer.IsWinner())
                    {
                        Console.WriteLine($"{ currentPlayer}'s score is now { currentPlayer.GetScore()}.");
                        Console.WriteLine(" ");
                        scoreCount = 0;

                        currentPlayer = (currentPlayer == _player1) ? _player2 : _player1;

                        rollAgain = 1;

                        // Information for the next round
                        Console.WriteLine("----- Next player's turn ----");
                        Console.WriteLine(" ");
                        Console.WriteLine($"Current player: {currentPlayer.GetName()}");
                        Console.WriteLine($"Current score: {currentPlayer.GetScore()}");
                        Console.WriteLine(" ");
                    }
                }
                // Keep looping while the game is still being played/there is no winner
            } while (gameRunning == true);
        }

        public void ShowWinner()
        {
            Console.WriteLine("----- WINNER ----");
            Console.WriteLine(" ");

            if (_player1.IsWinner())
            {
                Console.WriteLine($"Player 1, {_player1.GetName()}, is the winner with {_player1.GetScore()} points!");
            }
            else
            {
                Console.WriteLine($"Player 2, {_player2.GetName()}, is the winner with {_player2.GetScore()} points!");
            }
        }
    }
}
