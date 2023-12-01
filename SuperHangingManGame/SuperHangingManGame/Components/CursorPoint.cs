using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHangingManGame.Components
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
    }
}
