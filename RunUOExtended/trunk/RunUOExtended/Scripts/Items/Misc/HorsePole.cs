using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Commands;
using Server.Multis;

namespace Server.Items
{
	public class HorsePole : Item
	{
        private Mobile m_AnimalOwner;
        private BaseCreature m_Animal;
        private int m_Loyalty;
        private AIType m_AI;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile AnimalOwner
        {
            get
            {
                return m_AnimalOwner;
            }
            set
            {
                m_AnimalOwner = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public BaseCreature Animal
        {
            get
            {
                return m_Animal;
            }
            set
            {
                m_Animal = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Loyalty
        {
            get
            {
                return m_Loyalty;
            }
            set
            {
                m_Loyalty = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public AIType AI
        {
            get
            {
                return m_AI;
            }
            set
            {
                m_AI = value;
            }
        }
        [Constructable]
		public HorsePole()
			: base( 0x14E8 )
		{
			Weight = 10.0;
			Name = "Horse Pole";
		}

        public HorsePole(Serial serial)
			: base( serial )
		{
		}

        public void Free(Mobile from)
        {
            m_Animal.Blessed = false;

            if (from == null)
            {
                m_Animal.Tamable = true;
                m_Animal.Controlled = false;
                m_Animal.Loyalty = m_Loyalty;
                m_Animal.AI = m_AI;
                m_Animal.ControlOrder = OrderType.Release;
                m_Animal = null;
            }
            else
            {
                m_Animal.Tamable = true;
                m_Animal.Controlled = true;
                m_Animal.ControlMaster = m_AnimalOwner;
                m_Animal.Loyalty = m_Loyalty;
                m_Animal.AI = m_AI;
                m_Animal.ControlTarget = m_AnimalOwner;
                m_Animal.ControlOrder = OrderType.Follow;
                m_Animal = null;
                from.Say("*détache sa monture*");
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (this.Movable)
                from.SendMessage("This thing cannot work if movable");
            else if (!from.InRange(this.GetWorldLocation(), 2))
                from.SendLocalizedMessage(500486);   //That is too far away. 
            else if (Animal != null)
            {
                if (m_Animal.Deleted)
                    m_Animal = null;
                else
                {
                    if (AnimalOwner == null || AnimalOwner.Deleted)
                    {
                        Free(null);
                        from.SendMessage("Freedom is back for this poor ownerless animal !");
                    }
                    else if (from == AnimalOwner)
                    {
                        if (from.Followers + m_Animal.ControlSlots > from.FollowersMax)
                            from.SendMessage("Maximum followers reached!");
                        else
                            Free(from);
                    }
                    else
                        from.SendMessage("{0} already use the pole !", AnimalOwner.Name);
                }
            }
            else
            {
                from.Target = new HorsePoleTarget(this);
                from.SendMessage("Witch animal do you want to attach ?");
            }
        }

        private class HorsePoleTarget : Target
        {
            private HorsePole m_Pole;

            public HorsePoleTarget(HorsePole pole)
                : base(1, true, TargetFlags.None)
            {
                m_Pole = pole;
            }

            protected override void OnTarget(Mobile from, object targ)
            {
                if (!(m_Pole.Deleted))
                {
                    if (targ is BaseMount || targ is PackHorse || targ is PackLlama)
                    {
                        BaseCreature creature = targ as BaseCreature;

                        if ((creature.Controlled && creature.ControlMaster == from))
                        {
                            m_Pole.Animal = creature;
                            m_Pole.AnimalOwner = from;
                            m_Pole.Loyalty = creature.Loyalty;
                            m_Pole.AI = creature.AI;
                            creature.AI = AIType.AI_None;
                            creature.Tamable = false;
                            creature.ControlMaster = null;
                            creature.Blessed = true;

                            from.Say("*Attach the animal*");
                        }
                        else if (creature.AI == AIType.AI_None)
                        {
                            from.SendMessage("This animal is already attached");
                        }
                        else
                        {
                            from.SendMessage("This is not your animal !");
                        }
                    }
                    else if (targ is PlayerMobile)
                    {
                        Mobile m = (Mobile)targ;

                        if (m == from)
                        {
                            from.Say("*tried to attach himself to the pole*");
                        }
                        else
                        {
                            from.Say("*tried but failed to attach {0} to the pole*", m.Name);
                        }
                    }
                    else
                    {
                        from.SendMessage("This is not a mountable creature !");
                    }
                }
                return;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 

            writer.Write((Mobile)m_Animal);
            writer.Write((Mobile)m_AnimalOwner);
            writer.Write((int)m_Loyalty);
            writer.Write((int)m_AI);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Animal = (BaseCreature)reader.ReadMobile();
                        m_AnimalOwner = reader.ReadMobile();
                        m_Loyalty = reader.ReadInt();
                        m_AI = (AIType)reader.ReadInt();
                        break;
                    }
            }
        }
	}
}