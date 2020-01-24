using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TobbbformosPizzaAlkalmazasTobbTabla.model;
using TobbbformosPizzaAlkalmazasTobbTabla.Model;

namespace TobbbformosPizzaAlkalmazasTobbTabla.Repository
{
    class RepositoryOrderItemsView
    {
        /// <summary>
        /// Rendelések tételeit tartalmazza a megjelenítéshez
        /// </summary>
        private List<OrderItemsView> roiv;

        private int finalPrice = 0;

       

        /// <summary>
        /// Konstruktor, amely a rendelés száma alapján feltölti a listát
        /// </summary>
        /// <param name="orderNumber">Rendelés azonosító</param>
        public RepositoryOrderItemsView(int orderNumber, List<Item> items, List<Pizza> pizzas)
        {
            List<Item> iviews = items.FindAll(i => i.getOrderId() == orderNumber);
            foreach (Item i in iviews)
            {
                Pizza pizza = pizzas.Find(p => p.getId() == i.getPizzaId());
                finalPrice = finalPrice + i.getPiece() * pizza.getPrice();
                OrderItemsView oiv = new OrderItemsView(
                    orderNumber,
                    i.getPiece(),
                    pizza.getNeme(),
                    pizza.getPrice());
                roiv.Add(oiv);
            }
        }

        //roiv = new List<OrderItemsView>();

        public int getFinalPrice()
        {
            return finalPrice;
        }

        public DataTable getOrderItemsViewDT()
        {
            DataTable DT = new DataTable();

            DT.Columns.Add("pizza_nev",typeof(string));
            DT.Columns.Add("mennyiseg",typeof(int));
            DT.Columns.Add("egysegar",typeof(int));
            DT.Columns.Add("tetelar",typeof(int));

            foreach (OrderItemsView oiv in roiv)
            {
                DT.Rows.Add(oiv.Name, oiv.Piece, oiv.Price, oiv.Piece * oiv.Price);
            }

            return DT;
        }
        
    }
}
