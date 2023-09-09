using Microsoft.EntityFrameworkCore;
using morshop.entity;
using morshop.repository.Abstract;

namespace morshop.repository.Concrete
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ShopContext context):base(context)
        {
            
        }
        private ShopContext ShopContext
        {
            get{return context as ShopContext;}
        }
        public List<Order> GetOrders(string userId)
        {
                // return context.Orders.Where(i=>i.UserId==userId).ToList();
            var orders = ShopContext.Orders.Include(i=>i.OrderItems).ThenInclude(i=>i.Product).AsQueryable();

            if(userId!=null)
            {
                orders=orders.Where(i=>i.UserId==userId);
            }
            return orders.ToList();
        }
    }
}