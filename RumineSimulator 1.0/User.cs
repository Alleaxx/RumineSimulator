using System;
using System.Collections.Generic;

namespace RumineSimulator
{
    internal class User
    {
        public List<Events> UsersEvents = new List<Events>();
        private Random random = new Random();

        public string nickname { get; private set; }

        public string registration { get; private set; }

        public int messages { get; private set; }

        public int likes { get; private set; }

        public int StayPoss { get; private set; }

        public bool active { get; set; }

        public bool Rak { get; set; }

        public bool Banned { get; set; }

        public string group { get; private set; }

        public bool mod { get; private set; }

        public int ChangePoss { get; private set; }

        internal User(UserNickBank nicks)
        {
            this.nickname = nicks.SelectNick();
            this.registration = this.random.Next(2011, 2014).ToString();
            int num = this.random.Next(10);
            if (num == 0 | num == 1 | num == 2 | num == 3)
            {
                this.messages = this.random.Next(1, 1000);
                this.likes = this.random.Next(1, 1000);
            }
            else if (num == 4 | num == 5 | num == 6)
            {
                this.messages = this.random.Next(1, 3000);
                this.likes = this.random.Next(1, 2000);
            }
            else if (num == 7 | num == 8)
            {
                this.messages = this.random.Next(1, 8000);
                this.likes = this.random.Next(1, 8000);
            }
            else if (num == 9)
            {
                this.messages = this.random.Next(1, 12000);
                this.likes = this.random.Next(1, 12000);
            }
            this.StayPoss = this.random.Next(1, 11);
            this.Rak = this.random.Next(6) == 0;
            this.GroupGet();
            this.Banned = this.random.Next(15) == 0;
            if (this.random.Next(20) == 0 & !this.Banned)
            {
                this.mod = true;
                this.group = "Модератор";
            }
            else
                this.mod = false;
            this.ChangePoss = this.random.Next(1, 16);
            if (this.random.Next(5) == 0)
                this.active = false;
            else
                this.active = true;
        }

        public bool GroupGet()
        {
            int num = this.random.Next(12);
            string str;
            if (num == 0 | num == 1)
                str = "Посетители";
            else if (num == 2 | num == 3 | num == 4)
                str = "ПХЛ";
            else if (num == 5 | num == 6)
                str = "Олдфаги";
            else if (num == 7 | num == 8)
            {
                str = "ХХХL ПХЛ";
            }
            else
            {
                switch (num)
                {
                    case 9:
                        str = "Журналисты";
                        break;
                    case 10:
                        str = "Журналисты-олдфаги";
                        break;
                    case 11:
                        str = "Модератор бездны";
                        break;
                    default:
                        str = "Гости";
                        break;
                }
            }
            if (this.group == str)
                return false;
            this.group = str;
            return true;
        }

        public string GroupPromotion()
        {
            string group = this.group;
            if (this.group == "Посетители")
                this.group = this.random.Next(2) != 0 ? "Модератор бездны" : "ПХЛ";
            else if (this.group == "ПХЛ" | this.group == "Модератор бездны")
                this.group = this.random.Next(2) != 0 ? "Журналисты" : "ХХХL ПХЛ";
            else if (this.group == "Журналисты" | this.group == "ХХХL ПХЛ")
                this.group = "Олдфаги";
            else if (this.group == "Олдфаги" | this.group == "Модератор бездны")
                this.group = "Журналисты-олдфаги";
            return group;
        }

        public string GroupDown()
        {
            string group = this.group;
            if (this.group == "Журналисты-олдфаги")
            {
                switch (this.random.Next(4))
                {
                    case 0:
                        this.group = "ХХХL ПХЛ";
                        break;
                    case 1:
                        this.group = "Олдфаги";
                        break;
                    case 2:
                        this.group = "Модератор бездны";
                        break;
                    case 3:
                        this.group = "Журналисты";
                        break;
                }
            }
            else if (this.group == "Олдфаги" | this.group == "Модератор бездны" | this.group == "Журналисты")
                this.group = this.random.Next(2) != 0 ? "ПХЛ" : "ХХХL ПХЛ";
            else if (this.group == "ПХЛ" | this.group == "ХХХL ПХЛ")
                this.group = "Посетители";
            return group;
        }
    }
}
