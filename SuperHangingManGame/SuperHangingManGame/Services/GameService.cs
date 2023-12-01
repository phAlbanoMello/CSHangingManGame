using SuperHangingManGame.Components;
using SuperHangingManGame.Data.Constants;
using SuperHangingManGame.Interfaces;
using SuperHangingManGame.Services.Display;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace SuperHangingManGame.Services
{
    public class GameService : IGameService
    {
        private IGateManager _gateManager;
        private TextManager _textManager;

        private int gameSpeed = 1;
        private int currentDialogueIndex;
        private int remainingTries = 3;
        private ConsoleColor defaultColor = ConsoleColor.Cyan;
        private List<CursorPoint> cursorPoints = new List<CursorPoint>();
        private char[] guessedLetters;

        private int[] frequency = new int[] { 440, 494, 523, 440, 392, 440, 494, 523, 440, 494, 523, 440, 494, 523, 440, 392}; // A4, B4, C5, A4, G4, A4, B4, C5
        private int[] duration = new int[] { 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500}; // Durations in milliseconds

        private string[] logoArray = new string[]
        {
            "-----------------||||||||||||||||||||||||-----------------",
            "|||||||@@@@||||||||||||",
            "||||||@@@--|@@@@@@@|||||",
            "||||@@||@@@|||||||@@||||",
            "--------|--------|||@@||||||||||||||@@|||--------|--------",
            "||@@||||||||||||||||@@||",
            "|@@||||||||||||||||||@@|",
            "|@@||||||||||||||||||@@|",
            "||@@||||||||||||||||@@||",
            "--------|--------|||@@||||||||||||||@@|||--------|--------",
            "||||@@||||||||||||@@||||",
            "||||||@@||||||||@@||||||",
            "||||||||@@@@@@@@||||||||",
            "-----------------||||||||||||||||||||||||-----------------"
        };

        public GameService(IGateManager gateManager)
        {
            _gateManager = gateManager;
            _textManager = new TextManager();

        }

        public void SetTypingBaseSpeed(int speed)
        {
            if (speed <= 0)
            {
                speed = 1;
            }
            gameSpeed = speed;
        }

        private int Delay(int delayInMilliseconds)
        {
            return delayInMilliseconds / gameSpeed;
        }

        private async Task PlaySong(int[] freq, int[] duration)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < freq.Length; i++)
                {
                    Console.Beep(freq[i], duration[i]);
                }
            });
        }

        public async Task StartGame()
        {
            string[] secretWords = { "Tree" };
            Theme theme = new Theme(ConsoleColor.Green, "Nature", secretWords);
            Lock[] locks = { new Lock(0, secretWords[0]) };
            Gate gate = new Gate(theme, locks, 1, 0);
            Gate[] gates = { gate };
            ConsoleService.SetFontColor(defaultColor);
            
            //PlaySong(frequency, duration);

            await DrawLogo();

            await PlayIntro();

            await StartChallenges(gates);

            //await DialogueService.DisplayMessage($"{gate.Theme.Name}", Delay(100));
            //await DialogueService.SkipLines(1);
            //await DialogueService.DisplayMessage($"And it has*p(200) {gate.GetLocks().Length} Locks", Delay(30));
            //await DialogueService.SkipLines(1);

            //await DrawingService.DrawGate(gate, gameSpeed);

            //await DialogueService.SkipLines(3);
            //await DrawingService.DrawGuessingField(secretWords[0].Length);
            //await DialogueService.SkipLines(3);
        }
        private async Task PlayGate()
        {

        }

        private async Task DrawLogo()
        {
            await DialogueService.SkipLines(1);
            for (int i = 0; i < logoArray.Length; i++)
            {
                await DialogueService.DisplayMessage(logoArray[i], Delay(1));
            }
            await DialogueService.SkipLines(1);
        }

        private async Task StartChallenges(Gate[] gates)
        {
            for (int i = 0; i < gates.Length; i++)
            {
                await DialogueService.DisplayMessage(Messages.GATE_THEME_PRESENTATION, Delay(25));
                ConsoleService.SetFontColor(gates[i].Theme.Color);
                await DialogueService.DisplayMessage(gates[i].Theme.Name, Delay(25));

                Lock[] locks = gates[i].GetLocks();
                await DialogueService.SkipLines(1);
                cursorPoints.Add(new CursorPoint(ConsoleService.GetCursorPosition(), "Gate"));
                await DrawingService.DrawGate(gates[i], Delay(25));
                await DialogueService.SkipLines(3);
                ConsoleService.SetFontColor(defaultColor);

                for (int j = 0; j < locks.Length; j++)
                {
                    string secretWord = locks[j].SecretWord;
                    guessedLetters = new char[secretWord.Length];
                    await DialogueService.DisplayMessage(Messages.GUESS_CUE, Delay(25));
                    cursorPoints.Add(new CursorPoint(ConsoleService.GetCursorPosition(), "GuessingField"));
                    await DrawingService.DrawGuessingField(guessedLetters, secretWord);
                    await DialogueService.SkipLines(2);
                    await RunGame(secretWord);
                }

                //display gate's theme
                //draw gate
                //display guess cue
                //display guessing field
                //wait for player input
                //validate player input
                //if right, change the letter slot to the player guess
                //if wrong, display wrong guess message and warn about remaining tries
                //wait for the player guess until they either hit or miss the letter
            }
        }

        private async Task PlayIntro()
        {
            await DialogueService.SkipLines(2);
            await DialogueService.DisplayMessage(Messages.WELCOME_MESSAGE, Delay(10));
            await DialogueService.SkipLines(1);
            for (int i = 0; i < Messages.INTRO.Length; i++)
            {
                await DialogueService.DisplayMessage(Messages.INTRO[i], Delay(10));
            }
        }

        public static int GetNumberOfTries(string word)
        {
            return (int)Math.Ceiling(word.Length / 2.0);
        }

        private CursorPoint GetCursorPoint(string label)
        {
            Debug.WriteLine("Getting Cursor Point");
            if (cursorPoints == null)
            {
                Debug.WriteLine(" Null Cursor Point");
                throw new ArgumentNullException("CursorPoints list is null");
            }
            CursorPoint cursorPoint = cursorPoints.FirstOrDefault(item => item.label == label);
            Debug.WriteLine($"Returning : {cursorPoint.label}");
            return cursorPoint;
        }

        public void ClearLastLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public async Task RunGame(string secretWord)
        {
            string secretWordUpper = secretWord.ToUpper();
            remainingTries = (int)(secretWord.Length / 2);

            while (remainingTries > 0)
            {
                //DisplayGameState();
                char guess = char.ToUpper(Console.ReadKey().KeyChar);
    
                await DialogueService.SkipLines(1);
                //TODO : create function to add if empty and set if not.
                cursorPoints.Add(new CursorPoint(ConsoleService.GetCursorPosition(), "Dialogue"));

                if (!char.IsLetter(guess))
                {
                    await DialogueService.DisplayMessage("Enter a valid letter", Delay(25));
                    continue;
                }



                if (secretWordUpper.Contains(guess))
                {
                    UpdateGuessedLetters(guess, secretWordUpper);
                    ConsoleService.SetCursorPosition(GetCursorPoint("GuessingField"));
                    await DrawingService.DrawGuessingField(guessedLetters, secretWordUpper);
            
                    ConsoleService.SetCursorPosition(GetCursorPoint("Dialogue"));

                    if (IsWordGuessed(secretWordUpper))
                    {
                        await DialogueService.DisplayMessage(Messages.RIGHT_GUESS, Delay(25));
                    }
                    continue;
                }
                else
                {
                    remainingTries--;
                    await DialogueService.DisplayMessage(Messages.WRONG_GUESS, Delay(25));
                    await DialogueService.DisplayMessage($"{Messages.REMAINING_TRIES} {remainingTries}", Delay(25));
                }
            }

            if (remainingTries == 0)
            {
                Console.WriteLine($"Sorry, you ran out of tries. The correct word was: {secretWord}");
            }
        }
        private void DisplayGameState()
        {
            Console.WriteLine($"Word: {string.Join(" ", guessedLetters)}");
            Console.WriteLine($"Remaining Tries: {remainingTries}");
        }
        private void UpdateGuessedLetters(char guess, string secretWord)
        {
            for (int i = 0; i < secretWord.Length; i++)
            {
                if (secretWord[i] == guess)
                {
                    guessedLetters[i] = guess;
                }
            }
        }

        private bool IsWordGuessed(string secretWord)
        {
            return guessedLetters.ToString() == secretWord;
        }

        //private async Task PlayGate(Gate gate)
        //{
        //    string message = Messages.MESSAGES[currentDialogueIndex];
        //    _textManager.CreateNewText(message, ConsoleService.GetCursorPosition());
        //    await DialogueService.DisplayMessage(Messages.MESSAGES[currentDialogueIndex], delay);
        //    currentDialogueIndex++;
        //}
    }
}
