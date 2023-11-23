using SuperHangingManGame.Components;
using SuperHangingManGame.Interfaces;
using static SuperHangingManGame.Services.ConsoleService;

namespace SuperHangingManGame.Services
{
    public class GateDrawer : IGateDrawer
    {
        private const int drawingSpeed = 10;
        private const int gateHeight = 3;
        private const string gateFrame = "^------^";
        private const string gateBottomFrame = "~~~~~~~~";
        private const string closedLock = "|  ()  |";
        private const string openLock = "|  >>  |";
        private const string brokenLock = "|  XX  |";
        private const string gateWall = "||||||||";
        private const AlignPosition alignPosition = AlignPosition.Middle;

        public async Task DrawGate(Gate gate)
        {
            ConsoleService.SetFontColor(gate.Theme.Color);
            Lock[] locks = gate.GetLocks();

            if (locks.Length <= 0)
            {
                return;
            }

            string fullGateFrame = ComposeHorizontalStringShape(gateFrame, locks.Length);
            string fullGateBottomFrame = ComposeHorizontalStringShape(gateBottomFrame, locks.Length);

            string fullWall = ComposeHorizontalStringShape(gateWall, locks.Length);
            await DrawGateFrame(fullGateFrame);

            Console.WriteLine();
            await DrawGateWall(fullWall);
            ConsoleService.AlignText(fullGateFrame, AlignPosition.Middle);
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
                        await DialogueService.DisplayMessageInLine(closedLock, drawingSpeed);
                        break;
                    case LockedState.Open:
                        await DialogueService.DisplayMessageInLine(openLock, drawingSpeed);
                        break;
                    case LockedState.Broken:
                        await DialogueService.DisplayMessageInLine(brokenLock, drawingSpeed);
                        break;
                    default:
                        break;
                }
            }
        }

        private static async Task DrawGateFrame(string fullGateFrame)
        {
            ConsoleService.AlignText(fullGateFrame, AlignPosition.Middle);
            await DialogueService.DisplayMessageInLine(fullGateFrame, drawingSpeed);
        }
        private static async Task DrawGateWall(string fullGateWall)
        {
            for (int i = 0;i < gateHeight; i++)
            {
                ConsoleService.AlignText(fullGateWall, AlignPosition.Middle);
                await DialogueService.DisplayMessageInLine(fullGateWall, drawingSpeed);
                Console.WriteLine();
            }
        }

        private string ComposeHorizontalStringShape(string component, int times)
        {//TODO: Create a service for handling strings.
            string result = string.Empty;
            for (; times > 0; times--)
            {
                result += component;
            }
            return result;
        }
    }
}