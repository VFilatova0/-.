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
    public partial class Form5 : Form
    {
        public Form5()
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
            if (e.KeyChar == 44) e.KeyChar = (char)0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = int.Parse(textBox1.Text);
            double m = double.Parse(textBox2.Text);     //получение значения математического ожидания из textBox
            double k = double.Parse(textBox3.Text);     //получение значения потока Эрланга из textBox
            double P = 1;   //произведение случайных величин
            double[] x = new double[count];
            double[] F = new double[count];
            double l = 1 / m;
            //расчёт значения факториала k - 1
            double fact = 1;
            for(int i = 1; i <= (k - 1); i++)
            {
                fact *= i;
            }
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                for (int j = 1; j < k; j++)
                {
                    double R = rnd.NextDouble();    //генерация случайного числа в диапазоне от 0 до 1
                    P *= R;
                }
                double X = -m + Math.Log(P);   //формула для получения случайного числа, соответствующего распределению Эрланга
                X = Math.Round(X, 3);
                listBox1.Items.Add(X.ToString());
                x[i] = X;   //Присвоение значения X
                //Вычисление значения функции в точке X
                if (X >= 0 && l >= 0) F[i] = (Math.Pow(l, k) * Math.Pow(X, k - 1) * Math.Exp(-l * X)) / fact;
            }
            //Настраиваем осии графика
            chart1.ChartAreas[0].AxisX.Maximum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 100;
            //Определяем шаг сетки
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 10;
            //Добавляем вычисленные значения на график
            chart1.Series[0].Points.DataBindXY(x, F);
        } 
    }
}
//график
