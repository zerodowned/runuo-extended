using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
namespace Server.Gumps
{
    public class SaveGump : Gump
    {
        public SaveGump()
            : base(0, 0)
        {
            this.Closable = false;
            this.Disposable = false;
            this.Dragable = false;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(177, 234, 222, 96, 9200);
            this.AddAlphaRegion(204, 288, 160, 23);
            this.AddImage(192, 288, 11320);
            this.AddImage(352, 288, 11320);
            this.AddImage(357, 231, 10502);
            this.AddImage(194, 231, 10500);
            this.AddImageTiled(210, 231, 149, 37, 10501);
            this.AddLabel(220, 265, 33, @"World saving !");
            this.AddImageTiled(258, 171, 58, 59, 5608);
            this.AddLabel(220, 291, 600, @"...  Please Wait  ....");
            this.AddImage(362, 177, 10410);
            this.AddImage(126, 176, 10400);
            this.AddImage(166, 321, 10420);
            this.AddImage(344, 321, 10430);
            this.AddImageTiled(220, 331, 131, 18, 10304);
        }


    }
}
