namespace AoC
{
    class Day06
    {
        public static void Solution()
        {
            var contents = Utils.ReadContents(6, false);

            Solve(contents, 4);
            Solve(contents, 14);
        }

        private static void Solve(string contents, int distinctLetters)
        {
            var buffer = new CircularBuffer(distinctLetters);

            var data = contents.ToCharArray();
            for (var i = 0; i < data.Length; i++)
            {
                var character = data[i];
                buffer.Insert(character);

                // Skip uniqueness check until buffer is sufficiently filled
                if (i < distinctLetters - 1)
                {
                    continue;
                }

                if (buffer.IsUnique())
                {
                    Console.WriteLine($"Unique patter is {string.Join("", buffer.data)} after {i + 1}");
                    break;
                }
            }
        }
    }

    class CircularBuffer
    {
        public char[] data;
        private int pointer = 0;

        public CircularBuffer(int distinctLetters)
        {
            data = new char[distinctLetters];
        }
        public void Insert(char value)
        {
            data[pointer] = value;
            pointer += 1;

            // Wrap around
            if (pointer >= data.Length)
            {
                pointer = 0;
            }
        }
        public bool IsUnique()
        {
            var tmp = new HashSet<char>();

            foreach (var value in data)
            {
                // tmp already contains this value
                if (tmp.Contains(value))
                {
                    return false;
                }

                tmp.Add(value);
            }

            return true;
        }
    }
}