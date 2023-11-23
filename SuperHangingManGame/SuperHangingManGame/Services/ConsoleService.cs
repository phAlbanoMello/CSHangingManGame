namespace SuperHangingManGame.Services
{
    public static class ConsoleService
    {
        public static void AlignText(string finalString, AlignPosition alignPosition)
        {
            int screenWidth = Console.WindowWidth;
            int leftPadding;
            switch (alignPosition)
            {
                case AlignPosition.Left:
                    leftPadding = 0;
                    break;
                case AlignPosition.Right:
                    leftPadding = (screenWidth - finalString.Length);
                    break;
                case AlignPosition.Middle:
                    leftPadding = (screenWidth - finalString.Length) / 2;
                    break;
                default:
                    leftPadding = 0;
                    break;
            }
            Console.SetCursorPosition(leftPadding, Console.CursorTop);
        }

        public static void SetFontColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public enum AlignPosition { Left, Right, Middle }
    }
}
