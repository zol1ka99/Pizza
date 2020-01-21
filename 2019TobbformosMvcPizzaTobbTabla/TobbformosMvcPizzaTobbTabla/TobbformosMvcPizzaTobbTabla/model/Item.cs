using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TobbbformosPizzaAlkalmazasTobbTabla.Model
{
    partial class Item
    {
        private int itemId;
        private int pizzaId;
        private int piece;

        public Item(int itemID, int pizzaId, int piece)
        {
            this.itemId = itemID;
            this.pizzaId = pizzaId;
            this.piece = piece;
        }

        public int getItemId()
        {
            return itemId;
        }

        public int getPizzaId()
        {
            return pizzaId;
        }

        public int getPiece()
        {
            return piece;
        }
    }
}
