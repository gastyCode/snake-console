using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Snake
    {
        private Game game;
        private Direction playerDirection;
        private List<(int, int)> nodes = new List<(int, int)>();
        private char body = '\u2588';

        public int X { get; set; }
        public int Y { get; set; }
        public int Length { get; set; }

        public Snake(Game game, int x, int y)
        {
            this.game = game;
            X = x;
            Y = y;
            Length = 1;
            nodes.Add((X, Y));
            this.playerDirection = Direction.Right;
        }

        public void Update()
        {
            UpdateDirection();

            for (int i = 0; i < Length - 1; i++)
            {
                nodes[i] = nodes[i + 1];
            }
            nodes[Length - 1] = (X, Y);

            Move();
            Draw();
            CheckPosition();
            CheckBite();
        }

        private void CheckBite()
        {
            foreach(var node in nodes)
            {
                if(node.Item1 == X && node.Item2 == Y)
                {
                    game.running = false;
                }
            }
        }

        private void CheckPosition()
        {
            if(X < 1 || X > game.WINDOW_SIZE.Item1 - 2|| Y < 1 || Y > game.WINDOW_SIZE.Item2 - 2)
            {
                game.running = false;
            }
        }

        private void UpdateDirection()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.A:
                        if(playerDirection != Direction.Right)
                            playerDirection = Direction.Left;
                        break;
                    case ConsoleKey.D:
                        if (playerDirection != Direction.Left)
                            playerDirection = Direction.Right;
                        break;
                    case ConsoleKey.S:
                        if (playerDirection != Direction.Up)
                            playerDirection = Direction.Down;
                        break;
                    case ConsoleKey.W:
                        if (playerDirection != Direction.Down)
                            playerDirection = Direction.Up;
                        break;
                    case ConsoleKey.Escape:
                        game.running = false;
                        break;
                }
            }
        }

        private void Move()
        {
            if (playerDirection == Direction.Left)
            {
                X--;
            }
            else if (playerDirection == Direction.Right)
            {
                X++;
            }
            else if (playerDirection == Direction.Down)
            {
                Y++;
            }
            else if (playerDirection == Direction.Up)
            {
                Y--;
            }
        }

        public void AddNode()
        {
            nodes.Add((X, Y));
        }

        private void Draw()
        {
            foreach(var node in nodes)
            {
                Console.SetCursorPosition(node.Item1, node.Item2);
                Console.Write(body);
            }
        }
    }
    enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
}
