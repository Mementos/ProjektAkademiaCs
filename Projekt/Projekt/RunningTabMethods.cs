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
                listBox.Items.Add(String.Format("{0,10} {1,20} {2, 20} {3, 20} {4, 20}", ActivityList[i].Date, ActivityList[i].Distance, ActivityList[i].Time, ActivityList[i].Calories, ActivityList[i].GetInfo()));
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
            MyActivity.Distance = double.Parse(textDistance.Text);
            MyActivity.Time = double.Parse(textTime.Text);
            if (textDistance.Text == "" || textWeight.Text == "")
                e.Handled = true;
            else
                MyActivity.Calories = double.Parse(textDistance.Text) * double.Parse(textWeight.Text) * 1.01;
            textTime.Text = "";
            textDistance.Text = "";
            textWeight.Text = "";
            ActivityList.Add(MyActivity);

        }

    }
}
