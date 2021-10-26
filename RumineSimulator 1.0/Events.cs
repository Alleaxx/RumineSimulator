using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RumineSimulator
{
    internal class Events
    {
        private Form Description;
        private List<User> Inv_people = new List<User>();
        private Random random = new Random();

        public User Creator { get; private set; }

        public string date { get; private set; }

        public string description { get; private set; }

        public string reason { get; private set; }

        public string type { get; private set; }

        public string kr_name { get; set; }

        public int event_id { get; private set; }

        public double influence { get; private set; }

        public Events(
          string passed,
          UsersStorage Users,
          DateProgress Dates,
          double pages,
          int ev_amount)
        {
            this.event_id = ev_amount + 1;
            if (!(passed == "День"))
                return;
            this.DayGenerate(Users, Dates, pages);
        }

        private void DayGenerate(UsersStorage Users, DateProgress Dates, double pages)
        {
            this.Creator = Users.users[this.random.Next(Users.amount)];
            this.date = Dates.DateGenerateNumeric();
            if (this.Creator.active)
            {
                this.type = "Активный юзер";
                this.random.Next(3);
                if (this.random.Next(3) == 1)
                {
                    this.random.Next(3);
                    if (this.Creator.Rak & !this.Creator.Banned & !this.Creator.mod)
                    {
                        int num = this.random.Next(3);
                        this.type = "Бан";
                        switch (num)
                        {
                            case 0:
                                this.kr_name = "Бан рака";
                                this.Creator.Banned = true;
                                this.description = Dates.DateGenerateNumeric() + " забанили рака " + this.Creator.nickname;
                                this.influence = 0.02;
                                break;
                            case 1:
                                this.kr_name = "Бан активного рака";
                                this.Creator.Banned = true;
                                this.description = Dates.DateGenerateNumeric() + " рак " + this.Creator.nickname + " агресивно срачился, но все-таки был забанен";
                                this.influence = 0.05;
                                break;
                            default:
                                this.kr_name = "Рак без бана";
                                this.description = Dates.DateGenerateNumeric() + " раку " + this.Creator.nickname + " чудом удалось избежать бана";
                                this.influence = 0.0;
                                break;
                        }
                    }
                    else if (!this.Creator.Rak & !this.Creator.Banned & !this.Creator.mod & this.random.Next(2) == 0)
                    {
                        int num = this.random.Next(3);
                        this.type = "Бан";
                        switch (num)
                        {
                            case 0:
                                this.kr_name = "Злые модеры";
                                this.influence = -0.03;
                                this.Creator.Banned = true;
                                this.description = Dates.DateGenerateNumeric() + " злые модераторы забанили " + this.Creator.nickname;
                                break;
                            case 1:
                                this.kr_name = "Безумие олдфага";
                                this.influence = 0.01;
                                this.description = Dates.DateGenerateNumeric() + " олдфагис " + this.Creator.nickname + " нарывался на бан со своим безумством";
                                break;
                            default:
                                this.kr_name = "Восстания против модеров";
                                this.influence = 0.03;
                                this.description = Dates.DateGenerateNumeric() + " " + this.Creator.nickname + " активно восставал против модеров";
                                break;
                        }
                    }
                    else if (!this.Creator.Rak & this.Creator.Banned & !this.Creator.mod)
                    {
                        this.type = "Разбан";
                        switch (this.random.Next(3))
                        {
                            case 0:
                                this.kr_name = "Удачный разбан";
                                this.Creator.Banned = false;
                                this.influence = 0.01;
                                this.description = Dates.DateGenerateNumeric() + " благополучно разбанили " + this.Creator.nickname;
                                break;
                            case 1:
                                this.kr_name = "Разбан юзера с разборками";
                                this.Creator.Banned = false;
                                this.influence = 0.02;
                                this.description = Dates.DateGenerateNumeric() + " " + this.Creator.nickname + " с ором и проклятиями вытребовал разбана у админов";
                                break;
                            default:
                                this.kr_name = "Неудачный разбан юзера";
                                this.influence = -0.01;
                                this.description = Dates.DateGenerateNumeric() + " забаненый " + this.Creator.nickname + " хотел добиться разбана, но не вышло";
                                break;
                        }
                    }
                    else if (this.Creator.Rak & this.Creator.Banned & !this.Creator.mod)
                    {
                        this.type = "Разбан";
                        switch (this.random.Next(3))
                        {
                            case 0:
                                this.kr_name = "Разбан рака";
                                this.Creator.Banned = false;
                                this.influence = -0.02;
                                this.description = Dates.DateGenerateNumeric() + " к сожалению, разбанили неадеквата " + this.Creator.nickname;
                                break;
                            case 1:
                                this.kr_name = "Попытка выбраться из бани";
                                this.influence = 0.02;
                                this.description = Dates.DateGenerateNumeric() + " рак " + this.Creator.nickname + " попытался выбраться из бани, но не смог ";
                                break;
                            default:
                                this.kr_name = "Продление бана";
                                this.influence = -0.01;
                                this.description = Dates.DateGenerateNumeric() + " " + this.Creator.nickname + " вследствие своей раковитости добился продления своего бана!";
                                break;
                        }
                    }
                    else if ((this.Creator.Rak | !this.Creator.Rak) & this.Creator.mod & !this.Creator.Banned)
                    {
                        int num = this.random.Next(3);
                        this.type = "Буйство модеров";
                        switch (num)
                        {
                            case 0:
                                this.kr_name = "Бешеный модер";
                                this.influence = -0.04;
                                this.description = Dates.DateGenerateNumeric() + " модератор " + this.Creator.nickname + " бесился и грозил олдфагам их баном";
                                break;
                            case 1:
                                this.kr_name = "Ненависть к модерам";
                                this.influence = -0.03;
                                this.description = Dates.DateGenerateNumeric() + " модератор " + this.Creator.nickname + " вызывает все больше ненависти у юзеров";
                                break;
                            default:
                                this.kr_name = "Закрытие флудилок";
                                this.influence = -0.03;
                                this.description = Dates.DateGenerateNumeric() + " модератор " + this.Creator.nickname + " закрыл кучу флудилок и был весьма доволен этим";
                                break;
                        }
                    }
                    else
                    {
                        int num = this.random.Next(3);
                        this.type = "Разное";
                        switch (num)
                        {
                            case 0:
                                this.kr_name = "Хвальба мессагами";
                                this.influence = 0.01;
                                this.description = Dates.DateGenerateNumeric() + " пользователь " + this.Creator.nickname + " гордится своими сообщениями(" + (object)this.Creator.messages + " штук)";
                                break;
                            case 1:
                                this.kr_name = "Вирус Андрежа";
                                this.influence = -0.01;
                                this.description = Dates.DateGenerateNumeric() + " юзер " + this.Creator.nickname + " заразился вирусом Андрежа";
                                break;
                            default:
                                this.kr_name = "Майнкрафтмания";
                                this.influence = -0.03;
                                this.description = Dates.DateGenerateNumeric() + " пользователь " + this.Creator.nickname + " основал манию на майнкрафт";
                                break;
                        }
                    }
                }
                else
                {
                    int num1 = this.random.Next(5);
                    if (num1 == 0 | num1 == 3)
                    {
                        this.type = "Уход";
                        if (this.random.Next(this.Creator.StayPoss) == 0)
                        {
                            if (this.random.Next(2) == 0)
                            {
                                this.kr_name = "Незаметный уход";
                                this.influence = -0.03;
                                this.description = Dates.DateGenerateNumeric() + " с румайна по-тихому ушел " + this.Creator.nickname;
                                this.Creator.active = false;
                            }
                            else
                            {
                                this.kr_name = "Громкий уход";
                                this.influence = -0.05;
                                this.description = Dates.DateGenerateNumeric() + " юзер " + this.Creator.nickname + " свалил с румайна после гигантского срача";
                                this.Creator.active = false;
                            }
                        }
                        else
                        {
                            this.kr_name = "Неудавшийся уход";
                            this.influence = 0.01;
                            this.description = this.Creator.nickname + " не желает уходить с Румайна.";
                        }
                        this.date = Dates.DateGenerateNumeric();
                    }
                    else if (num1 == 1)
                    {
                        this.type = "Изменение юзера";
                        if (this.random.Next(this.Creator.ChangePoss) == 0)
                        {
                            if (!this.Creator.Rak)
                            {
                                this.kr_name = "Скатился";
                                this.influence = -0.04;
                                this.Creator.Rak = true;
                                this.description = Dates.DateGenerateNumeric() + " " + this.Creator.nickname + " стал раком ";
                            }
                            else if (!this.Creator.Rak & this.Creator.mod)
                            {
                                this.kr_name = "Скатился";
                                this.influence = -0.04;
                                this.description = Dates.DateGenerateNumeric() + " Модератор " + this.Creator.nickname + " явно начал скатываться ";
                            }
                            else if (this.Creator.Rak & this.Creator.mod)
                            {
                                this.kr_name = "Модер из пучины рака";
                                this.influence = 0.03;
                                this.Creator.Rak = false;
                                this.description = Dates.DateGenerateNumeric() + " Модератор " + this.Creator.nickname + " вылез из пучины ракования ";
                            }
                            else
                            {
                                this.kr_name = "Возврат из раков";
                                this.influence = 0.04;
                                this.Creator.Rak = false;
                                this.description = Dates.DateGenerateNumeric() + " стал адекватом " + this.Creator.nickname;
                            }
                        }
                        else
                        {
                            this.type = "Срач";
                            if (pages > 15.0)
                            {
                                this.kr_name = "Срач";
                                this.influence = 0.02;
                                this.description = Dates.DateGenerateNumeric() + " " + this.Creator.nickname + " провел хороший срач ";
                            }
                            else if (pages < 15.0)
                            {
                                this.kr_name = "Нытье";
                                this.influence = -0.02;
                                this.description = Dates.DateGenerateNumeric() + " " + this.Creator.nickname + " ноет про активность(" + (object)Math.Round(pages, 1) + " страниц в день)";
                            }
                        }
                    }
                    else if (num1 == 2 & this.random.Next(10) == 0)
                    {
                        this.type = "ДН ДД";
                        if (pages > 20.0 & this.random.Next(10) == 0)
                        {
                            if (this.random.Next(2) == 0)
                            {
                                this.kr_name = "ДД";
                                this.influence = 0.08;
                                this.description = Dates.DateGenerateNumeric() + " " + this.Creator.nickname + " провел День Добра!";
                            }
                            else
                            {
                                this.influence = 0.08;
                                this.kr_name = "ДН";
                                this.description = Dates.DateGenerateNumeric() + " " + this.Creator.nickname + " провел День Насвая!";
                            }
                        }
                        else
                        {
                            this.influence = -0.03;
                            this.description = Dates.DateGenerateNumeric() + " у пользователя " + this.Creator.nickname + " попытка основать День в Свою честь провалилась";
                        }
                    }
                    else if (num1 == 4)
                    {
                        this.type = "Получение группы";
                        if (this.random.Next(2) == 0)
                        {
                            string str = this.Creator.GroupPromotion();
                            if (str != this.Creator.group)
                            {
                                this.kr_name = "Повышение";
                                this.description = this.date + " пользователь " + this.Creator.nickname + " получил повышение с " + str + " до " + this.Creator.group + "!";
                                this.influence = 0.01;
                            }
                            else
                            {
                                this.kr_name = "Неудачное повышение";
                                this.description = this.date + " пользователь " + this.Creator.nickname + " хотел получить повышение... А не вышло!";
                                this.influence = 0.005;
                            }
                        }
                        else
                        {
                            string str = this.Creator.GroupDown();
                            if (str != this.Creator.group)
                            {
                                this.kr_name = "Понижение";
                                this.description = this.date + " пользователь " + this.Creator.nickname + " cкатился с " + str + " до " + this.Creator.group + "!";
                                this.influence = -0.005;
                            }
                            else
                            {
                                this.kr_name = "Падать некуда";
                                this.description = "Юзер " + this.Creator.nickname + " настолько убог, что ему со своей группой '" + str + "' и катиться некуда!";
                                this.influence = -0.005;
                            }
                        }
                    }
                    else
                    {
                        int num2 = this.random.Next(3);
                        this.type = "Разное";
                        switch (num2)
                        {
                            case 0:
                                this.kr_name = "Хвальба группой";
                                this.influence = 0.01;
                                this.description = Dates.DateGenerateNumeric() + " пользователь " + this.Creator.nickname + " явно хотел обратить внимание на свою группу - " + this.Creator.group;
                                break;
                            case 1:
                                this.kr_name = "В ударе";
                                this.influence = 0.03;
                                this.description = Dates.DateGenerateNumeric() + " юзер " + this.Creator.nickname + " в ударе";
                                break;
                            default:
                                this.kr_name = "Мечты о группе";
                                this.influence = 0.01;
                                this.description = Dates.DateGenerateNumeric() + " пользователь " + this.Creator.nickname + " мечтает о новой группе";
                                break;
                        }
                    }
                }
            }
            else if (!this.Creator.active)
            {
                this.type = "Неактивный юзер";
                if (this.random.Next(2) == 0)
                {
                    int num = this.random.Next(2);
                    if (this.Creator.mod & !this.Creator.Rak)
                    {
                        switch (num)
                        {
                            case 0:
                                this.kr_name = "Неактивный модер";
                                this.influence = -0.02;
                                this.description = Dates.DateGenerateNumeric() + " пользователи недобрым словом поминали неактивного модера " + this.Creator.nickname;
                                break;
                            case 1:
                                this.kr_name = "Зря ушедший модер";
                                this.influence = -0.01;
                                this.description = Dates.DateGenerateNumeric() + " юзеры с грустью вспоминают ушедшего модера " + this.Creator.nickname + "!";
                                break;
                        }
                    }
                    else if (this.Creator.mod & this.Creator.Rak)
                    {
                        switch (num)
                        {
                            case 0:
                                this.kr_name = "Воспоминания о раке-модере";
                                this.influence = -0.01;
                                this.description = Dates.DateGenerateNumeric() + " пользователи рады тому, что неадекватный модератор " + this.Creator.nickname + " больше не проявляет себя";
                                break;
                            case 1:
                                this.kr_name = "Труп не отдает модера";
                                this.influence = -0.02;
                                this.description = Dates.DateGenerateNumeric() + " юзеры с ором и восстаниями требуют снять умершего неадеквата " + this.Creator.nickname + " с модера";
                                break;
                        }
                    }
                    else
                    {
                        this.kr_name = "Радость от ухода";
                        this.influence = 0.01;
                        this.description = Dates.DateGenerateNumeric() + " в флудилке радовались уходу " + this.Creator.nickname;
                    }
                }
                else if (this.random.Next(0, 2) == 0)
                {
                    this.kr_name = "Возращение пользователя";
                    this.influence = 0.03;
                    this.Creator.active = true;
                    this.description = Dates.DateGenerateNumeric() + " пользователь " + this.Creator.nickname + " вернулся на сайт!";
                }
                else
                {
                    this.kr_name = "Воспоминания";
                    this.influence = -0.01;
                    this.description = Dates.DateGenerateNumeric() + " в флудилке вспоминали ушедшего " + this.Creator.nickname;
                }
            }
            else
            {
                this.influence = 0.0;
                this.Creator = (User)null;
                this.date = Dates.DateGenerateNumeric();
                this.description = Dates.DateGenerateNumeric() + " не произошло ничего особенного";
            }
            this.kr_name = this.kr_name + " " + (object)this.event_id;
            this.date = Dates.DateGenerateNumeric();
        }
    }
}
