namespace AoC
{
    class Day02
    {
        public static void Solution()
        {
            var contents = Utils.ReadContents(2, false);

            var totalWinScore = 0;
            var totalBestScore = 0;

            var actions = contents.Split("\n");
            foreach (var action in actions)
            {

                var opponent = (OpponentMove)(int)action[0];
                var self = (MyMove)(int)action[2];
                totalWinScore += CalculateScore(opponent, self);

                var bestMove = CalculateMove(opponent, self);
                totalBestScore += CalculateScore(opponent, bestMove);
            }

            Console.WriteLine($"Total score is {totalWinScore}.");
            Console.WriteLine($"Total score when using the real strategy is {totalBestScore}.");
        }

        private enum OpponentMove
        {
            Rock = 'A',
            Paper = 'B',
            Scissors = 'C'
        }

        private enum MyMove
        {
            RockAkaLose = 'X',
            PaperAkaDraw = 'Y',
            ScissorsAkaWin = 'Z'
        }

        private static int CalculateScore(OpponentMove opponent, MyMove self)
        {
            var moveScore = 0;

            // Add score for my move
            switch (self)
            {
                case MyMove.RockAkaLose: moveScore += 1; break;
                case MyMove.PaperAkaDraw: moveScore += 2; break;
                case MyMove.ScissorsAkaWin: moveScore += 3; break;
            }

            // Add score for win
            return moveScore + WinScore(opponent, self);
        }

        private static int WinScore(OpponentMove opponent, MyMove self)
        {
            if ((opponent == OpponentMove.Paper && self == MyMove.ScissorsAkaWin) || (opponent == OpponentMove.Rock && self == MyMove.PaperAkaDraw) || (opponent == OpponentMove.Scissors && self == MyMove.RockAkaLose))
            {
                // Win
                return 6;
            }
            else if ((opponent == OpponentMove.Paper && self == MyMove.PaperAkaDraw) || (opponent == OpponentMove.Rock && self == MyMove.RockAkaLose) || (opponent == OpponentMove.Scissors && self == MyMove.ScissorsAkaWin))
            {
                // Draw
                return 3;
            }
            // Loss
            return 0;
        }

        private static MyMove CalculateMove(OpponentMove opponent, MyMove self)
        {
            // Force a loss
            if (self == MyMove.RockAkaLose)
            {
                switch (opponent)
                {
                    case OpponentMove.Rock: return MyMove.ScissorsAkaWin;
                    case OpponentMove.Paper: return MyMove.RockAkaLose;
                    default: return MyMove.PaperAkaDraw; // Scissors case
                }
            }
            // Force a draw
            else if (self == MyMove.PaperAkaDraw)
            {
                switch (opponent)
                {
                    case OpponentMove.Rock: return MyMove.RockAkaLose;
                    case OpponentMove.Paper: return MyMove.PaperAkaDraw;
                    default: return MyMove.ScissorsAkaWin; // Scissors case
                }
            }

            // Force a win
            switch (opponent)
            {
                case OpponentMove.Rock: return MyMove.PaperAkaDraw;
                case OpponentMove.Paper: return MyMove.ScissorsAkaWin;
                default: return MyMove.RockAkaLose; // Scissors case
            }
        }
    }
}