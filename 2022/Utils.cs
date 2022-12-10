namespace AoC
{
    class Utils
    {
        public static string ReadContents(int day, bool sample)
        {
            var currentDir = AppDomain.CurrentDomain.BaseDirectory;

            var fileName = $"day{day:d2}.txt";
            if (sample)
            {
                fileName = fileName.Replace(".txt", ".sample.txt");
            }

            return System.IO.File.ReadAllText(System.IO.Path.Combine(currentDir, $@"../../../res/{fileName}")).TrimEnd();
        }
    }
}
