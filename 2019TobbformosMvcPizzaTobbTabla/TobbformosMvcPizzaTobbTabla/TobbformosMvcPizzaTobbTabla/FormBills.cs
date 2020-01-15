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
        private void tabControlPizzaFutarKFT_Selected(object sender, TabControlEventArgs e)
        {
            beallitSzamlakTabPagetIndulaskor();
            feltoltComboBoxotMegrendelokkel();
        }

        private void feltoltComboBoxotMegrendelokkel()
        {
            comboBoxMegrendelok.DataSource = repo.getCustumersName();
        }

        private void comboBoxMegrendelok_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMegrendelok.SelectedIndex < 0)
                return;
            listViewRendelesek.Visible = true;

            string megrendeloNev = comboBoxMegrendelok.Text;
            feltoltListViewtAdatokkal(megrendeloNev);
        }

        private void feltoltListViewtAdatokkal(string megrendeloNev)
        {
            List<Order> megrendelok=repo.getOrders(megrendeloNev);

            foreach(Order megrendelo in megrendelok)
            {
                ListViewItem lvi = new ListViewItem(megrendelo.getOrderId().ToString());
                lvi.SubItems.Add(megrendelo.getCourierId().ToString());
                lvi.SubItems.Add(megrendelo.getCustomerId().ToString());
                lvi.SubItems.Add(megrendelo.getDate().Substring(0,13).ToString());
                lvi.SubItems.Add(megrendelo.getTime().ToString().Replace(',',':'));
                lvi.SubItems.Add(megrendelo.getDone().ToString());

                listViewRendelesek.Items.Add(lvi);
                listViewRendelesek.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewRendelesek.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewRendelesek.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                listViewRendelesek.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);
                listViewRendelesek.AutoResizeColumn(4, ColumnHeaderAutoResizeStyle.ColumnContent);
                listViewRendelesek.AutoResizeColumn(5, ColumnHeaderAutoResizeStyle.HeaderSize);

            }
        }

        private void beallitSzamlakTabPagetIndulaskor()
        {
            listViewRendelesek.Visible = false;
            labelRendelesek.Visible = false;
            dataGridViewTelelek.Visible = false;
            labelTelelek.Visible = false;

            listViewRendelesek.GridLines = true;
            listViewRendelesek.View = View.Details;
            listViewRendelesek.FullRowSelect = true;

            listViewRendelesek.Columns.Add("Azonosító");
            listViewRendelesek.Columns.Add("Futár");
            listViewRendelesek.Columns.Add("Megrendelő");
            listViewRendelesek.Columns.Add("Dátum");
            listViewRendelesek.Columns.Add("Idő");
            listViewRendelesek.Columns.Add("Teljesités");

        }
    }
}
