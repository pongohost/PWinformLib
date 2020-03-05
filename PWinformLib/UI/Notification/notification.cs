using System;
using System.Collections;
using System.Drawing;

namespace PWinformLib.UI
{
    public class notification
    {
        static ArrayList listMsg = new ArrayList();
        static PopupNotifier[] soc = InitializeArray<PopupNotifier>(5);
        static int i = 1;
        static int[] ii = new int[5] { 1, 0, 0, 0, 0 };

        public static void Info(string Title, string Message, bool Autohide = false)
        {
            i = cek();
            if (i < 4 && i != 0)
            {
                CustomFont.Add("Helve", Properties.Resources.HelveticaNeueLight);
                soc[i] = new PopupNotifier()
                {
                    TitleText = Title,
                    ContentText = Message,
                    AnimationInterval = 10,
                    AutoHide = Autohide,
                    ShowOptionsButton = false,
                    ShowGrip = false,
                    ObjectNumber = i,
                    BodyColor = Color.FromArgb(24, 131, 233),
                    HeaderColor = Color.FromArgb(24, 131, 233),
                    Image = Properties.Resources.info,
                    LastPos = (i > 0) ? soc[i - 1].LastPos : 0,
                    ImageSize = new Size(40, 40)
                };
                soc[i].Popup();
                initNotif();
            }
            else
            {
                AntriPesan("Info", Title, Message);
            }
        }

        public static void Error(string Title, string Message, bool Autohide = false)
        {
            i = cek();
            Console.Out.WriteLine(i);
            if (i < 4 && i != 0)
            {
                CustomFont.Add("Helve", Properties.Resources.HelveticaNeueLight);
                soc[i] = new PopupNotifier()
                {
                    TitleText = Title,
                    ContentText = Message,
                    ContentFont = CustomFont.GetFont("Helve", 10, FontStyle.Regular),
                    TitleFont = CustomFont.GetFont("Helve", 12, FontStyle.Regular),
                    AnimationInterval = 10,
                    AutoHide = Autohide,
                    ShowOptionsButton = false,
                    ShowGrip = false,
                    ObjectNumber = i,
                    BodyColor = Color.FromArgb(218, 37, 28),
                    HeaderColor = Color.FromArgb(218, 37, 28),
                    Image = Properties.Resources.error,
                    LastPos = (i > 0) ? soc[i - 1].LastPos : 0,
                    ImageSize = new Size(40, 40)
                };
                soc[i].Popup();
                initNotif();
            }
            else
            {
                AntriPesan("Error", Title, Message);
            }
        }

        public static void Ok(string Title, string Message, bool Autohide = false)
        {

            i = cek();
            Console.Out.WriteLine(i);
            if (i < 4 && i != 0)
            {
                CustomFont.Add("Helve", Properties.Resources.HelveticaNeueLight);
                soc[i] = new PopupNotifier()
                {
                    TitleText = Title,
                    ContentText = Message,
                    ContentFont = CustomFont.GetFont("Helve", 10, FontStyle.Regular),
                    TitleFont = CustomFont.GetFont("Helve", 12, FontStyle.Regular),
                    AnimationInterval = 10,
                    AutoHide = Autohide,
                    ShowOptionsButton = false,
                    ShowGrip = false,
                    ObjectNumber = i,
                    BodyColor = Color.FromArgb(2, 163, 56),
                    HeaderColor = Color.FromArgb(2, 163, 56),
                    Image = Properties.Resources.ok,
                    LastPos = (i > 0) ? soc[i - 1].LastPos : 0,
                    ImageSize = new Size(40, 40)
                };
                soc[i].Popup();
                initNotif();
            }
            else
            {
                AntriPesan("Ok", Title, Message);
            }
        }

        public static void Warn(string Title, string Message, bool Autohide = false)
        {
            i = cek();
            Console.Out.WriteLine(i);
            if (i < 4 && i != 0)
            {
                CustomFont.Add("Helve", Properties.Resources.HelveticaNeueLight);
                soc[i] = new PopupNotifier()
                {
                    TitleText = Title,
                    ContentText = Message,
                    AnimationInterval = 10,
                    AutoHide = Autohide,
                    ShowOptionsButton = false,
                    ShowGrip = false,
                    ObjectNumber = i,
                    BodyColor = Color.FromArgb(244, 101, 36),
                    HeaderColor = Color.FromArgb(244, 101, 36),
                    Image = Properties.Resources.warning,
                    LastPos = (i > 0) ? soc[i - 1].LastPos : 0,
                    ImageSize = new Size(40, 40)
                };
                soc[i].Popup();
                initNotif();
            }
            else
            {
                AntriPesan("Warn", Title, Message);
            }
        }

        private static void initNotif()
        {
            soc[i].Disappear += new EventHandler((sender, e) => hapusarray(sender, e));
        }

        private static void hapusarray(object sender, EventArgs e)
        {
            int indeks = ((PopupNotifier)sender).ObjectNumber;
            soc[indeks].Dispose();
            ii[indeks] = 0;
            if (listMsg.Count > 0)
            {
                pesan nilai = (pesan)listMsg[0];
                if (nilai.jenis.Equals("Error"))
                    Error(nilai.judul, nilai.isi);
                if (nilai.jenis.Equals("Ok"))
                    Ok(nilai.judul, nilai.isi);
                if (nilai.jenis.Equals("Warn"))
                    Warn(nilai.judul, nilai.isi);
                if (nilai.jenis.Equals("Info"))
                    Info(nilai.judul, nilai.isi);
                listMsg.RemoveAt(0);
            }
        }

        private static void AntriPesan(String jenis, String Title, String Message)
        {
            pesan msgPesan = new pesan();
            msgPesan.jenis = jenis;
            msgPesan.judul = Title;
            msgPesan.isi = Message;
            listMsg.Add(msgPesan);
        }

        private static int cek()
        {
            int xx = 0;
            for (int x = 1; x < 5; x++)
            {
                if (ii[x] == 0)
                {
                    xx = x;
                    ii[x] = 1;
                    break;
                }
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

        class pesan
        {
            public String jenis { get; set; }
            public String judul { get; set; }
            public String isi { get; set; }
        }
    }
}
