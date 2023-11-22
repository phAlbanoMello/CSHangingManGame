using SuperHangingManGame.Interfaces;

namespace SuperHangingManGame.Services
{
    public class GateDrawer : IGateDrawer
    {
        public GateDrawer(){}

        public void DrawGate(Gate gate)
        {
            Lock[] locks = gate.GetLocks();

            if (locks.Length <= 0)
            {
                return;
            }

            for (int i = 0; i < locks.Length; i++)
            {
                Console.Write("--------");
            }

            Console.WriteLine();
            for (int i = 0; i < locks.Length; i++)
            {
                switch (locks[i].LockedState)
                {
                    case LockedState.Closed:
                        Console.Write("|  ()  |");
                        break;
                    case LockedState.Open:
                        Console.Write("|  >>  |");
                        break;
                    case LockedState.Broken:
                        Console.Write("|  XX  |");
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine();
            for (int i = 0; i < locks.Length; i++)
            {
                Console.Write("--------");
            }
            Console.WriteLine();
        }
    }
}