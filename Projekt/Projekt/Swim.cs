namespace Projekt
{
    class Swim : PhysicalActivity
    {

        public override string GetInfo()
        {
            return this.ActivityType + "_Swim";
        }

        public Swim()
        {

        }

        public Swim(string date, double distance, double time, double calories, ActivityType activityType)
        {
            this.Date = date;
            this.Distance = distance;
            this.Time = time;
            this.Calories = calories;
            this.ActivityType = activityType;
        }
    }
}
