using System.Collections.Generic;

namespace RumineSimulator
{
    internal class UsersStorage
    {
        public List<User> users = new List<User>();
        private UserNickBank NickBank = new UserNickBank();

        public int amount { get; private set; }

        public int modAmount { get; private set; }

        public int rakAmount { get; private set; }

        public int activeAmount { get; private set; }

        public int banAmount { get; private set; }

        public string user_text_data { get; private set; }

        public UsersStorage(int Amount) => this.amount = Amount;

        public void UsersGenerate() => this.users.Add(new User(this.NickBank));

        public void StatCount()
        {
            this.activeAmount = 0;
            this.banAmount = 0;
            this.rakAmount = 0;
            this.modAmount = 0;
            for (int index = 0; index < this.amount; ++index)
            {
                if (this.users[index].mod)
                    ++this.modAmount;
            }
            for (int index = 0; index < this.amount; ++index)
            {
                if (this.users[index].active)
                    ++this.activeAmount;
            }
            for (int index = 0; index < this.amount; ++index)
            {
                if (this.users[index].Banned)
                    ++this.banAmount;
            }
            for (int index = 0; index < this.amount; ++index)
            {
                if (this.users[index].Rak)
                    ++this.rakAmount;
            }
        }
    }
}
