namespace SuperHangingManGame.Components
{
    public class Text
    {
        private int _id;
        private string _value;
        private Tuple<int, int> _position;

        public int Id { get { return _id; } }
        public string Value { get { return _value; } }

        public Text(int id, string value, Tuple<int, int> position)
        {
            _id = id;
            _value = value;
            _position = position;
        }

        public Tuple<int, int> GetPosition()
        {
            return _position;
        }

        public void UpdateValue(string value)
        {
            _value = value;
        }
    }
}
