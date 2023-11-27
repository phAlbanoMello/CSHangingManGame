﻿using SuperHangingManGame.Components;

namespace SuperHangingManGame.Interfaces
{
    public interface IGateManager
    {
        public void LoadGates(Gate[] gates);
        public bool IsGateOpen(int index);
    }
}