using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KProject
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int dayNumber;
        double?[] height;
        double?[] heightS;
        double[] days;


        public MainWindow()
        {
            InitializeComponent();
        }

        public double? lagrangeInterpolation(double[] xs, double?[] ys, double x)
        {
            double t;
            double? y = 0.0;

            for (int k = 0; k < xs.Length; k++)
            {
                t = 1.0;
                for (int j = 0; j < xs.Length; j++)
                {
                    if (j != k)
                    {
                        t = t * ((x - xs[j]) / (xs[k] - xs[j]));
                    }
                }
                y += t * ys[k];
            }
            return y;
        }

        

        private void Next1_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dayNumber = int.Parse(Number_of_Days.Text);
            }
            catch (Exception x)
            {
                ValidationLB.Visibility = Visibility.Visible;
            }
            if(dayNumber >= 3)
            {
                height = new double?[dayNumber];
                _2.Visibility = Visibility.Visible;
                _1.Visibility = Visibility.Hidden;
            }
            else
            {
                ValidationLB.Visibility = Visibility.Visible;
            }
        }

        private void Next2_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                height[int.Parse(Number_Of_DayTB.Text) - 1] = double.Parse(ValueTB.Text);
                Number_Of_DayTB.Text = (int.Parse(Number_Of_DayTB.Text) + 1).ToString();
                ValueTB.Text = "";
            }
            catch (Exception x)
            {
                ValidationLB2.Visibility = Visibility.Visible;
            }
            if (int.Parse(Number_Of_DayTB.Text) > height.Length)
            {
                for (int i = 0; i < height.Length; i++)
                {
                    Number_Of_Days2.Items.Add(i+1);
                }
                int counter = 0;
                int counterNulls = 0;
                for (int i = 0; i < height.Length; i++)
                {
                    if(height[i] == null)
                    {
                        counterNulls++;
                    }
                }
                heightS = new double?[height.Length - counterNulls];
                for (int i = 0; i < height.Length; i++)
                {
                    if(height[i] != null)
                    {
                        heightS[counter] = height[i];
                        counter++;
                    }
                }
                counter = 0;
                days = new double[heightS.Length];
                for(int i = 0; i < height.Length; i++)
                {
                    if(height[i] != null)
                    {
                        days[counter] = i;
                        counter++;
                    }
                }
                Number_Of_Days2.SelectedIndex = 0;
                _3.Visibility = Visibility.Visible;
                _2.Visibility = Visibility.Hidden;
            }
        }

        private void Empty_Button_Click(object sender, RoutedEventArgs e)
        {
            if(int.Parse(Number_Of_DayTB.Text) <= height.Length)
            {
                try
                {
                    height[int.Parse(Number_Of_DayTB.Text) - 1] = null;
                    if (int.Parse(Number_Of_DayTB.Text) < height.Length)
                        Number_Of_DayTB.Text = (int.Parse(Number_Of_DayTB.Text) + 1).ToString();
                    else
                        Number_Of_DayTB.Text = "end";
                    ValueTB.Text = "";
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            Result_Label.Content = "Wynik dla dnia: " + Number_Of_Days2.SelectedValue.ToString()
                + " wynosi: " + lagrangeInterpolation(days, heightS, 
                int.Parse(Number_Of_Days2.SelectedValue.ToString())-1).ToString();
        }
    }
}
