﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PWinformLib.UI
{
    public static class CustomFont
    {
        //not clear if this is needed - there are reports that AddMemoryFont()
        // may not make the font available for all types of controls.
        //[DllImport("gdi32.dll")]
        //private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        #region Properties

        /// <summary>Gets or sets the families.</summary>
        private static Dictionary<string, PrivateFontCollection> Families
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        /// <summary>Creates the static instance of the <see cref="FontManager"/> class.</summary>
        static CustomFont()
        {
            CustomFont.Families = new Dictionary<string, PrivateFontCollection>();
        }

        #endregion

        #region Public Methods

        /// <summary>Used to add the font family.</summary>
        /// <param name="name">The name of the family.</param>
        /// <param name="data">The data.</param>
        public static void Add(string name, byte[] data)
        {
            //Convert the data into a pointer.
            IntPtr ptData = IntPtr.Zero;

            try
            {
                //Allocate the memory.
                ptData = Marshal.AllocCoTaskMem(data.Length);

                //Copy the byte data to the memory.
                Marshal.Copy(data, 0, ptData, data.Length);

                //Add font.
                if (!CustomFont.Families.ContainsKey(name))
                    CustomFont.Families.Add(name, new PrivateFontCollection());

                CustomFont.Families[name].AddMemoryFont(ptData, data.Length);
                //not clear if this is needed - there are reports that AddMemoryFont()
                // may not make the font available for all types of controls.
                //uint dummy = 0;
                //AddFontMemResourceEx(ptData, (uint)data.Length, IntPtr.Zero, ref dummy);
            }
            finally
            {
                Marshal.FreeCoTaskMem(ptData);
            }
        }

        /// <summary>True if a font has been loaded with the name passed.</summary>
        /// <param name="name">The name of the font.</param>
        /// <returns>True if the font exists.</returns>
        public static bool Contains(string name)
        {
            return CustomFont.Families.ContainsKey(name);
        }

        /// <summary>Used to dispose of all the loaded fonts.</summary>
        public static void Dispose()
        {
            foreach (KeyValuePair<string, PrivateFontCollection> keyValue in CustomFont.Families)
                keyValue.Value.Dispose();
            CustomFont.Families.Clear();
        }

        /// <summary>Gets the font which matching name in the size and style passed.</summary>
        /// <param name="name">The name of the font.</param>
        /// <param name="size">The size of the font required.</param>
        /// <param name="style">The style of the font required.</param>
        /// <returns>The font created, or null.</returns>
        public static Font GetFont(string name, float size, FontStyle style)
        {
            if (CustomFont.Families.ContainsKey(name))
            {
                foreach (FontFamily family in CustomFont.Families[name].Families)
                {
                    if (family.IsStyleAvailable(style))
                        return new Font(family, size, style);
                }
            }

            return null;
        }

        #endregion
    }
}
