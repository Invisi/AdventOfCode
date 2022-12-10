namespace AoC
{
    class Day10
    {
        public static void Solution()
        {
            var contents = Utils.ReadContents(10, false);

            var cpu = new CPU();

            var data = contents.Split("\n");
            foreach (var instruction in data)
            {
                switch (instruction.Split(" "))
                {
                    case ["noop"]:
                        cpu.Tick();
                        break;
                    case ["addx", var amount]:
                        cpu.AddX(int.Parse(amount));
                        break;
                    default: throw new InvalidDataException();
                }
            }

            Console.WriteLine($"Sum of all signal strengths is {cpu.sumSignalStrength}.");
        }
    }

    class CPU
    {
        private int clock = 0;
        private int x = 1;

        private List<int> recordedStrengths = new List<int>();

        public int sumSignalStrength
        {
            get
            {
                return recordedStrengths.Sum();
            }
        }

        public void Tick()
        {
            clock += 1;

            if (clock == 20 || (clock - 20) % 40 == 0)
            {
                recordedStrengths.Add(clock * x);
                // Console.WriteLine($"{clock}: {clock * x}");
            }

            if (x <= (clock % 40) && (clock % 40) <= x + 2)
            {
                Console.Write("#");
            }
            else
            {
                Console.Write(".");
            }
            if (clock % 40 == 0)
            {
                Console.WriteLine("");
            }
        }

        public void AddX(int amount)
        {
            Tick();
            Tick();
            x += amount;
        }
    }
}