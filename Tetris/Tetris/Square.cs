using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Tetris
{
    class Square : PictureBox
    {
    }
    class Shape
    {
        static List<Square> all = new List<Square>();
        public Square[] mas = new Square[4];
        public int[,] mas_per0 = new int[4,2];
        public int[,] mas_per1 = new int[4,2];
        Button New_game;
        int v = 0;
        Panel p;
        Label ball;
        Label lose;
        int a = 0;
        public Shape(Panel p,Label ball,Label lose,Button b)
        {
            New_game = b;
            this.p = p;
            this.ball = ball;
            this.lose = lose;
        }
        public void Creat(int a)
        {
            this.a = a;
            int top = -40;
            for (int i = 0; i < 4; i++)
            {
                mas[i] = new Square();
                mas[i].Size = new Size(20, 20);
                mas[i].Top = 0;
                mas[i].BackColor = Color.Red;
            }
            switch (a)
            {
                case 0:// I
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            mas[i].Left = (60 + 20 * i);
                        }

                        mas_per0[0, 0] = 20;
                        mas_per0[0, 1] = -20;
                        mas_per0[1, 0] = 0;
                        mas_per0[1, 1] = 0;
                        mas_per0[2, 0] = 40;
                        mas_per0[2, 1] = 20;
                        mas_per0[3, 0] = 60;
                        mas_per0[3, 1] = 40;
                        break;
                    }
                case 1: // O
                    {
                        mas[0].Left = 80;
                        mas[1].Left = 100;
                        mas[2].Left = 80;
                        mas[3].Left = 100;
                        mas[2].Top = 20;
                        mas[3].Top = 20;
                        break;
                    }
                case 2: // J
                    {
                        mas[0].Left = 80;
                        mas[1].Left = 80;
                        mas[2].Left = 100;
                        mas[3].Left = 120;
                        mas[1].Top = 20;
                        mas[2].Top = 20;
                        mas[3].Top = 20;

                        mas_per0[0, 0] = 20;
                        mas_per0[0, 1] = -20;
                        mas_per0[1, 0] = 0;
                        mas_per0[1, 1] = 0;
                        mas_per0[2, 0] = 40;
                        mas_per0[2, 1] = 20;
                        mas_per0[3, 0] = 60;
                        mas_per0[3, 1] = 40;
                        break;
                    }
                case 3:// L
                    {
                        mas[0].Left = 60;
                        mas[1].Left = 80;
                        mas[2].Left = 100;
                        mas[3].Left = 100;
                        mas[0].Top = 20;
                        mas[1].Top = 20;
                        mas[2].Top = 20;
                        break;
                    }
                case 4:// S
                    {
                        mas[0].Left = 60;
                        mas[1].Left = 80;
                        mas[2].Left = 80;
                        mas[3].Left = 100;
                        mas[0].Top = 20;
                        mas[1].Top = 20;
                        break;
                    }
                case 5: // Z
                    {
                        mas[0].Left = 80;
                        mas[1].Left = 100;
                        mas[2].Left = 100;
                        mas[3].Left = 120;
                        mas[2].Top = 20;
                        mas[3].Top = 20;
                        break;
                    }
                case 6: // T
                    {
                        mas[0].Left = 60;
                        mas[1].Left = 80;
                        mas[2].Left = 80;
                        mas[3].Left = 100;
                        mas[0].Top = 20;
                        mas[1].Top = 20;
                        mas[3].Top = 20;
                        break;
                    }
            }
            for (int i = 0; i < 4; i++)
            {
                mas[i].Top += top;
                all.Add(mas[i]);
                p.Controls.Add(all[all.Count-1]);
            }
        }
        public bool Move()
        {
            bool flag = true;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < all.Count-4; j++)
                {
                    if (all[all.Count -1 -i].Top + 20 == all[j].Top && all[all.Count - 1 - i].Left == all[j].Left)
                    {
                        flag = false;
                        break;
                    }
                }
                if (all[all.Count - 1 - i].Top + 20 == 400)
                {
                    flag = false;
                    break;
                }
            }

            if (flag)
            {
                for (int i = 0; i < 4; i++)
                {
                    all[all.Count - 1 - i].Top += 20;
                }
            }
            else
                Delet();
            return flag;
        }
        public int[,]  Povorot_mat(int[,] matr,int n)
        {
            int tmp;
            for (int i = 0; i < n / 2; i++)
            {
                for (int j = i; j < n - 1 - i; j++)
                {
                    tmp = matr[i, j];
                    matr[i, j] = matr[n - j - 1, i];
                    matr[n - j - 1, i] = matr[n - i - 1, n - j - 1];
                    matr[n - i - 1, n - j - 1] = matr[j, n - i - 1];
                    matr[j, n - i - 1] = tmp;
                }
            }
            return matr;
        }
        public void Povorot()
        {
            int n = 0;
            switch (a)
            {
                case 0:n = 4;break;
                case 1: return;
                default: n = 4;break;
            }
            int[,] mas = new int[20, 10];
            for (int i = 0; i < all.Count; i++)
            {
                try
                {
                    mas[all[i].Top / 20, all[i].Left / 20] = i + 1;
                }
                catch { return; }
            }
            int min_top= 1000;
            int min_left=1000;
            for (int i = all.Count - 1; i >= all.Count-n; i--)
            {
                min_left = Math.Min(all[i].Left, min_left);
                min_top = Math.Min(all[i].Top, min_top);
            }
            int[,] mas_help = new int[n, n];
            int k = 0;
            for (int i = min_top/20; i < min_top/20 +n; i++)
            {
                for (int j = min_left/20; j < min_left/20 +n; j++)
                {
                    try
                    {
                        if (mas[i, j] != 0)
                            k++;
                    }
                    catch { return; }
                    mas_help[i - (min_top / 20), j - min_left / 20] = mas[i, j];
                }
            }
            if (k > 4)
                return;
            mas_help = Povorot_mat(mas_help, n);
            for (int i = min_top / 20; i < min_top / 20 + n; i++)
            {
                for (int j = min_left / 20; j < min_left / 20 + n; j++)
                {
                    if (mas[i, j] != 0)
                        k++;
                    mas[i,j] = mas_help[i - (min_top / 20), j - min_left / 20];
                    if (mas[i, j] != 0)
                    {
                        all[mas[i, j] - 1].Top = (i) * 20;
                        all[mas[i, j] - 1].Left = (j-1) * 20;
                    }
                }
            }

        }
        public void Control(int n)
        {
            int b = 0;
            switch (n)
            {
                case 1: b = -20; break;
                case 2: b = 20; break;
                case 3: Povorot();return;
            }
            bool flag = true;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < all.Count - 4; j++)
                {
                    if (all[all.Count - 1 - i].Left + b == all[j].Left && all[all.Count - 1 - i].Top == all[j].Top)
                    {
                        flag = false;
                        break;
                    }
                }
                if (all[all.Count - 1 - i].Left + b == -20 || all[all.Count - 1 - i].Left + b == 200)
                {
                    flag = false;
                    break;
                }
            }

            if (flag)
            {
                for (int i = 0; i < 4; i++)
                {
                    all[all.Count - 1 - i].Left += b;
                }
            }
        }
        public void Delet()
        {
            int[,] mas = new int[20, 10];
            for (int i = 0; i < all.Count; i++)
            {
                try
                {
                    mas[all[i].Top / 20 - 1, all[i].Left / 20] = i + 1;
                }
                catch { StreamWriter sw = new StreamWriter("input.txt",true); lose.Visible = true;New_game.Visible = true;sw.WriteLine(ball.Text.Split()[1]);sw.Close();}
            }
            List<Square> help = new List<Square>();
            for (int i = 0; i < 20; i++)
            {
                int k = 0;
                for (int j = 0; j < 10; j++)
                {
                    if (mas[i,j]!=0)
                    k ++;
                }
                if (k == 10)
                {
                    string[] s = ball.Text.Split();
                    ball.Text = s[0] + " " + (int.Parse(s[1]) + 1000).ToString();
                    for (int j = 0; j < 10; j++)
                    {
                        p.Controls.Remove(all[mas[i, j]-1]);
                    }
                    for (int j = 0; j < all.Count; j++)
                    {
                        if (all[j].Top / 20 - 1 < i)
                        {
                            all[j].Top += 20;
                        }
                    }
                }
                else
                    for (int j = 0; j < 10; j++)
                    {
                        if (mas[i,j]!=0)
                        help.Add(all[mas[i, j]- 1]);
                    }
            }
            all.Clear();
            all = help;
            GC.Collect();
        }
    }
}
