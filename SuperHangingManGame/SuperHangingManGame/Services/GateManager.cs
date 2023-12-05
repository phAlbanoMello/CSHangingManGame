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

        public void LoadGates(Gate[] gates)
		{
			_gates = gates;
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