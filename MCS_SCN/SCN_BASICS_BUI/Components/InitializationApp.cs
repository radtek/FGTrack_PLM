using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Reflection;
using HTN.BITS.MCS.SCN.LIB.Scanner;
using System.Windows.Forms;

namespace HTN.BITS.MCS.SCN.UIL.Components
{
    public class InitializationApp
    {
        public void Initialize(SplashInitForm splash)
        {
            //Show the splash
            splash.ShowProgress(10);
            //... intialization first steps
            Thread.Sleep(200);
            //... update splash
            splash.ShowProgress(20);
            //... some intialization steps
            Thread.Sleep(200);
            //... update splash
            splash.ShowProgress(30);
            //... some intialization steps
            Thread.Sleep(200);
            //... update splash
            splash.ShowProgress(40);
            //... some intialization steps
            Thread.Sleep(200);
            //... update splash
            splash.ShowProgress(50);
            //... some intialization steps
            Thread.Sleep(200);
            //... update splash
            splash.ShowProgress(60);
            //... some intialization steps
            Thread.Sleep(200);
            //... update splash
            splash.ShowProgress(70);
            //... some intialization steps
            Thread.Sleep(200);
            //... update splash
            splash.ShowProgress(80);
            //... some intialization steps
            Thread.Sleep(200);
            //... update splash
            splash.ShowProgress(90);
            Thread.Sleep(200);
            //... update splash
            splash.ShowProgress(100);
            Thread.Sleep(300);

            
        }
    }
}
