
using morshop.entity;

namespace morshop.repository.Abstract
{
    public interface ICardRepository:IRepository<Card>
    {
        public Card GetCartByUserId(string userId);
        public void DeleteCard(int cardId,int productId);
        void ClearCard(int cardId);

    }


}