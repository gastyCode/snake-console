using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Game
    {
        public (int, int) WINDOW_SIZE = (40, 40);

        public int score = 0;

        public Snake snake;
        public List<Fruit> fruits = new List<Fruit>();
        public bool running;

        public Game()
        {
            Console.SetWindowSize(WINDOW_SIZE.Item1, WINDOW_SIZE.Item2);
            Console.SetBufferSize(WINDOW_SIZE.Item1, WINDOW_SIZE.Item2);
            Console.CursorVisible = false;

            snake = new Snake(this, WINDOW_SIZE.Item1 / 2, WINDOW_SIZE.Item2 / 2);
            running = true;
        }

        public void GameOver()
        {
            Console.Clear();

            string text = "Game Over!";
            string scoreText = $"Your final score is {score}";

            int textLength = text.Count();
            int scoreTextLength = scoreText.Count();

            Console.SetCursorPosition((WINDOW_SIZE.Item1 - textLength) / 2, WINDOW_SIZE.Item2 / 2);
            Console.WriteLine(text);
            Console.SetCursorPosition((WINDOW_SIZE.Item1 - scoreTextLength) / 2, (WINDOW_SIZE.Item2 / 2) + 1);
            Console.WriteLine(scoreText);

            Console.ReadKey();
        }

        public void Run()
        {
            while (running)
            {
                Console.Clear();

                DrawWalls();
                snake.Update();

                GenerateFruit(GetRandomPositon());

                foreach (Fruit fruit in fruits.ToList())
                {
                    fruit.Draw();
                    CheckCollision(snake, fruit);
                }

                WriteScore(score);

                Thread.Sleep(100);
            }
            GameOver();
        }
        public (int, int) GetRandomPositon()
        {
            Random rand = new Random();
            (int, int) position = (rand.Next(2, WINDOW_SIZE.Item1 - 2), rand.Next(2, WINDOW_SIZE.Item2 - 2));
            return position;
        }

        public void GenerateFruit((int, int) position)
        {
            if (fruits.Count == 0)
            {
                fruits.Add(new Fruit(position.Item1, position.Item2));
            }
        }

        public void DrawWalls()
        {
            char wall = '\u2592';

            Console.SetCursorPosition(0, 1);

            for(int i = 1; i < WINDOW_SIZE.Item1; i++)
            {
                Console.Write(wall);
            }

            for(int i = 0; i < WINDOW_SIZE.Item2; i++)
            {
                Console.Write(wall);
                Console.SetCursorPosition(WINDOW_SIZE.Item1 - 1, i);
                Console.WriteLine(wall);

            }

            Console.SetCursorPosition(0, WINDOW_SIZE.Item2-1);

            for (int i = 0; i < WINDOW_SIZE.Item1; i++)
            {
                Console.Write(wall);
            }
        }

        public void CheckCollision(Snake snake, Fruit fruit)
        {
            if (snake.X == fruit.X && snake.Y == fruit.Y)
            {
                fruits.Remove(fruit);
                snake.Length++;
                snake.AddNode();
                score++;
            }
        }

        public void WriteScore(int score)
        {
            Console.SetCursorPosition(1, 1);
            Console.Write($"Score: {score}");
        }
    }
}
