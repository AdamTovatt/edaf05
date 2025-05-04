namespace Tests.Helpers
{
    public readonly struct TestCasePath
    {
        public string InputPath { get; }
        public string AnswerPath { get; }

        private TestCasePath(string inputPath, string answerPath)
        {
            InputPath = inputPath;
            AnswerPath = answerPath;
        }

        public static TestCasePath From(string basePath, string subDirectory, string fileNameWithoutExtension)
        {
            string dir = Path.Combine(basePath, subDirectory);
            string input = Path.Combine(dir, $"{fileNameWithoutExtension}.in");
            string answer = Path.Combine(dir, $"{fileNameWithoutExtension}.ans");
            return new TestCasePath(input, answer);
        }
    }
}
