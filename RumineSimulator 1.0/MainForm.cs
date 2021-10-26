using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RumineSimulator
{
    public class MainForm : Form
    {
        private DateProgress Date = new DateProgress(29, 6, 2013, 30);
        private Activity CurrentActivity = new Activity(30.0);
        private UsersStorage UsersList = new UsersStorage(50);
        private EventsList EventsList = new EventsList();
        private Random random = new Random();
        private int tech = 0;
        private bool game = false;
        private IContainer components = (IContainer)null;
        private Timer Timer;
        private Timer timerUsers;
        private BindingSource userBindingSource;
        private BindingSource bindingSourceUsers;
        private DataSet dataSet1;
        private DataTable dataTable1;
        private TabPage tabUsersOld;
        private DataGridView dataGridViewUsers;
        private DataGridViewTextBoxColumn nicknameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn registrationDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn activeDataGridViewCheckBoxColumn;
        private DataGridViewCheckBoxColumn rakDataGridViewCheckBoxColumn;
        private DataGridViewTextBoxColumn groupDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn modDataGridViewCheckBoxColumn;
        private DataGridViewTextBoxColumn messagesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn likesDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn bannedDataGridViewCheckBoxColumn;
        private DataGridViewTextBoxColumn stayPossDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn changePossDataGridViewTextBoxColumn;
        private Label label3;
        private RichTextBox text_Users;
        private TabPage tabPageUsers;
        private Button btn_UserUpdate;
        private Label userValueNick;
        private Panel panelUserInfo;
        private TabControl tabControl1;
        private TabPage tabPageUserForumInfo;
        private Label userValueLikes;
        private Label userValueAct;
        private Label userValueGroup;
        private Label userValueRak;
        private Label userValueMess;
        private Label userValueReg;
        private Label userValueMod;
        private TabPage tabPageUserOtherInfo;
        private Label userValueLeave;
        private Label userValueChange;
        private Panel panelUserEvents;
        private TabControl tabControlUserEvents;
        private TabPage tabPageUserEventsLog;
        private RichTextBox text_UserEventsLog;
        private TabPage tabPageUserEventChange;
        private ListBox listUserEvents;
        private TabPage tabPageUserEventLook;
        private Label label9;
        private Label userValueEventInfl;
        private RichTextBox text_UserEvents;
        private Label userValueEventDate;
        private ComboBox comboBoxUsers;
        private Button btn_Find;
        private TextBox text_UserFind;
        private Label userValueTotalMod;
        private Label userValueTotalRak;
        private Label userValueTotalBan;
        private Label userValueTotalUsers;
        private Label userValueTotalAct;
        private ListBox list_Users;
        private Label label8;
        private TabPage tabPageEvents;
        private ComboBox comboBoxEvents;
        private Label eventValueInfluence;
        private Label eventValueChanse;
        private RichTextBox eventValueDescr;
        private Label eventValueInit;
        private Label eventValue;
        private Label eventValueDate;
        private Label eventValueType;
        private Label label10;
        private ListBox list_Events;
        private TabPage tabMine;
        private GroupBox groupBoxMainDate;
        private TabControl tabControl2;
        private TabPage tabPage1DateGroupBox;
        private TextBox tb_PagesperMonth;
        private TextBox tb_PagesperDay;
        private Label text_MonthPages;
        private Label label7;
        private TabPage tabPage2DateGroupBox;
        private GroupBox groupBox1;
        private TabControl tabControlPage1Info;
        private TabPage tabPageModifiers;
        private Label value_downMod;
        private Label value_upMod;
        private Label value_MonthMod;
        private Label value_SeasonModif;
        private Label value_Chanse;
        private TabPage tabPageStata;
        private TextBox tb_upMode;
        private TextBox tb_Chanse;
        private TextBox tb_SeasonMod;
        private TextBox tb_MonthMod;
        private TextBox tb_DownMod;
        private TextBox textBox1;
        private Label value_AllPages;
        private Panel panel3;
        private TabControl tabMainLog;
        private TabPage tabPageMainActivity;
        private ProgressBar progressBarMonth;
        private RichTextBox text_HistoryActivity;
        private TabPage tabPageMainEvents;
        private RichTextBox text_HistoryEvents;
        private Button btn_Start;
        private TabControl tabMain;
        private Label label1;
        private NumericUpDown numericUpDown1;

        public MainForm()
        {
            InitializeComponent();
            this.comboBoxEvents.SelectedItem = (object)"Все";
            this.comboBoxUsers.SelectedItem = (object)"Все";
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            this.timerUsers.Enabled = true;
            this.Timer.Interval = Convert.ToInt32(this.numericUpDown1.Value);
            this.UsersGenerate();
            this.game = true;
            this.btn_Start.Enabled = false;
        }

        private void InitialRefresh()
        {
            if (Math.Round(this.Date.AveragePage, 1) > 4.0)
            {
                double num = this.CurrentActivity.DayChange();
                this.Date.AddPages(num);
                this.tb_PagesperDay.Text = Convert.ToString(Math.Round(num));
                this.value_AllPages.Text = "Всего страниц " + Convert.ToString(Math.Round(this.Date.AllPages));
                this.EventDayCheck();
            }
            else
            {
                this.Timer.Enabled = false;
                int num = (int)MessageBox.Show("Вы проиграли! Активности у руминя 0! Итоговая лампада - " + (object)Math.Round(this.CurrentActivity.Lampada));
            }
        }

        private void TextLogWrite(string message, RichTextBox textbox)
        {
            textbox.Select(0, 0);
            textbox.SelectedText = message + "\n";
        }

        private void TextUpdateDay() => this.groupBoxMainDate.Text = this.Date.DateGenerateText("Полная");

        private void TextUpdateMonth()
        {
            double num = this.Date.MonthCount();
            this.groupBoxMainDate.Text = this.Date.DateGenerateText("Полная");
            this.text_MonthPages.Text = "За " + this.Date.DateGenerateText("Месяц");
            this.TextLogWrite("Среднее кол-во страниц за " + this.Date.DateGenerateText("Месяц+Год") + ": " + (object)Math.Round(num, 2), this.text_HistoryActivity);
            this.tb_PagesperMonth.Text = Convert.ToString(Math.Round(num, 2));
            this.value_MonthMod.Text = "Модиф месяца: " + Convert.ToString(this.CurrentActivity.MonthMod);
            this.value_Chanse.Text = "Адекватность: " + Convert.ToString(this.CurrentActivity.Chanse);
            this.value_downMod.Text = "Старение Румине: " + Convert.ToString(this.CurrentActivity.ActDownModif);
            this.value_upMod.Text = "Активность: " + Convert.ToString(this.CurrentActivity.ActUpModif);
            this.value_SeasonModif.Text = "Модификатор сезона " + this.CurrentActivity.SeasonMod.ToString();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            string season = this.Date.DayPass(this.CurrentActivity.ActivityPages, this.UsersList);
            if (season == "Год")
            {
                this.CurrentActivity.YearChange(this.Date.year);
                this.InitialRefresh();
                this.TextUpdateMonth();
                this.TextLogWrite("Настал " + (object)this.Date.year + "!", this.text_HistoryActivity);
            }
            else if (season == "ПолГода")
            {
                this.CurrentActivity.HalfYearChange();
                this.InitialRefresh();
                this.TextUpdateMonth();
            }
            else if (season == "Весна" | season == "Лето" | season == "Осень" | season == "Зима")
            {
                this.CurrentActivity.SeasonChange(season);
                this.InitialRefresh();
                this.TextUpdateMonth();
                if (season == "Весна" | season == "Осень" | season == "Зима")
                    this.TextLogWrite("Настала " + season + "!", this.text_HistoryActivity);
                else
                    this.TextLogWrite("Настало " + season + "!", this.text_HistoryActivity);
            }
            else if (season == "Месяц")
            {
                this.CurrentActivity.MonthChange();
                this.InitialRefresh();
                this.TextUpdateMonth();
            }
            else if (season == "ПолМесяца")
            {
                this.CurrentActivity.HalfMonthChange();
                this.InitialRefresh();
                this.TextUpdateDay();
            }
            else
            {
                this.TextUpdateDay();
                this.InitialRefresh();
            }
        }

        private void EventDayCheck()
        {
            int maxValue = 8 - Convert.ToInt32(this.CurrentActivity.ActivityPages / 10.0);
            if (maxValue < 1)
                maxValue = 1;
            this.eventValueChanse.Text = "Шанс события: " + (1.0 / Convert.ToDouble(maxValue) * 100.0).ToString();
            if (this.random.Next(maxValue) != 0)
                return;
            Events events = this.EventsList.EventGenerate("День", this.UsersList, this.Date, this.CurrentActivity.ActivityPages);
            if (this.comboBoxEvents.SelectedItem.ToString() == "Все" | this.comboBoxEvents.SelectedItem.ToString() == events.type)
                this.list_Events.Items.Add((object)events.kr_name);
            this.TextLogWrite(events.description, this.text_HistoryEvents);
            this.CurrentActivity.MonthMod += events.influence;
            this.value_MonthMod.Text = "Модиф месяца: " + (object)Math.Round(this.CurrentActivity.MonthMod, 3);
        }

        private void UsersGenerate()
        {
            if (this.tech <= this.UsersList.amount)
                return;
            this.timerUsers.Enabled = false;
            for (int index = 0; index < this.UsersList.amount; ++index)
                this.list_Users.Items.Add((object)this.UsersList.users[index].nickname);
            this.Timer.Enabled = true;
        }

        private void timerUsers_Tick(object sender, EventArgs e)
        {
            this.UsersList.UsersGenerate();
            ++this.tech;
            this.UsersGenerate();
        }

        private void UserUpdate(User Selected_User)
        {
            this.userValueNick.Text = Selected_User.nickname;
            this.userValueReg.Text = "Регистрация: " + Selected_User.registration.ToString();
            if (Selected_User.active)
                this.userValueAct.Text = "Активен";
            else
                this.userValueAct.Text = "Нективен";
            this.userValueGroup.Text = "Группа: " + Selected_User.group.ToString();
            Label userValueLikes = this.userValueLikes;
            int num1 = Selected_User.likes;
            string str1 = "Симпатий: " + num1.ToString();
            userValueLikes.Text = str1;
            Label userValueMess = this.userValueMess;
            num1 = Selected_User.messages;
            string str2 = "Сообщений: " + num1.ToString();
            userValueMess.Text = str2;
            if (Selected_User.mod)
                this.userValueMod.Text = "Модератор";
            else
                this.userValueMod.Text = "Обычный юзер";
            if (Selected_User.Rak)
                this.userValueRak.Text = "Рак";
            else
                this.userValueRak.Text = "Адекват";
            if (Selected_User.Banned)
                this.userValueGroup.Text = "Забанен (" + Selected_User.group + ")";
            this.userValueLeave.Text = "Шанс ухода: " + Math.Round(1.0 / Convert.ToDouble(Selected_User.StayPoss) * 100.0, 1).ToString() + "%";
            double num2 = 1.0 / Convert.ToDouble(Selected_User.ChangePoss) * 100.0;
            Label userValueChange = this.userValueChange;
            num1 = Selected_User.ChangePoss;
            string str3 = "Вероятность изменения: " + num1.ToString() + "%";
            userValueChange.Text = str3;
            this.listUserEvents.Items.Clear();
            this.text_UserEventsLog.Clear();
            for (int index = 0; index < Selected_User.UsersEvents.Count; ++index)
            {
                this.listUserEvents.Items.Add((object)Selected_User.UsersEvents[index].kr_name);
                this.TextLogWrite(Selected_User.UsersEvents[index].description, this.text_UserEventsLog);
            }
            this.userValueEventDate.Text = "Дата: ?";
            this.userValueEventInfl.Text = "Влияние: ?";
            this.text_UserEvents.Text = "";
            this.UsersList.StatCount();
            Label userValueTotalUsers = this.userValueTotalUsers;
            num1 = this.UsersList.amount;
            string str4 = "Всего юзеров: " + num1.ToString();
            userValueTotalUsers.Text = str4;
            Label userValueTotalBan = this.userValueTotalBan;
            num1 = this.UsersList.banAmount;
            string str5 = "Забанено: " + num1.ToString();
            userValueTotalBan.Text = str5;
            Label userValueTotalRak = this.userValueTotalRak;
            num1 = this.UsersList.rakAmount;
            string str6 = "Раков: " + num1.ToString();
            userValueTotalRak.Text = str6;
            Label userValueTotalMod = this.userValueTotalMod;
            num1 = this.UsersList.modAmount;
            string str7 = "Модераторов: " + num1.ToString();
            userValueTotalMod.Text = str7;
            Label userValueTotalAct = this.userValueTotalAct;
            num1 = this.UsersList.activeAmount;
            string str8 = "Активных: " + num1.ToString();
            userValueTotalAct.Text = str8;
        }

        private void btn_UserUpdate_Click(object sender, EventArgs e)
        {
            User Selected_User = (User)null;
            if (this.list_Users.SelectedItem == null)
                return;
            for (int index = 0; index < this.UsersList.amount; ++index)
            {
                if (this.list_Users.SelectedItem.ToString() == this.UsersList.users[index].nickname)
                    Selected_User = this.UsersList.users[index];
            }
            this.UserUpdate(Selected_User);
        }

        private void list_Users_SelectedIndexChanged(object sender, EventArgs e)
        {
            User Selected_User = (User)null;
            for (int index = 0; index < this.UsersList.amount; ++index)
            {
                if (this.list_Users.SelectedItem.ToString() == this.UsersList.users[index].nickname)
                    Selected_User = this.UsersList.users[index];
            }
            this.UserUpdate(Selected_User);
        }

        private void list_Events_SelectedIndexChanged(object sender, EventArgs e)
        {
            Events events = (Events)null;
            if (this.list_Events.SelectedItem == null)
                return;
            for (int index = 0; index < this.EventsList.ev_amount; ++index)
            {
                if (this.list_Events.SelectedItem.ToString() == this.EventsList.EventsListArr[index].kr_name)
                    events = this.EventsList.EventsListArr[index];
            }
            this.eventValueType.Text = "Тип События: " + events.type;
            this.eventValueDate.Text = "Дата: " + events.date;
            this.eventValueDescr.Text = events.description;
            this.eventValueInit.Text = "Инициатор: " + events.Creator.nickname;
            this.eventValueInfluence.Text = "Влияние: " + (object)events.influence;
        }

        private void listUserEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            Events events = (Events)null;
            User user = (User)null;
            for (int index = 0; index < this.UsersList.amount; ++index)
            {
                if (this.list_Users.SelectedItem.ToString() == this.UsersList.users[index].nickname)
                    user = this.UsersList.users[index];
            }
            if (this.listUserEvents.SelectedItem == null)
                return;
            for (int index = 0; index < user.UsersEvents.Count; ++index)
            {
                if (this.listUserEvents.SelectedItem.ToString() == user.UsersEvents[index].kr_name)
                    events = user.UsersEvents[index];
            }
            this.userValueEventDate.Text = "Дата: " + events.date;
            this.userValueEventInfl.Text = "Влияние: " + (object)events.influence;
            this.text_UserEvents.Text = events.description;
        }

        private void btn_Find_Click_1(object sender, EventArgs e)
        {
            string text = this.text_UserFind.Text;
            for (int index = 0; index < this.UsersList.users.Count<User>(); ++index)
            {
                if (this.list_Users.Items.Contains((object)text))
                    this.list_Users.SelectedItem = (object)text;
            }
        }

        private void text_UserFind_Click(object sender, EventArgs e) => this.text_UserFind.Clear();

        private void comboBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.game)
                return;
            if (this.comboBoxUsers.SelectedItem.ToString() == "Все")
            {
                this.list_Users.Items.Clear();
                for (int index = 0; index < this.UsersList.amount; ++index)
                    this.list_Users.Items.Add((object)this.UsersList.users[index].nickname);
            }
            else if (this.comboBoxUsers.SelectedItem.ToString() == "Обычные пользователи")
            {
                this.list_Users.Items.Clear();
                for (int index = 0; index < this.UsersList.amount; ++index)
                {
                    if (!this.UsersList.users[index].mod)
                        this.list_Users.Items.Add((object)this.UsersList.users[index].nickname);
                }
            }
            else if (this.comboBoxUsers.SelectedItem.ToString() == "Модераторы")
            {
                this.list_Users.Items.Clear();
                for (int index = 0; index < this.UsersList.amount; ++index)
                {
                    if (this.UsersList.users[index].mod)
                        this.list_Users.Items.Add((object)this.UsersList.users[index].nickname);
                }
            }
            else if (this.comboBoxUsers.SelectedItem.ToString() == "Раки")
            {
                this.list_Users.Items.Clear();
                for (int index = 0; index < this.UsersList.amount; ++index)
                {
                    if (this.UsersList.users[index].Rak)
                        this.list_Users.Items.Add((object)this.UsersList.users[index].nickname);
                }
            }
            else if (this.comboBoxUsers.SelectedItem.ToString() == "Адекваты")
            {
                this.list_Users.Items.Clear();
                for (int index = 0; index < this.UsersList.amount; ++index)
                {
                    if (!this.UsersList.users[index].Rak)
                        this.list_Users.Items.Add((object)this.UsersList.users[index].nickname);
                }
            }
            else if (this.comboBoxUsers.SelectedItem.ToString() == "Активные")
            {
                this.list_Users.Items.Clear();
                for (int index = 0; index < this.UsersList.amount; ++index)
                {
                    if (this.UsersList.users[index].active)
                        this.list_Users.Items.Add((object)this.UsersList.users[index].nickname);
                }
            }
            else if (this.comboBoxUsers.SelectedItem.ToString() == "Ушедшие")
            {
                this.list_Users.Items.Clear();
                for (int index = 0; index < this.UsersList.amount; ++index)
                {
                    if (!this.UsersList.users[index].active)
                        this.list_Users.Items.Add((object)this.UsersList.users[index].nickname);
                }
            }
            else if (this.comboBoxUsers.SelectedItem.ToString() == "Забаненые")
            {
                this.list_Users.Items.Clear();
                for (int index = 0; index < this.UsersList.amount; ++index)
                {
                    if (this.UsersList.users[index].Banned)
                        this.list_Users.Items.Add((object)this.UsersList.users[index].nickname);
                }
            }
            else if (this.comboBoxUsers.SelectedItem.ToString() == "Здоровые")
            {
                this.list_Users.Items.Clear();
                for (int index = 0; index < this.UsersList.amount; ++index)
                {
                    if (!this.UsersList.users[index].Banned)
                        this.list_Users.Items.Add((object)this.UsersList.users[index].nickname);
                }
            }
        }

        private void comboBoxEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxEvents.SelectedItem == null)
                return;
            if (this.comboBoxEvents.SelectedItem.ToString() == "Все")
            {
                this.list_Events.Items.Clear();
                for (int index = 0; index < this.EventsList.EventsListArr.Count; ++index)
                    this.list_Events.Items.Add((object)this.EventsList.EventsListArr[index].kr_name);
            }
            else if (this.comboBoxEvents.SelectedItem.ToString() == "Бан")
            {
                this.list_Events.Items.Clear();
                for (int index = 0; index < this.EventsList.EventsListArr.Count; ++index)
                {
                    if (this.EventsList.EventsListArr[index].type == "Бан")
                        this.list_Events.Items.Add((object)this.EventsList.EventsListArr[index].kr_name);
                }
            }
            else if (this.comboBoxEvents.SelectedItem.ToString() == "Разбан")
            {
                this.list_Events.Items.Clear();
                for (int index = 0; index < this.EventsList.EventsListArr.Count; ++index)
                {
                    if (this.EventsList.EventsListArr[index].type == "Разбан")
                        this.list_Events.Items.Add((object)this.EventsList.EventsListArr[index].kr_name);
                }
            }
            else if (this.comboBoxEvents.SelectedItem.ToString() == "Буйство модеров")
            {
                this.list_Events.Items.Clear();
                for (int index = 0; index < this.EventsList.EventsListArr.Count; ++index)
                {
                    if (this.EventsList.EventsListArr[index].type == "Буйство модеров")
                        this.list_Events.Items.Add((object)this.EventsList.EventsListArr[index].kr_name);
                }
            }
            else if (this.comboBoxEvents.SelectedItem.ToString() == "Уход")
            {
                this.list_Events.Items.Clear();
                for (int index = 0; index < this.EventsList.EventsListArr.Count; ++index)
                {
                    if (this.EventsList.EventsListArr[index].type == "Уход")
                        this.list_Events.Items.Add((object)this.EventsList.EventsListArr[index].kr_name);
                }
            }
            else if (this.comboBoxEvents.SelectedItem.ToString() == "Разное")
            {
                this.list_Events.Items.Clear();
                for (int index = 0; index < this.EventsList.EventsListArr.Count; ++index)
                {
                    if (this.EventsList.EventsListArr[index].type == "Разное")
                        this.list_Events.Items.Add((object)this.EventsList.EventsListArr[index].kr_name);
                }
            }
            else if (this.comboBoxEvents.SelectedItem.ToString() == "Изменение юзера")
            {
                this.list_Events.Items.Clear();
                for (int index = 0; index < this.EventsList.EventsListArr.Count; ++index)
                {
                    if (this.EventsList.EventsListArr[index].type == "Изменение юзера")
                        this.list_Events.Items.Add((object)this.EventsList.EventsListArr[index].kr_name);
                }
            }
            else if (this.comboBoxEvents.SelectedItem.ToString() == "ДН ДД")
            {
                this.list_Events.Items.Clear();
                for (int index = 0; index < this.EventsList.EventsListArr.Count; ++index)
                {
                    if (this.EventsList.EventsListArr[index].type == "ДН ДД")
                        this.list_Events.Items.Add((object)this.EventsList.EventsListArr[index].kr_name);
                }
            }
            else if (this.comboBoxEvents.SelectedItem.ToString() == "Получение группы")
            {
                this.list_Events.Items.Clear();
                for (int index = 0; index < this.EventsList.EventsListArr.Count; ++index)
                {
                    if (this.EventsList.EventsListArr[index].type == "Получение группы")
                        this.list_Events.Items.Add((object)this.EventsList.EventsListArr[index].kr_name);
                }
            }
            else if (this.comboBoxEvents.SelectedItem.ToString() == "Неактивный юзер")
            {
                this.list_Events.Items.Clear();
                for (int index = 0; index < this.EventsList.EventsListArr.Count; ++index)
                {
                    if (this.EventsList.EventsListArr[index].type == "Неактивный юзер")
                        this.list_Events.Items.Add((object)this.EventsList.EventsListArr[index].kr_name);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = (IContainer)new Container();
            this.Timer = new Timer(this.components);
            this.timerUsers = new Timer(this.components);
            this.bindingSourceUsers = new BindingSource(this.components);
            this.dataSet1 = new DataSet();
            this.dataTable1 = new DataTable();
            this.tabUsersOld = new TabPage();
            this.text_Users = new RichTextBox();
            this.label3 = new Label();
            this.dataGridViewUsers = new DataGridView();
            this.tabPageUsers = new TabPage();
            this.label8 = new Label();
            this.list_Users = new ListBox();
            this.userValueTotalAct = new Label();
            this.userValueTotalUsers = new Label();
            this.userValueTotalBan = new Label();
            this.userValueTotalRak = new Label();
            this.userValueTotalMod = new Label();
            this.text_UserFind = new TextBox();
            this.btn_Find = new Button();
            this.comboBoxUsers = new ComboBox();
            this.panelUserEvents = new Panel();
            this.tabControlUserEvents = new TabControl();
            this.tabPageUserEventLook = new TabPage();
            this.userValueEventDate = new Label();
            this.text_UserEvents = new RichTextBox();
            this.userValueEventInfl = new Label();
            this.label9 = new Label();
            this.tabPageUserEventChange = new TabPage();
            this.listUserEvents = new ListBox();
            this.tabPageUserEventsLog = new TabPage();
            this.text_UserEventsLog = new RichTextBox();
            this.panelUserInfo = new Panel();
            this.tabControl1 = new TabControl();
            this.tabPageUserOtherInfo = new TabPage();
            this.userValueChange = new Label();
            this.userValueLeave = new Label();
            this.tabPageUserForumInfo = new TabPage();
            this.userValueMod = new Label();
            this.userValueReg = new Label();
            this.userValueMess = new Label();
            this.userValueRak = new Label();
            this.userValueGroup = new Label();
            this.userValueAct = new Label();
            this.userValueLikes = new Label();
            this.userValueNick = new Label();
            this.btn_UserUpdate = new Button();
            this.tabPageEvents = new TabPage();
            this.list_Events = new ListBox();
            this.label10 = new Label();
            this.eventValueType = new Label();
            this.eventValueDate = new Label();
            this.eventValue = new Label();
            this.eventValueInit = new Label();
            this.eventValueDescr = new RichTextBox();
            this.eventValueChanse = new Label();
            this.eventValueInfluence = new Label();
            this.comboBoxEvents = new ComboBox();
            this.tabMine = new TabPage();
            this.btn_Start = new Button();
            this.panel3 = new Panel();
            this.tabMainLog = new TabControl();
            this.tabPageMainEvents = new TabPage();
            this.text_HistoryEvents = new RichTextBox();
            this.tabPageMainActivity = new TabPage();
            this.text_HistoryActivity = new RichTextBox();
            this.progressBarMonth = new ProgressBar();
            this.groupBox1 = new GroupBox();
            this.tabControlPage1Info = new TabControl();
            this.tabPageStata = new TabPage();
            this.value_AllPages = new Label();
            this.textBox1 = new TextBox();
            this.tabPageModifiers = new TabPage();
            this.value_Chanse = new Label();
            this.value_SeasonModif = new Label();
            this.value_MonthMod = new Label();
            this.value_upMod = new Label();
            this.value_downMod = new Label();
            this.tb_DownMod = new TextBox();
            this.tb_MonthMod = new TextBox();
            this.tb_SeasonMod = new TextBox();
            this.tb_Chanse = new TextBox();
            this.tb_upMode = new TextBox();
            this.groupBoxMainDate = new GroupBox();
            this.tabControl2 = new TabControl();
            this.tabPage2DateGroupBox = new TabPage();
            this.tabPage1DateGroupBox = new TabPage();
            this.label7 = new Label();
            this.text_MonthPages = new Label();
            this.tb_PagesperDay = new TextBox();
            this.tb_PagesperMonth = new TextBox();
            this.tabMain = new TabControl();
            this.nicknameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.registrationDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.activeDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            this.rakDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            this.groupDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.modDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            this.messagesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.likesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.bannedDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            this.stayPossDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.changePossDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.userBindingSource = new BindingSource(this.components);
            this.numericUpDown1 = new NumericUpDown();
            this.label1 = new Label();
            ((ISupportInitialize)this.bindingSourceUsers).BeginInit();
            this.dataSet1.BeginInit();
            this.dataTable1.BeginInit();
            this.tabUsersOld.SuspendLayout();
            ((ISupportInitialize)this.dataGridViewUsers).BeginInit();
            this.tabPageUsers.SuspendLayout();
            this.panelUserEvents.SuspendLayout();
            this.tabControlUserEvents.SuspendLayout();
            this.tabPageUserEventLook.SuspendLayout();
            this.tabPageUserEventChange.SuspendLayout();
            this.tabPageUserEventsLog.SuspendLayout();
            this.panelUserInfo.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageUserOtherInfo.SuspendLayout();
            this.tabPageUserForumInfo.SuspendLayout();
            this.tabPageEvents.SuspendLayout();
            this.tabMine.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabMainLog.SuspendLayout();
            this.tabPageMainEvents.SuspendLayout();
            this.tabPageMainActivity.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControlPage1Info.SuspendLayout();
            this.tabPageStata.SuspendLayout();
            this.tabPageModifiers.SuspendLayout();
            this.groupBoxMainDate.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1DateGroupBox.SuspendLayout();
            this.tabMain.SuspendLayout();
            ((ISupportInitialize)this.userBindingSource).BeginInit();
            this.numericUpDown1.BeginInit();
            this.SuspendLayout();
            this.Timer.Interval = 500;
            this.Timer.Tick += new EventHandler(this.Timer_Tick);
            this.timerUsers.Interval = 1;
            this.timerUsers.Tick += new EventHandler(this.timerUsers_Tick);
            this.bindingSourceUsers.DataSource = (object)this.userBindingSource;
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Tables.AddRange(new DataTable[1]
            {
        this.dataTable1
            });
            this.dataTable1.TableName = "Table1";
            this.tabUsersOld.Controls.Add((Control)this.dataGridViewUsers);
            this.tabUsersOld.Controls.Add((Control)this.label3);
            this.tabUsersOld.Controls.Add((Control)this.text_Users);
            this.tabUsersOld.Location = new Point(4, 27);
            this.tabUsersOld.Name = "tabUsersOld";
            this.tabUsersOld.Padding = new Padding(3);
            this.tabUsersOld.Size = new Size(676, 430);
            this.tabUsersOld.TabIndex = 1;
            this.tabUsersOld.Text = "Старый список(не работает)";
            this.tabUsersOld.UseVisualStyleBackColor = true;
            this.text_Users.Dock = DockStyle.Bottom;
            this.text_Users.Location = new Point(3, 362);
            this.text_Users.Name = "text_Users";
            this.text_Users.ReadOnly = true;
            this.text_Users.Size = new Size(670, 65);
            this.text_Users.TabIndex = 0;
            this.text_Users.Text = "    Ник             Активность      Посты    Симпатии     Уход    Рак ";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte)204);
            this.label3.Location = new Point(6, 5);
            this.label3.Name = "label3";
            this.label3.Size = new Size(205, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Список пользователей";
            this.dataGridViewUsers.AutoGenerateColumns = false;
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUsers.Columns.AddRange((DataGridViewColumn)this.nicknameDataGridViewTextBoxColumn, (DataGridViewColumn)this.registrationDataGridViewTextBoxColumn, (DataGridViewColumn)this.activeDataGridViewCheckBoxColumn, (DataGridViewColumn)this.rakDataGridViewCheckBoxColumn, (DataGridViewColumn)this.groupDataGridViewTextBoxColumn, (DataGridViewColumn)this.modDataGridViewCheckBoxColumn, (DataGridViewColumn)this.messagesDataGridViewTextBoxColumn, (DataGridViewColumn)this.likesDataGridViewTextBoxColumn, (DataGridViewColumn)this.bannedDataGridViewCheckBoxColumn, (DataGridViewColumn)this.stayPossDataGridViewTextBoxColumn, (DataGridViewColumn)this.changePossDataGridViewTextBoxColumn);
            this.dataGridViewUsers.DataSource = (object)this.bindingSourceUsers;
            this.dataGridViewUsers.Location = new Point(10, 28);
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.Size = new Size(658, 328);
            this.dataGridViewUsers.TabIndex = 2;
            this.tabPageUsers.Controls.Add((Control)this.btn_UserUpdate);
            this.tabPageUsers.Controls.Add((Control)this.userValueNick);
            this.tabPageUsers.Controls.Add((Control)this.panelUserInfo);
            this.tabPageUsers.Controls.Add((Control)this.panelUserEvents);
            this.tabPageUsers.Controls.Add((Control)this.comboBoxUsers);
            this.tabPageUsers.Controls.Add((Control)this.btn_Find);
            this.tabPageUsers.Controls.Add((Control)this.text_UserFind);
            this.tabPageUsers.Controls.Add((Control)this.userValueTotalMod);
            this.tabPageUsers.Controls.Add((Control)this.userValueTotalRak);
            this.tabPageUsers.Controls.Add((Control)this.userValueTotalBan);
            this.tabPageUsers.Controls.Add((Control)this.userValueTotalUsers);
            this.tabPageUsers.Controls.Add((Control)this.userValueTotalAct);
            this.tabPageUsers.Controls.Add((Control)this.list_Users);
            this.tabPageUsers.Controls.Add((Control)this.label8);
            this.tabPageUsers.Location = new Point(4, 27);
            this.tabPageUsers.Name = "tabPageUsers";
            this.tabPageUsers.Padding = new Padding(3);
            this.tabPageUsers.Size = new Size(676, 430);
            this.tabPageUsers.TabIndex = 3;
            this.tabPageUsers.Text = "Просмотр пользователей";
            this.tabPageUsers.UseVisualStyleBackColor = true;
            this.label8.AutoSize = true;
            this.label8.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.label8.Location = new Point(6, 3);
            this.label8.Name = "label8";
            this.label8.Size = new Size(160, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "Список пользователей";
            this.list_Users.FormattingEnabled = true;
            this.list_Users.ItemHeight = 18;
            this.list_Users.Location = new Point(12, 101);
            this.list_Users.Name = "list_Users";
            this.list_Users.Size = new Size(139, 310);
            this.list_Users.TabIndex = 1;
            this.list_Users.SelectedIndexChanged += new EventHandler(this.list_Users_SelectedIndexChanged);
            this.userValueTotalAct.AutoSize = true;
            this.userValueTotalAct.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.userValueTotalAct.Location = new Point(11, 62);
            this.userValueTotalAct.Name = "userValueTotalAct";
            this.userValueTotalAct.Size = new Size(63, 15);
            this.userValueTotalAct.TabIndex = 14;
            this.userValueTotalAct.Text = "Активных";
            this.userValueTotalUsers.AutoSize = true;
            this.userValueTotalUsers.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.userValueTotalUsers.Location = new Point(11, 45);
            this.userValueTotalUsers.Name = "userValueTotalUsers";
            this.userValueTotalUsers.Size = new Size(86, 15);
            this.userValueTotalUsers.TabIndex = 15;
            this.userValueTotalUsers.Text = "Всего юзеров";
            this.userValueTotalBan.AutoSize = true;
            this.userValueTotalBan.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.userValueTotalBan.Location = new Point(11, 80);
            this.userValueTotalBan.Name = "userValueTotalBan";
            this.userValueTotalBan.Size = new Size(72, 15);
            this.userValueTotalBan.TabIndex = 16;
            this.userValueTotalBan.Text = "Забаненых";
            this.userValueTotalRak.AutoSize = true;
            this.userValueTotalRak.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.userValueTotalRak.Location = new Point(124, 45);
            this.userValueTotalRak.Name = "userValueTotalRak";
            this.userValueTotalRak.Size = new Size(42, 15);
            this.userValueTotalRak.TabIndex = 17;
            this.userValueTotalRak.Text = "Раков";
            this.userValueTotalMod.AutoSize = true;
            this.userValueTotalMod.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.userValueTotalMod.Location = new Point(124, 62);
            this.userValueTotalMod.Name = "userValueTotalMod";
            this.userValueTotalMod.Size = new Size(88, 15);
            this.userValueTotalMod.TabIndex = 18;
            this.userValueTotalMod.Text = "Модераторов";
            this.text_UserFind.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.text_UserFind.Location = new Point(157, 21);
            this.text_UserFind.Name = "text_UserFind";
            this.text_UserFind.Size = new Size(88, 20);
            this.text_UserFind.TabIndex = 19;
            this.text_UserFind.Text = "Найти...";
            this.text_UserFind.Click += new EventHandler(this.text_UserFind_Click);
            this.btn_Find.FlatStyle = FlatStyle.Flat;
            this.btn_Find.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.btn_Find.Location = new Point(251, 19);
            this.btn_Find.Name = "btn_Find";
            this.btn_Find.Size = new Size(22, 23);
            this.btn_Find.TabIndex = 20;
            this.btn_Find.Text = ">";
            this.btn_Find.UseVisualStyleBackColor = true;
            this.btn_Find.Click += new EventHandler(this.btn_Find_Click_1);
            this.comboBoxUsers.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.comboBoxUsers.FormattingEnabled = true;
            this.comboBoxUsers.Items.AddRange(new object[9]
            {
        (object) "Все",
        (object) "Обычные пользователи",
        (object) "Модераторы",
        (object) "Раки",
        (object) "Адекваты",
        (object) "Активные",
        (object) "Ушедшие",
        (object) "Забаненые",
        (object) "Здоровые"
            });
            this.comboBoxUsers.Location = new Point(12, 19);
            this.comboBoxUsers.Name = "comboBoxUsers";
            this.comboBoxUsers.Size = new Size(139, 23);
            this.comboBoxUsers.TabIndex = 22;
            this.comboBoxUsers.SelectedIndexChanged += new EventHandler(this.comboBoxUsers_SelectedIndexChanged);
            this.panelUserEvents.Controls.Add((Control)this.tabControlUserEvents);
            this.panelUserEvents.Location = new Point(398, 3);
            this.panelUserEvents.Name = "panelUserEvents";
            this.panelUserEvents.Size = new Size(270, 419);
            this.panelUserEvents.TabIndex = 23;
            this.tabControlUserEvents.Controls.Add((Control)this.tabPageUserEventsLog);
            this.tabControlUserEvents.Controls.Add((Control)this.tabPageUserEventChange);
            this.tabControlUserEvents.Controls.Add((Control)this.tabPageUserEventLook);
            this.tabControlUserEvents.Dock = DockStyle.Fill;
            this.tabControlUserEvents.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.tabControlUserEvents.Location = new Point(0, 0);
            this.tabControlUserEvents.Name = "tabControlUserEvents";
            this.tabControlUserEvents.SelectedIndex = 0;
            this.tabControlUserEvents.Size = new Size(270, 419);
            this.tabControlUserEvents.SizeMode = TabSizeMode.FillToRight;
            this.tabControlUserEvents.TabIndex = 11;
            this.tabPageUserEventLook.Controls.Add((Control)this.label9);
            this.tabPageUserEventLook.Controls.Add((Control)this.userValueEventInfl);
            this.tabPageUserEventLook.Controls.Add((Control)this.text_UserEvents);
            this.tabPageUserEventLook.Controls.Add((Control)this.userValueEventDate);
            this.tabPageUserEventLook.Location = new Point(4, 25);
            this.tabPageUserEventLook.Name = "tabPageUserEventLook";
            this.tabPageUserEventLook.Padding = new Padding(3);
            this.tabPageUserEventLook.Size = new Size(262, 390);
            this.tabPageUserEventLook.TabIndex = 1;
            this.tabPageUserEventLook.Text = "Описание";
            this.tabPageUserEventLook.UseVisualStyleBackColor = true;
            this.userValueEventDate.AutoSize = true;
            this.userValueEventDate.Location = new Point(6, 17);
            this.userValueEventDate.Name = "userValueEventDate";
            this.userValueEventDate.Size = new Size(40, 16);
            this.userValueEventDate.TabIndex = 2;
            this.userValueEventDate.Text = "Дата";
            this.text_UserEvents.Location = new Point(3, 137);
            this.text_UserEvents.Name = "text_UserEvents";
            this.text_UserEvents.ReadOnly = true;
            this.text_UserEvents.Size = new Size(242, 114);
            this.text_UserEvents.TabIndex = 10;
            this.text_UserEvents.Text = "";
            this.userValueEventInfl.AutoSize = true;
            this.userValueEventInfl.Location = new Point(6, 33);
            this.userValueEventInfl.Name = "userValueEventInfl";
            this.userValueEventInfl.Size = new Size(64, 16);
            this.userValueEventInfl.TabIndex = 3;
            this.userValueEventInfl.Text = "Влияние";
            this.label9.AutoSize = true;
            this.label9.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Bold, GraphicsUnit.Point, (byte)204);
            this.label9.Location = new Point(6, 116);
            this.label9.Name = "label9";
            this.label9.Size = new Size(84, 18);
            this.label9.TabIndex = 11;
            this.label9.Text = "Описание";
            this.tabPageUserEventChange.Controls.Add((Control)this.listUserEvents);
            this.tabPageUserEventChange.Location = new Point(4, 25);
            this.tabPageUserEventChange.Name = "tabPageUserEventChange";
            this.tabPageUserEventChange.Padding = new Padding(3);
            this.tabPageUserEventChange.Size = new Size(262, 390);
            this.tabPageUserEventChange.TabIndex = 0;
            this.tabPageUserEventChange.Text = "Событие";
            this.tabPageUserEventChange.UseVisualStyleBackColor = true;
            this.listUserEvents.FormattingEnabled = true;
            this.listUserEvents.ItemHeight = 16;
            this.listUserEvents.Location = new Point(11, 6);
            this.listUserEvents.Name = "listUserEvents";
            this.listUserEvents.Size = new Size(237, 324);
            this.listUserEvents.TabIndex = 0;
            this.listUserEvents.SelectedIndexChanged += new EventHandler(this.listUserEvents_SelectedIndexChanged);
            this.tabPageUserEventsLog.Controls.Add((Control)this.text_UserEventsLog);
            this.tabPageUserEventsLog.Location = new Point(4, 25);
            this.tabPageUserEventsLog.Name = "tabPageUserEventsLog";
            this.tabPageUserEventsLog.Padding = new Padding(3);
            this.tabPageUserEventsLog.Size = new Size(262, 390);
            this.tabPageUserEventsLog.TabIndex = 2;
            this.tabPageUserEventsLog.Text = "Лог";
            this.tabPageUserEventsLog.UseVisualStyleBackColor = true;
            this.text_UserEventsLog.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.text_UserEventsLog.Location = new Point(7, 7);
            this.text_UserEventsLog.Name = "text_UserEventsLog";
            this.text_UserEventsLog.ReadOnly = true;
            this.text_UserEventsLog.Size = new Size(249, 376);
            this.text_UserEventsLog.TabIndex = 0;
            this.text_UserEventsLog.Text = "";
            this.panelUserInfo.Controls.Add((Control)this.tabControl1);
            this.panelUserInfo.Location = new Point(154, 119);
            this.panelUserInfo.Name = "panelUserInfo";
            this.panelUserInfo.Size = new Size(238, 292);
            this.panelUserInfo.TabIndex = 24;
            this.tabControl1.Controls.Add((Control)this.tabPageUserForumInfo);
            this.tabControl1.Controls.Add((Control)this.tabPageUserOtherInfo);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(238, 292);
            this.tabControl1.TabIndex = 0;
            this.tabPageUserOtherInfo.Controls.Add((Control)this.userValueLeave);
            this.tabPageUserOtherInfo.Controls.Add((Control)this.userValueChange);
            this.tabPageUserOtherInfo.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.tabPageUserOtherInfo.Location = new Point(4, 27);
            this.tabPageUserOtherInfo.Name = "tabPageUserOtherInfo";
            this.tabPageUserOtherInfo.Padding = new Padding(3);
            this.tabPageUserOtherInfo.Size = new Size(230, 261);
            this.tabPageUserOtherInfo.TabIndex = 1;
            this.tabPageUserOtherInfo.Text = "Прочее";
            this.tabPageUserOtherInfo.UseVisualStyleBackColor = true;
            this.userValueChange.AutoSize = true;
            this.userValueChange.Location = new Point(6, 45);
            this.userValueChange.Name = "userValueChange";
            this.userValueChange.Size = new Size(167, 16);
            this.userValueChange.TabIndex = 21;
            this.userValueChange.Text = "Вероятность изменения";
            this.userValueLeave.AutoSize = true;
            this.userValueLeave.Location = new Point(6, 29);
            this.userValueLeave.Name = "userValueLeave";
            this.userValueLeave.Size = new Size(83, 16);
            this.userValueLeave.TabIndex = 12;
            this.userValueLeave.Text = "Шанс ухода";
            this.tabPageUserForumInfo.Controls.Add((Control)this.userValueLikes);
            this.tabPageUserForumInfo.Controls.Add((Control)this.userValueAct);
            this.tabPageUserForumInfo.Controls.Add((Control)this.userValueGroup);
            this.tabPageUserForumInfo.Controls.Add((Control)this.userValueRak);
            this.tabPageUserForumInfo.Controls.Add((Control)this.userValueMess);
            this.tabPageUserForumInfo.Controls.Add((Control)this.userValueReg);
            this.tabPageUserForumInfo.Controls.Add((Control)this.userValueMod);
            this.tabPageUserForumInfo.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.tabPageUserForumInfo.Location = new Point(4, 27);
            this.tabPageUserForumInfo.Name = "tabPageUserForumInfo";
            this.tabPageUserForumInfo.Padding = new Padding(3);
            this.tabPageUserForumInfo.Size = new Size(230, 261);
            this.tabPageUserForumInfo.TabIndex = 0;
            this.tabPageUserForumInfo.Text = "Основное";
            this.tabPageUserForumInfo.UseVisualStyleBackColor = true;
            this.userValueMod.AutoSize = true;
            this.userValueMod.Location = new Point(6, 67);
            this.userValueMod.Name = "userValueMod";
            this.userValueMod.Size = new Size(82, 16);
            this.userValueMod.TabIndex = 8;
            this.userValueMod.Text = "Модератор";
            this.userValueReg.AutoSize = true;
            this.userValueReg.Location = new Point(6, 3);
            this.userValueReg.Name = "userValueReg";
            this.userValueReg.Size = new Size(92, 16);
            this.userValueReg.TabIndex = 3;
            this.userValueReg.Text = "Регистрация";
            this.userValueMess.AutoSize = true;
            this.userValueMess.Location = new Point(7, 83);
            this.userValueMess.Name = "userValueMess";
            this.userValueMess.Size = new Size(81, 16);
            this.userValueMess.TabIndex = 6;
            this.userValueMess.Text = "Сообщения";
            this.userValueRak.AutoSize = true;
            this.userValueRak.Location = new Point(6, 35);
            this.userValueRak.Name = "userValueRak";
            this.userValueRak.Size = new Size(32, 16);
            this.userValueRak.TabIndex = 9;
            this.userValueRak.Text = "Рак";
            this.userValueGroup.AutoSize = true;
            this.userValueGroup.Location = new Point(6, 51);
            this.userValueGroup.Name = "userValueGroup";
            this.userValueGroup.Size = new Size(55, 16);
            this.userValueGroup.TabIndex = 5;
            this.userValueGroup.Text = "Группа";
            this.userValueAct.AutoSize = true;
            this.userValueAct.Location = new Point(6, 19);
            this.userValueAct.Name = "userValueAct";
            this.userValueAct.Size = new Size(84, 16);
            this.userValueAct.TabIndex = 4;
            this.userValueAct.Text = "Активность";
            this.userValueLikes.AutoSize = true;
            this.userValueLikes.Location = new Point(7, 99);
            this.userValueLikes.Name = "userValueLikes";
            this.userValueLikes.Size = new Size(73, 16);
            this.userValueLikes.TabIndex = 7;
            this.userValueLikes.Text = "Симпатии";
            this.userValueNick.AutoSize = true;
            this.userValueNick.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.userValueNick.Location = new Point(176, 92);
            this.userValueNick.Name = "userValueNick";
            this.userValueNick.Size = new Size(43, 24);
            this.userValueNick.TabIndex = 13;
            this.userValueNick.Text = "Ник";
            this.btn_UserUpdate.FlatStyle = FlatStyle.Flat;
            this.btn_UserUpdate.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.btn_UserUpdate.Location = new Point(279, 18);
            this.btn_UserUpdate.Name = "btn_UserUpdate";
            this.btn_UserUpdate.Size = new Size(99, 24);
            this.btn_UserUpdate.TabIndex = 25;
            this.btn_UserUpdate.Text = "Обновить";
            this.btn_UserUpdate.UseVisualStyleBackColor = true;
            this.btn_UserUpdate.Click += new EventHandler(this.btn_UserUpdate_Click);
            this.tabPageEvents.Controls.Add((Control)this.comboBoxEvents);
            this.tabPageEvents.Controls.Add((Control)this.eventValueInfluence);
            this.tabPageEvents.Controls.Add((Control)this.eventValueChanse);
            this.tabPageEvents.Controls.Add((Control)this.eventValueDescr);
            this.tabPageEvents.Controls.Add((Control)this.eventValueInit);
            this.tabPageEvents.Controls.Add((Control)this.eventValue);
            this.tabPageEvents.Controls.Add((Control)this.eventValueDate);
            this.tabPageEvents.Controls.Add((Control)this.eventValueType);
            this.tabPageEvents.Controls.Add((Control)this.label10);
            this.tabPageEvents.Controls.Add((Control)this.list_Events);
            this.tabPageEvents.Location = new Point(4, 27);
            this.tabPageEvents.Name = "tabPageEvents";
            this.tabPageEvents.Padding = new Padding(3);
            this.tabPageEvents.Size = new Size(676, 430);
            this.tabPageEvents.TabIndex = 2;
            this.tabPageEvents.Text = "События";
            this.tabPageEvents.UseVisualStyleBackColor = true;
            this.list_Events.FormattingEnabled = true;
            this.list_Events.ItemHeight = 18;
            this.list_Events.Location = new Point(27, 39);
            this.list_Events.Name = "list_Events";
            this.list_Events.Size = new Size(205, 382);
            this.list_Events.TabIndex = 0;
            this.list_Events.SelectedIndexChanged += new EventHandler(this.list_Events_SelectedIndexChanged);
            this.label10.AutoSize = true;
            this.label10.Location = new Point(24, 18);
            this.label10.Name = "label10";
            this.label10.Size = new Size(155, 18);
            this.label10.TabIndex = 1;
            this.label10.Text = "Прошедшие события";
            this.eventValueType.AutoSize = true;
            this.eventValueType.Location = new Point(239, 56);
            this.eventValueType.Name = "eventValueType";
            this.eventValueType.Size = new Size(33, 18);
            this.eventValueType.TabIndex = 2;
            this.eventValueType.Text = "Тип";
            this.eventValueDate.AutoSize = true;
            this.eventValueDate.Location = new Point(238, 74);
            this.eventValueDate.Name = "eventValueDate";
            this.eventValueDate.Size = new Size(43, 18);
            this.eventValueDate.TabIndex = 3;
            this.eventValueDate.Text = "Дата";
            this.eventValue.AutoSize = true;
            this.eventValue.Location = new Point(238, 131);
            this.eventValue.Name = "eventValue";
            this.eventValue.Size = new Size(135, 18);
            this.eventValue.TabIndex = 4;
            this.eventValue.Text = "Краткое описание";
            this.eventValueInit.AutoSize = true;
            this.eventValueInit.Location = new Point(239, 92);
            this.eventValueInit.Name = "eventValueInit";
            this.eventValueInit.Size = new Size(83, 18);
            this.eventValueInit.TabIndex = 5;
            this.eventValueInit.Text = "Инициатор";
            this.eventValueDescr.Location = new Point(242, 151);
            this.eventValueDescr.Name = "eventValueDescr";
            this.eventValueDescr.ReadOnly = true;
            this.eventValueDescr.Size = new Size(426, 96);
            this.eventValueDescr.TabIndex = 6;
            this.eventValueDescr.Text = "";
            this.eventValueChanse.AutoSize = true;
            this.eventValueChanse.Location = new Point(346, 18);
            this.eventValueChanse.Name = "eventValueChanse";
            this.eventValueChanse.Size = new Size(109, 18);
            this.eventValueChanse.TabIndex = 7;
            this.eventValueChanse.Text = "Шанс события";
            this.eventValueInfluence.AutoSize = true;
            this.eventValueInfluence.Location = new Point(239, 113);
            this.eventValueInfluence.Name = "eventValueInfluence";
            this.eventValueInfluence.Size = new Size(67, 18);
            this.eventValueInfluence.TabIndex = 8;
            this.eventValueInfluence.Text = "Влияние";
            this.comboBoxEvents.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.comboBoxEvents.FormattingEnabled = true;
            this.comboBoxEvents.Items.AddRange(new object[10]
            {
        (object) "Все",
        (object) "Бан",
        (object) "Разбан",
        (object) "Уход",
        (object) "Неактивный юзер",
        (object) "Изменение юзера",
        (object) "Получение группы",
        (object) "Буйство модеров",
        (object) "ДН ДД",
        (object) "Разное"
            });
            this.comboBoxEvents.Location = new Point(185, 12);
            this.comboBoxEvents.Name = "comboBoxEvents";
            this.comboBoxEvents.Size = new Size(137, 24);
            this.comboBoxEvents.TabIndex = 9;
            this.comboBoxEvents.SelectedIndexChanged += new EventHandler(this.comboBoxEvents_SelectedIndexChanged);
            this.tabMine.BackColor = Color.WhiteSmoke;
            this.tabMine.Controls.Add((Control)this.label1);
            this.tabMine.Controls.Add((Control)this.numericUpDown1);
            this.tabMine.Controls.Add((Control)this.groupBoxMainDate);
            this.tabMine.Controls.Add((Control)this.groupBox1);
            this.tabMine.Controls.Add((Control)this.panel3);
            this.tabMine.Controls.Add((Control)this.btn_Start);
            this.tabMine.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.tabMine.Location = new Point(4, 27);
            this.tabMine.Name = "tabMine";
            this.tabMine.Padding = new Padding(3);
            this.tabMine.Size = new Size(676, 430);
            this.tabMine.TabIndex = 0;
            this.tabMine.Text = "Главная";
            this.tabMine.ToolTipText = "Страница с основной информацией о симуляции";
            this.btn_Start.FlatStyle = FlatStyle.Flat;
            this.btn_Start.Font = new Font("Microsoft Sans Serif", 15.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.btn_Start.Location = new Point(16, 384);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new Size(265, 38);
            this.btn_Start.TabIndex = 3;
            this.btn_Start.Text = "Начать симуляцию!";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new EventHandler(this.btn_Start_Click);
            this.panel3.Controls.Add((Control)this.tabMainLog);
            this.panel3.Location = new Point(290, 7);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(378, 415);
            this.panel3.TabIndex = 26;
            this.tabMainLog.Controls.Add((Control)this.tabPageMainActivity);
            this.tabMainLog.Controls.Add((Control)this.tabPageMainEvents);
            this.tabMainLog.Dock = DockStyle.Fill;
            this.tabMainLog.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.tabMainLog.Location = new Point(0, 0);
            this.tabMainLog.Name = "tabMainLog";
            this.tabMainLog.SelectedIndex = 0;
            this.tabMainLog.Size = new Size(378, 415);
            this.tabMainLog.TabIndex = 0;
            this.tabPageMainEvents.Controls.Add((Control)this.text_HistoryEvents);
            this.tabPageMainEvents.Location = new Point(4, 27);
            this.tabPageMainEvents.Name = "tabPageMainEvents";
            this.tabPageMainEvents.Padding = new Padding(3);
            this.tabPageMainEvents.Size = new Size(370, 384);
            this.tabPageMainEvents.TabIndex = 0;
            this.tabPageMainEvents.Text = "События";
            this.tabPageMainEvents.UseVisualStyleBackColor = true;
            this.text_HistoryEvents.BackColor = Color.LightBlue;
            this.text_HistoryEvents.BorderStyle = BorderStyle.FixedSingle;
            this.text_HistoryEvents.Dock = DockStyle.Fill;
            this.text_HistoryEvents.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.text_HistoryEvents.Location = new Point(3, 3);
            this.text_HistoryEvents.Name = "text_HistoryEvents";
            this.text_HistoryEvents.ReadOnly = true;
            this.text_HistoryEvents.Size = new Size(364, 378);
            this.text_HistoryEvents.TabIndex = 18;
            this.text_HistoryEvents.Text = "";
            this.tabPageMainActivity.Controls.Add((Control)this.progressBarMonth);
            this.tabPageMainActivity.Controls.Add((Control)this.text_HistoryActivity);
            this.tabPageMainActivity.Location = new Point(4, 27);
            this.tabPageMainActivity.Name = "tabPageMainActivity";
            this.tabPageMainActivity.Padding = new Padding(3);
            this.tabPageMainActivity.Size = new Size(370, 384);
            this.tabPageMainActivity.TabIndex = 1;
            this.tabPageMainActivity.Text = "Активность";
            this.tabPageMainActivity.UseVisualStyleBackColor = true;
            this.text_HistoryActivity.BackColor = Color.LemonChiffon;
            this.text_HistoryActivity.BorderStyle = BorderStyle.FixedSingle;
            this.text_HistoryActivity.Dock = DockStyle.Top;
            this.text_HistoryActivity.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.text_HistoryActivity.Location = new Point(3, 3);
            this.text_HistoryActivity.Name = "text_HistoryActivity";
            this.text_HistoryActivity.ReadOnly = true;
            this.text_HistoryActivity.Size = new Size(364, 350);
            this.text_HistoryActivity.TabIndex = 19;
            this.text_HistoryActivity.Text = "";
            this.progressBarMonth.Location = new Point(7, 358);
            this.progressBarMonth.Maximum = 31;
            this.progressBarMonth.Name = "progressBarMonth";
            this.progressBarMonth.Size = new Size(357, 23);
            this.progressBarMonth.TabIndex = 20;
            this.groupBox1.BackColor = Color.Gainsboro;
            this.groupBox1.Controls.Add((Control)this.tabControlPage1Info);
            this.groupBox1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.groupBox1.Location = new Point(13, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(271, 218);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры";
            this.tabControlPage1Info.Controls.Add((Control)this.tabPageModifiers);
            this.tabControlPage1Info.Controls.Add((Control)this.tabPageStata);
            this.tabControlPage1Info.Dock = DockStyle.Fill;
            this.tabControlPage1Info.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.tabControlPage1Info.Location = new Point(3, 22);
            this.tabControlPage1Info.Multiline = true;
            this.tabControlPage1Info.Name = "tabControlPage1Info";
            this.tabControlPage1Info.SelectedIndex = 0;
            this.tabControlPage1Info.Size = new Size(265, 193);
            this.tabControlPage1Info.TabIndex = 0;
            this.tabPageStata.Controls.Add((Control)this.tb_upMode);
            this.tabPageStata.Controls.Add((Control)this.tb_Chanse);
            this.tabPageStata.Controls.Add((Control)this.tb_SeasonMod);
            this.tabPageStata.Controls.Add((Control)this.tb_MonthMod);
            this.tabPageStata.Controls.Add((Control)this.tb_DownMod);
            this.tabPageStata.Controls.Add((Control)this.textBox1);
            this.tabPageStata.Controls.Add((Control)this.value_AllPages);
            this.tabPageStata.Location = new Point(4, 24);
            this.tabPageStata.Name = "tabPageStata";
            this.tabPageStata.Padding = new Padding(3);
            this.tabPageStata.Size = new Size(257, 165);
            this.tabPageStata.TabIndex = 1;
            this.tabPageStata.Text = "Не робит";
            this.tabPageStata.UseVisualStyleBackColor = true;
            this.value_AllPages.AutoSize = true;
            this.value_AllPages.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.value_AllPages.Location = new Point(6, 4);
            this.value_AllPages.Name = "value_AllPages";
            this.value_AllPages.Size = new Size(103, 16);
            this.value_AllPages.TabIndex = 14;
            this.value_AllPages.Text = "Всего страниц";
            this.textBox1.Location = new Point(154, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(100, 21);
            this.textBox1.TabIndex = 18;
            this.textBox1.Text = "Не робит";
            this.tabPageModifiers.BackColor = Color.Transparent;
            this.tabPageModifiers.Controls.Add((Control)this.value_downMod);
            this.tabPageModifiers.Controls.Add((Control)this.value_upMod);
            this.tabPageModifiers.Controls.Add((Control)this.value_MonthMod);
            this.tabPageModifiers.Controls.Add((Control)this.value_SeasonModif);
            this.tabPageModifiers.Controls.Add((Control)this.value_Chanse);
            this.tabPageModifiers.Location = new Point(4, 24);
            this.tabPageModifiers.Name = "tabPageModifiers";
            this.tabPageModifiers.Padding = new Padding(3);
            this.tabPageModifiers.Size = new Size(257, 165);
            this.tabPageModifiers.TabIndex = 0;
            this.tabPageModifiers.Text = "Модификаторы";
            this.value_Chanse.AutoSize = true;
            this.value_Chanse.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.value_Chanse.Location = new Point(6, 76);
            this.value_Chanse.Name = "value_Chanse";
            this.value_Chanse.Size = new Size(113, 16);
            this.value_Chanse.TabIndex = 7;
            this.value_Chanse.Text = "Адекватность: 4";
            this.value_SeasonModif.AutoSize = true;
            this.value_SeasonModif.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.value_SeasonModif.Location = new Point(6, 52);
            this.value_SeasonModif.Name = "value_SeasonModif";
            this.value_SeasonModif.Size = new Size(150, 16);
            this.value_SeasonModif.TabIndex = 16;
            this.value_SeasonModif.Text = "Модификатор сезона";
            this.value_MonthMod.AutoSize = true;
            this.value_MonthMod.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.value_MonthMod.Location = new Point(6, 28);
            this.value_MonthMod.Name = "value_MonthMod";
            this.value_MonthMod.Size = new Size(150, 16);
            this.value_MonthMod.TabIndex = 8;
            this.value_MonthMod.Text = "Модификатор месяца";
            this.value_upMod.AutoSize = true;
            this.value_upMod.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.value_upMod.Location = new Point(6, 100);
            this.value_upMod.Name = "value_upMod";
            this.value_upMod.Size = new Size(84, 16);
            this.value_upMod.TabIndex = 2;
            this.value_upMod.Text = "Активность";
            this.value_downMod.AutoSize = true;
            this.value_downMod.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.value_downMod.Location = new Point(6, 4);
            this.value_downMod.Name = "value_downMod";
            this.value_downMod.Size = new Size(72, 16);
            this.value_downMod.TabIndex = 5;
            this.value_downMod.Text = "Старение";
            this.tb_DownMod.Location = new Point(151, 30);
            this.tb_DownMod.Name = "tb_DownMod";
            this.tb_DownMod.ReadOnly = true;
            this.tb_DownMod.Size = new Size(100, 21);
            this.tb_DownMod.TabIndex = 17;
            this.tb_DownMod.Text = "Не робит";
            this.tb_MonthMod.Location = new Point(151, 57);
            this.tb_MonthMod.Name = "tb_MonthMod";
            this.tb_MonthMod.ReadOnly = true;
            this.tb_MonthMod.Size = new Size(100, 21);
            this.tb_MonthMod.TabIndex = 18;
            this.tb_MonthMod.Text = "Не робит";
            this.tb_SeasonMod.Location = new Point(151, 84);
            this.tb_SeasonMod.Name = "tb_SeasonMod";
            this.tb_SeasonMod.ReadOnly = true;
            this.tb_SeasonMod.Size = new Size(100, 21);
            this.tb_SeasonMod.TabIndex = 19;
            this.tb_SeasonMod.Text = "Не робит";
            this.tb_Chanse.Location = new Point(151, 111);
            this.tb_Chanse.Name = "tb_Chanse";
            this.tb_Chanse.ReadOnly = true;
            this.tb_Chanse.Size = new Size(100, 21);
            this.tb_Chanse.TabIndex = 20;
            this.tb_Chanse.Text = "Не робит";
            this.tb_upMode.Location = new Point(151, 135);
            this.tb_upMode.Name = "tb_upMode";
            this.tb_upMode.ReadOnly = true;
            this.tb_upMode.Size = new Size(100, 21);
            this.tb_upMode.TabIndex = 21;
            this.tb_upMode.Text = "Не робит";
            this.groupBoxMainDate.BackColor = Color.Gainsboro;
            this.groupBoxMainDate.Controls.Add((Control)this.tabControl2);
            this.groupBoxMainDate.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.groupBoxMainDate.Location = new Point(16, 10);
            this.groupBoxMainDate.Name = "groupBoxMainDate";
            this.groupBoxMainDate.Size = new Size(268, 100);
            this.groupBoxMainDate.TabIndex = 28;
            this.groupBoxMainDate.TabStop = false;
            this.groupBoxMainDate.Text = "Дата";
            this.tabControl2.Controls.Add((Control)this.tabPage1DateGroupBox);
            this.tabControl2.Controls.Add((Control)this.tabPage2DateGroupBox);
            this.tabControl2.Dock = DockStyle.Fill;
            this.tabControl2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.tabControl2.Location = new Point(3, 20);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new Size(262, 77);
            this.tabControl2.TabIndex = 0;
            this.tabPage2DateGroupBox.Location = new Point(4, 22);
            this.tabPage2DateGroupBox.Name = "tabPage2DateGroupBox";
            this.tabPage2DateGroupBox.Padding = new Padding(3);
            this.tabPage2DateGroupBox.Size = new Size(254, 51);
            this.tabPage2DateGroupBox.TabIndex = 1;
            this.tabPage2DateGroupBox.Text = "Прочая инфа(не работает)";
            this.tabPage2DateGroupBox.UseVisualStyleBackColor = true;
            this.tabPage1DateGroupBox.Controls.Add((Control)this.tb_PagesperMonth);
            this.tabPage1DateGroupBox.Controls.Add((Control)this.tb_PagesperDay);
            this.tabPage1DateGroupBox.Controls.Add((Control)this.text_MonthPages);
            this.tabPage1DateGroupBox.Controls.Add((Control)this.label7);
            this.tabPage1DateGroupBox.Location = new Point(4, 22);
            this.tabPage1DateGroupBox.Name = "tabPage1DateGroupBox";
            this.tabPage1DateGroupBox.Padding = new Padding(3);
            this.tabPage1DateGroupBox.Size = new Size(254, 51);
            this.tabPage1DateGroupBox.TabIndex = 0;
            this.tabPage1DateGroupBox.Text = "Страниц за...";
            this.tabPage1DateGroupBox.UseVisualStyleBackColor = true;
            this.label7.AutoSize = true;
            this.label7.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.label7.Location = new Point(6, 4);
            this.label7.Name = "label7";
            this.label7.Size = new Size(116, 16);
            this.label7.TabIndex = 23;
            this.label7.Text = "Вчерашний день";
            this.text_MonthPages.AutoSize = true;
            this.text_MonthPages.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.text_MonthPages.Location = new Point(6, 26);
            this.text_MonthPages.Name = "text_MonthPages";
            this.text_MonthPages.Size = new Size(110, 16);
            this.text_MonthPages.TabIndex = 10;
            this.text_MonthPages.Text = "Прошлый месяц";
            this.tb_PagesperDay.Location = new Point(139, 3);
            this.tb_PagesperDay.Name = "tb_PagesperDay";
            this.tb_PagesperDay.ReadOnly = true;
            this.tb_PagesperDay.Size = new Size(109, 20);
            this.tb_PagesperDay.TabIndex = 11;
            this.tb_PagesperMonth.Location = new Point(139, 25);
            this.tb_PagesperMonth.Name = "tb_PagesperMonth";
            this.tb_PagesperMonth.ReadOnly = true;
            this.tb_PagesperMonth.Size = new Size(109, 20);
            this.tb_PagesperMonth.TabIndex = 12;
            this.tabMain.Controls.Add((Control)this.tabMine);
            this.tabMain.Controls.Add((Control)this.tabPageEvents);
            this.tabMain.Controls.Add((Control)this.tabPageUsers);
            this.tabMain.Controls.Add((Control)this.tabUsersOld);
            this.tabMain.Dock = DockStyle.Fill;
            this.tabMain.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.tabMain.Location = new Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new Size(684, 461);
            this.tabMain.TabIndex = 15;
            this.nicknameDataGridViewTextBoxColumn.DataPropertyName = "nickname";
            this.nicknameDataGridViewTextBoxColumn.HeaderText = "Ник";
            this.nicknameDataGridViewTextBoxColumn.Name = "nicknameDataGridViewTextBoxColumn";
            this.nicknameDataGridViewTextBoxColumn.ReadOnly = true;
            this.registrationDataGridViewTextBoxColumn.DataPropertyName = "registration";
            this.registrationDataGridViewTextBoxColumn.HeaderText = "Регистрация";
            this.registrationDataGridViewTextBoxColumn.Name = "registrationDataGridViewTextBoxColumn";
            this.registrationDataGridViewTextBoxColumn.ReadOnly = true;
            this.activeDataGridViewCheckBoxColumn.DataPropertyName = "active";
            this.activeDataGridViewCheckBoxColumn.HeaderText = "Активность";
            this.activeDataGridViewCheckBoxColumn.Name = "activeDataGridViewCheckBoxColumn";
            this.rakDataGridViewCheckBoxColumn.DataPropertyName = "Rak";
            this.rakDataGridViewCheckBoxColumn.HeaderText = "Рак";
            this.rakDataGridViewCheckBoxColumn.Name = "rakDataGridViewCheckBoxColumn";
            this.groupDataGridViewTextBoxColumn.DataPropertyName = "group";
            this.groupDataGridViewTextBoxColumn.HeaderText = "Группа";
            this.groupDataGridViewTextBoxColumn.Name = "groupDataGridViewTextBoxColumn";
            this.groupDataGridViewTextBoxColumn.ReadOnly = true;
            this.modDataGridViewCheckBoxColumn.DataPropertyName = "mod";
            this.modDataGridViewCheckBoxColumn.HeaderText = "Модератор";
            this.modDataGridViewCheckBoxColumn.Name = "modDataGridViewCheckBoxColumn";
            this.modDataGridViewCheckBoxColumn.ReadOnly = true;
            this.messagesDataGridViewTextBoxColumn.DataPropertyName = "messages";
            this.messagesDataGridViewTextBoxColumn.HeaderText = "Сообщения";
            this.messagesDataGridViewTextBoxColumn.Name = "messagesDataGridViewTextBoxColumn";
            this.messagesDataGridViewTextBoxColumn.ReadOnly = true;
            this.likesDataGridViewTextBoxColumn.DataPropertyName = "likes";
            this.likesDataGridViewTextBoxColumn.HeaderText = "Симпатии";
            this.likesDataGridViewTextBoxColumn.Name = "likesDataGridViewTextBoxColumn";
            this.likesDataGridViewTextBoxColumn.ReadOnly = true;
            this.bannedDataGridViewCheckBoxColumn.DataPropertyName = "Banned";
            this.bannedDataGridViewCheckBoxColumn.HeaderText = "Бан";
            this.bannedDataGridViewCheckBoxColumn.Name = "bannedDataGridViewCheckBoxColumn";
            this.stayPossDataGridViewTextBoxColumn.DataPropertyName = "StayPoss";
            this.stayPossDataGridViewTextBoxColumn.HeaderText = "Уход";
            this.stayPossDataGridViewTextBoxColumn.Name = "stayPossDataGridViewTextBoxColumn";
            this.stayPossDataGridViewTextBoxColumn.ReadOnly = true;
            this.changePossDataGridViewTextBoxColumn.DataPropertyName = "ChangePoss";
            this.changePossDataGridViewTextBoxColumn.HeaderText = "Изменение";
            this.changePossDataGridViewTextBoxColumn.Name = "changePossDataGridViewTextBoxColumn";
            this.changePossDataGridViewTextBoxColumn.ReadOnly = true;
            this.userBindingSource.DataSource = (object)typeof(User);
            this.numericUpDown1.Increment = new Decimal(new int[4]
            {
        5,
        0,
        0,
        0
            });
            this.numericUpDown1.Location = new Point(216, 360);
            this.numericUpDown1.Maximum = new Decimal(new int[4]
            {
        2000,
        0,
        0,
        0
            });
            this.numericUpDown1.Minimum = new Decimal(new int[4]
            {
        10,
        0,
        0,
        0
            });
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new Size(68, 21);
            this.numericUpDown1.TabIndex = 29;
            this.numericUpDown1.Value = new Decimal(new int[4]
            {
        500,
        0,
        0,
        0
            });
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 366);
            this.label1.Name = "label1";
            this.label1.Size = new Size(192, 15);
            this.label1.TabIndex = 30;
            this.label1.Text = "Скорость дня(в миллисекундах)";
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Gainsboro;
            this.ClientSize = new Size(684, 461);
            this.Controls.Add((Control)this.tabMain);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Симулятор Румине";
            ((ISupportInitialize)this.bindingSourceUsers).EndInit();
            this.dataSet1.EndInit();
            this.dataTable1.EndInit();
            this.tabUsersOld.ResumeLayout(false);
            this.tabUsersOld.PerformLayout();
            ((ISupportInitialize)this.dataGridViewUsers).EndInit();
            this.tabPageUsers.ResumeLayout(false);
            this.tabPageUsers.PerformLayout();
            this.panelUserEvents.ResumeLayout(false);
            this.tabControlUserEvents.ResumeLayout(false);
            this.tabPageUserEventLook.ResumeLayout(false);
            this.tabPageUserEventLook.PerformLayout();
            this.tabPageUserEventChange.ResumeLayout(false);
            this.tabPageUserEventsLog.ResumeLayout(false);
            this.panelUserInfo.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageUserOtherInfo.ResumeLayout(false);
            this.tabPageUserOtherInfo.PerformLayout();
            this.tabPageUserForumInfo.ResumeLayout(false);
            this.tabPageUserForumInfo.PerformLayout();
            this.tabPageEvents.ResumeLayout(false);
            this.tabPageEvents.PerformLayout();
            this.tabMine.ResumeLayout(false);
            this.tabMine.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tabMainLog.ResumeLayout(false);
            this.tabPageMainEvents.ResumeLayout(false);
            this.tabPageMainActivity.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabControlPage1Info.ResumeLayout(false);
            this.tabPageStata.ResumeLayout(false);
            this.tabPageStata.PerformLayout();
            this.tabPageModifiers.ResumeLayout(false);
            this.tabPageModifiers.PerformLayout();
            this.groupBoxMainDate.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1DateGroupBox.ResumeLayout(false);
            this.tabPage1DateGroupBox.PerformLayout();
            this.tabMain.ResumeLayout(false);
            ((ISupportInitialize)this.userBindingSource).EndInit();
            this.numericUpDown1.EndInit();
            this.ResumeLayout(false);
        }
    }
}
