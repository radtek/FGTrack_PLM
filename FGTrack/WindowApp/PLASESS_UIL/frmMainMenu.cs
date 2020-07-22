using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraNavBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System.Threading;
using System.IO;
using System.Diagnostics;
using Winforms.Components.ApplicationIdleData;
using Winforms.Components;
using System.Net;
using System.Xml;
using DevExpress.XtraBars;
using HTN.BITS.UIL.PLASESS.Component;
using HTN.BITS.BLL.PLASESS;
using HTN.BITS.BEL.PLASESS;



namespace HTN.BITS.UIL.PLASESS
{
    public partial class frmMainMenu : BaseForm, IDisposable
    {
        private string _userid;

        public string UserID
        {
            get
            {
                return this._userid;
            }

            set
            {
                this._userid = value;
            }
        }

        public int ParentForm_Width
        {
            get
            {
                return (this.Width - (this.navBarControl1.Width + 20));
            }
        }

        //test by jack
        private NotifyIconEx notifyIcon;
        private System.Threading.Timer processTimer;

        private Color warnColour = Color.Red;
        private Color normalColour = Color.FromKnownColor(KnownColor.Silver);
        private bool isCleared = false;


        //private int i = 0;

        public frmMainMenu()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();

            GC.ReRegisterForFinalize(this);

            base.LookAndFeel.UseDefaultLookAndFeel = false;
            base.LookAndFeel.SkinName = UiUtility.ApplicationStyle;

            this.InitializeNotification();
            this.CustomInitializeComponent();
            //this.BackgroundImageLayout = ImageLayout.Stretch;

            this.ResizeEnd += delegate { this.Refresh(); };

            //test by jack
            if (UiUtility.IsAppIdleTime)
            {
                //base.AppIdle.Started += new EventHandler(applicationIdle_Started);
                //base.AppIdle.TickAsync += new EventHandler<TickEventArgs>(applicationIdle_TickAsync);
                //base.AppIdle.Stopped += new EventHandler(applicationIdle_Stopped);
                //base.AppIdle.Activity += new EventHandler<ActivityEventArgs>(applicationIdle_Activity);
                //base.AppIdle.WarnAsync += new EventHandler(applicationIdle_WarnAsync);
                //base.AppIdle.IdleAsync += new EventHandler(applicationIdle_IdleAsync);
            }
            else
            {
                this.lblReminding.Text = string.Empty;
            }

            //this.InitializeAutoCheckVersion();
        }

        private void InitializeNotification()
        {
            this.notifyIcon = new NotifyIconEx();
            this.notifyIcon.Text = "FGTrack Running";
            this.notifyIcon.Icon = base.Language.GetIcon("FG_Tracking");
            this.notifyIcon.Visible = true;
            //notifyIcon.ContextMenu = NotifyContextMenu;
            this.notifyIcon.BalloonClick += new EventHandler(this.OnClickBalloon);

        }

        private void OnClickBalloon(object sender, EventArgs e)
        {
            if (sender == this.notifyIcon)
            {
                if (this.notifyIcon.BalloonTitle.Equals("FGTrack Detected New Version"))
                {
                    this.Close();
                }
            }
        }

        #region "Auto Check Update Version"

        /*
        private void InitializeAutoCheckVersion()
        {
            if (UiUtility.IsAutoCheckVersion)
            {
                TimerCallback timerDelegate = new TimerCallback(this.ProcessRoutine);
                AutoResetEvent autoEvent = new AutoResetEvent(false);

                if (this.processTimer == null)
                {
                    System.TimeSpan chkTime = System.TimeSpan.Parse(UiUtility.CheckVersionTime);
                    this.processTimer = new System.Threading.Timer(timerDelegate, autoEvent, chkTime, chkTime); //every 3 min
                }
                else
                {
                    this.processTimer.Change(15000, 180000);
                }
            }
        }

        private void ProcessRoutine(object data)
        {
            FindFuncLoc();
        }

        private void FindFuncLoc()
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    bool isClearGrid = false;
                    Version curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                    Version newVersion = UiUtility.CheckVersion(false, out isClearGrid);//this.CheckVersion();
                    //newVersion = new Version("1.0.3.0");

                    if (curVersion.CompareTo(newVersion) != 0 && this.notifyIcon != null)
                    {
                        this.notifyIcon.ShowBalloon("FGTrack Detected New Version", "Please logoff and login again.", NotifyIconEx.NotifyInfoFlags.Info, 5);
                    }
                }));

                return;

            }
        }
         * */
        
        #endregion "Auto Check Update Version"

        #region "Application Idle"

        //void applicationIdle_Started(object sender, EventArgs e)
        //{
        //    this.isCleared = false;
        //    UpdateTimeDisplays(normalColour);
        //}

        //void applicationIdle_TickAsync(object sender, TickEventArgs e)
        //{
        //    BeginInvoke(new MethodInvoker(
        //        delegate() { 
        //            applicationIdle_Tick(sender, e);

        //        })
        //        );
        //}

        //void applicationIdle_Tick(object sender, TickEventArgs e)
        //{
        //    ////this.barStaticItem2.Caption = string.Format("Remaining: {0}", base.AppIdle.TimeRemaining.ToString());
        //    lblReminding.Text = string.Format("Remaining: {0}", base.AppIdle.TimeRemaining.ToString());
        //    if (e.IsWarnPeriod)
        //    {
        //        UpdateTimeDisplays(warnColour);
        //    }
        //    else
        //    {
        //        UpdateTimeDisplays(normalColour);
        //    }

        //}

        //void applicationIdle_Stopped(object sender, EventArgs e)
        //{
        //    UpdateTimeDisplays(normalColour);
        //    //lblActivity.Text = string.Empty;
        //}

        //void applicationIdle_Activity(object sender, ActivityEventArgs e)
        //{
        //    //lblActivity.Text = e.Message.ToString();
        //    UpdateTimeDisplays(normalColour);
        //}

        //void applicationIdle_WarnAsync(object sender, EventArgs e)
        //{
        //    Console.Beep();
        //}

        //void applicationIdle_IdleAsync(object sender, EventArgs e)
        //{
        //    BeginInvoke(new MethodInvoker(
        //        delegate() { applicationIdle_Idle(sender, e); })
        //        );
        //}

        //void applicationIdle_Idle(object sender, EventArgs e)
        //{
        //    UiUtility.IsLogOut = true;
        //    this.Close();

        //    //this.UserID = string.Empty;

        //    //if (this.MdiChildren.Length > 0)
        //    //{
        //    //    foreach (Form openningForm in this.MdiChildren)
        //    //    {
        //    //        openningForm.Close();
        //    //    }

        //    //    this.Close();
        //    //}
        //    //else
        //    //{
        //    //    this.Close();
        //    //}
        //}

        //void UpdateTimeDisplays(Color remainingForeColor)
        //{
        //    lblReminding.Appearance.ForeColor = remainingForeColor;
        //    lblReminding.Text = string.Format("Remaining: {0}", base.AppIdle.TimeRemaining.ToString());
        //    //this.barStaticItem2.Appearance.ForeColor = remainingForeColor;
        //    //this.barStaticItem2.Caption = string.Format("Remaining: {0}", base.AppIdle.TimeRemaining.ToString());
        //}

        #endregion "Application Idle"


        public void OpenJobOrderForm()
        {
            try
            {
                HTN.BITS.UIL.PLASESS.Transaction.frmJobOrder frmOpen = UiUtility.GetFromByName("Transaction.frmJobOrder") as HTN.BITS.UIL.PLASESS.Transaction.frmJobOrder;
                if (frmOpen == null) return;

                bool isFound = false;
                Form[] openForms = this.MdiChildren;
                foreach (Form frm in openForms)
                {
                    if (frmOpen.Name == frm.Name)
                    {
                        isFound = true;
                        frmOpen = frm as HTN.BITS.UIL.PLASESS.Transaction.frmJobOrder;
                        frmOpen.Activate();
                        break;
                    }
                }

                if (!isFound)
                {
                    if (frmOpen.Tag != null)
                    {
                        frmOpen.WindowState = this.SetFormWindowsState(frmOpen.Tag.ToString());
                    }

                    frmOpen.MdiParent = this;
                    frmOpen.Show();
                    this.Activate();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error OpenJobOrderForm", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void CustomInitializeComponent()
        {
            this.BackgroundImage = base.Language.GetBitmap("BackgroundPLS");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            
        }


        public bool LockMenu
        {
            get
            {
                return !this.navBarControl1.Enabled;
            }

            set
            {
                this.navBarControl1.Enabled = !value;
                this.barMDIList.Enabled = !value;
            }
        }

        public void ReloadMenu()
        {
            this.InitializeMenu();
        }

        public DevExpress.XtraBars.BarStaticItem ExecuteTime
        {
            get
            {
                return this.barExecuteTime;
            }

            set
            {
                if (this.barExecuteTime == value)
                    return;
            	this.barExecuteTime = value;
            }
        }

        

        private void InitializeMenu()
        {
            using (AdministratorBLL adminBLL = new AdministratorBLL())
            {


                List<MenuAuthentication> menuList = adminBLL.GetMenuAuthentication(this.UserID);
                if (menuList == null) return;

                string newGroup = "";

                NavBarGroup menuGroup = new NavBarGroup();
                NavBarItem menuItem;
                Bitmap img = null;

                this.navBarControl1.BeginUpdate();
                this.navBarControl1.Groups.Clear();


                foreach (MenuAuthentication menu in menuList)
                {
                    if (newGroup != menu.ProgramType)
                    {
                        menuGroup = this.navBarControl1.Groups.Add();
                        //Create Group
                        menuGroup.Name = menu.ProgramType;
                        menuGroup.Caption = menu.ProgramTypeName;
                        menuGroup.Appearance.Font = UiUtility.MenuGroupFont();
                        menuGroup.GroupCaptionUseImage = NavBarImage.Small;

                        img = base.Language.GetBitmap(menu.ProgramTypeImage);
                        if (img != null)
                        {
                            menuGroup.SmallImage = img;
                        }
                        //menuGroup.GroupStyle = NavBarGroupStyle.LargeIconsText;
                        menuGroup.GroupStyle = NavBarGroupStyle.SmallIconsText;

                        newGroup = menu.ProgramType;
                    }

                    menuItem = new NavBarItem();
                    menuItem.Name = menu.ProgramID + "_" + menu.ProgramKey;
                    menuItem.Hint = menu.ProgramName;
                    menuItem.Caption = menu.ProgramName;
                    // menuItem.Appearance.Font = UiUtility.MenuItemFont();

                    img = base.Language.GetBitmap(menu.IconImage);
                    if (img != null)
                    {
                        menuItem.SmallImage = img;
                    }


                    menuItem.LinkClicked += new NavBarLinkEventHandler(this.MenuItem_LinkClicked);

                    menuGroup.ItemLinks.Add(menuItem);
                }


                menuGroup = this.navBarControl1.Groups.Add();
                //Create Group
                menuGroup.Name = "mnuLogOff";
                menuGroup.Caption = "LOG OFF";
                menuGroup.Appearance.Font = UiUtility.MenuGroupFont();
                menuGroup.GroupCaptionUseImage = NavBarImage.Small;

                img = base.Language.GetBitmap("LogOff");
                if (img != null)
                {
                    menuGroup.SmallImage = img;
                }

                menuGroup.GroupStyle = NavBarGroupStyle.Default;
                this.navBarControl1.EndUpdate();
            }



        }

        private void InitializeStatus()
        {
            this.barStaticItem1.Caption = "User ID: " + this.UserID;
            this.barStaticItem2.Caption = "Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //this.lblVersion.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
           // MessageBox.Show(((DevExpress.XtraNavBar.NavBarItem)sender).Name);
            ////MessageBox.Show("test");
            //Form1 f = new Form1();
            //f.MdiParent = this;
            //f.Show();

        }

        private void OpenFromByName(string formName)
        {
            try
            {
                Form frmOpen = UiUtility.GetFromByName(formName);
                if (frmOpen == null) return;

                bool isFound = false;
                Form[] openForms = this.MdiChildren;
                if (openForms.Length == 0)
                {
                    //No open form
                    //frmOpen.MdiParent = this;
                    //frmOpen.Show();

                    if (this.bgwProcessInform.IsBusy)
                    {
                        this.bgwProcessInform.CancelAsync();
                    }
                    else
                    {
                        this.bgwProcessInform.RunWorkerAsync(frmOpen);
                    }
                }
                else
                {
                    foreach (Form frm in openForms)
                    {
                        if (frmOpen.Name == frm.Name)
                        {
                            isFound = true;
                            frm.Activate();
                            break;
                        }
                    }

                    if (!isFound)
                    {
                        //frmOpen.MdiParent = this;
                        //frmOpen.Show();
                        if (this.bgwProcessInform.IsBusy)
                        {
                            this.bgwProcessInform.CancelAsync();
                        }
                        else
                        {
                            this.bgwProcessInform.RunWorkerAsync(frmOpen);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            //this.InitializeMenu();

            //string lastActiveGroup = HTN.BITS.UIL.PLASESS.Properties.Settings.Default.LastActiveGroup;
            //this.navBarControl1.ActiveGroup = this.navBarControl1.Groups[lastActiveGroup];

            //this.InitializeStatus();

            ////if (!base.AppIdle.IsRunning)
            ////{
            ////    base.AppIdle.Start();
            ////}

            //Cursor.Current = Cursors.Default;
        }

        private void frmMainMenu_LoadCompleted()
        {
            this.InitializeMenu();

            string lastActiveGroup = HTN.BITS.UIL.PLASESS.Properties.Settings.Default.LastActiveGroup;
            this.navBarControl1.ActiveGroup = this.navBarControl1.Groups[lastActiveGroup];

            this.InitializeStatus();
            Cursor.Current = Cursors.Default;
        }

        private void MenuItem_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            string formName = ((NavBarItem)sender).Name.Substring(5);
            this.OpenFromByName(formName);
            
        }

        private void bgwProcessInform_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i += 10)
            {
                this.bgwProcessInform.ReportProgress(i);
                Thread.Sleep(1);
            }

            e.Result = e.Argument;
        }

        private void bgwProcessInform_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBarControl1.EditValue = e.ProgressPercentage;
        }

        private void bgwProcessInform_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Cursor.Current = Cursors.Default;
                XtraMessageBox.Show("You Cancel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else if (e.Error != null)
            {
                Cursor.Current = Cursors.Default;
                XtraMessageBox.Show("Worker exception: " + e.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                Cursor.Current = Cursors.Default;
                this.progressBarControl1.EditValue = 0;

                BaseForm form = (BaseForm)e.Result;
                //BaseForm form = (BaseForm)e.Result;
           
                if (form.Tag != null)
                {
                    form.WindowState = this.SetFormWindowsState(form.Tag.ToString());
                }

                form.MdiParent = this;
                form.Show();
                this.Activate();
            }

        }

        private FormWindowState SetFormWindowsState(string tag)
        {
            switch (tag)
            {
                case "FullMode":
                    return FormWindowState.Maximized;
                case "NormalMode":
                    return FormWindowState.Normal;
                default:
                    return FormWindowState.Normal;
            }
        }
    

        private void frmMainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (base.AppIdle.IsRunning)
            //{
            //    base.AppIdle.Stop();
            //}

            //if (this.processTimer != null)
            //{
            //    this.processTimer.Dispose();
            //}

            if (this.notifyIcon != null)
            {
                this.notifyIcon.Remove();
            }
        }

        private void frmMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (base.AppIdle.IsRunning)
            //{
            if (this.MdiChildren.Length > 0)
            {
                DialogResult isConfirm = XtraMessageBox.Show("Some form opening do you want to Exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (isConfirm == DialogResult.Yes)
                {
                    foreach (BaseForm openningForm in this.MdiChildren)
                    {
                        //this.MdiChildren[0].Close();
                        openningForm.SaveGridControlBeforeClose();
                        openningForm.Hide();
                        openningForm.Parent = null;

                        openningForm.Close();
                        
                        
                    }

                    HTN.BITS.UIL.PLASESS.Properties.Settings.Default.Save();
                    this.Controls.Clear();
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                HTN.BITS.UIL.PLASESS.Properties.Settings.Default.Save();
                this.Controls.Clear();
                e.Cancel = false;
            }
            //}
            //else
            //{
            //    if (this.MdiChildren.Length > 0)
            //    {
            //        foreach (BaseForm openningForm in this.MdiChildren)
            //        {
            //            openningForm.SaveGridControlBeforeClose();
            //            openningForm.Hide();
            //            openningForm.Parent = null;

            //            openningForm.Close();
            //        }
            //    }

            //    HTN.BITS.UIL.PLASESS.Properties.Settings.Default.Save();
            //    this.Controls.Clear();
            //    e.Cancel = false;
            //}
        }

        private void navBarControl1_Click(object sender, EventArgs e)
        {
            NavBarControl navMainMenu = (NavBarControl)sender;
            Point point = navMainMenu.PointToClient(Control.MousePosition);
            NavBarHitInfo info = navMainMenu.CalcHitInfo(point);

            if (info.NavBar.PressedGroup != null)
            {
                NavBarGroup navGroup = info.NavBar.PressedGroup;
                if (navGroup.Name.Equals("mnuLogOff"))
                {
                    UiUtility.IsLogOut = true;
                    
                    this.Close();
                    //this.frmMainMenu_FormClosed(this, new FormClosedEventArgs(CloseReason.FormOwnerClosing));
                    //this.frmMainMenu_FormClosing(this, new FormClosingEventArgs(CloseReason.FormOwnerClosing, false));
                }
                else
                {
                    HTN.BITS.UIL.PLASESS.Properties.Settings.Default.LastActiveGroup = navGroup.Name;
                }
            }
        }

        private void Logoff()
        {
            //if (base.AppIdle.IsRunning)
            //{
            //    base.AppIdle.Stop();
            //}

            this.Controls.Clear();
            this.Close();

            //
            //Application.ExitThread();

            if (this.processTimer != null)
            {
                this.processTimer.Dispose();
            }

            if (this.notifyIcon != null)
            {
                this.notifyIcon.Remove();
            }

            Application.Exit();
            //System.Diagnostics.Process.Start(Application.ExecutablePath); 



            //Thread.Sleep(1000);



            //ThreadStart operation = new ThreadStart(Logoff_Thread);
            //Thread thr = new Thread(operation);
            //thr.Start();
        }

        private void Logoff_Thread()
        {
            //Application.Restart();
            //string appNewStart = string.Format("{0}\\{1}", Application.StartupPath, UiUtility.ApplicationName);

            //Process oldProc = Process.GetCurrentProcess();
            //oldProc.CloseMainWindow();
            //Application.Exit();

            //Thread.Sleep(100);

            //Process proc = new Process();
            //proc.StartInfo.FileName = appNewStart;
            //proc.StartInfo.Arguments = "";
            //proc.Start();

            
        }

        private void navBarControl1_Resize(object sender, EventArgs e)
        {
            this.CustomInitializeComponent();
        }


        #region IDisposable Members

        void IDisposable.Dispose()
        {
            //if (base.AppIdle.IsRunning)
            //{
            //    base.AppIdle.Stop();
            //}

            if (this.processTimer != null)
            {
                this.processTimer.Dispose();
            }

            if (this.notifyIcon != null)
            {
                this.notifyIcon.Remove();
            }

            GC.SuppressFinalize(this);
        }

        #endregion

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                using (frmUserInfo fUserInfo = new frmUserInfo())
                {
                    fUserInfo.USER_ID = this._userid;
                    fUserInfo.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }


    }
}
