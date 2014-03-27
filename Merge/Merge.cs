using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Merge
{
    public static class Merge
    {
        static Random rnd = new Random();

        public static int[,] CreateGrid(int width)
        {
            return new int[width, width];
        }

        public static List<Point> AvailableSpaces(int[,] gridArray)
        {
            var ret = new List<Point>();
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                for (int x = 0; x < gridArray.GetLength(0); x++)
                {
                    if (gridArray[x, y] == 0)
                        ret.Add(new Point(x, y));
                }
            }
            return ret;
        }

        public static void AddTile(ref int[,] gridArray)
        {
            var availableSpaces = AvailableSpaces(gridArray);
            if (availableSpaces.Count == 0)
                throw new Exception("No space to add tile!");

            var newTileValue = (rnd.Next(10) < 9) ? 2 : 4; 

            var randomAvailable = availableSpaces[rnd.Next(availableSpaces.Count)];
            gridArray[randomAvailable.X, randomAvailable.Y] = newTileValue;
        }

        public static bool AvailableMoves(int[,] gridArray)
        {
            var width = gridArray.GetLength(0);
            var height = gridArray.GetLength(1);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (width > x+1 && gridArray[x, y] == gridArray[x + 1, y])
                        return true;
                    if (height > y+1 && gridArray[x, y] == gridArray[x, y + 1])
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Move grid down
        /// </summary>
        /// <param name="gridArray"></param>
        /// <returns>
        /// -1 if unable to move down
        /// Integer value of newly created tiles
        /// </returns>
        public static int Down(ref int[,] gridArray)
        {
            var resultArray = gridArray.ShadowCopy();
            var width = resultArray.GetLength(0);
            var height = resultArray.GetLength(1);
            var additionalScore = 0;
            // Compact each X, bottom to top
            for (int x = 0; x < width; x++)
            {
                var thisColumnBottomToTop = new int[height];
                for (int y = 0; y < height; y++)
                {
                    thisColumnBottomToTop[y] = (resultArray[x, (height - 1) - y]);
                }
                additionalScore += Compact(ref thisColumnBottomToTop);

                for (int y = 0; y < height; y++)
                {
                    resultArray[x, (height - 1) - y] = thisColumnBottomToTop[y];
                }
            }
            if (ValuesEqual(gridArray, resultArray))
            {
                return -1;
            }
            gridArray = resultArray;
            return additionalScore;
        }

        public static int Right(ref int[,] gridArray)
        {
            var resultArray = gridArray.ShadowCopy();
            var width = resultArray.GetLength(0);
            var height = resultArray.GetLength(1);
            var additionalScore = 0;
            // Compact each Y, right to left
            for (int y = 0; y < width; y++)
            {
                var thisRowRightToLeft = new int[width];
                for (int x = 0; x < width; x++)
                {
                    thisRowRightToLeft[x] = (resultArray[(width - 1) - x, y]);
                }

                additionalScore += Compact(ref thisRowRightToLeft);

                for (int x = 0; x < width; x++)
                {
                    resultArray[(width - 1) - x, y] = thisRowRightToLeft[x];
                }
            }
            if (ValuesEqual(gridArray, resultArray))
            {
                return -1;
            }
            gridArray = resultArray;
            return additionalScore;
        }

        public static int Up(ref int[,] gridArray)
        {
            var resultArray = gridArray.ShadowCopy();
            var width = resultArray.GetLength(0);
            var height = resultArray.GetLength(1);
            var additionalScore = 0;
            // Compact each X, top to bottom
            for (int x = 0; x < width; x++)
            {
                var thisColumnTopToBottom = new int[height];
                for (int y = 0; y < height; y++)
                {
                    thisColumnTopToBottom[y] = (resultArray[x, y]);
                }
                additionalScore += Compact(ref thisColumnTopToBottom);

                for (int y = 0; y < height; y++)
                {
                    resultArray[x, y] = thisColumnTopToBottom[y];
                }
            }
            if (ValuesEqual(gridArray, resultArray))
            {
                return -1;
            }
            gridArray = resultArray;
            return additionalScore;
        }

        public static int Left(ref int[,] gridArray)
        {
            var resultArray = gridArray.ShadowCopy();
            var width = resultArray.GetLength(0);
            var height = resultArray.GetLength(1);
            var additionalScore = 0;
            // Compact each Y, left to right
            for (int y = 0; y < height; y++)
            {
                var thisRowLeftToRight = new int[width];
                for (int x = 0; x < width; x++)
                {
                    thisRowLeftToRight[x] = (resultArray[x, y]);
                }
                additionalScore += Compact(ref thisRowLeftToRight);

                for (int x = 0; x < width; x++)
                {
                    resultArray[x, y] = thisRowLeftToRight[x];
                }
            }
            if (ValuesEqual(gridArray, resultArray))
            {
                return -1;
            }
            gridArray = resultArray;
            return additionalScore;
        }

        /// <summary>
        /// Compacts array contents to the left. Fills remainder with 0.
        /// 1020 becomes 1200.
        /// 0201 becomes 2100.
        /// 1100 becomes 2000.
        /// 1010 becomes 2000.
        /// </summary>
        /// <param name="lineArray"></param>
        /// <returns>Score value (newly merged block values added together)</returns>
        private static int Compact(ref int[] lineArray)
        {
            var rowScore = 0;
            var intRow = ShiftLeft(lineArray);

            // Combine equal neighbors
            for (int i = 0; i < intRow.Length - 1; i++)
            {
                if (intRow[i] == intRow[i + 1])
                {
                    intRow[i] += intRow[i + 1];
                    intRow[i + 1] = 0;
                    intRow = ShiftLeft(intRow);
                    rowScore += intRow[i];
                }
            }

            lineArray = intRow;
            return rowScore;
        }

        /// <summary>
        /// Skips 0s and shifts items left
        /// 1020 becomes 1200.
        /// 0201 becomes 2100.
        /// </summary>
        /// <param name="intRow"></param>
        /// <returns></returns>
        private static int[] ShiftLeft(int[] intRow)
        {
            var newRow = new int[intRow.Length];
            var newIndex = 0;
            for (int i = 0; i < intRow.Length; i++)
            {
                if (intRow[i] != 0)
                {
                    newRow[newIndex] = intRow[i];
                    newIndex++;
                }
            }
            return newRow;
        }

        private static int[,] ShadowCopy(this int[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var ret = new int[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    ret[x, y] = original[x, y];
                }
            }
            return ret;
        }

        private static bool ValuesEqual<T>(T[,] first, T[,] second)
        {
            if (first.Rank != second.Rank)
                return false;
            if (first.GetLength(0) != second.GetLength(0))
                return false;
            if (first.GetLength(1) != second.GetLength(1))
                return false;

            for (int y = 0; y < first.GetLength(1); y++)
            {
                for (int x = 0; x < first.GetLength(0); x++)
                {
                    if (!(first[x, y].Equals(second[x, y])))
                        return false;
                }
            }
            return true;
        }
    }
}
