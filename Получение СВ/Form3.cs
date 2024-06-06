using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Получение_СВ
{
    public partial class Form3 : Form
    {
        public Form3()
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

        private void button1_Click(object sender, EventArgs e)
        {
            int count = int.Parse(textBox1.Text);
            double l = double.Parse(textBox2.Text);     //получение интенсивности из textBox 
            double[] x = new double[count];
            double[] F = new double[count];
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                double R = rnd.NextDouble();    //генерация случайного числа в диапазоне от 0 до 1
                double X = (-1 / l) * Math.Log(R);   //формула для получения случайной величины с экспоненциальным (показательным) распределением
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
                if (X >= 0) F[i] = l * Math.Exp(-(l * X));
                else F[i] = 0;
            }
            //Настраиваем осии графика
            chart1.ChartAreas[0].AxisX.Maximum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = l + 1;
            //Определяем шаг сетки
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 5;
            //Добавляем вычисленные значения на график
            chart1.Series[0].Points.DataBindXY(x, F);
        }
    }
}
