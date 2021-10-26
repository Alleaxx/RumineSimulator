using System;
using System.Collections.Generic;

namespace RumineSimulator
{
    internal class UserNickBank
    {
        private List<string> free_nicks = new List<string>();
        private Random random = new Random();

        public UserNickBank()
        {
            this.free_nicks.Add("naswai");
            this.free_nicks.Add("Allexx");
            this.free_nicks.Add("3JIou_Kpunep");
            this.free_nicks.Add("NIGHTDANGER");
            this.free_nicks.Add("Капут-противогаз");
            this.free_nicks.Add("CrashBoy01");
            this.free_nicks.Add("MegaZerg");
            this.free_nicks.Add("DenTech");
            this.free_nicks.Add("gromda");
            this.free_nicks.Add("Andrej2001");
            this.free_nicks.Add("HerrManeling");
            this.free_nicks.Add("Dimalav567");
            this.free_nicks.Add("Niket");
            this.free_nicks.Add("Wheatley");
            this.free_nicks.Add("Dedepete");
            this.free_nicks.Add("ArtemkaFomin");
            this.free_nicks.Add("Alex.G.");
            this.free_nicks.Add("BeZZe");
            this.free_nicks.Add("Ме");
            this.free_nicks.Add("frendly herobrin rus");
            this.free_nicks.Add("InFeRnAl_KiD");
            this.free_nicks.Add("Пумба :D");
            this.free_nicks.Add("NeZoX");
            this.free_nicks.Add("SuperM");
            this.free_nicks.Add("overstalker");
            this.free_nicks.Add("Gevorg2012");
            this.free_nicks.Add("CheessteR");
            this.free_nicks.Add("senyaiv");
            this.free_nicks.Add("(Slime)");
            this.free_nicks.Add("dapimex");
            this.free_nicks.Add("Lektorrr");
            this.free_nicks.Add("CrazyBanana");
            this.free_nicks.Add("Anthony Kiedis");
            this.free_nicks.Add("Dj_fantom");
            this.free_nicks.Add("IlyaSidorin");
            this.free_nicks.Add("TheProFinch");
            this.free_nicks.Add("raxar");
            this.free_nicks.Add("sk909");
            this.free_nicks.Add("DjSteve");
            this.free_nicks.Add("SirPomidor");
            this.free_nicks.Add("Машок");
            this.free_nicks.Add("Winlocker");
            this.free_nicks.Add("GeXOn");
            this.free_nicks.Add("ArtPlayMan");
            this.free_nicks.Add("Injustice");
            this.free_nicks.Add("BlueMoshka");
            this.free_nicks.Add("MesHo4eK");
            this.free_nicks.Add("Ruslanzh");
            this.free_nicks.Add("anatolgol");
            this.free_nicks.Add("GamerXP");
            this.free_nicks.Add("DezmutNour");
            this.free_nicks.Add("Stairdeck");
            this.free_nicks.Add("vasilek - vitalik");
            this.free_nicks.Add("tomkoro");
            this.free_nicks.Add("Direct");
            this.free_nicks.Add("egor01");
            this.free_nicks.Add("minemineminecraft");
            this.free_nicks.Add("lolnoob");
            this.free_nicks.Add("Лорхан");
            this.free_nicks.Add("SttttttTePaN");
            this.free_nicks.Add("MikFreeD");
            this.free_nicks.Add("Azik ^ 3 ^");
            this.free_nicks.Add("NEOevil2");
            this.free_nicks.Add("frokys");
            this.free_nicks.Add("MR_GamesOloloev");
            this.free_nicks.Add("A_l_e_x_0_0_7");
            this.free_nicks.Add("G3Forse");
            this.free_nicks.Add("NestorRoyce");
            this.free_nicks.Add("buckij");
            this.free_nicks.Add("vision");
            this.free_nicks.Add("mr.steve");
            this.free_nicks.Add("fenos");
            this.free_nicks.Add("DXYVAD");
            this.free_nicks.Add("MrSennator");
            this.free_nicks.Add("KROLant");
            this.free_nicks.Add("Arthas_Men");
            this.free_nicks.Add("FrayForce");
            this.free_nicks.Add("ЯndomTrash");
            this.free_nicks.Add("FireStoneeeCraft");
            this.free_nicks.Add("saxalin");
            this.free_nicks.Add("Kain");
            this.free_nicks.Add("tonchik");
            this.free_nicks.Add("LORDfito");
            this.free_nicks.Add("The_Faos");
            this.free_nicks.Add("Syngli");
            this.free_nicks.Add("Shadowind");
            this.free_nicks.Add("Yarrow1107");
        }

        public string SelectNick()
        {
            string freeNick;
            if (this.free_nicks.Count <= 1)
            {
                freeNick = this.random.Next(1000).ToString();
            }
            else
            {
                int index = this.random.Next(this.free_nicks.Count);
                freeNick = this.free_nicks[index];
                this.free_nicks.RemoveAt(index);
            }
            return freeNick;
        }
    }
}
