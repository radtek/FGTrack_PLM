using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Globalization;
using System.Drawing;


namespace HTN.BITS.LIB
{
    public class LanguageRes
    {

        protected ResourceManager resourceManager;

        public LanguageRes()
        {
            this.InitializeResources();
        }

        public LanguageRes(string languageSelect)
        {
            this.InitializeResources();
            this.ChangeLanguage(languageSelect);
        }

        public void InitializeResources()
        {
            Assembly objAssembly = Assembly.GetExecutingAssembly();
            resourceManager = new ResourceManager("HTN.BITS.LIB.App_GlobalResources.LanguageResource", objAssembly);
            resourceManager.IgnoreCase = true;

        }


        public void ChangeLanguage(string languageSelect)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageSelect);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageSelect);
            }
            catch (Exception ex)
            {
                string strError = ex.ToString();
                throw;

            }
        }

        public string GetValue(string key)
        {
            String sCLange = "";
            try
            {
                sCLange = resourceManager.GetString(key);

                if (sCLange == null)
                    return key;
                else
                    return sCLange;
            }
            catch (Exception ex)
            {
                string errorstring = ex.ToString();
                return String.Format("[?:{0}]", key);
            }
        }

        public Bitmap GetBitmap(string key)
        {
            Bitmap image = null;
            try
            {
                image = (Bitmap)resourceManager.GetObject(key);

                return image;
            }
            catch (Exception ex)
            {
                string errorstring = ex.ToString();
                return null;
            }
        }

        public Icon GetIcon(string key)
        {
            Icon icon = null;
            try
            {
                icon = (Icon)resourceManager.GetObject(key);

                return icon;
            }
            catch (Exception ex)
            {
                string errorstring = ex.ToString();
                return null;
            }
        }
    }
}
