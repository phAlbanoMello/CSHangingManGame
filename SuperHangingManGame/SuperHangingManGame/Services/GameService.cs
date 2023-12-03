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
        private bool isGuessingTime = false;

        private int[] frequency = new int[] { 440, 494, 523, 440, 392, 440, 494, 523, 440, 494, 523, 440, 494, 523, 440, 392}; // A4, B4, C5, A4, G4, A4, B4, C5
        private int[] duration = new int[] { 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500}; // Durations in milliseconds

        private string[] logoArray = new string[]
        {
            "||----------------||||||||||||||||||||||||----------------||",
            "||                |||||||@@@@|||||||||||||                ||",
            "||                ||||||@@@--|@@@@@@@|||||                ||",
            "||                ||||@@||@@@|||||||@@||||                ||",
            "||-------|--------|||@@||||||||||||||@@|||--------|-------||",
            "||                ||@@||||||||||||||||@@||                ||",
            "||                |@@||||||||||||||||||@@|                ||",
            "||                |@@||||||||||||||||||@@|                ||",
            "||                ||@@||||||||||||||||@@||                ||",
            "||-------|--------|||@@||||||||||||||@@|||--------|-------||",
            "||                ||||@@||||||||||||@@||||                ||",
            "||                ||||||@@||||||||@@||||||                ||",
            "||                ||||||||@@@@@@@@||||||||                ||",
            "||----------------||||||||||||||||||||||||----------------||"
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
            string[] secretWords = { "Tree", "Mountain", "Cave" };
            Theme theme = new Theme(ConsoleColor.Green, "Nature", secretWords);
            Lock[] locks = { new Lock(0, secretWords[0]), new Lock(1, secretWords[1]), new Lock(2, secretWords[2]) };
            
            string[] secretWordsB = { "Asphalt", "Street", "Building", "Traffic" };
            Theme themeB = new Theme(ConsoleColor.Blue, "City", secretWordsB);
            Lock[] locksB = { new Lock(0, secretWordsB[0]), new Lock(1, secretWordsB[1]), new Lock(2, secretWordsB[2]), new Lock(3, secretWordsB[3])};
            Gate gate = new Gate(theme, locks, 1, 0);
            Gate gateB = new Gate(themeB, locksB, 2, 1);
           
            Gate[] gates = { gate, gateB };

            _gateManager.LoadGates(gates);

            ConsoleService.SetFontColor(defaultColor);

            PlaySong(frequency, duration);

            await DrawLogo();

            await PlayIntro();

            await StartChallenges(gates);
            await DialogueService.SkipLines(5);
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
        private async Task DrawGate(Gate gate)
        {
            ConsoleService.SetCursorPosition(GetCursorPoint("Gate"));
            await DrawingService.DrawGate(gate, Delay(25));
            await DialogueService.SkipLines(1);
            ConsoleService.SetFontColor(defaultColor);
        }

        private async Task StartChallenges(Gate[] gates)
        {
            for (int i = 0; i < gates.Length; i++)
            {
                int lockBreackThreshold = gates[i].GetLocks().Length / 2;
                AddCursorPoint("ThemeIntro");
                ClearLineAtPoint(GetCursorPoint("ThemeIntro"));
                ConsoleService.SetCursorPosition(GetCursorPoint("ThemeIntro"));
                ConsoleService.SetFontColor(gates[i].Theme.Color);
                await DialogueService.DisplayMessage(Messages.GATE_THEME_PRESENTATION, Delay(25));
                AddCursorPoint("ThemeName");
                ClearLineAtPoint(GetCursorPoint("ThemeName"));
                ConsoleService.SetCursorPosition(GetCursorPoint("ThemeName"));
                await DialogueService.DisplayMessage(gates[i].Theme.Name, Delay(80));

                Lock[] locks = gates[i].GetLocks();
                await DialogueService.SkipLines(1);
                AddCursorPoint("Gate");
                ClearLineAtPoint(GetCursorPoint("Gate"));
                await DrawGate(gates[i]);
                ConsoleService.SetFontColor(defaultColor);

                for (int j = 0; j < locks.Length; j++)
                {
                    string secretWord = locks[j].SecretWord;
                    guessedLetters = new char[secretWord.Length];
                    await DialogueService.DisplayMessage(Messages.GUESS_CUE, Delay(25));
                    AddCursorPoint("GuessingField");
                    ClearLineAtPoint(GetCursorPoint("GuessingField"));
                    await DrawingService.DrawGuessingField(guessedLetters, secretWord);
                    await DialogueService.SkipLines(2);
                    await RunGame(secretWord);
                    await DrawGate(gates[i]);
                    //Next lock
                }
                int brokenLocks = _gateManager.GetBrokenLocksCount(gates[i]);
                if (brokenLocks > lockBreackThreshold)
                {
                    ConsoleService.SetFontColor(ConsoleColor.Red);
                    await SetDialogue("You*p(100) will*p(100) rot*p(100) here...", Delay(150));
                    await DialogueService.SkipLines(3);
                    //TODO: GameOver here
                    //Ask to try again or close.

                }
                else
                {
                    ConsoleService.SetFontColor(gates[i].Theme.Color);
                    await SetDialogue($"The {gates[i].Theme.Name} gate is *p(100)open", Delay(150));
                    await DialogueService.SkipLines(2);
                }
                //Next gate
            }
            //Gates finished
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

        private static int GetNumberOfTries(string word)
        {
            return (int)Math.Ceiling(word.Length / 2.0);
        }

        public void ClearLineAtPoint(CursorPoint cursorPoint)
        {
            ConsoleService.SetCursorPosition(cursorPoint);
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.BufferWidth));
            ConsoleService.SetCursorPosition(cursorPoint);
        }

        private void AddCursorPoint(string label)
        {
            bool hasCursorPoint = cursorPoints.FirstOrDefault((item)=> item.label == label) != null;
            if (!hasCursorPoint) {
                cursorPoints.Add(new CursorPoint(ConsoleService.GetCursorPosition(), label));
            }
        }

        private CursorPoint GetCursorPoint(string label)
        {
            CursorPoint cursorPoint = cursorPoints.FirstOrDefault((item) => item.label == label);
            if (cursorPoint != null) return cursorPoint;
            return new CursorPoint();
        }

        private async Task SetDialogue(string message, int delay)
        {
            CursorPoint dialoguePoint = GetCursorPoint("Dialogue");
            ClearLineAtPoint(dialoguePoint);
            ConsoleService.SetCursorPosition(dialoguePoint);
            await DialogueService.DisplayMessage(message, delay);
        }
        private async Task SetGuessingField(char[] guessedLetters, string secretWordUpper)
        {
            CursorPoint guessingPoint = GetCursorPoint("GuessingField");
            ClearLineAtPoint(guessingPoint);
            ConsoleService.SetCursorPosition(guessingPoint);
            await DrawingService.DrawGuessingField(guessedLetters, secretWordUpper);
        }

        public async Task RunGame(string secretWord)
        {
            string secretWordUpper = secretWord.ToUpper();
            remainingTries = GetNumberOfTries(secretWord);
           
            AddCursorPoint("TryCount");
            ClearLineAtPoint(GetCursorPoint("TryCount"));
            await DialogueService.DisplayMessage($"Remaining Tries : *p(1200){remainingTries}", Delay(80));
            
            await Task.Delay(1000);
            await DialogueService.SkipLines(1);
            
            AddCursorPoint("Dialogue");
            await SetDialogue("Enter your guess", Delay(25));
            
            while (remainingTries > 0)
            {
                char guess = ' ';
                isGuessingTime = true;
                if (isGuessingTime)
                {
                    guess = char.ToUpper(Console.ReadKey().KeyChar);
                }
                isGuessingTime = false;
                //TODO : Find a way to stop the player from pressing the guess multiple times
                await DialogueService.SkipLines(1);
                
                if (!char.IsLetter(guess))
                {
                    await SetDialogue("That's not even a letter", Delay(25));
                    continue;
                }

                if (secretWordUpper.Contains(guess))
                {
                    UpdateGuessedLetters(guess, secretWordUpper);
                    await SetGuessingField(guessedLetters, secretWordUpper);
                    await Task.Delay(500);
                    await SetDialogue("That's right.", Delay(25));
                    await Task.Delay(500);
                    if (IsWordGuessed(secretWordUpper))
                    {
                        await SetDialogue(Messages.RIGHT_WORD_GUESS, Delay(25));
                        _gateManager.SetLockStateBySecretWord(secretWord, LockedState.Open);
                        break;
                    }
                    continue;
                }
                else
                {
                    remainingTries--;
                    ClearLineAtPoint(GetCursorPoint("TryCount"));
                    ConsoleService.SetCursorPosition(GetCursorPoint("TryCount"));
                    await DialogueService.DisplayMessage($"{Messages.REMAINING_TRIES} {remainingTries}", Delay(25));
                    continue;
                }
            }

            if (remainingTries == 0)
            {
                await SetDialogue(Messages.FAILED_WORD_GUESS, Delay(25));
                _gateManager.SetLockStateBySecretWord(secretWord, LockedState.Broken);
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
            string guessedString = new string(guessedLetters);
            return string.Equals(guessedString, secretWord, StringComparison.OrdinalIgnoreCase);
        }
    }
}
