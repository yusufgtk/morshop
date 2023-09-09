using morshop.entity;

namespace morshop.business.Abstract
{
    public interface ICardService
    {
        List<Card> GetAll();
        void Create(Card entity);
        void Update(Card entity);
        void Delete(Card entity);
        Card GetCardByUserId(string userId);
        void AddCard(string userId, int productId, int quantity);
        public void DeleteCard(string userId, int productId);
        void ClearCard(int cardId);
    }
}