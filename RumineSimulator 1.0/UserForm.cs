using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RumineSimulator
{
    public class UserForm : Form
    {
        private IContainer components = (IContainer)null;
        private Label text_mess;
        private Label text_nick;
        private Label text_likes;
        private Label text_group;
        private Label text_reg;
        private Label text_Act;
        private Label text_mod;
        private Label text_rak;
        private RichTextBox richTextBox1;

        public UserForm(
          string nick,
          string reg,
          bool act,
          bool rak,
          bool mod,
          int likes,
          int leavePoss,
          int messages,
          string group)
        {
            this.InitializeComponent();
            this.text_Act.Text = "Активен? " + act.ToString();
            this.text_group.Text = "Группа ";
            this.text_likes.Text = "Симпатии " + (object)likes;
            this.text_mess.Text = "Сообщения " + (object)messages;
            this.text_mod.Text = "Модератор? " + mod.ToString();
            this.text_nick.Text = nick;
            this.text_rak.Text = "Рак? " + rak.ToString();
            this.text_reg.Text = "Регистрация " + reg;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.text_mess = new Label();
            this.text_nick = new Label();
            this.text_likes = new Label();
            this.text_group = new Label();
            this.text_reg = new Label();
            this.text_Act = new Label();
            this.text_mod = new Label();
            this.text_rak = new Label();
            this.richTextBox1 = new RichTextBox();
            this.SuspendLayout();
            this.text_mess.AutoSize = true;
            this.text_mess.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.text_mess.Location = new Point(12, 130);
            this.text_mess.Name = "text_mess";
            this.text_mess.Size = new Size(82, 16);
            this.text_mess.TabIndex = 0;
            this.text_mess.Text = "Сообщений";
            this.text_nick.AutoSize = true;
            this.text_nick.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte)204);
            this.text_nick.Location = new Point(12, 27);
            this.text_nick.Name = "text_nick";
            this.text_nick.Size = new Size(41, 20);
            this.text_nick.TabIndex = 1;
            this.text_nick.Text = "Ник";
            this.text_likes.AutoSize = true;
            this.text_likes.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.text_likes.Location = new Point(12, 146);
            this.text_likes.Name = "text_likes";
            this.text_likes.Size = new Size(73, 16);
            this.text_likes.TabIndex = 2;
            this.text_likes.Text = "Симпатий";
            this.text_group.AutoSize = true;
            this.text_group.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.text_group.Location = new Point(12, 162);
            this.text_group.Name = "text_group";
            this.text_group.Size = new Size(55, 16);
            this.text_group.TabIndex = 3;
            this.text_group.Text = "Группа";
            this.text_reg.AutoSize = true;
            this.text_reg.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.text_reg.Location = new Point(10, 114);
            this.text_reg.Name = "text_reg";
            this.text_reg.Size = new Size(92, 16);
            this.text_reg.TabIndex = 4;
            this.text_reg.Text = "Регистрацня";
            this.text_Act.AutoSize = true;
            this.text_Act.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.text_Act.Location = new Point(12, 98);
            this.text_Act.Name = "text_Act";
            this.text_Act.Size = new Size(70, 16);
            this.text_Act.TabIndex = 5;
            this.text_Act.Text = "Активен?";
            this.text_mod.AutoSize = true;
            this.text_mod.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.text_mod.Location = new Point(13, 178);
            this.text_mod.Name = "text_mod";
            this.text_mod.Size = new Size(89, 16);
            this.text_mod.TabIndex = 6;
            this.text_mod.Text = "Модератор?";
            this.text_rak.AutoSize = true;
            this.text_rak.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
            this.text_rak.Location = new Point(14, 194);
            this.text_rak.Name = "text_rak";
            this.text_rak.Size = new Size(39, 16);
            this.text_rak.TabIndex = 7;
            this.text_rak.Text = "Рак?";
            this.richTextBox1.Location = new Point(13, 250);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new Size(203, 71);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(228, 333);
            this.Controls.Add((Control)this.richTextBox1);
            this.Controls.Add((Control)this.text_rak);
            this.Controls.Add((Control)this.text_mod);
            this.Controls.Add((Control)this.text_Act);
            this.Controls.Add((Control)this.text_reg);
            this.Controls.Add((Control)this.text_group);
            this.Controls.Add((Control)this.text_likes);
            this.Controls.Add((Control)this.text_nick);
            this.Controls.Add((Control)this.text_mess);
            this.Name = nameof(UserForm);
            this.Text = nameof(UserForm);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
