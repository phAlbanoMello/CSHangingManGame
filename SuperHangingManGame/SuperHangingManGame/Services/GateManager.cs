using SuperHangingManGame.Components;
using SuperHangingManGame.Interfaces;

namespace SuperHangingManGame.Services
{
	public class GateManager : IGateManager
	{
		private Gate[] _gates;

		public Gate[] Gates => _gates;

        public GateManager()
        {
			//TODO:get GatesData from serializer
        }

		public Gate[] GetGates()
		{
			return _gates;
		}

        public void LoadGates()
		{
            string[] secretWords = { "Tree", "Mountain", "Cave" };
            Theme theme = new Theme(ConsoleColor.Green, "Nature", secretWords);
            Lock[] locks = { new Lock(0, secretWords[0]), new Lock(1, secretWords[1]), new Lock(2, secretWords[2]) };

            string[] secretWordsB = { "Asphalt", "Street", "Building", "Traffic" };
            Theme themeB = new Theme(ConsoleColor.Blue, "City", secretWordsB);
            Lock[] locksB = { new Lock(0, secretWordsB[0]), new Lock(1, secretWordsB[1]), new Lock(2, secretWordsB[2]), new Lock(3, secretWordsB[3]) };
            Gate gate = new Gate(theme, locks, 1, 0);
            Gate gateB = new Gate(themeB, locksB, 2, 1);

            _gates = new Gate[] { gate, gateB };
		}

		public bool IsGateOpen(int index)
		{
			return !_gates[index].IsLocked;
		}

		public void SetLockStateBySecretWord(string secretWord, LockedState lockedState)
		{
			for (int i = 0; i < _gates.Length; i++)
			{
				Lock[] locks = Gates[i].GetLocks();
				for (int j = 0; j < locks.Length; j++)
				{
					if (locks[j].SecretWord == secretWord)
					{
						locks[j].SetLockedState(lockedState); 
						break;
					}
				}
			}
		}

        public int GetBrokenLocksCount(Gate gate)
        {
			int brokenLocksCount = 0;
			Lock[] locks = gate.GetLocks();
            for (int i = 0; i < locks.Length; i++)
			{
				if (locks[i].LockedState == LockedState.Broken) brokenLocksCount++;
			}
			return brokenLocksCount;
        }
    }
}