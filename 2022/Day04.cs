namespace AoC
{
    class Day04
    {
        public static void Solution()
        {
            var contents = Utils.ReadContents(4, false);

            var matchingJobs = 0;
            var overlappingJobs = 0;

            var data = contents.Split("\n");
            foreach (var line in data)
            {
                var lineData = ParseLine(line);

                if ((lineData.Item1 >= lineData.Item3 && lineData.Item2 <= lineData.Item4) || (lineData.Item3 >= lineData.Item1 && lineData.Item4 <= lineData.Item2))
                {
                    matchingJobs += 1;
                    overlappingJobs += 1;
                }
                else if (lineData.Item3 <= lineData.Item1 && lineData.Item1 <= lineData.Item4 || lineData.Item1 <= lineData.Item3 && lineData.Item3 <= lineData.Item2)
                {
                    overlappingJobs += 1;
                }
            }

            Console.WriteLine($"{matchingJobs} jobs fully overlap.");
            Console.WriteLine($"{overlappingJobs} jobs overlap partially or fully.");
        }

        private static (int, int, int, int) ParseLine(string line)
        {
            var data = line.Split(",");
            var data1 = data[0].Split("-");
            var data2 = data[1].Split("-");

            int start1 = 0;
            int start2 = 0;
            int end1 = 0;
            int end2 = 0;

            int.TryParse(data1[0], out start1);
            int.TryParse(data2[0], out start2);
            int.TryParse(data1[1], out end1);
            int.TryParse(data2[1], out end2);

            return (start1, end1, start2, end2);
        }
    }
}