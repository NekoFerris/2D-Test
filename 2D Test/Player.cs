namespace Pong
{
    internal class Player
    {
        private MoveableObject MoveableObject { get; set; }
        private string Name = "noname";
        private int Score = 0;
        public Player(MoveableObject moveableObject, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }
            MoveableObject = moveableObject;
        }
        public void SetName(string name)
        {
            Name = name;
        }
        public string GetName()
        {
            return Name;
        }
        public void AddPoint()
        {
            Score++;
        }
        public int GetPoint()
        {
            return Score;
        }
        public void ResetPoints()
        {
            Score = 0;
        }
    }
}
