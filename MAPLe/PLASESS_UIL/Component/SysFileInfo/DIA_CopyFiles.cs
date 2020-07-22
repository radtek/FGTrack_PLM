using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HTN.BITS.UIL.PLASESS.Component.SysFileInfo
{
    public partial class DIA_CopyFiles : BaseForm, ICopyFilesDiag
    {
        // Properties
        public System.ComponentModel.ISynchronizeInvoke SynchronizationObject { get; set; }

        public DIA_CopyFiles()
        {
            InitializeComponent();
        }

        // Methods
        public void update(Int32 totalFiles, Int32 copiedFiles, Int64 totalBytes, Int64 copiedBytes, String currentFilename)
        {
            Prog_TotalFiles.Properties.Maximum = totalFiles;
            Prog_TotalFiles.EditValue = copiedFiles;

            Prog_CurrentFile.Properties.Maximum = 100;
            if (totalBytes != 0)
            {
                Prog_CurrentFile.EditValue = Convert.ToInt32((100f / (totalBytes / 1024f)) * (copiedBytes / 1024f));
            }

            Lab_TotalFiles.Text = "Total files (" + copiedFiles + "/" + totalFiles + ")";
            Lab_CurrentFile.Text = currentFilename;
        }

        private void DIA_CopyFiles_Closed(object sender, System.EventArgs e)
        {
            RaiseCancel();
        }
        private void RaiseCancel()
        {
            if (EN_cancelCopy != null)
            {
                EN_cancelCopy();
            }
        }

        //Events
        public event CopyFiles.DEL_cancelCopy EN_cancelCopy;

        private void btnCancelCopy_Click(object sender, EventArgs e)
        {
            RaiseCancel();
        }
    }
}