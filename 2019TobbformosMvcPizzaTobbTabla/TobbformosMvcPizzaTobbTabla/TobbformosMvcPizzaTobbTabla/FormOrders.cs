using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TobbbformosPizzaAlkalmazasTobbTabla.Model;
using TobbbformosPizzaAlkalmazasTobbTabla.Repository;

namespace TobbformosMvcPizzaTobbTabla
{
    public partial class FormPizzaFutarKft : Form
    {
        DataGridView dgv;

        private void fillComboBoxCustomer()
        {
            comboBoxCustomer.DataSource = null;
            comboBoxCustomer.DataSource=repo.getCustumersName();
        }

        private void createDinamicDataGridViewToOrders()
        {
            if (dgv != null)
                return;
            dgv = new DataGridView();

            dgv.CellContentClick += Dgv_CellContentClick;
            dgv.DefaultValuesNeeded += Dgv_DefaultValuesNeeded;
            dgv.EditingControlShowing += Dgv_EditingControlShowing;

            DataGridViewComboBoxColumn cbc = new DataGridViewComboBoxColumn();
            cbc.Name = "pizzaName";
            cbc.HeaderText = "      Pizza név    ";
            cbc.MaxDropDownItems = 5;
            cbc.DataSource = repo.getPizzasName();
            cbc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;            


            DataGridViewButtonColumn bcPlusz = new DataGridViewButtonColumn();
            bcPlusz.UseColumnTextForButtonValue = true;
            bcPlusz.HeaderText = "Mennyiség növelés";
            bcPlusz.Name = "bcPlusz";
            bcPlusz.Text = "+";
            bcPlusz.AutoSizeMode =DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewButtonColumn bcMinusz = new DataGridViewButtonColumn();
            bcMinusz.UseColumnTextForButtonValue = true;
            bcMinusz.HeaderText = "Mennyiség csökkentés";
            bcMinusz.Name = "bcMinusz";
            bcMinusz.Text = "-";
            bcMinusz.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn tbcMennyiseg = new DataGridViewTextBoxColumn();
            tbcMennyiseg.HeaderText = "Mennyiség";
            tbcMennyiseg.Name = "mennyiseg";
            tbcMennyiseg.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            DataGridViewTextBoxColumn tbcEgysegar = new DataGridViewTextBoxColumn();
            tbcEgysegar.HeaderText = "Egységár";
            tbcEgysegar.Name = "egysegar";
            tbcEgysegar.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgv.Location = new Point(30, 60);
            dgv.Width = 600;

            dgv.Columns.Add(cbc);
            dgv.Columns.Add(bcPlusz);
            dgv.Columns.Add(bcMinusz);
            dgv.Columns.Add(tbcMennyiseg);
            dgv.Columns.Add(tbcEgysegar);

            tabPageMegrendeles.Controls.Add(dgv);

      }

        private void Dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            
        }

        private void Dgv_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[3].Value = "1";
        }

        private void Dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Debug.WriteLine("dgv:" + e.RowIndex + ", " + e.ColumnIndex);

            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                if (e.ColumnIndex==1)
                {
                    Debug.WriteLine("dgv: + gomb");
                    int db = Convert.ToInt32(
                        dgv.Rows[e.RowIndex].Cells["mennyiseg"].Value.ToString());
                    db = db + 1;
                    dgv.Rows[e.RowIndex].Cells["mennyiseg"].Value = db;
                } else if (e.ColumnIndex == 2)
                {
                    Debug.WriteLine("dgv: - gomb");
                    int db = Convert.ToInt32(
                        dgv.Rows[e.RowIndex].Cells["mennyiseg"].Value.ToString());
                    if (db > 1)
                        db = db - 1;
                    dgv.Rows[e.RowIndex].Cells["mennyiseg"].Value = db;
                }
            }
        }
    }
}
