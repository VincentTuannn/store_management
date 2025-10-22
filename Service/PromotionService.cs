using AutoMapper;
using store_management.DTO;
using store_management.Entity;
using store_management.Repository;

namespace store_management.Service
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _repository;
        private readonly IMapper _mapper;

        public PromotionService(IPromotionRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<decimal> CalculateDiscountAsync(int promoId, decimal orderAmount)
        {
            var promotion = _repository.GetPromotionByID(promoId);
            if (promotion == null || promotion.Status != "active" || DateTime.UtcNow < promotion.StartDate || DateTime.UtcNow > promotion.EndDate)
                return 0;

            if (orderAmount < promotion.MinOrderAmount) return 0;
            if (promotion.UsedCount >= promotion.UsageLimit && promotion.UsageLimit > 0) return 0;

            return promotion.DiscountType == "percent" ? orderAmount * (promotion.DiscountValue / 100) : promotion.DiscountValue;
        }

        public async Task<promotionsDTO> CreatePromotionAsync(promotionsDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Promotion_code)) throw new ArgumentException("Promotion code is required.");
            if (_repository.GetPromotions().Any(p => p.PromotionCode == dto.Promotion_code))
                throw new InvalidOperationException("Promotion code already exists.");
            if (dto.Start_date > dto.End_date) throw new ArgumentException("Start date must be before end date.");

            var promotion = _mapper.Map<Promotions>(dto);
            _repository.InsertPromotion(promotion);
            _repository.Save();
            return _mapper.Map<promotionsDTO>(promotion);
        }

        public async Task DeletePromotionAsync(int id)
        {
            var promotion = _repository.GetPromotionByID(id);
            if (promotion == null) throw new KeyNotFoundException();
            if (promotion.Status == "active") throw new InvalidOperationException("Cannot delete active promotion.");

            _repository.DeletePromotion(id);
            _repository.Save();
        }

        public async Task<List<promotionsDTO>> GetAllPromotionsAsync()
        {
            var promotions = await Task.FromResult(_repository.GetPromotions());
            return _mapper.Map<List<promotionsDTO>>(promotions);
        }

        public async Task<promotionsDTO> GetPromotionByIdAsync(int id)
        {
            var promotion = _repository.GetPromotionByID(id);
            if (promotion == null) throw new KeyNotFoundException($"Promotion with ID {id} not found.");
            return _mapper.Map<promotionsDTO>(promotion);
        }

        public async Task UpdatePromotionAsync(int id, promotionsDTO dto)
        {
            var promotion = _repository.GetPromotionByID(id);
            if (promotion == null) throw new KeyNotFoundException();

            _mapper.Map(dto, promotion);
            _repository.UpdatePromotion(promotion);
            _repository.Save();
        }
    }
}
