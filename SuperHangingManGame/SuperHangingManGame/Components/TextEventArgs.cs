using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHangingManGame.Components
{
    public class TextEventArgs : EventArgs
    {
        public Text WrittenText { get; }

        public TextEventArgs(Text writtenText)
        {
            WrittenText = writtenText;
        }
    }
}
