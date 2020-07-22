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
using DevExpress.XtraReports.UI;

namespace HTN.BITS.UIL.PLASESS.PopupForms
{
    public partial class frmPrintMaterialCard : BaseDialogForm, IDisposable
    {
        public frmPrintMaterialCard()
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

        ~frmPrintMaterialCard()
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

        private string _ARRIVAL_NO;
        private string _ARRIVAL_TYPE;
        private DateTime _ARRIVAL_DATE;
        private string _MTL_SEQ_NO;
        private string _MTL_CODE;
        private string _MTL_NAME;
        private decimal _TOTAL_QTY;
        private int _SEQ_NO;

        private string _USER_ID;

        private string _MTL_TYPE;

        private List<T_ARRIVAL_DTL> _LIST_ARRIVAL_DTL = null;

        #endregion

        #region "Property Member"

        public string ARRIVAL_NO
        {
            get
            {
                return _ARRIVAL_NO;
            }
            set
            {
                if (_ARRIVAL_NO == value)
                    return;
                _ARRIVAL_NO = value;
            }
        }

        public string ARRIVAL_TYPE
        {
            get
            {
                return _ARRIVAL_TYPE;
            }
            set
            {
                if (_ARRIVAL_TYPE == value)
                    return;
                _ARRIVAL_TYPE = value;
            }
        }

        public DateTime ARRIVAL_DATE
        {
            get
            {
                return _ARRIVAL_DATE;
            }
            set
            {
                if (_ARRIVAL_DATE == value)
                    return;
                _ARRIVAL_DATE = value;
            }
        }

        public string MTL_SEQ_NO
        {
            get
            {
                return _MTL_SEQ_NO;
            }
            set
            {
                if (_MTL_SEQ_NO == value)
                    return;
                _MTL_SEQ_NO = value;
            }
        }
        public string MTL_CODE
        {
            get
            {
                return _MTL_CODE;
            }
            set
            {
                if (_MTL_CODE == value)
                    return;
                _MTL_CODE = value;
            }
        }
        public string MTL_NAME
        {
            get
            {
                return _MTL_NAME;
            }
            set
            {
                if (_MTL_NAME == value)
                    return;
                _MTL_NAME = value;
            }
        }
        public decimal TOTAL_QTY
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

        public string MTL_TYPE
        {
            get
            {
                return _MTL_TYPE;
            }
            set
            {
                if (_MTL_TYPE == value)
                    return;
                _MTL_TYPE = value;
            }
        }

        public List<T_ARRIVAL_DTL> LIST_ARRIVAL_DTL
        {
            get
            {
                return _LIST_ARRIVAL_DTL;
            }
            
            set
            {
                if (_LIST_ARRIVAL_DTL == value)
                    return;
                _LIST_ARRIVAL_DTL = value;
            }
        }


        #endregion


        private void BindingDataToTextField()
        {
            this.txtJOB_NO.Text = this.ARRIVAL_NO;
            this.txtArrival_type.Text = this.ARRIVAL_TYPE;
            this.txtArrval_date.EditValue = this.ARRIVAL_DATE;
            this.txtTOTAL_QTY.EditValue = this.TOTAL_QTY;
        }

        private void Get_ProductCard_List()
        {
            List<MaterialCard> lstPrdCard = null;
            try
            {
                base.BeginProcessing("Begin Load data...", "Please Waiting for Loading Data");

                using (ArrivalBLL jobOrdBll = new ArrivalBLL())
                {
                    lstPrdCard = jobOrdBll.GetSelectMtlCard(this.SEQ_NO);
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

        private void frmPrintProductCard_LoadCompleted()
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

        private void btnPrintCard_Click(object sender, EventArgs e)
        {
            List<MaterialCard> lstPrdCard = null;

            try
            {
                if (this.cardSelect.SelectedCount > 0)
                {
                    lstPrdCard = new List<MaterialCard>(this.cardSelect.SelectedCount);
                    for (int i = 0; i < this.cardSelect.SelectedCount; i++)
                    {
                        lstPrdCard.Add((MaterialCard)this.cardSelect.GetSelectedRow(i));
                    }

                    this.PrintProductCard_A4(this.LIST_ARRIVAL_DTL, this.MTL_SEQ_NO, lstPrdCard);

                }
                else
                {
                    XtraMessageBox.Show(this, "Please Select Material Card to Print", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void PrintProductCard_A4(List<T_ARRIVAL_DTL> jobOrdNo, string prodSEQNo, List<MaterialCard> lstPrdCard)
        {
            int printSeq = 0;
            try
            {
                base.BeginProcessing("Begin Load Report...", "Please Waiting for Loading Report");

                DataSet ds;

                using (ArrivalBLL jobOrdBll = new ArrivalBLL())
                {
                    ds = jobOrdBll.PrintProductCardReport(jobOrdNo, prodSEQNo, lstPrdCard, this.USER_ID, out printSeq);
                }

                ReportViewer viewer = new ReportViewer();
                viewer.AutoCloseAfterPrint = true;
                

                //RPT_PRODUCT_CARD rpt = new RPT_PRODUCT_CARD();
                XtraReport rpt = new RPT_MATERIAL_CARD_LABEL_3();
                //string fixRpt = HTN.BITS.UIL.PLASESS.Properties.Settings.Default.FixProductCardReport;//UiUtility.FixProductCardReport;
                //if (string.IsNullOrEmpty(fixRpt))
                //{
                //    //rpt = new RPT_MATERIAL_CARD();
                //}
                //else
                //{
                //    rpt = UiUtility.GetReportByName(fixRpt);
                //}

                if (rpt != null)
                {
                    rpt.DataSource = ds;
                    rpt.Parameters["paramUserPrint"].Value = this.USER_ID;
                    //rpt.Parameters["paramPRODUCTION_TYPE"].Value = this.MTL_TYPE;
                    rpt.CreateDocument();

                    viewer.SetReport(rpt);
                    viewer.LogPrintTime(printSeq);

                    base.FinishedProcessing();

                    viewer.ShowDialog();
                }
                else
                {
                    XtraMessageBox.Show("Report is null");
                }



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

        private void PrintProductCard_LABEL(List<T_ARRIVAL_DTL> jobOrdNo, string prodSEQNo, List<MaterialCard> lstPrdCard)
        {
            int printSeq = 0;
            try
            {
                base.BeginProcessing("Begin Load Report...", "Please Waiting for Loading Report");

                


                ReportViewer viewer = new ReportViewer();
                viewer.AutoCloseAfterPrint = true;

                XtraReport rpt = null;

                DataSet ds;
                using (ArrivalBLL jobOrdBll = new ArrivalBLL())
                {
                    ds = jobOrdBll.PrintProductCardReport(jobOrdNo, prodSEQNo, lstPrdCard, this.USER_ID, out printSeq);
                }

                rpt = new RPT_MATERIAL_CARD_LABEL_2();

             //MaterialCard objMTL = null;
             //objMTL = lstPrdCard.Find(x => (x.WH_ID == "LC00002"));//PRESS
             //if (objMTL != null)
             //{
             //    rpt = new RPT_MATERIAL_CARD_LABEL_PRESS();
             //    //change ds val

             //}
             //else
             //{
             //    rpt = new RPT_MATERIAL_CARD_LABEL_2();
             //    //rpt = new RPT_MATERIAL_CARD_LABEL();
         
             //}
                    
            //    }
          //      else
           //     {
          //          rpt = UiUtility.GetReportByName(fixRpt);
          //      }

                if (rpt != null)
                {
                    rpt.DataSource = ds;
                    rpt.Parameters["paramUserPrint"].Value = this.USER_ID;
                    rpt.CreateDocument();

                    viewer.SetReport(rpt);
                    viewer.LogPrintTime(printSeq);

                    base.FinishedProcessing();

                    viewer.ShowDialog();
                }
                else
                {
                    XtraMessageBox.Show("Report is null");
                }



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

        private void frmPrintProductCard_FormClosed(object sender, FormClosedEventArgs e)
        {
            //base.SaveGridLayout(this.Name, this.grdProductCard.Views[0]);
            this.Controls.Clear();
        }

        private void bbiPrintPCardA4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<MaterialCard> lstPrdCard = null;
            try
            {
                if (this.cardSelect.SelectedCount > 0)
                {
                    lstPrdCard = new List<MaterialCard>(this.cardSelect.SelectedCount);
                    for (int i = 0; i < this.cardSelect.SelectedCount; i++)
                    {
                        lstPrdCard.Add((MaterialCard)this.cardSelect.GetSelectedRow(i));
                    }

                    this.PrintProductCard_A4(this.LIST_ARRIVAL_DTL, this.MTL_SEQ_NO, lstPrdCard);

                }
                else
                {
                    XtraMessageBox.Show(this, "Please Select Material Card to Print", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void bbiPrintPCardLabel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<MaterialCard> lstPrdCard = null;
            try
            {
                if (this.cardSelect.SelectedCount > 0)
                {
                    lstPrdCard = new List<MaterialCard>(this.cardSelect.SelectedCount);
                    for (int i = 0; i < this.cardSelect.SelectedCount; i++)
                    {
                        lstPrdCard.Add((MaterialCard)this.cardSelect.GetSelectedRow(i));
                    }

                    this.PrintProductCard_LABEL(this.LIST_ARRIVAL_DTL, this.MTL_SEQ_NO, lstPrdCard);

                }
                else
                {
                    XtraMessageBox.Show(this, "Please Select Material Card to Print", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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