using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace ISM.Touch
{
    public partial class frmCombo : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        LookUpEdit editcontrol;
        #endregion
        #region[Constructure]
        public frmCombo(Control control)
        {
            try
            {
                InitializeComponent();
                int height = 10;
                int width=310;

                editcontrol = (LookUpEdit)control;
                string valuemember = editcontrol.Properties.ValueMember;
                string displaymember = editcontrol.Properties.DisplayMember;

                if (editcontrol.Properties.DataSource is DataTable)
                {
                    DataTable dt = (DataTable)editcontrol.Properties.DataSource;
                    if (dt != null)
                    {
                        RadioGroupItem rgi = new RadioGroupItem();
                        rgi.Value = null;
                        rgi.Description = "Empty";
                        radioGroup1.Properties.Items.Add(rgi);
                        foreach (DataRow dr in dt.Rows)
                        {
                            rgi = new RadioGroupItem();
                            rgi.Value = dr[valuemember];
                            rgi.Description = Convert.ToString(dr[displaymember]);
                            radioGroup1.Properties.Items.Add(rgi);
                            if (height <= 330)
                                height = height + 40;
                            else if (width <= 710)
                            {
                                width = width + 220;
                            }
                        }
                        this.Size = new Size(width, height);
                    }
                }
                else if (editcontrol.Properties.DataSource is DataView)
                {
                    DataView dt = (DataView)editcontrol.Properties.DataSource;
                    if (dt != null)
                    {
                        RadioGroupItem rgi = new RadioGroupItem();
                        rgi.Value = null;
                        rgi.Description = "Empty";
                        radioGroup1.Properties.Items.Add(rgi);
                        foreach (DataRowView dr in dt)
                        {
                            rgi = new RadioGroupItem();
                            rgi.Value = dr[valuemember];
                            rgi.Description = Convert.ToString(dr[displaymember]);
                            radioGroup1.Properties.Items.Add(rgi);
                            if (height <= 330)
                                height = height + 40;
                            else if (width <= 710)
                            {
                                width = width + 220;
                            }
                        }
                        this.Size = new Size(width, height);
                    }
                }
                radioGroup1.EditValue = editcontrol.EditValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[Control Events]
        private void frmCombo_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            editcontrol.EditValue = radioGroup1.EditValue;
            this.Close();
        }
        #endregion
    }
}