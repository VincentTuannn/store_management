using store_management.DTO;

namespace store_management.Service
{
    public interface IPromotionService
    {
        Task<List<promotionsDTO>> GetAllPromotionsAsync();
        Task<promotionsDTO> GetPromotionByIdAsync(int id);
        Task<promotionsDTO> CreatePromotionAsync(promotionsDTO dto);
        Task UpdatePromotionAsync(int id, promotionsDTO dto);
        Task DeletePromotionAsync(int id);
        Task<decimal> CalculateDiscountAsync(int promoId, decimal orderAmount);
    }
}
