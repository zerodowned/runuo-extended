using Server;
using System;
using System.Collections;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Items;
using Server.Commands;

namespace Server.FarmingSystem
{
    public class FarmingSystemCore
    {
        private static readonly TimeSpan TheDelay = TimeSpan.FromMinutes(30.0);
        public static Timer TheTimer;
        public static Dictionary<Serial, Chicken> Chickens = new Dictionary<Serial, Chicken>();

        public static void Initialize()
        {
            Utility.PushColor(ConsoleColor.Cyan);
            Console.WriteLine("Farming System starting up ...");
            Utility.PopColor();
            Utility.PushColor(ConsoleColor.DarkCyan);

            TheTimer = Timer.DelayCall(TheDelay, TheDelay, new TimerCallback(OnTimerTick));

            // Here is the details

            Utility.PopColor();

        }

        private static void OnTimerTick()
        {
            int count;


            Utility.PushColor(ConsoleColor.Cyan);
            Console.WriteLine(String.Format("[{0} {1}] Farming System is working hard ! ", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
            Utility.PopColor();
            Utility.PushColor(ConsoleColor.DarkCyan);


            // Eggs from chickens
            count = 0;
            Console.Write("Eggs from chickens ...");
            List<Serial> deletedChickens = new List<Serial>();
            foreach (KeyValuePair<Serial, Chicken> kvp in Chickens)
                if (Utility.Random(5) >= 4)
                {
                    Chicken poule = kvp.Value;
                    if (poule != null && !poule.Deleted)
                    {
                        count++;
                        poule.PlaySound(poule.BaseSoundID);
                        new Eggs().MoveToWorld(poule.Location, poule.Map);
                    }
                    else
                        deletedChickens.Add(kvp.Key);
                }
            while (deletedChickens.Count != 0)
            {
                Chickens.Remove(deletedChickens[0]);
                deletedChickens.RemoveAt(0);
            }
            Console.WriteLine(String.Format("({0}/{1} chickens gave eggs)", count, Chickens.Count));

            Utility.PopColor();
        }
    } 
}
