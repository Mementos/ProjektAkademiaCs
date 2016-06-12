using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace Projekt
{
    class ListOfPhysicalActivities
    {
        public static void SaveList(ListBox listBox)
        {
            string filename = "lista.txt";
            StreamWriter save = new StreamWriter(filename);
            foreach (string str in listBox.Items)
            {
                save.WriteLine(str);
            }
            save.Close();
        }

        public static void LoadList(string fileName, ListBox listBox, List<PhysicalActivity> ActivityList)
        {
            listBox.Items.Clear();
            PhysicalActivity MyActivity;
            
            //= new PhysicalActivity();
            StreamReader load = new StreamReader(fileName);
            string text = File.ReadAllText(fileName);
            string[] tab = text.Split(new char[] { '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            

            for (int i = 0; i < tab.Length; i += 5)
            {
                try
                {
                    switch (tab[i + 4])
                    {
                        case "Competition_Swim\r":
                            MyActivity = new Swim();
                            MyActivity.ActivityType = ActivityType.Competition;
                            break;
                        case "Training_Swim\r":
                            MyActivity = new Swim();
                            MyActivity.ActivityType = ActivityType.Training;
                            break;
                        case "Interval_Swim\r":
                            MyActivity = new Swim();
                            MyActivity.ActivityType = ActivityType.Interval;
                            break;
                        case "Competition_Run\r":
                            MyActivity = new Run();
                            MyActivity.ActivityType = ActivityType.Competition;
                            break;
                        case "Training_Run\r":
                            MyActivity = new Run();
                            MyActivity.ActivityType = ActivityType.Training;
                            break;
                        case "Interval_Run\r":
                            MyActivity = new Run();
                            MyActivity.ActivityType = ActivityType.Interval;
                            break;
                        default:
                            MyActivity = new Run();
                            MyActivity.ActivityType = ActivityType.Interval;
                            break;
                    }
                    MyActivity.Date = tab[i];
                    MyActivity.Distance = Double.Parse(tab[i + 1]);
                    MyActivity.Time = Double.Parse(tab[i + 2]);
                    MyActivity.Calories = Double.Parse(tab[i + 3]);
                    MyActivity.Info = tab[i + 4];
                    ActivityList.Add(MyActivity);
                    RunningTabMethods.UpdateRunList(listBox, ActivityList);
                }
                catch
                {

                }
            }

            load.Close();
        }
    }
}
