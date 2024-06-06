using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Получение_СВ
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form f1 = Application.OpenForms[0];    //отрываем главную форму
            f1.Show();      //показываем главную форму
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            listBox1.Items.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || (int)e.KeyChar == 8 || e.KeyChar == 45 || e.KeyChar == 44)) e.KeyChar = (char)0;
            if (e.KeyChar == '-') e.KeyChar = (char)0;
            if (e.KeyChar == 44) e.KeyChar = (char)0;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || (int)e.KeyChar == 8 || e.KeyChar == 45 || e.KeyChar == 44)) e.KeyChar = (char)0;
            if (e.KeyChar == '-' && textBox2.Text.Length > 0) e.KeyChar = (char)0;
            if (e.KeyChar == '-' && textBox2.SelectionStart != 0) e.KeyChar = (char)0;
            if (e.KeyChar == 44 && textBox2.Text.Contains(",")) e.KeyChar = (char)0;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || (int)e.KeyChar == 8 || e.KeyChar == 45 || e.KeyChar == 44)) e.KeyChar = (char)0;
            if (e.KeyChar == '-' && textBox3.Text.Length > 0) e.KeyChar = (char)0;
            if (e.KeyChar == '-' && textBox3.SelectionStart != 0) e.KeyChar = (char)0;
            if (e.KeyChar == 44 && textBox3.Text.Contains(",")) e.KeyChar = (char)0;
        }

        private void textBox4_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || (int)e.KeyChar == 8 || e.KeyChar == 45 || e.KeyChar == 44)) e.KeyChar = (char)0;
            if (e.KeyChar == '-' && textBox4.Text.Length > 0) e.KeyChar = (char)0;
            if (e.KeyChar == '-' && textBox4.SelectionStart != 0) e.KeyChar = (char)0;
            if (e.KeyChar == 44 && textBox4.Text.Contains(",")) e.KeyChar = (char)0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = int.Parse(textBox1.Text);
            double a = double.Parse(textBox2.Text);   //получение значения минимума a
            double b = double.Parse(textBox3.Text);   //получение значения максимума b
            double d = double.Parse(textBox4.Text);     //получение значения моды d
            double[] x = new double[count];
            double[] F = new double[count];
            Random rnd = new Random();
            double X = 0;
            for (int i = 0; i < count; i++)
            {
                double R = rnd.NextDouble();    //генерация случайного числа в диапазоне от 0 до 1
                if (R >= 0 && R <= ((d - a) / (b - a))) X = a + Math.Sqrt((d - a) * (b - a) * R);
                else if (R > (d - a) / (b - a) && R <= 1) X = b - Math.Sqrt((b - d) * (b - a) * (1 - R));
                X = Math.Round(X, 3);
                listBox1.Items.Add(X.ToString());
                x[i] = X;   //Присвоение значения X
                /*for (int p = 0; p < count - 1; p++)
                {
                    for (int j = 0; j < count - 1; j++)
                    {
                        if (x[j] > x[j + 1])
                        {
                            double temp = x[j];
                            x[j] = x[j + 1];
                            x[j + 1] = temp;
                        }
                    }
                }*/
                //Вычисление значения функции в точке X
                if (X < a) F[i] = 0;
                else if (X >= a && X < d) F[i] = 2 * (X - a) / (b - a) * (d - a);
                else if (X == d) F[i] = 2 / b - a;
                else if (X > d && X <= b) F[i] = 2 * (b - X) / (b - a) * (b - d);
                else if (X > b) F[i] = 0;
            }
            //Настраиваем осии графика
            chart1.ChartAreas[0].AxisX.Maximum = a;
            chart1.ChartAreas[0].AxisX.Maximum = b;
            //Определяем шаг сетки
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
            //Добавляем вычисленные значения на график
            chart1.Series[0].Points.DataBindXY(x, F);
        }
    }
}
