using morshop.entity;
using morshop.repository.Concrete;

namespace morshop.repository.Abstract
{
    public interface IOrderRepository:IRepository<Order>
    {
        List<Order> GetOrders(string userId);
    }
}