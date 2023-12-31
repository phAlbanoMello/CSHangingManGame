﻿namespace SuperHangingManGame.Components
{
    public class Gate
    {
        private Theme _theme;
        private int _level;
        private int _index;
        private bool _locked;
        private Lock[] _locks;

        public int Index => _index;
        public int Level => _level;
        public bool IsLocked => _locked;
        public Theme Theme => _theme;

        public Gate(Theme theme, Lock[] locks, int level, int index)
        {
            _theme = theme;
            _level = level;
            _index = index;
            _locks = locks;
        }
        public Gate(){}

        public void UnlockGate()
        {
            if (_locked)
            {
                _locked = false;
            }
        }
        public void LockGate()
        {
            if (!_locked)
            {
                _locked = true;
            }
        }

        public void CloseLock(int lockIndex)
        {
            _locks[lockIndex].SetLockedState(LockedState.Closed);
        }
        public void OpenLock(int lockIndex)
        {
            _locks[lockIndex].SetLockedState(LockedState.Open);
        }
        public void BreakLock(int lockIndex)
        {
            _locks[lockIndex].SetLockedState(LockedState.Broken);
        }
        
        public Lock[] GetLocks()
        {
            return _locks;
        }
    }
}
