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
        private static readonly ServerLanguage LANG = ServerLanguage.EN;
        private static int L { get { return (int)LANG; } }
        private static readonly Dictionary<string, string[]> m_Datas = new Dictionary<string, string[]>()
        {
            //Vendors
            {"vendor-barberM",new string[]{"the barber","le coiffeur"}},
            {"vendor-barberF",new string[]{"the barber","la coiffeuse"}},
            
            // Dialogs
            {"dialog-comeCloser",new string[]{"Please come closer !","Approchez vous un peu !"}},
            {"dialog-whatDoYouWant",new string[]{"What do you want today ?","Que voulez vous aujourd'hui ?"}},
            {"dialog-notForPoor",new string[]{"I guess this is not for poor people !!","Visiblement je suis trop pauvre !"}},
            {"dialog-thanksBuddy",new string[]{"Thanks Buddy !","Merci mon brave !"}},
            
            // Emotes
            {"emote-detachMount",new string[]{"detach his mount","detache sa monture"}},
            {"emote-attachMount",new string[]{"attach his mount","attache sa monture au poteau"}},
            {"emote-attachMyself",new string[]{"tried to attach himself to the pole","a essaye de s'attacher au poteau"}},
            {"emote-attachHuman",new string[]{"tried but failed to attach {0} to the pole","a essaye d'attacher {0}, mais n'a pas reussi"}},
            
            // Labels
            {"label-cancel",new string[]{"Cancel","Annuler"}},
            {"label-menu",new string[]{"Menu","Menu"}},
            {"label-makeChoice",new string[]{"Make a choice :","Faites votre choix"}},
            {"label-newHairCut",new string[]{"New HairCut ({0} GP)","Nouveaux cheveux ({0} PO)"}},
            {"label-newBeardCut",new string[]{"New BeardCut ({0} GP)","Nouvelle barbe ({0} PO)"}},
            {"label-hairCut",new string[]{"Hair Cut","Coupe de cheveux"}},
            {"label-beardCut",new string[]{"Beard Cut","Barbe et moustache"}},

            // Body
            {"body-ShortHair",new string[]{"Short Hair","Cheveux court"}},
            {"body-LongHair",new string[]{"Long Hair","Cheveux long"}},
            {"body-Ponytail",new string[]{"Ponytail","Queue de cheval"}},
            {"body-Mohawk",new string[]{"Mohawk","Mohawk"}},
            {"body-Pageboy",new string[]{"Pageboy","Pageboy"}},
            {"body-Receding",new string[]{"Receding","Receding"}},
            {"body-2tails",new string[]{"2-tails","2 Lulus"}},
            {"body-Topknot",new string[]{"Topknot","Topknot"}},
            {"body-Afro",new string[]{"Afro","Afro"}},
            {"body-Bald",new string[]{"Bald","Chauve"}},
            {"body-Goatee",new string[]{"Goatee","Pinch"}},
            {"body-MedShortBeard",new string[]{"Med Short Beard","Barbe plutot longue"}},
            {"body-Vandyke",new string[]{"Vandyke","Vandyke"}},
            {"body-LongBeard",new string[]{"Long Beard","Barbe longue"}},
            {"body-ShortBeard",new string[]{"Short Beard","Barbe courte"}},
            {"body-MedLongBeard",new string[]{"Med Long Beard","Barbe plutot courte"}},
            {"body-Mustache",new string[]{"Mustache","Moustache"}},
            {"body-Nobeard",new string[]{"No beard","Imberbe"}},
            
            //Animals
            {"animal-chicken",new string[]{"a chicken","un poulet"}},
            
            //Corpses
            {"corpse-chicken",new string[]{"a chicken corpse","un cadavre de poulet"}},
            
            //Items
            {"item-horsePole",new string[]{"Horse Pole","Poteau d'arnachement"}},

            //Messages
            {"message-cannotBeMovable",new string[]{"This thing cannot work if movable","N'est utilisable que lorsque fixe au sol"}},
            {"message-freedomOwnlerless",new string[]{"Freedom is back for this poor ownerless animal !","La monture s'en va d'elle meme"}},
            {"message-maxFollowers",new string[]{"Maximum followers reached !","Vous avez trop de suivant pour detacher cette creature."}},
            {"message-poleAlreadyUsed",new string[]{"{0} already use the pole !","Ce poteau est déjà utilisé par {0}"}},
            {"message-witchToAttach",new string[]{"Witch animal do you want to attach ?","Quel animal voulez vous attacher ?"}},
            {"message-alreadyAttached",new string[]{"This animal is already attached","Cette creature est déjà attachee !"}},
            {"message-notYourAnimal",new string[]{"This is not your animal !","Cette creature n'est pas sous votre controle !"}},
            {"message-notMountable",new string[]{"This is not a mountable creature !","Cette creature n'est pas une monture !"}},

        };

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

        public static string Corpse(string name)
        {
            string key = "corpse-" + name;
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
