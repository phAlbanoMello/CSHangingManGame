namespace SuperHangingManGame
{
    public class Gate
    {
        private string _theme;
        private int _level;
        private int _index;
        private bool _locked;
        private Lock[] _locks;
        private int[] _openLocksIndexes;
        private int[] _closedLocksIndexes;

        public int Level { get { return _level; } private set { } }
        public bool isLocked { get { return _locked; } private set { } }

        public Gate(string theme, Lock[] locks, int level, int index)
        {
            _theme = theme;
            _level = level;
            _index = index;
            _locks = locks;
        }

        public void Unlock()
        {
            if (_locked)
            {
                _locked = false;
            }
        }
        public void Lock()
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

        public int GetClosedLocks()
        {
            int count = 0;
            for (int i = 0; i < _locks.Length; i++)
            {
                if (_locks[i].LockedState == LockedState.Closed)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
