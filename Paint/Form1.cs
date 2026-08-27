/*Colegio Técnico Antônio Teixeira Fernandes (Univap)
 * Curso Técnico em Informática - Data de Entrega: 28 / 05 / 2025
 * Autores do Projeto: Matheus de Oliveira Alves Bastos
 *                     João Gabriel Andrade de Freitas
 *
 * Turma: 3F
 * Atividade Proposta em aula
 * Observação: N/A
 * 
 * 
 * ******************************************************************/

using System.Drawing;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.MouseClick += Form1_MouseClick;

            string pasta = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Archives"
             );

            Directory.CreateDirectory(pasta);

            caminho = Path.Combine(pasta, "Data.dat");
        }
        bool desenharlinha = false;
        int contadorlinha = 1;
        int contadorpentagono = 1;
        int contadorretangulo = 1;
        int contadorlosango = 1;
        int contadortriangulo = 1;
        String caminho;
        bool desenhartriangulo = false;
        bool desenharpentagono = false;
        int espessuralinha = 1;
        bool desenharlosango = false;
        bool desenharretangulo = false;
        int clickCount = 0;
        String cor = "preto";
        String padraolinha = "Linha sólida";
        

        int x1, y1, x2, y2;

        float[] vetorlinha;

        int tx1, ty1, tx2, ty2, tx3, ty3;

        int px1, py1, px2, py2, px3, py3, px4, py4, px5, py5;

        int lx1, ly1, lx2, ly2, lx3, ly3, lx4, ly4;

        int rx1, ry1, rx2, ry2;

        int R = 0;
        int G = 0;
        int B = 0;



        private void DesenharLinha(PaintEventArgs e, Pen cor, int x1, int y1, int x2, int y2)
        {
            e.Graphics.DrawLine(cor, x1, y1, x2, y2);

        }

        private void DesenharRetangulo(PaintEventArgs e, Pen pen, int x, int y, int largura, int altura)
        {
            x = Math.Min(rx1, rx2);
            y = Math.Min(ry1, ry2);
            largura = Math.Abs(rx2 - rx1);
            altura = Math.Abs(ry2 - ry1);
            e.Graphics.DrawRectangle(pen, x, y, largura, altura);
        }

        private Color morbius(int R, int G, int B)
        {
            Color cor = new Color();
            cor = Color.FromArgb(R, G, B);
            Pen caneta = new Pen(cor, 1);
            return cor;
        }

        private Pen CanetaEspessura()
        {
            if (listBox1.GetSelected(0))
            {
                espessuralinha = 1;
            }
            if (listBox1.GetSelected(3))
            {
                espessuralinha = 3;
            }
            if (listBox1.GetSelected(6))
            {
                espessuralinha = 5;
            }
            if (listBox1.GetSelected(9))
            {
                espessuralinha = 8;
            }

            Color cor = new Color();
            cor = Color.FromArgb(R, G, B);
            Pen caneta = new Pen(cor, espessuralinha);

            if (listBox2.GetSelected(0))
            {
                padraolinha = "Linha sólida";
            }
            if (listBox2.GetSelected(1))
            {
                vetorlinha = [10, 2];
                caneta.DashPattern = vetorlinha;
                padraolinha = "Linha tracejada";
            }
            if (listBox2.GetSelected(2))
            {
                vetorlinha = [2, 10];
                caneta.DashPattern = vetorlinha;
                padraolinha = "Linha pontilhada";
            }
            if (listBox2.GetSelected(3))
            {
                vetorlinha = [10, 2, 2, 2];
                caneta.DashPattern = vetorlinha;
                padraolinha = "Linha traço ponto";
            }
            if (listBox2.GetSelected(4))
            {
                vetorlinha = [10, 2, 2, 2, 2, 2];
                caneta.DashPattern = vetorlinha;
                padraolinha = "Linha traço ponto ponto";
            }

            return caneta;
        }

        private void DesenharTriangulo(PaintEventArgs e, Pen cor, int tx1, int ty1, int tx2, int ty2, int tx3, int ty3)
        {
            int[] ver = { 0, 1, 2, 3, 2, 3, 4, 5, 4, 5, 0, 1 };
            int[] v = { tx1, ty1, tx2, ty2, tx3, ty3 };
            for (int cont = 0; cont <= 8; cont += 4)
            {
                DesenharLinha(e, cor, v[ver[cont]], v[ver[cont + 1]], v[ver[cont + 2]], v[ver[cont + 3]]);
            }
        }

        private void DesenharPentagono(PaintEventArgs e, Pen cor, int tx1, int ty1, int tx2, int ty2, int tx3, int ty3, int tx4, int ty4, int tx5, int ty5)
        {
            int[] ver = { 0, 1, 2, 3, 2, 3, 4, 5, 4, 5, 6, 7, 6, 7, 8, 9, 8, 9, 0, 1 };
            int[] v = { px1, py1, px2, py2, px3, py3, px4, py4, px5, py5 };
            for (int cont = 0; cont <= 16; cont += 4)
            {
                DesenharLinha(e, cor, v[ver[cont]], v[ver[cont + 1]], v[ver[cont + 2]], v[ver[cont + 3]]);
            }
        }

        private void DesenharLosango(PaintEventArgs e, Pen cor, int tx1, int ty1, int tx2, int ty2, int tx3, int ty3, int tx4, int ty4)
        {
            int[] ver = { 0, 1, 2, 3, 2, 3, 4, 5, 4, 5, 6, 7, 6, 7, 0, 1 };
            int[] v = { lx1, ly1, lx2, ly2, lx3, ly3, lx4, ly4 };
            for (int cont = 0; cont <= 12; cont += 4)
            {
                DesenharLinha(e, cor, v[ver[cont]], v[ver[cont + 1]], v[ver[cont + 2]], v[ver[cont + 3]]);
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (desenharlinha == true)
            {
                clickCount++;
                if (clickCount == 1)
                {
                    x1 = e.X;
                    y1 = e.Y;
                }
                else if (clickCount == 2)
                {
                    x2 = e.X;
                    y2 = e.Y;
                    Invalidate();
                    clickCount = 0;
                    File.AppendAllText(caminho, "Coordenadas " + contadorlinha + "ª linha \n x1 = " + x1 + "\n x2 = " + x2 + "\n y1 = " + y1 + "\n y2 = " + y2 + "\n Cor = " + cor + "\n Espessura = " + espessuralinha + "\n Padrão da linha = " + padraolinha + "\n \n");
                    contadorlinha++;
                }
            }
            else if (desenharretangulo == true)
            {
                clickCount++;
                if (clickCount == 1)
                {
                    rx1 = e.X;
                    ry1 = e.Y;
                }
                else if (clickCount == 2)
                {
                    rx2 = e.X;
                    ry2 = e.Y;
                    Invalidate();
                    clickCount = 0;
                    File.AppendAllText(caminho, "Coordenadas " + contadorretangulo + "º quadrado \n rx1 = " + rx1 + "\n rx2 = " + rx2 + "\n ry1 = " + ry1 + "\n ry2 = " + ry2 + "\n Cor = " + cor + "\n Espessura = " + espessuralinha + "\n Padrão da linha = " + padraolinha + "\n \n");
                    contadorretangulo++;
                }
            }

            else if (desenhartriangulo == true)
            {
                clickCount++;
                if (clickCount == 1)
                {
                    tx1 = e.X;
                    ty1 = e.Y;
                }
                else if (clickCount == 2)
                {
                    tx2 = e.X;
                    ty2 = e.Y;
                }
                else if (clickCount == 3)
                {
                    tx3 = e.X;
                    ty3 = e.Y;
                    Invalidate();
                    clickCount = 0;
                    File.AppendAllText(caminho, "Coordenadas " + contadortriangulo + "º retangulo \n tx1 = " + tx1 + "\n tx2 = " + tx2 + "\n tx3 = " + tx3 + "\n ty1 = " + ty1 + "\n ty2 = " + ty2 + "\n ty2 = " + ty2 + "\n Cor = " + cor + "\n Espessura = " + espessuralinha + "\n Padrão da linha = " + padraolinha + "\n \n");
                    contadortriangulo++;
                }
            }
            else if (desenharpentagono == true)
            {
                clickCount++;
                switch (clickCount)
                {
                    case 1:
                        px1 = e.X;
                        py1 = e.Y;
                        break;
                    case 2:
                        px2 = e.X;
                        py2 = e.Y;
                        break;
                    case 3:
                        px3 = e.X;
                        py3 = e.Y;
                        break;
                    case 4:
                        px4 = e.X;
                        py4 = e.Y;
                        break;
                    case 5:
                        px5 = e.X;
                        py5 = e.Y;
                        Invalidate();
                        clickCount = 0;
                        File.AppendAllText(caminho, "Coordenadas " + contadorpentagono + "º pentagono \n px1 = " + px1 + "\n px2 = " + px2 + "\n px3 = " + px3 + "\n px4 = " + px4 + "\n px5 = " + px5 + "\n py1 = " + py1 + "\n py2 = " + py2 + "\n py3 = " + py3 + "\n py4 = " + py4 + "\n py5 = " + py5 + "\n Cor = " + cor + "\n Espessura = " + espessuralinha + "\n Padrão da linha = " + padraolinha + "\n \n");
                        contadorpentagono++;
                        break;
                }
            }
            else if (desenharlosango == true)
            {
                clickCount++;
                switch (clickCount)
                {
                    case 1:
                        lx1 = e.X;
                        ly1 = e.Y;
                        break;
                    case 2:
                        lx2 = e.X;
                        ly2 = e.Y;
                        break;
                    case 3:
                        lx3 = e.X;
                        ly3 = e.Y;
                        break;
                    case 4:
                        lx4 = e.X;
                        ly4 = e.Y;
                        Invalidate();
                        clickCount = 0;
                        File.AppendAllText(caminho, "Coordenadas " + contadorlosango + "º losango \n lx1 = " + lx1 + "\n lx2 = " + lx2 + "\n lx3 = " + lx3 + "\n lx4 = " + lx4 + "\n ly1 = " + ly1 + "\n ly2 = " + ly2 + "\n ly3 = " + ly3 + "\n ly4 = " + ly4 + "\n Cor = " + cor + "\n Espessura = " + espessuralinha + "\n Padrão da linha = " + padraolinha + "\n \n");
                        contadorlosango++;
                        break;
                }
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (desenharlinha == true && clickCount == 0)
            {
                DesenharLinha(e, CanetaEspessura(), x1, y1, x2, y2);
            }

            if (desenharretangulo == true && clickCount == 0)
            {
                DesenharRetangulo(e, CanetaEspessura(), rx1, ry1, rx2, ry2);
            }

            if (desenhartriangulo && clickCount == 0)
            {
                DesenharTriangulo(e, CanetaEspessura(), tx1, ty1, tx2, ty2, tx3, ty3);
            }

            if (desenharpentagono && clickCount == 0)
            {
                DesenharPentagono(e, CanetaEspessura(), px1, py1, px2, py2, px3, py3, px4, py4, px5, py5);
            }

            if (desenharlosango && clickCount == 0)
            {
                DesenharLosango(e, CanetaEspessura(), lx1, ly1, lx2, ly2, lx3, ly3, lx4, ly4);
            }

        }

        private void button17_Click(object sender, EventArgs e)
        {
            x1 = 0;
            x2 = 0;
            padraolinha = "Linha sólida";
            desenhartriangulo = false;
            desenharpentagono = false;
            desenharlosango = false;
            desenharretangulo = false;
            desenharlinha = true;
            clickCount = 0;
            Invalidate();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            padraolinha = "Linha sólida";
            rx1 = 0;
            ry1 = 0;
            rx2 = 0;
            ry2 = 0;
            desenhartriangulo = false;
            desenharpentagono = false;
            desenharlosango = false;
            desenharretangulo = true;
            desenharlinha = false;
            clickCount = 0;
            Invalidate();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            padraolinha = "Linha sólida";
            px1 = 0;
            px2 = 0;
            px3 = 0;
            px4 = 0;
            px5 = 0;
            py1 = 0;
            py2 = 0;
            py3 = 0;
            py4 = 0;
            py5 = 0;
            desenhartriangulo = false;
            desenharpentagono = true;
            desenharlosango = false;
            desenharretangulo = false;
            desenharlinha = false;
            clickCount = 0;
            Invalidate();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            padraolinha = "Linha sólida";
            lx1 = 0;
            ly1 = 0;
            lx2 = 0;
            ly2 = 0;
            lx3 = 0;
            ly3 = 0;
            lx4 = 0;
            ly4 = 0;
            desenhartriangulo = false;
            desenharpentagono = false;
            desenharlosango = true;
            desenharretangulo = false;
            desenharlinha = false;
            clickCount = 0;
            Invalidate();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            padraolinha = "Linha sólida";
            tx1 = 0;
            ty1 = 0;
            tx2 = 0;
            ty2 = 0;
            tx3 = 0;
            ty3 = 0;
            desenhartriangulo = true;
            desenharpentagono = false;
            desenharlosango = false;
            desenharretangulo = false;
            desenharlinha = false;
            clickCount = 0;
            Invalidate();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            cor = "preto";
            R = 0;
            G = 0;
            B = 0;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            cor = "branco";
            R = 255;
            G = 255;
            B = 255;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cor = "vermelho";
            R = 255;
            G = 0;
            B = 0;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            cor = "amarelo escuro";
            R = 192;
            G = 192;
            B = 0;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            cor = "rosa claro";
            R = 255;
            G = 192;
            B = 255;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            cor = "amarelo claro";
            R = 255;
            G = 255;
            B = 192;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cor = "ciano claro";
            R = 128;
            G = 255;
            B = 255;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cor = "azul";
            R = 0;
            G = 0;
            B = 255;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            cor = "azul claro";
            R = 192;
            G = 192;
            B = 255;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            cor = "verde claro";
            R = 128;
            G = 255;
            B = 128;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cor = "laranja";
            R = 255;
            G = 165;
            B = 0;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            cor = "vinho escuro";
            R = 128;
            G = 0;
            B = 32;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cor = "amarelo";
            R = 255;
            G = 255;
            B = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cor = "verde limão";
            R = 50;
            G = 205;
            B = 50;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            cor = "cinza";
            R = 128;
            G = 128;
            B = 128;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cor = "rosa";
            R = 255;
            G = 0;
            B = 255;
        }

        
    }
}
