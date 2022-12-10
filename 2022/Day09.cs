namespace AoC
{
    class Day09
    {
        private static (int, int) START_POSITION = (11, 5);
        private static (int, int) positionH = START_POSITION;
        private static List<(int, int)> positions = new List<(int, int)> { START_POSITION };
        private static HashSet<(int, int)> visitedPositions = new HashSet<(int, int)>() { START_POSITION };
        public static void Solution()
        {
            var contents = Utils.ReadContents(9, false);

            // Uncomment for part 1
            //Solve(contents);

            // Part2
            positions = new List<(int, int)> { START_POSITION, START_POSITION, START_POSITION, START_POSITION, START_POSITION, START_POSITION, START_POSITION, START_POSITION, START_POSITION };
            visitedPositions = new HashSet<(int, int)>() { START_POSITION };
            Solve(contents);
        }

        private static void Solve(string contents)
        {
            var data = contents.Split("\n");
            foreach (var row in data)
            {
                Console.WriteLine($"== {row} ==");
                switch (row.Split(" "))
                {
                    case ["R", var amount]:
                        {
                            for (var i = 0; i < int.Parse(amount); i++)
                            {
                                positionH = (positionH.Item1 + 1, positionH.Item2);
                                MoveT();
                                PrintBoard();
                            }
                            break;
                        }
                    case ["L", var amount]:
                        {
                            for (var i = 0; i < int.Parse(amount); i++)
                            {
                                positionH = (positionH.Item1 - 1, positionH.Item2);
                                MoveT();
                                PrintBoard();
                            }
                            break;
                        }
                    case ["U", var amount]:
                        {
                            for (var i = 0; i < int.Parse(amount); i++)
                            {
                                positionH = (positionH.Item1, positionH.Item2 + 1);
                                MoveT();
                                PrintBoard();
                            }
                            break;
                        };
                    case ["D", var amount]:
                        {
                            for (var i = 0; i < int.Parse(amount); i++)
                            {
                                positionH = (positionH.Item1, positionH.Item2 - 1);
                                MoveT();
                                PrintBoard();
                            }
                            break;
                        };
                    default: throw new InvalidDataException();
                }
            }

            Console.WriteLine($"T visited {visitedPositions.Count} unique positions.");
        }

        private static void MoveT()
        {
            var observedPosition = positionH;
            for (var i = 0; i < positions.Count; i++)
            {
                var positionT = positions[i];
                if (i > 0)
                {
                    observedPosition = positions[i - 1];
                }

                var xDiff = observedPosition.Item1 - positionT.Item1;
                var yDiff = observedPosition.Item2 - positionT.Item2;

                // Diagonal movement
                if (Math.Abs(xDiff) > 1 && Math.Abs(yDiff) == 1)
                {
                    positionT.Item1 += (xDiff > 0) ? xDiff - 1 : xDiff + 1;
                    positionT.Item2 += yDiff;
                }
                else if (Math.Abs(yDiff) > 1 && Math.Abs(xDiff) == 1)
                {
                    positionT.Item2 += (yDiff > 0) ? yDiff - 1 : yDiff + 1;
                    positionT.Item1 += xDiff;
                }
                else if ((Math.Abs(yDiff), Math.Abs(xDiff)) == (2, 2))
                {
                    // Proper diagonal movement but with hardcoded variables
                    positionT.Item2 += (yDiff > 0) ? 1 : -1;
                    positionT.Item1 += (xDiff > 0) ? 1 : -1;
                }
                // Simple movement
                else
                if (Math.Abs(xDiff) > 1)
                {
                    positionT.Item1 += (xDiff > 0) ? xDiff - 1 : xDiff + 1;
                }
                else if (Math.Abs(yDiff) > 1)
                {
                    positionT.Item2 += (yDiff > 0) ? yDiff - 1 : yDiff + 1;
                }

                positions[i] = positionT;

                if (i == positions.Count - 1)
                {
                    visitedPositions.Add(positionT);
                }
            }
        }

        private static void PrintBoard()
        {
            return; // Remove for board printouts

            for (var y = 20; y >= 0; y--)
            {
                for (var x = 0; x < 26; x++)
                {
                    var validOtherPos = -1;
                    for (var i = 0; i < positions.Count; i++)
                    {
                        if (positions[i] == (x, y))
                        {
                            validOtherPos = validOtherPos = i;
                            break;
                        }
                    }
                    if (positionH.Item1 == x && positionH.Item2 == y)
                    {
                        Console.Write("H");
                    }
                    else if (validOtherPos >= 0)
                    {
                        Console.Write(validOtherPos + 1);
                    }
                    else if (START_POSITION == (x, y))
                    {
                        Console.Write("s");
                    }
                    else if (visitedPositions.Contains((x, y)))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n-----");
        }
    }
}