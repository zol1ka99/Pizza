using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TobbbformosPizzaAlkalmazasEgyTabla.Model
{
    partial class Futar
    {
        public string getInsert()
        {
            return
                "INSERT INTO `pfutar` (`fazon`, `fnev`, `ftel`) " +
                "VALUES ('" +
                id +
                "', '" +
                getNeme() +
                "', '" +
                getPrice() +
                "');";
        }

        public string getUpdate(int id)
        {
            return
                "UPDATE `pfutar` SET `fnev` = '" +
                getNeme() +
                "', `ftel` = '" +
                getPrice() +
                "' WHERE `ppizza`.`fazon` = " +
                id;
        }

        public static string getSQLCommandDeleteAllRecord()
        {
            return "DELETE FROM pfutar";
        }

        public static string getSQLCommandGetAllRecord()
        {
            return "SELECT * FROM pfutar";
        }
    }
}
