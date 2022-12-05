using System.Net.Sockets;
using System.Net;

namespace ServerAlpha.Server.ServerCommand
{
    internal class Player
    {
        // Fields
        private IPEndPoint endPoint;
        private int hp;
        private bool ready = false;
        private Tuple<float, float> position = new Tuple<float, float>(0, 0);
        
        // Property
        public IPEndPoint EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }
        public int HP
        {
            get { return hp; }
            set 
            { 
                if(hp + value > 100)
                {
                    hp = 100;
                }
                else if(hp + value < 0)
                {
                    hp = 0;
                }
                else
                {
                    hp += value;
                }
            }
        }
        public bool Ready
        {
            get { return ready; }
            set { ready = !ready; }
        }
        public Tuple<float, float> Position
        {
            get { return position; }
            set { position = value; }
        }

        // Construct
        public Player(IPEndPoint endPoint)
        {
            this.endPoint = endPoint;
            hp = 100;
        }

    }
}