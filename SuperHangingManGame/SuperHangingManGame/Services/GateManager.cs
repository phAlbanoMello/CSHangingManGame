using SuperHangingManGame.Components;
using SuperHangingManGame.Interfaces;

namespace SuperHangingManGame.Services
{
	public class GateManager : IGateManager
	{
		private Gate[] _gates;

		public Gate[] Gates => _gates;

		public void LoadGates(Gate[] gates)
		{
			_gates = gates;
		}

		public bool IsGateOpen(int index)
		{
			return !_gates[index].IsLocked;
		}
	}
}