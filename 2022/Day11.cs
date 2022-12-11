namespace AoC
{
    class Day11
    {
        public static void Solution()
        {
            var contents = Utils.ReadContents(11, false);

            var monkeyRegistry = new List<Monkey>();

            var data = contents.Split("\n");

            var monkeyName = 0;
            var monkeyItems = new List<Int64>();
            Int64 monkeyTest = 0;
            Int64 factor = 1;
            var monkeyYes = 0;
            var monkeyNo = 0;
            Func<Int64, Int64> monkeySee = (value) => 0;

            foreach (var line in data)
            {
                switch (line.TrimStart().Split(" "))
                {
                    case ["Monkey", var index]:
                        {
                            var iIndex = int.Parse(index.Replace(":", ""));
                            monkeyName = iIndex;
                            break;
                        }
                    case ["Starting", "items:", ..]:
                        {
                            var items = line.Trim().Replace("Starting items: ", "").Split(",").Select(a => Int64.Parse(a));
                            monkeyItems = new List<Int64>();
                            monkeyItems.AddRange(items);
                            break;
                        }
                    case ["Operation:", "new", _, "old", var action, var amount]:
                        {
                            if (action == "*")
                            {
                                if (amount == "old")
                                {
                                    monkeySee = (value) => value * value;
                                }
                                else
                                {
                                    monkeySee = (value) => value * Int64.Parse(amount);
                                }
                            }
                            else if (action == "+")
                            {
                                if (amount == "old")
                                {
                                    monkeySee = (value) => value + value;
                                }
                                else
                                {
                                    monkeySee = (value) => value + Int64.Parse(amount);
                                }
                            }
                            break;
                        }
                    case ["Test:", "divisible", "by", var amount]:
                        {
                            monkeyTest = Int64.Parse(amount);
                            factor *= monkeyTest;
                            break;
                        }
                    case ["If", var clause, _, _, _, var target]:
                        {
                            if (clause == "true:")
                            {
                                monkeyYes = int.Parse(target);
                            }
                            else
                            {
                                monkeyNo = int.Parse(target);
                                monkeyRegistry.Add(new Monkey(monkeyName, monkeyYes, monkeyNo, monkeyTest, monkeyItems, monkeySee));
                            }
                            break;
                        }
                }
            }

            // Part 1
            // for (var i = 0; i < 20; i++)
            // {
            //     Console.WriteLine($"\n\n\nRound {i + 1}");
            //     foreach (var monkey in monkeyRegistry)
            //     {
            //         monkey.Business(monkeyRegistry, true);
            //     }
            //     foreach (var monkey in monkeyRegistry)
            //     {
            //         Console.WriteLine($"Monkey {monkey.name} ({monkey.inspectedItems}): {string.Join(", ", monkey.items)}");
            //     }
            // }

            // Part 2
            for (var i = 0; i < 10000; i++)
            {
                foreach (var monkey in monkeyRegistry)
                {
                    monkey.Business(monkeyRegistry, false, factor);
                }
                if ((i + 1) % 1000 == 0)
                {
                    Console.WriteLine($"\n\n\nRound {i + 1}");
                    foreach (var monkey in monkeyRegistry)
                    {
                        Console.WriteLine($"Monkey {monkey.name} ({monkey.inspectedItems}): {string.Join(", ", monkey.items)}");
                    }
                }
            }

            var topMonkeyBusiness = monkeyRegistry.OrderBy(a => a.inspectedItems).Reverse().Take(2).Select(a => a.inspectedItems).Aggregate((a, x) => a * x);
            Console.WriteLine($"The two top monkeys had a business level of {topMonkeyBusiness}");
        }
    }

    class Monkey
    {
        public int name;
        public List<Int64> items;
        public Int64 monkeyDo;
        public Func<Int64, Int64> monkeySee;
        public int targetYes = 0;
        public int targetNo = 0;

        public Int64 inspectedItems;
        public Monkey(int name, int yes, int no, Int64 monkeyDo, List<Int64> items, Func<Int64, Int64> see)
        {
            this.items = new List<Int64>();
            this.name = name;
            this.targetYes = yes;
            this.targetNo = no;
            this.monkeyDo = monkeyDo;
            this.items = items;
            monkeySee = see;
        }

        public void Business(List<Monkey> registry, bool relief, Int64 factor = 0)
        {
            MonkeyLog($"Monkey {name}:");
            foreach (var item in items)
            {
                MonkeyLog($"\tMonkey inspects an item with {item}");
                Int64 inspected = monkeySee(item);
                MonkeyLog($"\tLevel is multiplied to {inspected}");
                if (relief)
                {
                    inspected = (Int64)Math.Floor((decimal)inspected / 3);
                    MonkeyLog($"\tMonkey is bored, level drops to {inspected}");
                }
                else
                {
                    inspected %= factor;
                }
                var action = inspected % monkeyDo == 0;
                MonkeyLog($"\tDivision result is {action}");
                if (action)
                {
                    MonkeyLog($"\tMonkey throws item to {targetYes}");
                    registry[targetYes].items.Add(inspected);
                }
                else
                {
                    MonkeyLog($"\tMonkey throws item to {targetNo}");
                    registry[targetNo].items.Add(inspected);
                }
            }
            inspectedItems += items.Count;
            items.Clear();
        }

        public void MonkeyLog(string l)
        {
            // Console.WriteLine(l);
        }
    }
}