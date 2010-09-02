using System;
using System.Collections;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Mobiles
{
    public class Barber : Mobile
	{

        [Constructable]
        public Barber()
        {
            InitStats(100, 100, 25);

            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();

            if (Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
                Title = "the Barber";
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
                Title = "the Barber";
            }

            AddItem(new FancyShirt(Utility.RandomNeutralHue()));
            AddItem(new ShortPants(Utility.RandomNeutralHue()));
            AddItem(new Boots(Utility.RandomNeutralHue()));
            Utility.AssignRandomHair(this);
            if (Utility.RandomBool())
                Utility.AssignRandomFacialHair(this, HairHue);
        }

        public Barber(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if ((from.InRange(this, 12)) && !(from.InRange(this, 1)))
            {
                this.Say("Please come closer !");
                return;
            }
            BarberCanHandle(from);
        }

        public void BarberCanHandle(Mobile from)
        {
            this.Say("What do you want today ?");
            from.SendGump(new BarberGump(from));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
	}
    public class BarberGump : Gump
    {
        public int HairCutPrice
        {
            get
            {
                return 300;
            }
        }
        public int FacialHairCutPrice
        {
            get
            {
                return 200;
            }
        }
        public BarberGump(Mobile owner)
            : base(0, 0)
        {
            string label0 = string.Format("New HairCut ({0} PO)", HairCutPrice);
            string label1 = string.Format("New BeardCut ({0} PO)", FacialHairCutPrice);

            AddBackground(80, 15, 340, 360, 5054);
            AddAlphaRegion(90, 25, 320, 340);

            AddPage(0);

            AddPage(1);
            AddHtml(215, 30, 280, 27, "Menu", false, false);
            AddHtml(170, 45, 280, 27, "Make a choice :", false, false);
            AddButton(110, 80, 0xFA5, 0xFB7, 2, GumpButtonType.Reply, 1);
            AddButton(110, 110, 0xFA5, 0xFB7, 3, GumpButtonType.Reply, 1);
            AddButton(110, 320, 0xFA5, 0xFB7, 0, GumpButtonType.Reply, 1);

            AddLabel(145, 80, 0, label0);
            AddLabel(145, 110, 0, label1);
            AddLabel(145, 320, 0, "Cancel");

        }


        public override void OnResponse(NetState state, RelayInfo info)
        {
            PlayerMobile from = (PlayerMobile)state.Mobile;
            Container pack = from.Backpack;
            int availgold = pack.GetAmount(typeof(Gold));
            switch (info.ButtonID)
            {
                case 0: // Close/Cancel 
                    {
                        break;
                    }
                case 2: // New Hair 
                    {
                        if (availgold >= HairCutPrice)
                        {
                            from.CloseGump(typeof(BarberGump));
                            from.SendGump(new CutGump(from, 0, HairCutPrice));
                        }
                        else
                        {
                            from.Say("I guess this is not for poor people !!");
                        }
                        break;
                    }
                case 3: // New Beard 
                    {

                        if (availgold >= FacialHairCutPrice)
                        {
                            from.CloseGump(typeof(BarberGump));
                            from.SendGump(new CutGump(from, 1, FacialHairCutPrice));
                        }
                        else
                        {
                            from.Say("I guess this is not for poor people !!");
                        }
                        break;
                    }
            }
        }
    }
    public class CutGump : Gump
    {
        private int m_type;
        private int m_price;
        public CutGump(PlayerMobile owner, int type, int price)
            : base(0, 0)
        {
            m_type = type;
            m_price = price;
            AddBackground(80, 15, 340, 360, 5054);
            AddAlphaRegion(90, 25, 320, 340);

            AddPage(0);

            AddPage(1);

            if (m_type == 0)
            {
                AddHtml(220, 40, 280, 27, "HairCut", false, false);

                AddButton(110, 70, 0xFA5, 0xFA7, 3, GumpButtonType.Reply, 1);
                AddButton(110, 100, 0xFA5, 0xFA7, 4, GumpButtonType.Reply, 1);
                AddButton(110, 130, 0xFA5, 0xFA7, 5, GumpButtonType.Reply, 1);
                AddButton(110, 160, 0xFA5, 0xFA7, 6, GumpButtonType.Reply, 1);
                AddButton(110, 190, 0xFA5, 0xFA7, 7, GumpButtonType.Reply, 1);
                AddButton(110, 220, 0xFA5, 0xFA7, 8, GumpButtonType.Reply, 1);
                AddButton(110, 250, 0xFA5, 0xFA7, 9, GumpButtonType.Reply, 1);
                AddButton(110, 280, 0xFA5, 0xFA7, 10, GumpButtonType.Reply, 1);
                AddButton(110, 310, 0xFA5, 0xFA7, 11, GumpButtonType.Reply, 1);
                AddButton(110, 340, 0xFA5, 0xFA7, 12, GumpButtonType.Reply, 1);

                AddLabel(145, 70, 0, "Short");
                AddLabel(145, 100, 0, "Long");
                AddLabel(145, 130, 0, "Ponytail");
                AddLabel(145, 160, 0, "Mohawk");
                AddLabel(145, 190, 0, "Pageboy");
                AddLabel(145, 220, 0, "Receding");
                AddLabel(145, 250, 0, "2-tails");
                AddLabel(145, 280, 0, "Topknot");
                AddLabel(145, 310, 0, "Afro");
                AddLabel(145, 340, 0, "Bald");
            }
            else
            {
                AddHtml(220, 40, 280, 27, "BeardCut", false, false);
                
                AddButton(110, 70, 0xFA5, 0xFA7, 21, GumpButtonType.Reply, 1);
                AddButton(110, 100, 0xFA5, 0xFA7, 22, GumpButtonType.Reply, 1);
                AddButton(110, 130, 0xFA5, 0xFA7, 23, GumpButtonType.Reply, 1);
                AddButton(110, 160, 0xFA5, 0xFA7, 24, GumpButtonType.Reply, 1);
                AddButton(110, 190, 0xFA5, 0xFA7, 25, GumpButtonType.Reply, 1);
                AddButton(110, 220, 0xFA5, 0xFA7, 26, GumpButtonType.Reply, 1);
                AddButton(110, 250, 0xFA5, 0xFA7, 27, GumpButtonType.Reply, 1);
                AddButton(110, 280, 0xFA5, 0xFA7, 28, GumpButtonType.Reply, 1);

                AddLabel(145, 70, 0, "Goatee");
                AddLabel(145, 100, 0, "Med Short Beard");
                AddLabel(145, 130, 0, "Vandyke");
                AddLabel(145, 160, 0, "Long Beard");
                AddLabel(145, 190, 0, "Short Beard");
                AddLabel(145, 220, 0, "Med Long Beard");
                AddLabel(145, 250, 0, "Mustache");
                AddLabel(145, 280, 0, "No beard");
            }

        }


        public void CutHair(int newhairid, Mobile from, int type, int price)
        {
            Container pack = from.Backpack;
            Item gold = pack.FindItemByType(typeof(Gold));
            if (gold != null)
            {
                if (gold.Amount < m_price)
                {
                    from.Say("I guess this is not for poor people !!");
                    from.Frozen = false;
                }
                else
                {
                    gold.Amount = gold.Amount - m_price;
                    from.Frozen = true;
                    from.PlaySound(0x248);
                    new InternalTimer(0, from, newhairid, type).Start();
                }
            }
            else
            {
                from.Say("I guess this is not for poor people !!");
            }
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 0:
                    {
                        break;
                    }
                case 3:
                    {
                        CutHair(0x203B, from, 0, m_price);
                        break;
                    }
                case 4:
                    {
                        CutHair(0x203C, from, 0, m_price);
                        break;
                    }
                case 5:
                    {
                        CutHair(0x203D, from, 0, m_price);
                        break;
                    }
                case 6:
                    {
                        CutHair(0x2044, from, 0, m_price);
                        break;
                    }
                case 7:
                    {
                        CutHair(0x2045, from, 0, m_price);
                        break;
                    }
                case 8:
                    {
                        CutHair(from.Female ? 0x2046 : 0x2048, from, 0, m_price);
                        break;
                    }
                case 9:
                    {
                        CutHair(0x2049, from, 0, m_price);
                        break;
                    }
                case 10:
                    {
                        CutHair(0x204A, from, 0, m_price);
                        break;
                    }
                case 11:
                    {
                        CutHair(0x2047, from, 0, m_price);
                        break;
                    }
                case 12:
                    {
                        CutHair(0, from, 0, m_price);
                        break;
                    }
                case 21:
                    {
                        CutHair(0x2040, from, 1, m_price);
                        break;
                    }
                case 22:
                    {
                        CutHair(0x204B, from, 1, m_price);
                        break;
                    }
                case 23:
                    {
                        CutHair(0x204D, from, 1, m_price);
                        break;
                    }
                case 24:
                    {
                        CutHair(0x203E, from, 1, m_price);
                        break;
                    }
                case 25:
                    {
                        CutHair(0x203F, from, 1, m_price);
                        break;
                    }
                case 26:
                    {
                        CutHair(0x204C, from, 1, m_price);
                        break;
                    }
                case 27:
                    {
                        CutHair(0x2041, from, 1, m_price);
                        break;
                    }
                case 28:
                    {
                        CutHair(0, from, 1, m_price);
                        break;
                    }
            }
        }
        private class InternalTimer : Timer
        {
            private int m_station;
            private Mobile m_from;
            private int m_hairid;
            private int m_type;

            public InternalTimer(int station, Mobile from, int hairid, int type)
                : base(TimeSpan.FromSeconds(6.0))
            {
                m_station = station;
                m_from = from;
                m_hairid = hairid;
                m_type = type;
            }
            protected override void OnTick()
            {
                switch (m_station)
                {
                    case 0:
                        {
                            CutHairCommand(m_hairid, m_from, m_type);
                            new InternalTimer(1, m_from, m_hairid, m_type).Start();
                            break;
                        }
                    case 1:
                        {
                            m_from.PlaySound(0x248);
                            m_from.Frozen = false;
                            m_from.Say("Thanks Buddy !");
                            break;
                        }
                }
            }
            public void CutHairCommand(int newhairid, Mobile from, int type)
            {
                switch (type)
                {
                    case 0:
                        {
                            from.HairItemID = newhairid;
                            Item item = from.FindItemOnLayer(Layer.Hair);
                            if (item != null)
                            {
                                item.Delete();
                            }

                            break;
                        }
                    case 1:
                        {
                            from.FacialHairItemID = newhairid;
                            Item item = from.FindItemOnLayer(Layer.FacialHair);
                            if (item != null)
                            {
                                item.Delete();
                            }

                            break;

                        }
                }
                from.PlaySound(0x248);
            }
        }
    }
}