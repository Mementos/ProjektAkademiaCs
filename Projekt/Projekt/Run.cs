namespace Projekt
{
    class Run : PhysicalActivity
    {

        public override string GetInfo()
        {
            return this.ActivityType + "_Run";
        }

        public Run()
        {

        }

        public Run(string date, double distance, double time, double calories, ActivityType activityType)
        {
            this.Date = date;
            this.Distance = distance;
            this.Time = time;
            this.Calories = calories;
            this.ActivityType = activityType;
        }
    }
}
