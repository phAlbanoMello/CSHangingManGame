using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHangingManGame.Data.Constants
{
    public static class Messages
    {
        public static readonly string WELCOME_MESSAGE = "Welcome to Ourobouro's Gates*p(1000)\n";
        public static readonly string[] INTRO = {
            "You are trapped inside a maze of ancient gates...*p(1000)\n",
            "Each gate has locks that can be opened if you guess their secret words.\n"
        };
        public static readonly string GATE_THEME_PRESENTATION = "Current gate theme is*p(1000)\n";
        public static readonly string GUESS_CUE = "Can you guess it,*p(70) challenger?\n";
        public static readonly string RIGHT_WORD_GUESS = "You opened a lock";
        public static readonly string FAILED_WORD_GUESS = "You just broke a lock...";
        public static readonly string REMAINING_TRIES = "Incorrect!*p(70) Remaining tries:";
    }
}
