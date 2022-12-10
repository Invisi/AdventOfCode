namespace AoC
{
    class Day01
    {
        public static void Solution()
        {
            var contents = Utils.ReadContents(1, false);

            var sumInventories = new List<int>();

            // Find elves' inventory
            var inventories = contents.Split("\n\n");
            foreach (var inventory in inventories)
            {
                var sumOfInventory = 0;

                // Sum up inventory
                foreach (var item in inventory.Split("\n"))
                {
                    var value = 0;
                    if (!Int32.TryParse(item, out value))
                    {
                        Console.WriteLine($"Failed to parse integer: {item}");
                    }
                    sumOfInventory += value;
                }

                sumInventories.Add(sumOfInventory);
            }

            sumInventories.Sort();
            sumInventories.Reverse();
            Console.WriteLine($"Highest amount carried is {sumInventories[0]}.");
            Console.WriteLine($"Top three inventories: {sumInventories[0]}, {sumInventories[1]}, {sumInventories[2]} => {sumInventories[0] + sumInventories[1] + sumInventories[2]}.");
        }
    }
}