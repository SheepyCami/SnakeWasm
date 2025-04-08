using System;
using System.Collections.Generic;
using SnakeWasm.Game.Services; // Add this using directive

namespace Snake.Game
{
    public class GameState
    {
        public int Rows { get; }
        public int Cols { get; }
        public GridValue[,] Grid { get; }
        public Direction Dir { get; private set; }
        public int Score { get; private set; }
        public bool GameOver { get; private set; }

        private readonly LinkedList<Direction> dirChanges = new();
        private readonly LinkedList<Position> snakePositions = new();
        private static readonly Random random = new();

        private int foodEaten = 0;
        public bool CoffeeEaten { get; private set; } = false;

        public GameState(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Grid = new GridValue[rows, cols];
            Dir = Direction.Right;

            AddSnake();
            AddFood();
        }

        private void AddSnake()
        {
            int r = Rows / 2;
            for (int c = 1; c < 3; c++)
            {
                Grid[r, c] = GridValue.Snake;
                snakePositions.AddLast(new Position(r, c));
            }
        }

        private IEnumerable<Position> EmptyPositions()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (Grid[r, c] == GridValue.Empty)
                        yield return new Position(r, c);
                }
            }
        }

        private void AddFood()
        {
            var empty = new List<Position>(EmptyPositions());
            if (empty.Count == 0) return;

            var pos = empty[random.Next(empty.Count)];
            Grid[pos.Row, pos.Col] = GridValue.Food;
        }

        private void AddCoffee()
        {
            var empty = new List<Position>(EmptyPositions());
            if (empty.Count == 0) return;

            var pos = empty[random.Next(empty.Count)];
            Grid[pos.Row, pos.Col] = GridValue.Coffee;
        }

        public Position HeadPosition() => snakePositions.First?.Value ?? throw new InvalidOperationException("The snake has no head.");
        public Position TailPosition() => snakePositions.Last?.Value ?? throw new InvalidOperationException("The snake has no tail.");
        public IEnumerable<Position> SnakePositions() => snakePositions;

        private void AddHead(Position pos)
        {
            snakePositions.AddFirst(pos);
            Grid[pos.Row, pos.Col] = GridValue.Snake;
        }

        private void RemoveTail()
        {
            Position tail = TailPosition();
            Grid[tail.Row, tail.Col] = GridValue.Empty;
            snakePositions.RemoveLast();
        }

        private Direction GetLastDirection() =>
            dirChanges.Count == 0 ? Dir : dirChanges.Last?.Value ?? Dir;

        private bool CanChangeDirection(Direction newDir)
        {
            if (dirChanges.Count == 2) return false;

            var lastDir = GetLastDirection();
            return newDir != lastDir && newDir != lastDir.Opposite();
        }

        public void ChangeDirection(Direction dir)
        {
            if (CanChangeDirection(dir))
                dirChanges.AddLast(dir);
        }

        private bool OutsideGrid(Position pos) =>
            pos.Row < 0 || pos.Row >= Rows || pos.Col < 0 || pos.Col >= Cols;

        private GridValue WillHit(Position newHeadPos)
        {
            if (OutsideGrid(newHeadPos)) return GridValue.Outside;
            if (newHeadPos == TailPosition()) return GridValue.Empty;
            return Grid[newHeadPos.Row, newHeadPos.Col];
        }

        public void Move()
        {
            if (dirChanges.Count > 0)
            {
                Dir = dirChanges.First?.Value ?? Dir;
                dirChanges.RemoveFirst();
            }

            var newHeadPos = HeadPosition().Translate(Dir);
            var hit = WillHit(newHeadPos);

            if (hit == GridValue.Outside || hit == GridValue.Snake)
            {
                GameOver = true;
                return;
            }

            switch (hit)
            {
                case GridValue.Empty:
                    RemoveTail();
                    AddHead(newHeadPos);
                    break;

                case GridValue.Food:
                    AddHead(newHeadPos);
                    Score++;
                    foodEaten++;

                    if (foodEaten % 10 == 0)
                        AddCoffee();
                    else
                        AddFood();
                    break;

                case GridValue.Coffee:
                    AddHead(newHeadPos);
                    Score++;
                    CoffeeEaten = true;
                    AddFood(); // keep gameplay flowing
                    break;
            }
        }

        public void ResetCoffeeEaten() => CoffeeEaten = false;
    }
}
