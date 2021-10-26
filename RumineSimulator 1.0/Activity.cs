using System;

namespace RumineSimulator
{
    internal class Activity
    {
        private Random random = new Random();

        public double ActivityPages { get; private set; }

        public double PrevActivityPages { get; private set; }

        public int ActDownModif { get; private set; }

        public int ActUpModif { get; private set; }

        public int Chanse { get; private set; }

        public double Lampada { get; private set; }

        public double MonthMod { get; set; }

        public double SeasonMod { get; private set; }

        public Activity(double activity)
        {
            this.ActivityPages = activity;
            this.ActDownModif = 12;
            this.ActUpModif = 6;
            this.Chanse = 4;
            this.MonthMod = 1.0;
            this.SeasonMod = 1.25;
        }

        public double DayChange()
        {
            this.PrevActivityPages = this.ActivityPages;
            int num = (uint)this.random.Next(0, 4) <= 0U ? 2 : 1;
            if (this.random.Next(0, this.Chanse) == 0)
                this.ActivityPages -= this.ActivityPages * (double)this.random.Next(0, this.ActDownModif * num) * 0.01 * (2.0 - this.MonthMod);
            else
                this.ActivityPages += this.ActivityPages * (double)this.random.Next(0, this.ActUpModif * num) * 0.01 * (0.0 + this.MonthMod);
            if (this.ActivityPages > 65.0)
            {
                ++this.Lampada;
                this.ActivityPages *= 0.95;
                if (this.ActivityPages > 100.0)
                    this.ActivityPages *= 0.7;
            }
            else if (this.ActivityPages < 3.0)
                this.ActivityPages += (double)this.random.Next(0, 4);
            else if (Math.Round(this.PrevActivityPages) == Math.Round(this.ActivityPages))
                this.ActivityPages *= (double)this.random.Next(9, 12) * 0.1;
            this.Lampada += this.ActivityPages * 0.01;
            return this.ActivityPages * this.SeasonMod;
        }

        public void HalfMonthChange()
        {
        }

        public void MonthChange() => this.Chanse = 4;

        public void SeasonChange(string season)
        {
            if (season == "Весна")
                this.SeasonMod = 1.1;
            else if (season == "Лето")
                this.SeasonMod = 1.25;
            else if (season == "Осень")
            {
                this.SeasonMod = 0.7;
            }
            else
            {
                if (!(season == "Зима"))
                    return;
                this.SeasonMod = 0.9;
            }
        }

        public void HalfYearChange() => this.ActDownModif += this.random.Next(0, 2);

        public void YearChange(int year)
        {
            if (year > 2016)
            {
                this.MonthMod = 0.8;
                this.ActDownModif += this.random.Next(2, 5);
            }
            else
            {
                if (year >= 2016)
                    return;
                this.MonthMod = 1.0;
                this.ActDownModif += this.random.Next(2, 4);
            }
        }
    }
}
