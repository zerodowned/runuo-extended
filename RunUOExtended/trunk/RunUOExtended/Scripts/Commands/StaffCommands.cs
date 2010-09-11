using System;
using System.Collections.Generic;
using Server.Targeting;
using Server.Network;
using System.Reflection;
using Server.Items;
using System.Collections;
using Server.Mobiles;

namespace Server.Commands
{
    public class StaffCommands
    {
        private static List<Mobile> m_HearAll = new List<Mobile>();

        public static void Initialize()
        {
            CommandSystem.Register("HearAll", AccessLevel.GameMaster, new CommandEventHandler(HearAll_OnCommand));
            CommandSystem.Register("SayThis", AccessLevel.GameMaster, new CommandEventHandler(SayThis_OnCommand));
            CommandSystem.Register("GmMe", AccessLevel.GameMaster, new CommandEventHandler(GmMe_OnCommand));
            CommandSystem.Register("Refresh", AccessLevel.GameMaster, new CommandEventHandler(Refresh_OnCommand));
            CommandSystem.Register("Clone", AccessLevel.GameMaster, new CommandEventHandler(Clone_OnCommand));
            CommandSystem.Register("CloneMe", AccessLevel.GameMaster, new CommandEventHandler(CloneMe_OnCommand));
            EventSink.Speech += new SpeechEventHandler(HearAllOnSpeech);
        }

        public static void HearAllOnSpeech(SpeechEventArgs e)
        {
            if (m_HearAll.Count > 0)
            {
                string msg = String.Format("({0}): {1}", e.Mobile.RawName, e.Speech);

                for (int i = 0; i < m_HearAll.Count; ++i)
                {
                    m_HearAll[i].SendMessage(msg);
                }
            }
        }
        [Usage("HearAll")]
        [Description("Enable or Disable hearing everything in the world.")]
        public static void HearAll_OnCommand(CommandEventArgs e)
        {
            if (m_HearAll.Contains(e.Mobile))
            {
                m_HearAll.Remove(e.Mobile);
                e.Mobile.SendMessage("HearAll deactivated.");
            }
            else
            {
                m_HearAll.Add(e.Mobile);
                e.Mobile.SendMessage("HearAll enabled.");
            }
        }
        [Usage("SayThis <text>")]
        [Description("Forces Target to Say <text>.")]
        public static void SayThis_OnCommand(CommandEventArgs e)
        {
            string toSay = e.ArgString.Trim();

            if (toSay.Length > 0)
                e.Mobile.Target = new SayThisTarget(toSay);
            else
                e.Mobile.SendMessage("Format: SayThis \"<text>\"");
        }
        [Usage("GmMe")]
        [Description("Helps senior staff members set their body to GM style.")]
        public static void GmMe_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            CommandLogging.WriteLine(from, "{0} {1} is assuming a GM body", from.AccessLevel, CommandLogging.Format(from));
            from.Blessed = true;
            DisRobe(from, Layer.Shoes);
            DisRobe(from, Layer.Pants);
            DisRobe(from, Layer.Shirt);
            DisRobe(from, Layer.Helm);
            DisRobe(from, Layer.Gloves);
            DisRobe(from, Layer.Neck);
            DisRobe(from, Layer.Hair);
            DisRobe(from, Layer.Waist);
            DisRobe(from, Layer.InnerTorso);
            DisRobe(from, Layer.MiddleTorso);
            DisRobe(from, Layer.Arms);
            DisRobe(from, Layer.Cloak);
            DisRobe(from, Layer.OuterTorso);
            DisRobe(from, Layer.OuterLegs);
            from.AddItem(new GMRobe());

            for (int i = 0; i < from.Skills.Length; ++i)
                from.Skills[i].Base = 120;
        }
        [Usage("Refresh")]
        [Description("Sets all targets stats to full.")]
        public static void Refresh_OnCommand(CommandEventArgs e)
        {
            e.Mobile.Target = new freshTarget();
        }

        [Usage("Clone")]
        [Description("Assume the form of another Player or Creature.")]
        public static void Clone_OnCommand(CommandEventArgs e)
        {
            e.Mobile.Target = new CloneTarget();
        }

        [Usage("CloneMe")]
        [Description("Makes an exact duplicate of you at your present location and hides you")]
        public static void CloneMe_OnCommand(CommandEventArgs e)
        {
            BaseCreature m = new BaseCreature(AIType.AI_None, FightMode.None, 10, 1, 0.2, 0.4);
            m.InitStats(10, 10, 10);
            m.SetSkill(SkillName.Cooking, 65, 88);
            m.SetSkill(SkillName.Snooping, 65, 88);
            m.SetSkill(SkillName.Stealing, 65, 88);
            if (m.Female = Utility.RandomBool())
            {
                m.Body = 0x1A6;
                m.Name = NameList.RandomName("female");


            }
            else
            {
                m.Body = 0x1A4;
                m.Name = NameList.RandomName("male");

            }
            e.Mobile.Hidden = true;
            m.Dex = e.Mobile.Dex;
            m.Int = e.Mobile.Int;
            m.Str = e.Mobile.Str;
            m.Fame = e.Mobile.Fame;
            m.Karma = e.Mobile.Karma;
            m.NameHue = e.Mobile.NameHue;
            m.SpeechHue = e.Mobile.SpeechHue;
            m.Criminal = e.Mobile.Criminal;
            m.Name = e.Mobile.Name;
            m.Title = e.Mobile.Title;
            m.Female = e.Mobile.Female;
            m.Body = e.Mobile.Body;
            m.Hue = e.Mobile.Hue;
            m.Hits = e.Mobile.HitsMax;
            m.Mana = e.Mobile.ManaMax;
            m.Stam = e.Mobile.StamMax;
            m.BodyMod = e.Mobile.Body;
            m.Map = e.Mobile.Map;
            m.Location = e.Mobile.Location;
            m.Direction = e.Mobile.Direction;
            m.HairItemID = e.Mobile.HairItemID;
            m.FacialHairItemID = e.Mobile.FacialHairItemID;
            m.HairHue = e.Mobile.HairHue;
            m.FacialHairHue = e.Mobile.FacialHairHue;

            for (int i = 0; i < e.Mobile.Skills.Length; i++)
                m.Skills[i].Base = e.Mobile.Skills[i].Base;

            ArrayList items = new ArrayList(e.Mobile.Items);
            for (int i = 0; i < items.Count; i++)
            {
                Item item = (Item)items[i]; //my favorite line of code, ever. 

                if (((item != null) && (item.Parent == e.Mobile) && (item != e.Mobile.Backpack)))
                {
                    Type t = item.GetType();
                    ConstructorInfo c = t.GetConstructor(Type.EmptyTypes);
                    if (c != null)
                    {
                        try
                        {
                            object o = c.Invoke(null);
                            if (o != null && o is Item)
                            {
                                Item newItem = (Item)o;
                                CopyProperties(newItem, item);
                                item.OnAfterDuped(newItem);
                                newItem.Parent = null;
                                m.AddItem(newItem);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
        private class SayThisTarget : Target
        {
            private string m_toSay;

            public SayThisTarget(string s)
                : base(-1, false, TargetFlags.None)
            {
                m_toSay = s;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {

                if (targeted is Mobile)
                {
                    Mobile targ = (Mobile)targeted;
                    targ.Say(m_toSay);

                }
                else if (targeted is Item)
                {
                    Item objet = targeted as Item;
                    objet.PublicOverheadMessage(MessageType.Regular, 0, false, "" + m_toSay + "");
                }

            }
        }

        private static void DisRobe(Mobile m_from, Layer layer)
        {
            if (m_from.FindItemOnLayer(layer) != null)
            {
                Item item = m_from.FindItemOnLayer(layer);
                m_from.PlaceInBackpack(item); // Place in a bag first? 
            }
        }
        public class freshTarget : Target
        {
            public freshTarget()
                : base(12, false, TargetFlags.None)
            {
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Mobile)
                {
                    Mobile targ = (Mobile)targeted;
                    if (!from.CanSee(targ))
                    {
                        from.SendMessage("The target is not in your line of sight!");
                    }
                    else
                    {
                        targ.Hits = targ.HitsMax;
                        targ.Mana = targ.ManaMax;
                        targ.Stam = targ.StamMax;
                    }
                }
            }
        }
        private class CloneTarget : Target
        {
            public CloneTarget()
                : base(-1, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Mobile)
                {
                    Mobile targ = (Mobile)targeted;

                    if (from != targ && from.AccessLevel > targ.AccessLevel)
                    {
                        CommandLogging.WriteLine(from, "{0} {1} is cloning {2}", from.AccessLevel, CommandLogging.Format(from), CommandLogging.Format(targ));

                        from.Dex = targ.Dex;
                        from.Int = targ.Int;
                        from.Str = targ.Str;
                        from.Fame = targ.Fame;
                        from.Karma = targ.Karma;
                        from.NameHue = targ.NameHue;
                        from.SpeechHue = targ.SpeechHue;

                        from.Name = targ.Name;
                        from.Title = targ.Title;
                        from.Female = targ.Female;
                        from.Body = targ.Body;
                        from.Hue = targ.Hue;

                        from.Hits = from.HitsMax;
                        from.Mana = from.ManaMax;
                        from.Stam = from.StamMax;

                        from.Location = targ.Location;
                        from.Direction = targ.Direction;

                        from.HairItemID = targ.HairItemID;
                        from.FacialHairItemID = targ.FacialHairItemID;
                        from.HairHue = targ.HairHue;
                        from.FacialHairHue = targ.FacialHairHue;

                        if (!targ.Player)
                            from.BodyMod = targ.Body;
                        else
                            from.BodyMod = 0;

                        for (int i = 0; i < from.Skills.Length; i++)
                            from.Skills[i].Base = targ.Skills[i].Base;


                        ArrayList m_items = new ArrayList(from.Items);
                        for (int i = 0; i < m_items.Count; i++)
                        {
                            Item item = (Item)m_items[i];
                            if (((item.Parent == from) && (item != from.Backpack)))
                                item.Delete();
                        }


                        ArrayList items = new ArrayList(targ.Items);
                        for (int i = 0; i < items.Count; i++)
                        {
                            Item item = (Item)items[i]; //my favorite line of code, ever. 

                            if (((item != null) && (item.Parent == targ) && (item != targ.Backpack)))
                            {
                                Type t = item.GetType();
                                ConstructorInfo c = t.GetConstructor(Type.EmptyTypes);
                                if (c != null)
                                {
                                    try
                                    {
                                        object o = c.Invoke(null);
                                        if (o != null && o is Item)
                                        {
                                            Item newItem = (Item)o;
                                            CopyProperties(newItem, item);
                                            item.OnAfterDuped(newItem);
                                            newItem.Parent = null;
                                            from.AddItem(newItem);
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }

                        targ.Frozen = true;
                        targ.Hidden = true;
                    }
                }
            }
        }
        private static void CopyProperties(Item dest, Item src)
        {
            PropertyInfo[] props = src.GetType().GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                try
                {
                    if (props[i].CanRead && props[i].CanWrite)
                    {

                        // These properties must not be copied during the dupe, they get set implicitely by placing
                        // items properly using "DropItem()" etc. .
                        switch (props[i].Name)
                        {
                            case "Parent":
                            case "TotalWeight":
                            case "TotalItems":
                            case "TotalGold":
                                break;
                            default:
                                props[i].SetValue(dest, props[i].GetValue(src, null), null);
                                break;
                        }
                        // end exceptions 
                    }
                }
                catch
                {
                }

                // BaseArmor, BaseClothing, BaseJewel, BaseWeapon: copy nested classes
                // ToDo: If someone knows something about dynamic casting these 4 blocks
                //       could be integrated into one...
                if (src is BaseWeapon)
                {
                    object src_obj = ((BaseWeapon)src).Attributes;
                    object dest_obj = ((BaseWeapon)dest).Attributes;

                    if (src_obj != null && dest_obj != null)
                        CopyProperties(dest_obj, src_obj);

                    src_obj = ((BaseWeapon)src).SkillBonuses;
                    dest_obj = ((BaseWeapon)dest).SkillBonuses;

                    if (src_obj != null && dest_obj != null)
                        CopyProperties(dest_obj, src_obj);

                    src_obj = ((BaseWeapon)src).WeaponAttributes;
                    dest_obj = ((BaseWeapon)dest).WeaponAttributes;

                    if (src_obj != null && dest_obj != null)
                        CopyProperties(dest_obj, src_obj);

                    src_obj = ((BaseWeapon)src).AosElementDamages;
                    dest_obj = ((BaseWeapon)dest).AosElementDamages;

                    if (src_obj != null && dest_obj != null)
                        CopyProperties(dest_obj, src_obj);
                }
                else if (src is BaseArmor)
                {
                    object src_obj = ((BaseArmor)src).Attributes;
                    object dest_obj = ((BaseArmor)dest).Attributes;

                    if (src_obj != null && dest_obj != null)
                        CopyProperties(dest_obj, src_obj);

                    src_obj = ((BaseArmor)src).SkillBonuses;
                    dest_obj = ((BaseArmor)dest).SkillBonuses;

                    if (src_obj != null && dest_obj != null)
                        CopyProperties(dest_obj, src_obj);

                    src_obj = ((BaseArmor)src).ArmorAttributes;
                    dest_obj = ((BaseArmor)dest).ArmorAttributes;

                    if (src_obj != null && dest_obj != null)
                        CopyProperties(dest_obj, src_obj);
                }
                else if (src is BaseJewel)
                {
                    object src_obj = ((BaseJewel)src).Attributes;
                    object dest_obj = ((BaseJewel)dest).Attributes;

                    if (src_obj != null && dest_obj != null)
                        CopyProperties(dest_obj, src_obj);

                    src_obj = ((BaseJewel)src).SkillBonuses;
                    dest_obj = ((BaseJewel)dest).SkillBonuses;

                    if (src_obj != null && dest_obj != null)
                        CopyProperties(dest_obj, src_obj);

                    src_obj = ((BaseJewel)src).Resistances;
                    dest_obj = ((BaseJewel)dest).Resistances;

                    if (src_obj != null && dest_obj != null)
                        CopyProperties(dest_obj, src_obj);

                }
                else if (src is BaseClothing)
                {
                    object src_obj = ((BaseClothing)src).Attributes;
                    object dest_obj = ((BaseClothing)dest).Attributes;

                    if (src_obj != null && dest_obj != null)
                        CopyProperties(dest_obj, src_obj);

                    src_obj = ((BaseClothing)src).SkillBonuses;
                    dest_obj = ((BaseClothing)dest).SkillBonuses;

                    if (src_obj != null && dest_obj != null)
                        CopyProperties(dest_obj, src_obj);

                    src_obj = ((BaseClothing)src).ClothingAttributes;
                    dest_obj = ((BaseClothing)dest).ClothingAttributes;

                    if (src_obj != null && dest_obj != null)
                        CopyProperties(dest_obj, src_obj);

                    src_obj = ((BaseClothing)src).Resistances;
                    dest_obj = ((BaseClothing)dest).Resistances;

                    if (src_obj != null && dest_obj != null)
                        CopyProperties(dest_obj, src_obj);

                }
                // end copying nested classes

            }
        }
        //Duplicates props between two items of same type 
        private static void CopyProperties(object dest, object src)
        {
            PropertyInfo[] props = src.GetType().GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                try
                {
                    if (props[i].CanRead && props[i].CanWrite)
                    {
                        props[i].SetValue(dest, props[i].GetValue(src, null), null);
                    }
                }
                catch
                {
                }
            }
        }
    }
}
