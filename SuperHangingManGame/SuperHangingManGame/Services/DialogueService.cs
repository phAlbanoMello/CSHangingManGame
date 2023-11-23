using System.Text;
using System.Text.RegularExpressions;
using static SuperHangingManGame.Services.ConsoleService;

namespace SuperHangingManGame.Services
{
    public static class DialogueService
    {

        private const string pattern = @"\*p\((\d+)\)"; //*p(int delayinMilliseconds)

        /// <summary>
        /// Display Messages with support for pause patterns on the strings that can be marked with *p(int delayInMilliseconds)
        /// </summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="typingDelay">Delay between each character</param>
        /// <param name="skipLine">If the cursor should go to the next line after displaying the message</param>
        /// <returns></returns>
        public static async Task DisplayMessage(string message, int typingDelay, AlignPosition alignment = AlignPosition.Middle)
        {
            string finalString = message;
            Pause[] pauses = ExtractPauses(message, out finalString);

            ConsoleService.AlignText(finalString, alignment);

            for (int i = 0; i < finalString.Length; i++)
            {
                foreach (var pause in pauses.Where(pause => i == pause._pauseIndex))
                {
                    await Task.Delay(pause._delay);
                }
                await Task.Delay(typingDelay);
                Console.Write(finalString[i]);
            }

            Console.WriteLine();
        }

        public static async Task DisplayMessageInLine(string message, int typingDelay)
        {
            string finalString = message;
            Pause[] pauses = ExtractPauses(message, out finalString);

            for (int i = 0; i < finalString.Length; i++)
            {
                foreach (var pause in pauses.Where(pause => i == pause._pauseIndex))
                {
                    await Task.Delay(pause._delay);
                }
                await Task.Delay(typingDelay);
                Console.Write(finalString[i]);
            }
        }

        public static async Task SkipLines(int lineCount)
        {
            for (int i = 0; i < lineCount; i++)
            {
                Console.WriteLine();
            }
        }

        public static Pause[] ExtractPauses(string input, out string finalString)
        {
            List<Pause> pauses = new List<Pause>();
            StringBuilder finalStringBuilder = new StringBuilder(input);

            Match match = Regex.Match(input, pattern);
            while (match.Success)
            {
                int pauseIndex = match.Index;
                int delay = int.Parse(match.Groups[1].Value);

                pauses.Add(new Pause(pauseIndex, delay));
                finalStringBuilder.Remove(pauseIndex, match.Length);
                match = Regex.Match(finalStringBuilder.ToString(), pattern);
            }
            finalString = finalStringBuilder.ToString();
            return pauses.ToArray();
        }
        public struct Pause
        {
            public int _pauseIndex;
            public int _delay;

            public Pause(int pauseIndex, int delay)
            {
                _pauseIndex = pauseIndex;
                _delay = delay;
            }
        }
    }
}