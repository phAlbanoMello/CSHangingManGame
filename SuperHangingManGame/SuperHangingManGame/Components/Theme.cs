namespace SuperHangingManGame.Components
{
    public class Theme
    {
        private ConsoleColor color;
        private string name;
        private string[] secretWords;

        public ConsoleColor Color => color;
        public string Name => name;
        public string[] SecretWords => secretWords;

        public Theme(ConsoleColor color, string name, string[] secretWords) {
            this.color = color;
            this.name = name;
            this.secretWords = secretWords;
        }
    }
}
