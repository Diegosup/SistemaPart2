using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaParticulas2
{
    internal class Particula
    {
        public Size Size { get; set; }
        public Point Location { get; set; }
        public double Rebote { get; set; }
        public int MoveStepX { get; set; }
        public int MoveStepY { get; set; }
        public int SpeedX { get { return MoveStepX; } }
        public int SpeedY { get { return MoveStepY; } }
        public double Mass { get { return Size.Width * Size.Height; } }
        public bool life { get; set; }
        public Color colorP { get; set; }
        public Color contorno { get; set; }

        public Particula(Size size, Point location)
        {
            this.Size = size;
            this.Location = location;
        }

        public bool IntersectsWith(Particula otherParticula)
        {
            Rectangle rect1 = new Rectangle(Location, Size);
            Rectangle rect2 = new Rectangle(otherParticula.Location, otherParticula.Size);
            return rect1.IntersectsWith(rect2);
        }

        public void Update()
        {
            // No hay necesidad de actualizar la pelota en este momento
        }
    }
}
