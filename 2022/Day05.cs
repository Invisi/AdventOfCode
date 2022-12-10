namespace AoC
{
    class Day05
    {
        public static void Solution()
        {
            var contents = Utils.ReadContents(5, false);

            // Toggle to get the solution for part 2 (moving multiple crates at the same time)
            var part2Behaviour = true;

            var data = contents.Split("\n\n");
            var stackData = data[0].Split("\n");

            // Init amount of stacks
            var stacks = new Stack<char>[stackData[0].Length / 4 + 1];
            var stackLists = new List<char>[stackData[0].Length / 4 + 1];
            for (var i = 0; i < stacks.Length; i++)
            {
                stacks[i] = new Stack<char>();
                stackLists[i] = new List<char>();
            }

            // Iterate stacks, skip last line since we don't need it
            for (var i = 0; i < stackData.Length - 1; i++)
            {
                var line = stackData[i];

                for (var k = 0; k <= line.Length / 4; k += 1)
                {
                    var index = k * 4;
                    var letter = line.Skip(index).Take(3).ToList()[1];
                    if (letter != ' ')
                    {
                        stackLists[k].Add(letter);
                    }
                }
            }

            // Insert into stack in proper order
            // Could be optimized by iterating from the back instead of front
            for (var i = 0; i < stackLists.Length; i++)
            {
                stackLists[i].Reverse();
                foreach (var item in stackLists[i])
                {
                    stacks[i].Push(item);
                }
            }

            // Execute moves
            foreach (var move in data[1].Split("\n"))
            {
                if (move.Split(" ") is [_, var amount, _, var source, _, var target])
                {
                    var iAmount = int.Parse(amount);
                    var iSource = int.Parse(source);
                    var iTarget = int.Parse(target);

                    if (part2Behaviour && iAmount > 1)
                    {
                        // Move crates onto temporary stack to simulate grabbing multiple (preserves order)
                        var tempStack = new Stack<char>();
                        for (var i = 0; i < iAmount; i++)
                        {
                            tempStack.Push(stacks[iSource - 1].Pop());
                        }
                        for (var i = 0; i < iAmount; i++)
                        {
                            stacks[iTarget - 1].Push(tempStack.Pop());
                        }
                    }
                    // Move crate piece by piece
                    else
                    {
                        for (var i = 0; i < iAmount; i++)
                        {
                            stacks[iTarget - 1].Push(stacks[iSource - 1].Pop());
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Failed to understand move set: {move}");
                }
            }

            // Output top of each stack
            Console.Write("Resulting crate list: ");
            foreach (var stack in stacks)
            {
                Console.Write(stack.First());
            }
            Console.WriteLine();
        }
    }
}
