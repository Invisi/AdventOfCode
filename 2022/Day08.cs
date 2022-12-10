namespace AoC
{
    class Day08
    {
        public static void Solution()
        {
            var contents = Utils.ReadContents(8, false);

            var data = contents.Split("\n");

            var columns = data[0].Length;
            var rows = data.Length;

            // Init tree grid
            var trees = new int[columns, rows];
            for (var y = 0; y < data.Length; y++)
            {
                for (var x = 0; x < data[y].Length; x++)
                {
                    trees[x, y] = int.Parse("" + data[y][x]);
                }
            }

            // Part 1
            var visibleTrees = (rows - 2) * 2 + columns * 2;
            for (var x = 1; x < rows - 1; x++)
            {
                for (var y = 1; y < columns - 1; y++)
                {
                    if (IsVisible(x, y, trees))
                    {
                        visibleTrees += 1;
                    }
                }
            }
            Console.WriteLine($"{visibleTrees} are visible.");

            // Part 2
            var bestScenicScore = 0;
            for (var x = 1; x < rows; x++)
            {
                for (var y = 1; y < columns - 1; y++)
                {
                    var score = ScenicScore(x, y, trees);
                    if (score > bestScenicScore)
                    {
                        bestScenicScore = score;
                    }
                }
            }

            Console.WriteLine($"Highest scenic score is {bestScenicScore}.");
        }

        private static bool IsVisible(int x, int y, int[,] trees)
        {
            // This isn't great code but I gave up

            var result = true;
            // Check from left
            for (var x2 = 0; x2 < x; x2++)
            {
                if (trees[x, y] <= trees[x2, y])
                {
                    result = false;
                    break;
                }
            }
            if (result)
            {
                return true;
            }
            result = true;

            // Check from right
            for (var x2 = trees.GetLength(1) - 1; x2 > x; x2--)
            {
                if (trees[x, y] <= trees[x2, y])
                {
                    result = false;
                    break;
                }
            }
            if (result)
            {
                return true;
            }
            result = true;

            // Check from top
            for (var y2 = 0; y2 < y; y2++)
            {
                if (trees[x, y] <= trees[x, y2])
                {
                    result = false;
                    break;
                }
            }
            if (result)
            {
                return true;
            }
            result = true;

            // Check from bottom
            for (var y2 = trees.GetLength(0) - 1; y2 > y; y2--)
            {
                if (trees[x, y] <= trees[x, y2])
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        private static int ScenicScore(int x, int y, int[,] trees)
        {
            var scores = new int[4];

            // Look up
            for (var y2 = y - 1; y2 >= 0; y2--)
            {
                scores[0]++;
                if (trees[x, y] <= trees[x, y2])
                {
                    break;
                }
            }

            // Look left
            for (var x2 = x - 1; x2 > 0; x2--)
            {
                scores[1]++;
                if (trees[x, y] <= trees[x2, y])
                {
                    break;
                }
            }

            // Look right
            for (var x2 = x + 1; x2 < trees.GetLength(0); x2++)
            {
                scores[2]++;
                if (trees[x, y] <= trees[x2, y])
                {
                    break;
                }
            }

            // Look down
            for (var y2 = y + 1; y2 < trees.GetLength(1); y2++)
            {
                scores[3]++;
                if (trees[x, y] <= trees[x, y2])
                {
                    break;
                }
            }

            var sc = scores[0] * scores[1] * scores[2] * scores[3];
            if (sc > 0)
            {
                Console.WriteLine($"{x},{y} has a score of {sc}");
            }
            return sc;
        }
    }
}