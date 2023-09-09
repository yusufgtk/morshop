using morshop.business.Abstract;
using morshop.entity;
using morshop.repository.Abstract;

namespace morshop.business.Concrete
{
    public class CardManager : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardManager(ICardRepository cardRepository)
        {
            _cardRepository=cardRepository;
        }

        public void AddCard(string userId, int productId, int quantity)
        {
            var card = _cardRepository.GetCartByUserId(userId);
            if(card!=null)
            {
                var index = card.CardItems.FindIndex(i=>i.ProductId==productId);
                if(index<0)
                {
                    card.CardItems.Add(new CardItem(){
                        CardId=card.Id,
                        ProductId=productId,
                        Quantity=quantity
                    });
                }
                else
                {
                    card.CardItems[index].Quantity+=quantity;
                }
                _cardRepository.Update(card);
            }
        }

        public void ClearCard(int cardId)
        {
            _cardRepository.ClearCard(cardId);
        }

        public void Create(Card entity)
        {
            _cardRepository.Create(entity);
        }

        public void Delete(Card entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteCard(string userId, int productId)
        {
            var card = _cardRepository.GetCartByUserId(userId);
            _cardRepository.DeleteCard(card.Id,productId);
        }

        public List<Card> GetAll()
        {
            throw new NotImplementedException();
        }

        public Card GetCardByUserId(string userId)
        {
            return _cardRepository.GetCartByUserId(userId);
        }

        public void Update(Card entity)
        {
            throw new NotImplementedException();
        }
    }
}