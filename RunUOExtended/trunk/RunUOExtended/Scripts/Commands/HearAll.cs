using System;
using System.Collections.Generic;

namespace Server.Commands
{
    public class HearAll
    {
        private static List<Mobile> m_HearAll = new List<Mobile>();

        public static void Initialize()
        {
            CommandSystem.Register("hearall", AccessLevel.GameMaster, new CommandEventHandler(HearAll_OnCommand));
            EventSink.Speech += new SpeechEventHandler(OnSpeech);
        }

        public static void OnSpeech(SpeechEventArgs e)
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
    }
}
