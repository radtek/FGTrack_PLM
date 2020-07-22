using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HTN.BITS.UIL.PLASESS.Component;
using HTN.BITS.UIL.PLASESS.Reports;
using HTN.BITS.QRCodeLib;
using HTN.BITS.BEL.PLASESS;
using HTN.BITS.BLL.PLASESS;
using HTN.BITS.UIL.PLASESS.Component.GridViewControl;

namespace HTN.BITS.UIL.PLASESS.PopupForms
{
    public partial class frmPrintRTProductCard : BaseDialogForm, IDisposable
    {
        public frmPrintRTProductCard()
        {
            InitializeComponent();

            //for set idle time
            base.DialogIdle.IdleTime = System.TimeSpan.Parse(UiUtility.DialogIdleTime);
            base.DialogIdle.IdleAsync += new EventHandler(this.DialogIdle_IdleAsync);

            base.LoadFormLayout();
            base.LoadGridLayout(this.grdProductCard);
            this.cardSelect = new GridCheckMarksSelection(this.grvProductCard);
            this.cardSelect.ClearSelection();
        }

        #region "Dialog Idle Time"

        ~frmPrintRTProductCard()
        {
            if (base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Stop();
            }
            base.DialogIdle.IdleAsync -= new EventHandler(this.DialogIdle_IdleAsync);
        }

        private void DialogIdle_IdleAsync(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(
                delegate() { this.DialogIdle_Idle(sender, e); })
                );
        }

        private void DialogIdle_Idle(object sender, EventArgs e)
        {
            if (base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Stop();
            }

            this.DialogResult = DialogResult.Cancel;
        }

        #endregion 

        #region Private Member

        private GridCheckMarksSelection cardSelect;

        private string _JOB_NO;
        private string _PROD_SEQ_NO;
        private string _PRODUCT_NO;
        private string _PRODUCT_NAME;
        private int _TOTAL_QTY;
        private int _SEQ_NO;

        private string _USER_ID;

        private string _PRODUCTION_TYPE;

        #endregion

        #region "Property Member"

        public string JOB_NO
        {
            get
            {
                return _JOB_NO;
            }
            set
            {
                if (_JOB_NO == value)
                    return;
                _JOB_NO = value;
            }
        }
        public string PROD_SEQ_NO
        {
            get
            {
                return _PROD_SEQ_NO;
            }
            set
            {
                if (_PROD_SEQ_NO == value)
                    return;
                _PROD_SEQ_NO = value;
            }
        }
        public string PRODUCT_NO
        {
            get
            {
                return _PRODUCT_NO;
            }
            set
            {
                if (_PRODUCT_NO == value)
                    return;
                _PRODUCT_NO = value;
            }
        }
        public string PRODUCT_NAME
        {
            get
            {
                return _PRODUCT_NAME;
            }
            set
            {
                if (_PRODUCT_NAME == value)
                    return;
                _PRODUCT_NAME = value;
            }
        }
        public int TOTAL_QTY
        {
            get
            {
                return _TOTAL_QTY;
            }
            set
            {
                if (_TOTAL_QTY == value)
                    return;
                _TOTAL_QTY = value;
            }
        }
        public int SEQ_NO
        {
            get
            {
                return _SEQ_NO;
            }
            set
            {
                if (_SEQ_NO == value)
                    return;
                _SEQ_NO = value;
            }
        }
        public string USER_ID
        {
            get
            {
                return _USER_ID;
            }
            set
            {
                if (_USER_ID == value)
                    return;
                _USER_ID = value;
            }
        }

        public string PRODUCTION_TYPE
        {
            get
            {
                return _PRODUCTION_TYPE;
            }
            set
            {
                if (_PRODUCTION_TYPE == value)
                    return;
                _PRODUCTION_TYPE = value;
            }
        }
        #endregion


        private void BindingDataToTextField()
        {
            this.txtJOB_NO.Text = this.JOB_NO;
            this.txtPRODUCT_NO.Text = this.PRODUCT_NO;
            this.txtPRODUCT_NAME.Text = this.PRODUCT_NAME;
            this.txtTOTAL_QTY.EditValue = this.TOTAL_QTY;
        }

        private void Get_ProductCard_List()
        {
            List<ProductCard> lstPrdCard = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (QCReturnBLL qcReturnBll = new QCReturnBLL())
                {
                    lstPrdCard = qcReturnBll.GetSelectProductCard(this.SEQ_NO);
                }

                this.grdProductCard.DataSource = lstPrdCard;
                this.dntProductCard.DataSource = lstPrdCard;

                //default check all
                this.cardSelect.SelectAll();
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                base.FinishedProcessing();
            }
        }

        private void frmPrintProductCard_Load(object sender, EventArgs e)
        {
            //this.BindingDataToTextField();
            //this.Get_ProductCard_List();

            //if (!base.DialogIdle.IsRunning)
            //{
            //    base.DialogIdle.Start();
            //}
        }

        private void frmPrintRTProductCard_LoadCompleted()
        {
            this.BindingDataToTextField();
            this.Get_ProductCard_List();

            if (!base.DialogIdle.IsRunning)
            {
                base.DialogIdle.Start();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void btnPrintCard_Click(object sender, EventArgs e)
        //{
        //    List<ProductCard> lstPrdCard = null;
        //    try
        //    {
        //        if (this.cardSelect.SelectedCount > 0)
        //        {
        //            lstPrdCard = new List<ProductCard>(this.cardSelect.SelectedCount);
        //            for (int i = 0; i < this.cardSelect.SelectedCount; i++)
        //            {
        //                lstPrdCard.Add((ProductCard)this.cardSelect.GetSelectedRow(i));
        //            }

        //            this.PrintProductCard(this.txtJOB_NO.Text, this.PROD_SEQ_NO, lstPrdCard);

        //        }
        //        else
        //        {
        //            XtraMessageBox.Show(this, "Please Select Product Card to Print", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //    }
        //}

        //private void PrintProductCard(string qcReturnNo, string prodSEQNo, List<ProductCard> lstPrdCard)
        //{
        //    int printSeq = 0;
        //    try
        //    {
        //        base.BeginProcessing("Begin Load Report...", "Please Waiting for Loading Report");

        //        DataSet ds;

        //        using (QCReturnBLL qcReturnBll = new QCReturnBLL())
        //        {
        //            ds = qcReturnBll.PrintProductCardReport(qcReturnNo, prodSEQNo, lstPrdCard, this.USER_ID, out printSeq);
        //        }

        //        ReportViewer viewer = new ReportViewer();
        //        viewer.AutoCloseAfterPrint = true;

        //        RPT_PRODUCT_CARD rpt = new RPT_PRODUCT_CARD();

        //        rpt.DataSource = ds;
        //        rpt.Parameters["paramUserPrint"].Value = this.USER_ID;
        //        rpt.Parameters["paramPRODUCTION_TYPE"].Value = this.PRODUCTION_TYPE;
        //        rpt.CreateDocument();

        //        viewer.SetReport(rpt);

        //        viewer.LogPrintTime(printSeq);
        //        base.FinishedProcessing();
        //        viewer.ShowDialog();

        //    }
        //    catch (Exception ex)
        //    {
        //        base.FinishedProcessing();

        //        XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        //    }
        //    finally
        //    {
        //        base.FinishedProcessing();
        //    }
        //}

        private void PrintProductCard_A4(string qcReturnNo, string prodSEQNo, List<ProductCard> lstPrdCard)
        {
            int printSeq = 0;
            try
            {
                base.BeginProcessing("Begin Load Report...", "Please Waiting for Loading Report");

                DataSet ds;

                using (QCReturnBLL qcReturnBll = new QCReturnBLL())
                {
                    ds = qcReturnBll.PrintProductCardReport(qcReturnNo, prodSEQNo, lstPrdCard, this.USER_ID, out printSeq);
                }

                ReportViewer viewer = new ReportViewer();
                viewer.AutoCloseAfterPrint = true;


                RPT_PRODUCT_CARD rpt = new RPT_PRODUCT_CARD();

                rpt.DataSource = ds;
                rpt.Parameters["paramUserPrint"].Value = this.USER_ID;
                rpt.Parameters["paramPRODUCTION_TYPE"].Value = this.PRODUCTION_TYPE;
                rpt.CreateDocument();

                viewer.SetReport(rpt);
                viewer.LogPrintTime(printSeq);

                base.FinishedProcessing();

                viewer.ShowDialog();
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();

                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                base.FinishedProcessing();
            }
        }

        private void PrintProductCard_LABEL(string qcReturnNo, string prodSEQNo, List<ProductCard> lstPrdCard)
        {
            int printSeq = 0;
            try
            {
                base.BeginProcessing("Begin Load Report...", "Please Waiting for Loading Report");

                DataSet ds;

                using (QCReturnBLL qcReturnBll = new QCReturnBLL())
                {
                    ds = qcReturnBll.PrintProductCardReport(qcReturnNo, prodSEQNo, lstPrdCard, this.USER_ID, out printSeq);
                }

                ReportViewer viewer = new ReportViewer();
                viewer.AutoCloseAfterPrint = true;


                RPT_PRODUCT_CARD_8545 rpt = new RPT_PRODUCT_CARD_8545();

                rpt.DataSource = ds;
                rpt.Parameters["paramUserPrint"].Value = this.USER_ID;
                rpt.Parameters["paramPRODUCTION_TYPE"].Value = this.PRODUCTION_TYPE;
                rpt.CreateDocument();

                viewer.SetReport(rpt);
                viewer.LogPrintTime(printSeq);

                base.FinishedProcessing();

                viewer.ShowDialog();
 
            }
            catch (Exception ex)
            {
                base.FinishedProcessing();

                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                base.FinishedProcessing();
            }
        }



        #region IDisposable Members

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #endregion

        private void frmPrintRTProductCard_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Controls.Clear();
        }

        private void bbiPrintPCardA4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<ProductCard> lstPrdCard = null;
            try
            {
                if (this.cardSelect.SelectedCount > 0)
                {
                    lstPrdCard = new List<ProductCard>(this.cardSelect.SelectedCount);
                    for (int i = 0; i < this.cardSelect.SelectedCount; i++)
                    {
                        lstPrdCard.Add((ProductCard)this.cardSelect.GetSelectedRow(i));
                    }

                    this.PrintProductCard_A4(this.txtJOB_NO.Text, this.PROD_SEQ_NO, lstPrdCard);

                }
                else
                {
                    XtraMessageBox.Show(this, "Please Select Product Card to Print", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void bbiPrintPCardLabel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<ProductCard> lstPrdCard = null;
            try
            {
                if (this.cardSelect.SelectedCount > 0)
                {
                    lstPrdCard = new List<ProductCard>(this.cardSelect.SelectedCount);
                    for (int i = 0; i < this.cardSelect.SelectedCount; i++)
                    {
                        lstPrdCard.Add((ProductCard)this.cardSelect.GetSelectedRow(i));
                    }

                    this.PrintProductCard_LABEL(this.txtJOB_NO.Text, this.PROD_SEQ_NO, lstPrdCard);

                }
                else
                {
                    XtraMessageBox.Show(this, "Please Select Product Card to Print", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void ddbPrint_Click(object sender, EventArgs e)
        {
            DropDownButton ddb2 = (DropDownButton)sender;
            ddb2.ShowDropDown();
        }


    }
}