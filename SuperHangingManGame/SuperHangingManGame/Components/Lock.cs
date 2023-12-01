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
        private string _secretWord;

        public LockedState LockedState { get { return _lockedState; } private set { }}
        public int Index { get { return _index; } private set { }}

        public string SecretWord { get { return _secretWord; } private set { } }

        public Lock(int index, string secretWord, LockedState state = LockedState.Closed)
        {
            _lockedState = state;
            _index = index;
            _secretWord = secretWord;
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
