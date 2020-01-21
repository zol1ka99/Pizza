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
            dataGridViewTelelek.Visible = false;
            labelTelelek.Visible = false;
            if (comboBoxMegrendelok.SelectedIndex < 0)
                return;
            listViewRendelesek.Visible = true;
            labelRendelesek.Visible = true;

            string megrendeloNev = comboBoxMegrendelok.Text;
            feltoltListViewtAdatokkal(megrendeloNev);
        }

        private void feltoltListViewtAdatokkal(string megrendeloNev)
        {
            listViewRendelesek.Items.Clear();
            List<Order> megrendelok=repo.getOrders(megrendeloNev);
            foreach(Order megrendelo in megrendelok)
            {
                ListViewItem lvi = new ListViewItem(megrendelo.getOrderId().ToString());
                lvi.SubItems.Add(megrendelo.getCourierId().ToString());
                lvi.SubItems.Add(megrendelo.getCustomerId().ToString());
                lvi.SubItems.Add(megrendelo.getDate().Substring(0,13).ToString());
                lvi.SubItems.Add(megrendelo.getTime().ToString().Replace(',',':'));
                if (megrendelo.getDone())
                    lvi.SubItems.Add("Teljesítve");
                else
                    lvi.SubItems.Add("Nem teljesítve");
                listViewRendelesek.Items.Add(lvi);
            }
            listViewRendelesek.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewRendelesek.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewRendelesek.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewRendelesek.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewRendelesek.AutoResizeColumn(5, ColumnHeaderAutoResizeStyle.ColumnContent);
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

            listViewRendelesek.Columns[1].TextAlign = HorizontalAlignment.Right;
            listViewRendelesek.Columns[2].TextAlign = HorizontalAlignment.Right;
            listViewRendelesek.Columns[3].TextAlign = HorizontalAlignment.Right;
        }

        private void listViewRendelesek_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewRendelesek.SelectedItems.Count == 1)
            {
                dataGridViewTelelek.Visible = true;
                labelTelelek.Visible = true;
            }
            else
            {
                dataGridViewTelelek.Visible = false;
                labelTelelek.Visible = false;
            }

        }
    }
}
