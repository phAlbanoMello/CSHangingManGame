using SuperHangingManGame.Components;
using SuperHangingManGame.Data.Constants;
using SuperHangingManGame.Interfaces;
using System.ComponentModel;

namespace SuperHangingManGame.Services.Display
{
    public static class DrawingService
    {
        //TODO : FINISH DEFINING INTERFACES FOR THE SERVICE LOCATOR AND PROPER INITIALIZATION
        //TODO : FINISH DRAWING SERVICE CLASS
        //TODO : GO BACK TO GAME CLASS AND RESUME FINISHING CORE LOOP 

        private const int drawingSpeed = 10;
        private const int gateHeight = 3;

        public static async Task DrawGate(Gate gate)
        {
            ConsoleService.SetFontColor(gate.Theme.Color);
            Lock[] locks = gate.GetLocks();

            if (locks.Length <= 0)
            {
                return;
            }

            string fullGateFrame = ComposeSequenceStringShape(DrawingTemplates.GATE_FRAME, locks.Length, Direction.HORIZONTAL);
            string fullGateBottomFrame = ComposeSequenceStringShape(DrawingTemplates.GATE_FRAME_BOTTOM, locks.Length, Direction.HORIZONTAL);

            string fullWall = ComposeSequenceStringShape(DrawingTemplates.GATE_WALL, locks.Length, Direction.HORIZONTAL);
            await DrawGateFrame(fullGateFrame);

            Console.WriteLine();
            await DrawGateWall(fullWall);
            ConsoleService.AlignText(fullGateFrame, ConsoleService.AlignPosition.Middle);
            await DrawLocks(locks);
            Console.WriteLine();
            await DrawGateWall(fullWall);
            await DrawGateFrame(fullGateBottomFrame);
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
        private static async Task DrawGateFrame(string fullGateFrame)
        {
            ConsoleService.AlignText(fullGateFrame, ConsoleService.AlignPosition.Middle);
            await DialogueService.DisplayMessageInLine(fullGateFrame, drawingSpeed);
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
        private static string ComposeSequenceStringShape(string input, int length, Direction direction)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Repetitions should be greater than zero.");
            }

            string result = string.Empty;

            for (int i = 0; i < length; i++)
            {
                result += input;

                if (direction == Direction.VERTICAL && i < length - 1)
                {
                    result += "\n";
                }
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