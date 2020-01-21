using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TobbbformosPizzaAlkalmazasTobbTabla.Model;

namespace TobbbformosPizzaAlkalmazasTobbTabla.Repository
{
    class RepositoryOrderItemsView
    {
        /// <summary>
        /// Rendelések tételeit tartalmazza a megjelenítéshez
        /// </summary>
        private List<RepositoryOrderItemsView> roiv;

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
            }
        }
    }
}
