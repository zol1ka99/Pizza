using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2019TobbformosMvcPizzaEgyTabla.model
{
    class Futar
    {
        private int id;
        private string name;
        private int phonenumber;

        public Futar(int id, string name, int phonenumber)
        {
            this.id = id;
            this.name = name;
            this.phonenumber = phonenumber;
        }

        public Futar(int id, string name, string phonenumber)
        {
            this.id = id;
            if (!isValidName(name))
                throw new ModelFutarNotValidNameException("A futár neve nem megfelelő!");
            if (isValidPhonenumber(phonenumber))
                throw new ModelPizzaNotValidPhonenumberException("A futár telefonszáma nem megfelelő!");
            this.name = name;
            this.phonenumber = Convert.ToInt32(phonenumber);
        }

        public void update (Futar modified)
        {
            this.name = modified.getName();
            this.phonenumber = modified.getPhonenumber();
        }
        private bool isValidPhonenumber(string phonenumber)
        {
            int eredmeny = 0;
            if (int.TryParse(phonenumber, out eredmeny))
                return true;
            else
                return false;
        }
        private bool isValidName(string name)
        {
            if (name == string.Empty)
                return false;
            if (!char.IsUpper(name.ElementAt(0)))
                return false;
            for (int i = 1; i < name.Length; i = i + 1)
                if (
                    !char.IsLetter(name.ElementAt(i))
                        &&
                    (!char.IsWhiteSpace(name.ElementAt(i)))

                    )
                    return false;
            return true;
        }

        public void setID(int id)
        {
            this.id = id;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public void setPhonenumber(int phonenumber)
        {
            this.phonenumber = phonenumber;
        }
        public int getId()
        {
            return id;
        }
        public string getName()
        {
            return name;
        }
        public int getPhonenumber()
        {
            return phonenumber;
        }

    }
}
