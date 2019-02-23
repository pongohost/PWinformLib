using System;
using System.Drawing;

namespace PWinformLib.UI
{
    public class notification
    {
        static PopupNotifier[] soc = InitializeArray<PopupNotifier>(5);
        static int i = 1;
        static int[] ii = new int[5] { 0, 0, 0, 0, 0 };

        /*public notification(int Delay)
        {

        }*/

        public static void Info(String Title, String Message)
        {
            i = cek();
            soc[i].TitleText = Title;
            soc[i].ContentText = Message;
            soc[i].AnimationInterval = 10;
            soc[i].ShowOptionsButton = false;
            soc[i].ShowGrip = false;
            soc[i].ObjectNumber = i;
            soc[i].BodyColor = Color.PowderBlue;
            soc[i].HeaderColor = Color.PowderBlue;
            soc[i].Image = Properties.Resources.info;
            soc[i].ImageSize = new Size(75, 75);
            soc[i].Popup();
            initNotif();
        }

        public static void Error(String Title, String Message)
        {
            i = cek();
            soc[i].TitleText = Title;
            soc[i].ContentText = Message;
            soc[i].AnimationInterval = 10;
            soc[i].ShowOptionsButton = false;
            soc[i].ShowGrip = false;
            soc[i].ObjectNumber = i;
            soc[i].BodyColor = Color.MistyRose;
            soc[i].HeaderColor = Color.MistyRose;
            soc[i].Image = Properties.Resources.error;
            soc[i].ImageSize = new Size(75, 75);
            soc[i].Popup();
            initNotif();
        }

        public static void Ok(String Title, String Message)
        {
            i = cek();
            soc[i].TitleText = Title;
            soc[i].ContentText = Message;
            soc[i].AnimationInterval = 10;
            soc[i].ShowOptionsButton = false;
            soc[i].ShowGrip = false;
            soc[i].ObjectNumber = i;
            soc[i].BodyColor = Color.LightCyan;
            soc[i].HeaderColor = Color.LightCyan;
            soc[i].Image = Properties.Resources.ok;
            soc[i].ImageSize = new Size(75, 75);
            soc[i].Popup();
            initNotif();
        }

        public static void Warn(String Title, String Message)
        {
            i = cek();
            soc[i].TitleText = Title;
            soc[i].ContentText = Message;
            soc[i].AnimationInterval = 10;
            soc[i].ShowOptionsButton = false;
            soc[i].ShowGrip = false;
            soc[i].ObjectNumber = i;
            soc[i].BodyColor = Color.LemonChiffon;
            soc[i].HeaderColor = Color.LemonChiffon;
            soc[i].Image = Properties.Resources.warning;
            soc[i].ImageSize = new Size(75, 75);
            soc[i].Popup();
            initNotif();
        }

        private static void initNotif()
        {
            if (i == 1) soc[i].Disappear += new EventHandler((sender, e) => hapusarray(sender, e, 1));
            if (i == 2) soc[i].Disappear += new EventHandler((sender, e) => hapusarray(sender, e, 2));
            if (i == 3) soc[i].Disappear += new EventHandler((sender, e) => hapusarray(sender, e, 3));
            if (i == 4) soc[i].Disappear += new EventHandler((sender, e) => hapusarray(sender, e, 4));
            if (i == 5) soc[i].Disappear += new EventHandler((sender, e) => hapusarray(sender, e, 5));
        }

        private static void hapusarray(object sender, EventArgs e, int indeks)
        {
            ii[indeks] = 0;
        }

        private static int cek()
        {
            int xx = 1;
            for (int x = 1; x < 5; x++)
            {
                if (ii[x] == 0)
                {
                    xx = x;
                    ii[x] = 1;
                    break;
                }
                System.Diagnostics.Debug.WriteLine(x + " x " + xx);
            }
            return xx;
        }

        private static T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
            {
                array[i] = new T();
            }

            return array;
        }
    }
}
