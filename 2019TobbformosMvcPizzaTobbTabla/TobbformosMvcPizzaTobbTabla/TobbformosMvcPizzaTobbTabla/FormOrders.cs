using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        DataGridView dgv = new DataGridView();
        private void fillComboBoxCustomer()
        {
            comboBoxCustomer.DataSource = null;
            comboBoxCustomer.DataSource = repo.getCustumersName();
        }

        private void createDinamicDataGridViewOrders()
        {
            DataGridViewComboBoxColumn cbc = new DataGridViewComboBoxColumn();
            cbc.Name = "pizzaName";
            cbc.HeaderText = "Pizza név";
            cbc.MaxDropDownItems = 5;
            cbc.DataSource = repo.getPizzas();

            DataGridViewButtonColumn bcPlusz = new DataGridViewButtonColumn();
            bcPlusz.HeaderText = "Mennyiség növelés";
            bcPlusz.Name = "bcPlusz";
            bcPlusz.Text = "+";


            dgv.Location = new Point(30, 50);

            dgv.Columns.Add(cbc);

            dgv.Columns.Add(bcPlusz);

            tabPageMegrendeles.Controls.Add(dgv);
            //tabPageMegrendeles.Refresh();
        }
    }
}
