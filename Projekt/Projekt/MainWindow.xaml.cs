using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.ComponentModel;

namespace Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<PhysicalActivity> ActivityList;

        private double totalDistance;

        public double TotalDistance
        {
            get { return totalDistance; }
            set
            {
                double temp = 0;
                foreach (var item in ActivityList)
                {
                    
                  temp += item.Distance;
                }

                totalDistance = temp;
                NotifyPropertyChanged("TotalDistance");
            }
        }





        private int runCount;

        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int RunCount
        {
            get
            {
                return runCount;
            }
            set
            {
                runCount = ActivityList.Count;
                NotifyPropertyChanged("RunCount");
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            ActivityList = new List<PhysicalActivity>();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            labelDate.Content = DateTime.Now.Date.ToLongDateString();
            labelTime.Content = DateTime.Now.ToLongTimeString();
            if (File.Exists("lista.txt"))
            {
                ListOfPhysicalActivities.LoadList("lista.txt", listBox, ActivityList);
                RunCount++;
                TotalDistance++;
            }

        }
        
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            labelTime.Content = DateTime.Now.ToLongTimeString();
            labelDate.Content = (DateTime.Now.Date.ToLongDateString());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aby wprowadzić nową notatkę zakładce 'Notatka', należy kolejno:\n" +
                " -wybrać interesującą nas datę,\n -wprowadzić treść notatki,\n" + " -wcisnąć przycisk 'Zapisz notatkę'.\n" + 
                "Od tego momentu po wybraniu tej daty, mamy możliwość przeglądania umieszczonej w tym dniu notatki.\n\n\n" +
                "Aby zapisać naszą aktywność należy kolejno w zakładce 'Progres':\n" +
                " -wpisać w odpowiednie pola przebyty przez nas dystans, czas, w którym go pokonaliśmy oraz naszą wagę,\n" +
                " -wybrać z rozwijalnej listy wykonywaną przez nas aktywność oraz jej typ,\n" +
                " -dodać nasz wynik przy pomocy przycisku 'Umieść na liście.'\n" +
                "W celu usunięcia aktywności z listy należy wcisnąć przycisk 'Usuń z listy'. " +
                "Możliwe jest też usunięcie wszystkich aktywności przy pomocy przycisku 'Wyczyść listę'" +
                "dzięki przyciskowi 'Zapisz progres' możemy zapisać nasze aktywności do osobnego pliku", "Pomoc");
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Program służy do planowania treningu.\n Dzięki zapisywaniu swoich wyników mamy możliwość wglądu w nasze osiągnięcia i na bierząco analizowania naszych postępów.", "O programie");
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            richTextBox.SelectAll();
            richTextBox.Cut();
            string data = Date.SelectedDate.Value.ToShortDateString() + ".rtf";

            Note note = new Note();
            note.LoadFile(data, richTextBox);

            //Load.LoadFile(data, richTextBox);
        }

        private void buttonSaveNote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string data = Date.SelectedDate.Value.ToShortDateString() + ".rtf";

                Note note = new Note();
                note.SaveFile(data, richTextBox);
                //Save.SaveFile(data, richTextBox);
            }
            catch
            {
                MessageBox.Show("Wybierz Datę!");
            }

        }

        private void textDistance_KeyDown(object sender, KeyEventArgs e)
        {
            RunningTabMethods.PeriodKey(sender, e, textDistance);
        }

        private void textTime_KeyDown(object sender, KeyEventArgs e)
        {
            RunningTabMethods.PeriodKey(sender, e, textTime);
        }

        private void textWeight_KeyDown(object sender, KeyEventArgs e)
        {
            RunningTabMethods.PeriodKey(sender, e, textWeight);
        }
        private void textTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            RunningTabMethods.CheckingDigit(sender, e);
        }

        private void textDistance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            RunningTabMethods.CheckingDigit(sender, e);
        }

        private void textWeight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            RunningTabMethods.CheckingDigit(sender, e);
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            ActivityList.Clear();
            RunningTabMethods.UpdateRunList(listBox, ActivityList);
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActivityList.RemoveAt(listBox.SelectedIndex);
                RunningTabMethods.UpdateRunList(listBox, ActivityList);
            }
            catch (Exception)
            {
                MessageBox.Show("Zaznacz aktywność do usunięcia");
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            bool isRun = false;
            if (chosenSport.Text == "Bieganie")
                isRun = true;

            ActivityType chosenActivity;
            switch (chosenTypeOfActivity.Text)
            {
                case "Interwały":
                    chosenActivity = ActivityType.Interval;
                    break;
                case "Trening":
                    chosenActivity = ActivityType.Training;
                    break;
                case "Zawody":
                    chosenActivity = ActivityType.Competition;
                    break;
                default:
                    chosenActivity = ActivityType.Training;
                    break;
            }


            RunningTabMethods.SaveLol(sender, e, ActivityList, textDistance, textTime, textWeight, isRun, chosenActivity);
            RunningTabMethods.UpdateRunList(listBox, ActivityList);
            RunCount++;
            TotalDistance++;

        }

        private void butSaveProgress_Click(object sender, RoutedEventArgs e)
        {
            ListOfPhysicalActivities.SaveList(listBox);
        }
    }
}
