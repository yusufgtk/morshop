using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using morshop.entity;
using morshop.repository.Abstract;

namespace morshop.repository.Concrete
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        public CardRepository(ShopContext context):base(context)
        {
            
        }
        private ShopContext ShopContext
        {
            get{return context as ShopContext;}
        }
        public void ClearCard(int cardId)
        {
            var cmd = "DELETE FROM CardItems WHERE CardId = @c";
            var cardIdParameter = new SqliteParameter("@c", cardId);
            context.Database.ExecuteSqlRaw(cmd, cardIdParameter);
        }

        public void DeleteCard(int cardId, int productId)
        {
            var cmd = "DELETE FROM CardItems WHERE CardId = @i1 AND ProductId = @i2";
            var cardIdParameter = new SqliteParameter("@i1", cardId);
            var productIdParameter = new SqliteParameter("@i2", productId);
            context.Database.ExecuteSqlRaw(cmd, cardIdParameter, productIdParameter);
        }

        public Card GetCartByUserId(string userId)
        {
            return ShopContext.Cards.Include(i=>i.CardItems).ThenInclude(i=>i.Product).FirstOrDefault(i=>i.UserId==userId);
        }
        public override void Update(Card entity)
        {
            ShopContext.Cards.Update(entity);
            ShopContext.SaveChanges();
        }
    }
}