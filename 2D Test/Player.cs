using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Test
{
    internal class Player
    {
        private MoveableObject MoveableObject { get; set; }
        public string Name { get; } = "noname";
        int Score { get; set; } = 0;
        public Player(MoveableObject moveableObject)
        {
            MoveableObject = moveableObject;
        }
        public Player(MoveableObject moveableObject, string name)
        {
            Name = name;
            MoveableObject = moveableObject;
        }
    }
}
