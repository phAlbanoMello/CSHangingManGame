using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHangingManGame
{
    public class Lock
    {
        private LockedState _lockedState;
        private int _index;

        public LockedState LockedState { get { return _lockedState; } private set { }}
        public int Index { get { return _index; } private set { }}

        //TODO: maybe insert secret word logic here

        public Lock(int index)
        {
            _lockedState = LockedState.Closed;
            _index = index;
        }

        public void SetLockedState(LockedState state)
        {
            _lockedState = state;
        }
    }

    public enum LockedState
    {
        Closed = 0,
        Open,
        Broken
    }
}
