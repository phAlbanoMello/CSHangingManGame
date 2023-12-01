using SuperHangingManGame.Components;
using SuperHangingManGame.Interfaces;

namespace SuperHangingManGame.Services
{
    public class TextManager : ITextManager
    {
        private static readonly Stack<Text> textCollection = new Stack<Text>();

        public Stack<Text> TextCollection => textCollection;

        public static event EventHandler<TextEventArgs> TextWritten;

        static TextManager()
        {
            TextWritten = null;
        }

        public Text GetNextText()
        {
            return TextCollection.Count > 0 ? TextCollection.Pop() : null;
        }

        public Text CreateNewText(string text, Tuple<int, int> position)
        {
            int newId = GetValidId();
            Text textComponent = new Text(newId, text, position);

            TextCollection.Push(textComponent);
            OnTextWritten(new TextEventArgs(textComponent));

            return textComponent;
        }

        protected void OnTextWritten(TextEventArgs e)
        {
            TextWritten?.Invoke(null, e);
        }

        private bool ValidateId(int id)
        {
            return !TextCollection.Any(textComponent => textComponent.Id == id);
        }
        private int GetValidId()
        {
            Random random = new Random();
            int newId;
            do
            { 
                newId = random.Next(1000, 10000);
            } while (!ValidateId(newId));

            return newId;
        }
    }
}







