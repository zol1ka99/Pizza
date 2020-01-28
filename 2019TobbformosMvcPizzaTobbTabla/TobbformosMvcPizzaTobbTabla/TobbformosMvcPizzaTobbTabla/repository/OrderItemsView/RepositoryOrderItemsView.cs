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

        /// <summary>
        /// A fizetendő végösszeg
        /// </summary>
        private int finalPrice=0;

        /// <summary>
        /// Konstruktor, amely a rendelés száma alapján feltölti a listát
        /// </summary>
        /// <param name="orderNumber">Rendelés azonosító</param>
        public RepositoryOrderItemsView(int orderNumber, List<Item> items, List<Pizza> pizzas)
        {
            roiv = new List<OrderItemsView>();
            List<Item> iviews = items.FindAll(i => i.getOrderId() == orderNumber);
            finalPrice = 0;
            foreach(Item i in iviews)
            {
                Pizza pizza = pizzas.Find(p => p.getId() == i.getPizzaId());
                finalPrice = finalPrice + i.getPiece() * pizza.getPrice();
                OrderItemsView oiv = new OrderItemsView(
                        orderNumber,
                        i.getPiece(),
                        pizza.getNeme(),
                        pizza.getPrice()
                    );
                roiv.Add(oiv);
            }
        }

        public int getFinalPrice()
        {
            return finalPrice;
        }

        public DataTable getOrderItemsViewDT()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("pizza_nev",typeof(string));
            dt.Columns.Add("mennyiseg",typeof(int));
            dt.Columns.Add("egysegar",typeof(int));
            dt.Columns.Add("tetelar",typeof(int));

            foreach(OrderItemsView oiv in roiv)
            {
                dt.Rows.Add(oiv.Name, oiv.Piece, oiv.Price, oiv.Piece * oiv.Price);
            }
            return dt;
        }
    }
}
