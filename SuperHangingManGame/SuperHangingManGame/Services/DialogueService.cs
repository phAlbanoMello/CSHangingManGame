using static System.Net.Mime.MediaTypeNames;

namespace SuperHangingManGame.Services
{
    public static class DialogueService
    {
        private static int typingDelay = 50;

        public static async Task DisplayMessage(string message, Pause? pause)
        {//TODO: Make it receive an array of pauses
            int currentIndex = 0;
            int currentWordIndex = 0;

            if (pause == null)
            {
                pause = new Pause("", 0);
            }

            while (currentIndex < message.Length)
            {
                Console.Write(message[currentIndex]);

                if (char.IsWhiteSpace(message[currentIndex]) || currentIndex == message.Length - 1)
                {
                    string currentWord = message.Substring(currentWordIndex, currentIndex - currentWordIndex + 1).Trim();
                    if (string.Equals(currentWord, pause.Value._word, StringComparison.OrdinalIgnoreCase))
                    {
                        await Task.Delay(pause.Value._delay); // Pause after the specified word
                    }
                    currentWordIndex = currentIndex + 1; // Move to the next word
                }

                await Task.Delay(typingDelay);
                currentIndex++;
            }

            Console.WriteLine();





           // string[] words = message.Split(' ');

           //  foreach (string word in words)
           //  {
           //     Console.Write(word + " ");

           //     if (string.Equals(word.Trim(new[] { '.', ',', '!', '?' }), pauseWord, StringComparison.OrdinalIgnoreCase))
           //     {
           //         await Task.Delay(speed * 5); // Pause after the specified word
           //     }
           //     else
           //     {
           //         await Task.Delay(speed);
           //     }
           //  }
         




           // for (int i = 0; i < message.Length; i++)
           // {
           //     if (pause != null)
           //     {
           //         if (message[i] == ' ' || i == message.Length - 1)
           //         {
           //             string currentWord = message.Substring(0, i + 1).Trim();
           //             if (string.Equals(currentWord, pause.Value._word, StringComparison.OrdinalIgnoreCase))
           //             {
           //                 await Task.Delay(pause.Value._delay);
           //             }
           //         }
           //     }
           //     Console.Write(message[i]);
           //     await Task.Delay(typingDelay);
           // }
           //Console.WriteLine();
        }

        public static async Task SkipLines(int lineCount)
        {
            for (int i = 0; i < lineCount; i++)
            {
                Console.WriteLine();
            }
        }
        public struct Pause
        {
            public string _word;
            public int _delay;

            /// <summary>
            /// Struct to be used on the typing behaviour.
            /// </summary>
            /// <param name="index">Index at which to pause the typing</param>
            /// <param name="delay">wait in milliseconds before resume typing</param>
            public Pause(string word, int delay)
            {
                _word = word;
                _delay = delay;
            }
        }
    }
}