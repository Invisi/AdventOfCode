namespace AoC
{
    class Day03
    {
        public static void Solution()
        {
            var contents = Utils.ReadContents(3, false);

            var EMPTY_ITEM = '0';
            var sumPriority = 0;
            var sumGroupPriority = 0;
            var elfBackpacks = new List<char>[3];
            var elfCounter = 0;

            var backpacks = contents.Split("\n");
            foreach (var backpack in backpacks)
            {
                var backpackContents = backpack.ToCharArray();
                var compartmentA = backpackContents.Take(backpack.Length / 2);
                var compartmentB = backpackContents.Skip(backpack.Length / 2);

                var sameItem = EMPTY_ITEM;
                foreach (var item in compartmentA)
                {
                    if (compartmentB.Contains(item))
                    {
                        sameItem = item;
                        break;
                    }
                }

                if (sameItem == EMPTY_ITEM)
                {
                    Console.WriteLine($"Failed to find common item in backpack: {backpack}.");
                    continue;
                }

                var priority = LetterPriority(sameItem);
                sumPriority += priority;
                // Console.WriteLine($"{backpack} -> {sameItem} ({priority})");

                // Part 2, elf group logic
                elfBackpacks[elfCounter] = backpack.ToList();
                elfCounter += 1;
                if (elfCounter > 2)
                {
                    sameItem = EMPTY_ITEM;
                    foreach (var item in elfBackpacks[0])
                    {
                        if (elfBackpacks[1].Contains(item) && elfBackpacks[2].Contains(item))
                        {
                            sameItem = item;
                            break;
                        }
                    }

                    if (sameItem == EMPTY_ITEM)
                    {
                        Console.WriteLine("Failed to find common item in group backpacks.");
                        continue;
                    }

                    priority = LetterPriority(sameItem);
                    sumGroupPriority += priority;

                    // Reset vars
                    elfCounter = 0;
                    elfBackpacks = new List<char>[3];
                }
            }

            Console.WriteLine($"Sum of priorities: {sumPriority}.");
            Console.WriteLine($"Sum of group priorities: {sumGroupPriority}.");

        }

        private static int LetterPriority(char input)
        {
            var value = (int)input;
            // a-z [1,26]
            if (value >= 97)
            {
                return value - 97 + 1;
            }

            // A-Z [27,52]
            return value - 65 + 27;
        }
    }
}