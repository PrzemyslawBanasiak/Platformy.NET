using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class GameHandler
    {
        int[,] board = new int[12, 12];
        private MainWindow window;

        public GameHandler(MainWindow wind)
        {
            window = wind;
        }

        public void BeginPlay()
        {
            CreateZeroFilledBoard();
            CreateRandomPopulation();
        }

        public void Next()
        {
            int[,] newBoard = new int[12, 12];
            for (int i = 1; i <= 10; ++i)
            {
                for (int j = 1; j <= 10; ++j)
                {
                    int num = CountNeighbours(i, j);
                    if (board[i, j] == 0 && num == 3) RiseCell(newBoard, i, j);
                    if (board[i, j] == 1 && (num == 2 || num == 3)) RiseCell(newBoard, i, j);
                    if (board[i, j] == 1 && (num < 2 || num > 3)) KillCell(newBoard, i, j);
                }
            }
            board = newBoard;
        }

        private int CountNeighbours(int i, int j)
        {
            int count = board[i - 1, j - 1] + board[i, j - 1] + board[i + 1, j - 1];
            count += board[i - 1, j] + board[i + 1, j];
            count += board[i - 1, j + 1] + board[i, j + 1] + board[i + 1, j + 1];
            return count;
        }

        private void CreateRandomPopulation()
        {
            Random rnd = new Random();
            int pop = rnd.Next(10, 30);
            for (int i = 0; i < pop; ++i)
                RiseCell(board, rnd.Next(1, 10), rnd.Next(1, 10));
        }

        private void CreateZeroFilledBoard()
        {
            for (int i = 0; i < 12; ++i)
            {
                for (int j = 0; j < 12; ++j)
                {
                    board[i, j] = 0;
                }
            }
        }

        private void KillCell(int[,] board, int i, int j)
        {
            board[i, j] = 0;
            window.OnCellDie(i, j);
        }

        private void RiseCell(int[,] board, int i, int j)
        {
            board[i, j] = 1;
            window.OnCellRise(i, j);
        }
    }
}
