using Microsoft.EntityFrameworkCore;
using store_management.Data;
using store_management.Entity;

namespace store_management.Repository
{
    public class PromotionRepository : IPromotionRepository, IDisposable
    {
        private AppDbContext context;
        public PromotionRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void DeletePromotion(int promotion_id)
        {
            Promotions promotion = context.Promotions.Find(promotion_id);
            if (promotion != null)
            {
                context.Promotions.Remove(promotion);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Promotions GetPromotionByID(int promotion_id)
        {
            return context.Promotions.Find(promotion_id);
        }

        public IEnumerable<Promotions> GetPromotions()
        {
            return context.Promotions.ToList();
        }

        public void InsertPromotion(Promotions promotion)
        {
            context.Promotions.Add(promotion);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdatePromotion(Promotions promotion)
        {
            context.Entry(promotion).State = EntityState.Modified;
        }
    }
}
