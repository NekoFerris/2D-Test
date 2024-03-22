namespace Pong
{
    internal class Player
    {
        private MoveableObject MoveableObject { get; set; }
        public string Name { get; } = "noname";
        int Score { get; set; } = 0;
        public Player(MoveableObject moveableObject, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }
            MoveableObject = moveableObject;
        }
    }
}
