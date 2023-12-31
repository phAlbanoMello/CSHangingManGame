﻿namespace SuperHangingManGame.Components
{
    public class CursorPoint
    {
        public int left { get; private set; }
        public int top { get; private set; }
        public string label { get; private set; }

        public CursorPoint(Tuple<int,int> cursorPosition, string label)
        {
            left = cursorPosition.Item1;
            top = cursorPosition.Item2;
            this.label = label;
        }
        public CursorPoint() {
            left = 0; top = 0; label = "";
        }
    }
}
