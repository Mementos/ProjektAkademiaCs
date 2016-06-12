using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;




namespace Projekt
{
    class RunningTabMethods
    {
        public static void CheckingDigit(object sender, TextCompositionEventArgs e)
        {
            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || e.Text == ","))
                e.Handled = true;
        }

        public static void PeriodKey(object sender, KeyEventArgs e, TextBox textBox)
        {
            if (e.Key == Key.OemComma && (sender as TextBox).Text.Contains(','))
                e.Handled = true;
            if (e.Key == Key.OemComma && textBox.SelectionStart == 0)
                e.Handled = true;
        }

        public static void UpdateRunList(ListBox listBox, List<PhysicalActivity> ActivityList)
        {
            listBox.Items.Clear();
            for (int i = 0; i < ActivityList.Count; i++)
            {
                listBox.Items.Add(String.Format("{0,-10} {1,15} {2, 15} {3, 20} {4, 35}", ActivityList[i].Date, ActivityList[i].Distance, ActivityList[i].Time, ActivityList[i].Calories, ActivityList[i].GetInfo()));
            }
        }
        public static void SaveLol(object sender, RoutedEventArgs e, List<PhysicalActivity> ActivityList, TextBox textDistance, TextBox textTime, TextBox textWeight, bool isRun, ActivityType chosenActivity)
        {
            PhysicalActivity MyActivity;
            if (isRun)
                MyActivity = new Run();
            else
                MyActivity = new Swim();

            MyActivity.ActivityType = chosenActivity;
         
            MyActivity.Date = DateTime.Today.ToShortDateString();
            try
            {
                MyActivity.Distance = double.Parse(textDistance.Text);
            }
            catch
            {
                MessageBox.Show("Wpisz pokonany dystans!");
            }
            try
            {
                MyActivity.Time = double.Parse(textTime.Text);
            }
            catch
            {
                MessageBox.Show("Wpisz czas!");
            }
            try
            {
                MyActivity.Calories = double.Parse(textDistance.Text) * double.Parse(textWeight.Text) * 1.01;
            }
            catch
            {
                MessageBox.Show("Wpisz wagę, aby obliczyć spalone kalorie!");
            } 
            textTime.Text = "";
            textDistance.Text = "";
            textWeight.Text = "";
            ActivityList.Add(MyActivity);

        }

    }
}
