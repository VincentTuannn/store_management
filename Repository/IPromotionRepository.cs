using store_management.Entity;

namespace store_management.Repository
{
    public interface IPromotionRepository : IDisposable
    {
        IEnumerable<Promotions> GetPromotions();
        Promotions GetPromotionByID(int promotion_id);
        void InsertPromotion(Promotions promotion);
        void DeletePromotion(int promotion_id);
        void UpdatePromotion(Promotions promotion);
        void Save();
    }
}
