
using System;
using System.Collections.Generic;
using System.Linq;

namespace RumineSimulator
{
    internal class DateProgress
    {
        private int day;
        private int month;
        private string data;
        private List<double> PrevMonth = new List<double>();
        private List<User> ActiveUsersperDay = new List<User>();
        private Random random = new Random();

        public int year { get; private set; }

        public double AllPages { get; private set; }

        public double AveragePage { get; private set; }

        public DateProgress(int day, int month, int year, int AvAct)
        {
            this.day = day;
            this.month = month;
            this.year = year;
            this.AveragePage = (double)AvAct;
            this.PrevMonth.Add((double)AvAct);
        }

        public void AddPages(double pages) => this.PrevMonth.Add(pages);

        public double MonthCount()
        {
            this.AveragePage = (uint)this.PrevMonth.Count <= 0U ? 30.0 : this.PrevMonth.Average();
            this.PrevMonth.Clear();
            return this.AveragePage;
        }

        public string DayPass(double pages, UsersStorage users)
        {
            this.ActiveUsersperDay.Clear();
            if (this.MonthCheck())
            {
                ++this.day;
                return this.day == 15 ? "ПолМесяца" : "День";
            }
            if (this.month == 12)
            {
                this.day = 1;
                this.month = 1;
                ++this.year;
                return "Год";
            }
            this.day = 1;
            ++this.month;
            if (this.month == 3 & this.day == 1)
                return "Весна";
            if (this.month == 6 & this.day == 1)
                return "Лето";
            if (this.month == 9 & this.day == 1)
                return "Осень";
            if (this.month == 12 & this.day == 1)
                return "Зима";
            return this.month == 7 ? "ПолГода" : "Месяц";
        }

        private void ActiveUsersPerDay(UsersStorage Users, int pages)
        {
            List<int> intList = new List<int>();
            for (int index = 0; index < pages / 2; ++index)
                intList.Add(this.random.Next(Users.amount));
            for (int index = 1; index < intList.Count; ++index)
            {
                if (intList[index] == intList[index - 1])
                    intList.RemoveAt(index);
            }
            for (int index1 = 0; index1 < intList.Count; ++index1)
            {
                int index2 = intList[index1];
                if (Users.users[index2].active)
                    this.ActiveUsersperDay.Add(Users.users[intList[index1]]);
            }
        }

        public string DateGenerateText(string type)
        {
            if (type == "Полная")
                return Convert.ToString(this.day) + this.MonthSelector("ТекстДата") + Convert.ToString(this.year);
            if (type == "Месяц")
                return this.MonthSelector("ПредМесяц");
            if (!(type == "Месяц+Год"))
                return "null";
            this.MonthSelector("ПредМесяц");
            return this.month != 1 ? this.MonthSelector("ПредМесяц") + Convert.ToString(this.year) : this.MonthSelector("ПредМесяц") + Convert.ToString(this.year - 1);
        }

        public string DateGenerateNumeric() => (this.day >= 10 ? this.day.ToString() : "0" + (object)this.day) + "." + (this.month >= 10 ? this.month.ToString() : "0" + (object)this.month) + "." + (this.year - 2000).ToString();

        public string PrevDayDateGenerate()
        {
            string str1 = Convert.ToString(this.month);
            string str2 = Convert.ToString(this.month - 1);
            if (int.Parse(str1) < 10)
            {
                str1 += "0";
                str1.Reverse<char>();
            }
            else if (int.Parse(str2) < 10)
            {
                str2 += "0";
                str2.Reverse<char>();
            }
            if (this.day == 1)
            {
                if (this.month == 1 | this.month == 3 | this.month == 5 | this.month == 7 | this.month == 8 | this.month == 10 | this.month == 12)
                    this.data = Convert.ToString(31) + "." + str2 + "." + Convert.ToString(this.year - 2000);
                else if (this.month == 4 | this.month == 6 | this.month == 9 | this.month == 11)
                    this.data = Convert.ToString(30) + "." + str2 + "." + Convert.ToString(this.year - 2000);
                else
                    this.data = Convert.ToString(28) + "." + str2 + "." + Convert.ToString(this.year - 2000);
            }
            else
                this.data = Convert.ToString(this.day - 1) + "." + Convert.ToString(str1) + "." + Convert.ToString(this.year - 2000);
            return this.data;
        }

        private string MonthSelector(string pad)
        {
            string str = "";
            if (pad == "ТекстДата")
            {
                switch (this.month)
                {
                    case 1:
                        str = " Января ";
                        break;
                    case 2:
                        str = " Февраля ";
                        break;
                    case 3:
                        str = " Марта ";
                        break;
                    case 4:
                        str = " Апреля ";
                        break;
                    case 5:
                        str = " Мая ";
                        break;
                    case 6:
                        str = " Июня ";
                        break;
                    case 7:
                        str = " Июля ";
                        break;
                    case 8:
                        str = " Августа ";
                        break;
                    case 9:
                        str = " Сентября ";
                        break;
                    case 10:
                        str = " Октября ";
                        break;
                    case 11:
                        str = " Ноября ";
                        break;
                    case 12:
                        str = " Декабря ";
                        break;
                    case 13:
                        str = "Января ";
                        break;
                }
            }
            else
            {
                if (pad == "Обычный")
                {
                    switch (this.month)
                    {
                        case 0:
                            str = "Декабрь ";
                            break;
                        case 1:
                            str = "Январь ";
                            break;
                        case 2:
                            str = " Февраль ";
                            break;
                        case 3:
                            str = "Март ";
                            break;
                        case 4:
                            str = "Апрель ";
                            break;
                        case 5:
                            str = "Май ";
                            break;
                        case 6:
                            str = "Июнь ";
                            break;
                        case 7:
                            str = "Июль ";
                            break;
                        case 8:
                            str = "Август ";
                            break;
                        case 9:
                            str = "Сентябрь ";
                            break;
                        case 10:
                            str = "Октябрь ";
                            break;
                        case 11:
                            str = "Ноябрь ";
                            break;
                        case 12:
                            str = "Декабрь ";
                            break;
                        case 13:
                            str = "Январь ";
                            break;
                    }
                    return str;
                }
                if (pad == "ПредМесяц")
                {
                    switch (this.month)
                    {
                        case 1:
                            str = "Декабрь ";
                            break;
                        case 2:
                            str = " Январь ";
                            break;
                        case 3:
                            str = "Февраль ";
                            break;
                        case 4:
                            str = "Март ";
                            break;
                        case 5:
                            str = "Апрель ";
                            break;
                        case 6:
                            str = "Май ";
                            break;
                        case 7:
                            str = "Июнь ";
                            break;
                        case 8:
                            str = "Июль ";
                            break;
                        case 9:
                            str = "Август ";
                            break;
                        case 10:
                            str = "Сентябрь ";
                            break;
                        case 11:
                            str = "Октябрь ";
                            break;
                        case 12:
                            str = "Ноябрь ";
                            break;
                    }
                    return str;
                }
            }
            return str;
        }

        private bool MonthCheck() => !((this.month == 1 | this.month == 3 | this.month == 5 | this.month == 7 | this.month == 8 | this.month == 10 | this.month == 12) & this.day == 31) && !((this.month == 4 | this.month == 6 | this.month == 9 | this.month == 11) & this.day == 30) && !(this.month == 2 & this.day == 28);
    }
}
