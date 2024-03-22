using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    internal class PlayerManager
    {
        public Player Player1 { get; set; } = new(null, null);
        public Player Player2 { get; set; } = new(null, null);
        public PlayerManager()
        {

        }
        public void Reset()
        {
            Player1.ResetPoints();
            Player2.ResetPoints();
        }
    }
}
