using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaParticulas2
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using static System.Net.Mime.MediaTypeNames;

    public partial class Form1 : Form
    {
        private Bitmap bmp;
        public int width, height, posix, posiy, Irange;
        private Graphics g;
        private int count = 0;
        private int n = 100;//numero de partículas, si n=1 podrá ver como se desvanece y se mantiene siempre una partícula
        private List<Particula> Particulas = new List<Particula>();
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            width = 450;
            height = 100;
            posix = 100;
            posiy = 0;
            Irange = 300;
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
            Bitmap image = new Bitmap(res.TextR);

            for (int i = 0; i < n; i++)
            {
                int rn1 = random.Next(1, 10);
                int rn2 = random.Next(32, 38);
                int ballPosy = 112; // Ajustar la posición vertical de la pelota según su índice en la lista
                int ballPosx = random.Next(103, 450);
                double rebote = random.NextDouble() * 2; // Asignar un valor de rebote aleatorio
                                                         //int moveStepX = (random.Next(2) == 0 ? -1 : 1) * (int)(random.NextDouble() * 5) + 1; // Asignar un movimiento en X aleatorio
                int moveStepX = (random.Next(2, 10) * 1) + 1;
                int moveStepY = 0;// Asignar un movimiento hacia abajo aleatorio
                                                              //int moveStepY = (random.Next(2) == 0 ? -1 : 1) * (int)(random.NextDouble() * 5) + 1; // Asignar un movimiento en Y aleatorio
                Particula newParticula = new Particula(new Size(rn1, rn2), new Point(ballPosx, ballPosy));
                newParticula.Rebote = rebote; // Asignar el valor de rebote a la nueva pelota
                newParticula.MoveStepX = moveStepX; // Asignar el movimiento en X a la nueva pelota
                newParticula.MoveStepY = moveStepY; // Asignar el movimiento en Y a la nueva pelota
                newParticula.life = true;
                Color pixelColor = image.GetPixel(random.Next(0, 1584), random.Next(0, 1195));//ancho y alto
                int alpha = 610;
                int alphaValue = (int)Math.Round((double)alpha / 255 * 100);

                Color nuevoColor = Color.FromArgb(alphaValue, pixelColor.R, pixelColor.G, pixelColor.B);
                newParticula.colorP = nuevoColor;
                newParticula.contorno = Color.FromArgb(220, 20, 60); 

                Particulas.Add(newParticula);
            }
        }

        private void GenerateBalls()
        {

            Bitmap image = new Bitmap(res.TextR);
            List<Color> colores = new List<Color>() { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange };
            int rn1 = random.Next(1, 10);
            int rn2 = random.Next(32, 38);
            int ballPosy = 112; // Ajustar la posición vertical de la pelota según su índice en la lista
            int ballPosx = random.Next(103, 450);
            double rebote = random.NextDouble() * 2; // Asignar un valor de rebote aleatorio
            int moveStepX = (random.Next(2, 10) * 1) + 1;
            int moveStepY = 0;
            Particula newParticula = new Particula(new Size(rn1, rn2), new Point(ballPosx, ballPosy));
            newParticula.Rebote = rebote; // Asignar el valor de rebote a la nueva pelota
            newParticula.MoveStepX = moveStepX; // Asignar el movimiento en X a la nueva pelota
            newParticula.MoveStepY = moveStepY; // Asignar el movimiento en Y a la nueva pelota
            newParticula.life = true;
            Color pixelColor = image.GetPixel(random.Next(0,1584), random.Next(0, 1195));//ancho y alto
            int alpha = 610;
            int alphaValue = (int)Math.Round((double)alpha / 255 * 100);
            //Color nuevoColor = Color.FromArgb(alphaValue, 255, 255, 255);
	    
            Color nuevoColor = Color.FromArgb(alphaValue, pixelColor.R, pixelColor.G, pixelColor.B);
            newParticula.colorP = nuevoColor;
            newParticula.contorno = Color.FromArgb(220, 20, 60);
            Particulas.Add(newParticula);
               
            
        }


        private void MoveBalls(int width,int height,int posix ,int posiy,int Irange)
        {
            foreach (Particula particula in Particulas)
            {
                int ballPosx = particula.Location.X;
                int ballPosy = particula.Location.Y;
                

                ballPosx += particula.MoveStepX;
                if (ballPosx < Irange )
                {
                    int alpha = 610;
                    int alphaValue = (int)Math.Round((double)alpha / 255 * 100);
                    Color nuevoC= Color.FromArgb(alphaValue, particula.contorno.R, particula.contorno.G, particula.contorno.B);
                    Color nuevoColor = Color.FromArgb(alphaValue, particula.colorP.R, particula.colorP.G, particula.colorP.B);
                    particula.colorP = nuevoColor;
                    particula.contorno = nuevoC;
                }
               
                if (ballPosx > Irange && ballPosx < width)
                {
                    int alpha = 255 - (int)Math.Round((ballPosx - 300.0) / 150.0 * 255.0);
                    int alphaValue = (int)Math.Round((double)alpha / 255 * 100);
                    Color nuevoC = Color.FromArgb(alphaValue, particula.contorno.R, particula.contorno.G, particula.contorno.B);
                    Color nuevoColor = Color.FromArgb(alphaValue, particula.colorP.R, particula.colorP.G, particula.colorP.B);
                    particula.colorP = nuevoColor;
                    particula.contorno = nuevoC;
                }
                if (ballPosx < 0)
                {
                    // La pelota ha alcanzado el extremo izquierdo, por lo que reaparece en la posición del extremo derecho
                    ballPosx = width - particula.Size.Width;
                }
                else if (ballPosx + particula.Size.Width > width)
                {
                    // La pelota ha alcanzado el extremo derecho, por lo que reaparece en la posición del extremo izquierdo
                    ballPosx = posix;
                }

                ballPosy += particula.MoveStepY;
                
                particula.Location = new Point(ballPosx, ballPosy);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                
                
            count++;
            int limit = random.Next(90, 100);
           
            // Dibujar imagen de fondo
            g.DrawImage(res.sableR, 0, 0, pictureBox1.Width, pictureBox1.Height);

            for (int i = 0; i < Particulas.Count; i++)
            {
               
                Particula pelota = Particulas[i];
                if (count > limit)
                {

                    pelota.life = false;
                    Particulas.Remove(pelota);
                    GenerateBalls();
                    count = 0;
                }
                if (pelota.life)
                {
                    g.FillEllipse(new SolidBrush(pelota.colorP), pelota.Location.X, pelota.Location.Y, pelota.Size.Width, pelota.Size.Height);
                    g.DrawEllipse(new Pen(pelota.contorno), pelota.Location.X, pelota.Location.Y, pelota.Size.Width, pelota.Size.Height);
                    pelota.Update();
                }
            }

            MoveBalls(width, height, posix, posiy, Irange);
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
                // Obtener las coordenadas del punto de clic
                MouseEventArgs me = (MouseEventArgs)e;
                Point coordinates = me.Location;

                // Mostrar las coordenadas en un MessageBox
                MessageBox.Show($"X: {coordinates.X}, Y: {coordinates.Y}");
            
        }
    }
}
