using System;
using System.Collections.Generic;

namespace Server
{
    public class Strings
    {
        public enum ServerLanguage
        {
            EN,
            FR
        }
        private static readonly ServerLanguage LANG = ServerLanguage.FR;
        private static int L { get { return (int)LANG; } }
        private static readonly Dictionary<string, string[]> m_Datas = new Dictionary<string, string[]>();

        public static void Initialize()
        {
            //Vendors
            m_Datas.Add("vendor-barberM",new string[]{"the barber","le coiffeur"});
            m_Datas.Add("vendor-barberF",new string[]{"the barber","la coiffeuse"});
            
            // Dialogs
            m_Datas.Add("dialog-comeCloser",new string[]{"Please come closer !","Approchez vous un peu !"});
            m_Datas.Add("dialog-whatDoYouWant",new string[]{"What do you want today ?","Que voulez vous aujourd'hui ?"});
            m_Datas.Add("dialog-notForPoor",new string[]{"I guess this is not for poor people !!","Visiblement je suis trop pauvre !"});
            m_Datas.Add("dialog-thanksBuddy",new string[]{"Thanks Buddy !","Merci mon brave !"});
            
            // Emotes
            m_Datas.Add("emote-detachMount",new string[]{"detach his mount","detache sa monture"});
            m_Datas.Add("emote-attachMount",new string[]{"attach his mount","attache sa monture au poteau"});
            m_Datas.Add("emote-attachMyself",new string[]{"tried to attach himself to the pole","a essaye de s'attacher au poteau"});
            m_Datas.Add("emote-attachHuman",new string[]{"tried but failed to attach {0} to the pole","a essaye d'attacher {0}, mais n'a pas reussi"});
            
            // Labels
            m_Datas.Add("label-cancel",new string[]{"Cancel","Annuler"});
            m_Datas.Add("label-menu",new string[]{"Menu","Menu"});
            m_Datas.Add("label-makeChoice",new string[]{"Make a choice :","Faites votre choix"});
            m_Datas.Add("label-newHairCut",new string[]{"New HairCut ({0} GP)","Nouveaux cheveux ({0} PO)"});
            m_Datas.Add("label-newBeardCut",new string[]{"New BeardCut ({0} GP)","Nouvelle barbe ({0} PO)"});
            m_Datas.Add("label-hairCut",new string[]{"Hair Cut","Coupe de cheveux"});
            m_Datas.Add("label-beardCut",new string[]{"Beard Cut","Barbe et moustache"});

            // Body
            m_Datas.Add("body-ShortHair",new string[]{"Short Hair","Cheveux court"});
            m_Datas.Add("body-LongHair",new string[]{"Long Hair","Cheveux long"});
            m_Datas.Add("body-Ponytail",new string[]{"Ponytail","Queue de cheval"});
            m_Datas.Add("body-Mohawk",new string[]{"Mohawk","Mohawk"});
            m_Datas.Add("body-Pageboy",new string[]{"Pageboy","Pageboy"});
            m_Datas.Add("body-Receding",new string[]{"Receding","Receding"});
            m_Datas.Add("body-2tails",new string[]{"2-tails","2 Lulus"});
            m_Datas.Add("body-Topknot",new string[]{"Topknot","Topknot"});
            m_Datas.Add("body-Afro",new string[]{"Afro","Afro"});
            m_Datas.Add("body-Bald",new string[]{"Bald","Chauve"});
            m_Datas.Add("body-Goatee",new string[]{"Goatee","Pinch"});
            m_Datas.Add("body-MedShortBeard",new string[]{"Med Short Beard","Barbe plutot longue"});
            m_Datas.Add("body-Vandyke",new string[]{"Vandyke","Vandyke"});
            m_Datas.Add("body-LongBeard",new string[]{"Long Beard","Barbe longue"});
            m_Datas.Add("body-ShortBeard",new string[]{"Short Beard","Barbe courte"});
            m_Datas.Add("body-MedLongBeard",new string[]{"Med Long Beard","Barbe plutot courte"});
            m_Datas.Add("body-Mustache",new string[]{"Mustache","Moustache"});
            m_Datas.Add("body-Nobeard",new string[]{"No beard","Imberbe"});
            
            //Animals
            m_Datas.Add("animal-chicken",new string[]{"a chicken","un poulet"});
            
            //Items
            m_Datas.Add("item-horsePole",new string[]{"Horse Pole","Poteau d'arnachement"});

            //Messages
            m_Datas.Add("message-cannotBeMovable",new string[]{"This thing cannot work if movable","N'est utilisable que lorsque fixe au sol"});
            m_Datas.Add("message-freedomOwnlerless",new string[]{"Freedom is back for this poor ownerless animal !","La monture s'en va d'elle meme"});
            m_Datas.Add("message-maxFollowers",new string[]{"Maximum followers reached !","Vous avez trop de suivant pour detacher cette creature."});
            m_Datas.Add("message-poleAlreadyUsed",new string[]{"{0} already use the pole !","Ce poteau est déjà utilisé par {0}"});
            m_Datas.Add("message-witchToAttach",new string[]{"Witch animal do you want to attach ?","Quel animal voulez vous attacher ?"});
            m_Datas.Add("message-alreadyAttached",new string[]{"This animal is already attached","Cette creature est déjà attachee !"});
            m_Datas.Add("message-notYourAnimal",new string[]{"This is not your animal !","Cette creature n'est pas sous votre controle !"});
            m_Datas.Add("message-notMountable",new string[]{"This is not a mountable creature !","Cette creature n'est pas une monture !"});

        }

        public static string Dialog(string name)
        {
            string key = "dialog-" + name;
            if (m_Datas.ContainsKey(key))
                return m_Datas[key][L];
            return name;
        }

        public static string Emote(string name)
        {
            string key = "dialog-" + name;
            if (m_Datas.ContainsKey(key))
                return "*" + m_Datas[key][L] + "*";
            return "*" + name + "*";
        }

        public static string Message(string name)
        {
            string key = "message-" + name;
            if (m_Datas.ContainsKey(key))
                return m_Datas[key][L];
            return name;
        }

        public static string Vendor(string name)
        {
            string key = "vendor-" + name;
            if (m_Datas.ContainsKey(key))
                return m_Datas[key][L];
            return name;
        }

        public static string Animal(string name)
        {
            string key = "animal-" + name;
            if (m_Datas.ContainsKey(key))
                return m_Datas[key][L];
            return name;
        }

        public static string Item(string name)
        {
            string key = "item-" + name;
            if (m_Datas.ContainsKey(key))
                return m_Datas[key][L];
            return name;
        }

        public static string Body(string name)
        {
            string key = "body-" + name;
            if (m_Datas.ContainsKey(key))
                return m_Datas[key][L];
            return name;
        }

        public static string Label(string name)
        {
            string key = "label-" + name;
            if (m_Datas.ContainsKey(key))
                return m_Datas[key][L];
            return name;
        }
    }
}
