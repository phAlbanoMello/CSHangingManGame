using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHangingManGame.Data.Configs
{
    public class MessagesConfig
    {
        public string WELCOME_MESSAGE { get; set; }
        public string[] INTRO { get; set; }
        public string GATE_THEME_PRESENTATION { get; set; }
        public string GUESS_CUE { get; set; }
        public string RIGHT_WORD_GUESS { get; set; }
        public string FAILED_WORD_GUESS { get; set; }
        public string REMAINING_TRIES { get; set; }
    }
}
