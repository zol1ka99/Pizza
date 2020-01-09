using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TobbbformosPizzaAlkalmazasEgyTabla.Repository;
using TobbbformosPizzaAlkalmazasEgyTabla.Model;
using System.Diagnostics;

namespace _2019TobbformosMvcPizzaEgyTabla
{
    public partial class FormPizzaFutarKft : Form
    {
        /// <summary>
        /// Pizzákat tartalmazó adattábla
        /// </summary>
        private DataTable FutarDT = new DataTable();
        /// <summary>
        /// Tárolja a pizzákat listában
        /// </summary>
        private Repository Frepo = new Repository();

        bool ujFutarfelvitel = false;

        private void buttonBetoltesFutar_Click(object sender, EventArgs e)
        {
        //Adatbázisban pizza tábla kezelése
        RepositoryDatabaseTablePizza rtp = new RepositoryDatabaseTablePizza();
            //A repo-ba lévő pizza listát feltölti az adatbázisból
            repo.setPizzas(rtp.getPizzasFromDatabaseTable());
            frissitAdatokkalDataGriedViewtf();
            beallitFutarDataGriViewt();
            beallitGombokatIndulaskor();            

            dataGridViewFutar.SelectionChanged += DataGridViewFutar_SelectionChanged;
        }

        private void beallitGombokatIndulaskorf()
        {
            panelFutar.Visible = false;
            panelModositTorolGombok.Visible = false ;
            if (dataGridViewFutar.SelectedRows.Count != 0)
                buttonUjFutar.Visible = false;
            else
                buttonUjFutar.Visible = true;
        }

        private void DataGridViewPizzak_SelectionChanged(object sender, EventArgs e)
        {
            
            if (ujAdatfelvitel)
            {
                beallitGombokatKattintaskor();
            }           
            if (dataGridViewFutar.SelectedRows.Count == 1)
            {
                panelPizza.Visible = true;
                panelModositTorolGombok.Visible = true;
                buttonUjPizza.Visible = true;
                textBoxPizzaAzonosito.Text =
                    dataGridViewFutar.SelectedRows[0].Cells[0].Value.ToString();
                textBoxPizzaNev.Text =
                    dataGridViewFutar.SelectedRows[0].Cells[1].Value.ToString();
                textBoxPizzaAr.Text =
                    dataGridViewFutar.SelectedRows[0].Cells[2].Value.ToString();
            }
            else
            {
                panelPizza.Visible = false;
                panelModositTorolGombok.Visible = false;
                buttonUjPizza.Visible = false;
            }
        }

        private void frissitAdatokkalDataGriedViewtf()
        {
            //Adattáblát feltölti a repoba lévő pizza listából
            pizzasDT = repo.getFutarDataTableFromList();
            //Pizza DataGridView-nak a forrása a pizza adattábla
            dataGridViewFutar.DataSource = null;
            dataGridViewFutar.DataSource = FutarDT;
        }

        private void beallitFutarDataGriViewt()
        {
            FutarDT.Columns[0].ColumnName = "Azonosító";
            FutarDT.Columns[0].Caption = "Futár azonosító";
            FutarDT.Columns[1].ColumnName = "Név";
            FutarDT.Columns[1].Caption = "Futár név";
            FutarDT.Columns[2].ColumnName = "Telefonszám";
            FutarDT.Columns[2].Caption = "Futar telefonszám";

            dataGridViewFutar.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dataGridViewFutar.ReadOnly = true;
            dataGridViewFutar.AllowUserToDeleteRows = false;
            dataGridViewFutar.AllowUserToAddRows = false;
            dataGridViewFutar.MultiSelect = false;
        }

        private void buttonTorolPizza_Click(object sender, EventArgs e)
        {
            torolHibauzenetet();
            if ((dataGridViewFutar.Rows == null) ||
                (dataGridViewFutar.Rows.Count == 0))
                return;
            //A felhasználó által kiválasztott sor a DataGridView-ban            
            int sor = dataGridViewFutar.SelectedRows[0].Index;
            if (MessageBox.Show(
                "Valóban törölni akarja a sort?",
                "Törlés",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                //1. törölni kell a listából
                int id = -1;
                if (!int.TryParse(
                         dataGridViewFutar.SelectedRows[0].Cells[0].Value.ToString(),
                         out id))
                    return;
                try
                {
                    repo.deleteFutarFromList(id);
                }
                catch (RepositoryExceptionCantDelete recd)
                {
                    kiirHibauzenetet(recd.Message);
                    Debug.WriteLine("A futár törlés nem sikerült, nincs a listába!");
                }
                //2. törölni kell az adatbázisból
                RepositoryDatabaseTablePizza rdtp = new RepositoryDatabaseTablePizza();
                try
                {
                    rdtp.deletePizzaFromDatabase(id);
                }
                catch (Exception ex)
                {
                    kiirHibauzenetet(ex.Message);
                }
                //3. frissíteni kell a DataGridView-t  
                frissitAdatokkalDataGriedViewtf();
                if (dataGridViewFutar.SelectedRows.Count <= 0)
                {
                    buttonUjFutar.Visible = true;
                }
                beallitFutarDataGriViewt();
            }
        }
        private void buttonModositPizza_Click(object sender, EventArgs e)
        {
            torolHibauzenetet();
            errorProviderPizzaName.Clear();
            errorProviderPizzaPrice.Clear();
            try
            {
                Futar modosult = new Futar(
                    Convert.ToInt32(textBoxFutarAzonosito.Text),
                    textBoxFutarNev.Text,
                    textBoxFutarAr.Text
                    );
                int azonosito = Convert.ToInt32(textBoxPizzaAzonosito.Text);
                //1. módosítani a listába
                try
                {
                    repo.updateFutarInList(azonosito, modosult);
                }
                catch (Exception ex)
                {
                    kiirHibauzenetet(ex.Message);
                    return;
                }
                //2. módosítani az adatbáziba
                RepositoryDatabaseTablePizza rdtp = new RepositoryDatabaseTablePizza();
                try
                {
                    rdtp.updateFutarInDatabase(azonosito, modosult);
                }
                catch (Exception ex)
                {
                    kiirHibauzenetet(ex.Message);
                }
                //3. módosítani a DataGridView-ban           
                frissitAdatokkalDataGriedViewt();
            }
            catch (ModelPizzaNotValidNameExeption nvn)
            {
                errorProviderPizzaName.SetError(textBoxFutarNev, nvn.Message);
            }
            catch (ModelPizzaNotValidPriceExeption nvp)
            {
                errorProviderPizzaName.SetError(textBoxFutarTel, nvp.Message);
            }
            catch (RepositoryExceptionCantModified recm)
            {
                kiirHibauzenetet(recm.Message);
                Debug.WriteLine("Módosítás nem sikerült, a futár nincs a listába!");
            }
            catch (Exception ex)
            { }
        }

        private void buttonUjMentes_Click(object sender, EventArgs e)
        {
            torolHibauzenetet();
            errorProviderPizzaName.Clear();
            errorProviderPizzaPrice.Clear();
            try
            {
                Futar ujPizza = new Futar(
                    Convert.ToInt32(textBoxPizzaAzonosito.Text),
                    textBoxPizzaNev.Text,
                    textBoxPizzaAr.Text
                    );
                int azonosito = Convert.ToInt32(textBoxPizzaAzonosito.Text);
                //1. Hozzáadni a listához
                try
                {
                    repo.addPizzaToList(ujPizza);
                }
                catch(Exception ex)
                {
                    kiirHibauzenetet(ex.Message);
                    return;
                }
                //2. Hozzáadni az adatbázishoz
                RepositoryDatabaseTablePizza rdtp = new RepositoryDatabaseTablePizza();
                try
                {
                    rdtp.insertPizzaToDatabase(ujPizza);
                }
                catch (Exception ex)
                {
                    kiirHibauzenetet(ex.Message);
                }
                //3. Frissíteni a DataGridView-t
                beallitGombokatUjPizzaMegsemEsMentes();
                frissitAdatokkalDataGriedViewt();
                if (dataGridViewFutar.SelectedRows.Count == 1)
                {
                    beallitPizzaDataGriViewt();
                }

            }
            catch (ModelPizzaNotValidNameExeption nvn)
            {
                errorProviderPizzaName.SetError(textBoxPizzaNev, nvn.Message);
            }
            catch (ModelPizzaNotValidPriceExeption nvp)
            {
                errorProviderPizzaName.SetError(textBoxPizzaAr, nvp.Message);
            }
            catch (Exception ex)
            {
            }
        }

        private void buttonUjPizza_Click(object sender, EventArgs e)
        {
            ujAdatfelvitel = true;
            beallitGombokatTextboxokatUjPizzanal();
            int ujPizzaAzonosito = repo.getNextPizzaId();
            textBoxPizzaAzonosito.Text = ujPizzaAzonosito.ToString();
        }

        private void buttonMegsem_Click(object sender, EventArgs e)
        {
            beallitGombokatUjPizzaMegsemEsMentes();
        }    

        private void beallitGombokatUjPizzaMegsemEsMentes()
        {
            if ((dataGridViewFutar.Rows != null) &&
                (dataGridViewFutar.Rows.Count > 0))
                dataGridViewFutar.Rows[0].Selected = true;
            buttonUjMentes.Visible = false;
            buttonMegsem.Visible = false;
            panelModositTorolGombok.Visible = true;
            ujAdatfelvitel = false;

            textBoxPizzaNev.Text = string.Empty;
            textBoxPizzaAr.Text = string.Empty;
          
        }
        private void beallitGombokatTextboxokatUjPizzanal()
        {
            panelPizza.Visible = true;
            panelModositTorolGombok.Visible = false;
            textBoxPizzaNev.Text = string.Empty;
            textBoxPizzaAr.Text = string.Empty;
        }

        private void beallitGombokatKattintaskor()
        {
            ujAdatfelvitel = false;
            buttonUjMentes.Visible = false;
            buttonMegsem.Visible = false;
            panelModositTorolGombok.Visible = true;
            errorProviderPizzaName.Clear();
            errorProviderPizzaPrice.Clear();
        }

        private void textBoxPizzaNev_TextChanged(object sender, EventArgs e)
        {
            kezelUjMegsemGombokat();
        }

        private void textBoxPizzaAr_TextChanged(object sender, EventArgs e)
        {
            kezelUjMegsemGombokat();
        }

        private void kezelUjMegsemGombokat()
        {
            if (ujAdatfelvitel == false)
                return;
            if ((textBoxPizzaNev.Text != string.Empty) ||
                (textBoxPizzaAr.Text != string.Empty))
            {
                buttonUjMentes.Visible = true;
                buttonMegsem.Visible = true;
            }
            else
            {
                buttonUjMentes.Visible = false;
                buttonMegsem.Visible = false;
            }
        }

    }
}
