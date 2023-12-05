using SuperHangingManGame.Components;
using SuperHangingManGame.Data.Constants;

namespace SuperHangingManGame.Services.Display
{
    public static class DrawingService
    {
        private const int drawingSpeed = 10;
        private const int gateHeight = 3;

        public static async Task DrawGuessingField(char[] guessedLetters, string secretWord)
        {
            string guessingField = "";
            
            for (int i = 0; i < secretWord.Length; i++)
            {
                if (guessedLetters.Contains(secretWord[i]))
                {
                    guessingField += $"[{secretWord[i]}]";
                }
                else
                {
                    guessingField += "[_]";
                }
            }
            ConsoleService.AlignText(guessingField, ConsoleService.AlignPosition.Middle);
            await DialogueService.DisplayMessageInLine(guessingField, 60);
        }

        public static async Task DrawGate(Gate gate, int speed)
        {
            ConsoleService.SetFontColor(gate.Theme.Color);
            Lock[] locks = gate.GetLocks();

            if (locks.Length <= 0)
            {
                return;
            }

            string fullGateFrame = ComposeSequenceStringShape(DrawingTemplates.GATE_FRAME, locks.Length);
            string fullGateBottomFrame = ComposeSequenceStringShape(DrawingTemplates.GATE_FRAME_BOTTOM, locks.Length);


            string fullWall = ComposeSequenceStringShape(DrawingTemplates.GATE_WALL, locks.Length);
            await DrawGateFrame(fullGateFrame, speed);

            Console.WriteLine();
            await DrawGateWall(fullWall);
            ConsoleService.AlignText(fullGateFrame, ConsoleService.AlignPosition.Middle);
            await DrawLocks(locks);
            Console.WriteLine();
            await DrawGateWall(fullWall);
            await DrawGateFrame(fullGateBottomFrame, speed);
            Console.WriteLine();
        }
        private static async Task DrawLocks(Lock[] locks)
        {
            for (int i = 0; i < locks.Length; i++)
            {
                switch (locks[i].LockedState)
                {
                    case LockedState.Closed:
                        await DialogueService.DisplayMessageInLine(DrawingTemplates.CLOSED_LOCK, drawingSpeed);
                        break;
                    case LockedState.Open:
                        await DialogueService.DisplayMessageInLine(DrawingTemplates.OPEN_LOCK, drawingSpeed);
                        break;
                    case LockedState.Broken:
                        await DialogueService.DisplayMessageInLine(DrawingTemplates.BROKEN_LOCK, drawingSpeed);
                        break;
                    default:
                        break;
                }
            }
        }
        private static async Task DrawGateFrame(string fullGateFrame, int speed)
        {
            ConsoleService.AlignText(fullGateFrame, ConsoleService.AlignPosition.Middle);
            await DialogueService.DisplayMessageInLine(fullGateFrame, drawingSpeed / speed);
        }
        private static async Task DrawGateWall(string fullGateWall)
        {
            for (int i = 0; i < gateHeight; i++)
            {
                ConsoleService.AlignText(fullGateWall, ConsoleService.AlignPosition.Middle);
                await DialogueService.DisplayMessageInLine(fullGateWall, drawingSpeed);
                Console.WriteLine();
            }
        }
        private static string ComposeSequenceStringShape(string input, int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Lenght should be greater than zero.");
            }

            string result = string.Empty;

            for (int i = 0; i < length; i++)
            {
                result += input;
            }
            return result;
        }

        public enum Direction
        {
            HORIZONTAL,
            VERTICAL
        }
    }
}