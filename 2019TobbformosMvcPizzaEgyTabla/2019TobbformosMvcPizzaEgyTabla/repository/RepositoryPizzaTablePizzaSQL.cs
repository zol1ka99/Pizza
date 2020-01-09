using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TobbbformosPizzaAlkalmazasEgyTabla.Model;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace TobbbformosPizzaAlkalmazasEgyTabla.Repository
{
    partial class RepositoryDatabaseTablePizza
    {
        public List<Futar> getPizzasFromDatabaseTable()
        {
            List<Futar> pizzas = new List<Futar>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = Futar.getSQLCommandGetAllRecord();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr["pnev"].ToString();
                    bool goodResult = false;
                    int id = -1;
                    goodResult = int.TryParse(dr["pazon"].ToString(), out id);
                    if (goodResult)
                    {
                        int price = -1;
                        goodResult = int.TryParse(dr["par"].ToString(), out price);
                        if (goodResult)
                        {
                            Futar p = new Futar(id, name, price);
                            pizzas.Add(p);
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                throw new RepositoryException("Pizzaadatok beolvasása az adatbázisból nem sikerült!");
            }
            return pizzas;
        }

        public void deletePizzaFromDatabase(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "DELETE FROM ppizza WHERE pazon=" + id;
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                Debug.WriteLine(id + " idéjű pizza törlése nem sikerült.");
                throw new RepositoryException("Sikertelen törlés az adatbázisból.");
            }
        }

        public void updatePizzaInDatabase(int id, Futar modified)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = modified.getUpdate(id);
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                Debug.WriteLine(id + " idéjű pizza módosítása nem sikerült.");
                throw new RepositoryException("Sikertelen módosítás az adatbázisból.");
            }
        }

        public void insertPizzaToDatabase(Futar ujPizza)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = ujPizza.getInsert();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                Debug.WriteLine(e.Message);
                Debug.WriteLine(ujPizza + " pizza beszúrása adatbázisba nem sikerült.");
                throw new RepositoryException("Sikertelen beszúrás az adatbázisból.");
            }
        }
    }
}
