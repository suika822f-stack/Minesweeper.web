namespace Minesweeper.web
{
    public static class MineSweeper
    {
        public static string[,] CreateBoard(int boardSize, int bombNumber, int safeRow, int safeCol)
        {
            if (bombNumber > boardSize * boardSize - 9)
            {
                throw new Exception("地雷が多すぎます");
            }
            string[,] bombboard = GetBombBoard(boardSize, bombNumber, safeRow, safeCol);
            return GetBoard(bombboard);

        }

        /// <summary>
        /// 爆弾の入った盤面の生成
        /// </summary>
        /// <param name="boardSize"></param>
        /// <param name="bomb"></param>
        /// <returns></returns>
        private static string[,] GetBombBoard(int boardSize, int bomb, int safeRow, int safeCol)
        {
            Random random = new Random();
            string[,] board = new string[boardSize, boardSize];
            for (int i = 0; i < bomb; i++)
            {
                int row;
                int column;
                while (true)
                {
                    row = random.Next(boardSize);
                    column = random.Next(boardSize);
                    bool isSafeArea =(row >= safeRow - 1 && row <= safeRow + 1 && column >= safeCol - 1 && column <= safeCol + 1);

                    if (isSafeArea)
                    {
                        continue;
                    }

                    if (board[row, column] == null)
                    {
                        break;
                    }
                }
                board[row, column] = "*";
            }
            return board;
        }
        /// <summary>
        /// すべてのマスに数字が入った盤面の生成
        /// </summary>
        /// <param name="bombboard"></param>
        /// <returns></returns>
        private static string[,] GetBoard(string[,] bombboard)
        {
            int length = bombboard.GetLength(1);
            for (int row = 0; row < length; row++)
            {
                for (int column = 0; column < length; column++)
                {
                    if (bombboard[row, column] != "*")
                    {
                        int bombNumber = GetBombNumber(bombboard, row, column);
                        bombboard[row, column] = bombNumber.ToString();
                    }
                }
            }
            return bombboard;
        }
        /// <summary>
        /// あるマスの周りの爆弾の数を返す
        /// </summary>
        /// <param name="bombboard"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private static int GetBombNumber(string[,] bombboard, int row, int column)
        {
            int bombNumber = 0;
            if (bombboard[row, column] == null)
            {
                for (int i = row - 1; i <= row + 1; i++)
                {
                    for (int j = column - 1; j <= column + 1; j++)
                    {
                        try
                        {
                            if (bombboard[i, j] == "*")
                            {
                                bombNumber++;
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            continue;
                        }

                    }
                }
            }
            return bombNumber;
        }

    }
}
