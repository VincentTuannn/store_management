namespace store_management.DTO
{
    public class promotionsDTO
    {
        private int promotion_id;
        private string promotion_code;
        private string description;
        private string discount_type;
        private decimal discount_value;
        private DateTime start_date;
        private DateTime end_date;
        private decimal min_order_amount;
        private int usage_limit;
        private int used_count;
        private string status;

        public promotionsDTO(int promotion_id, string promotion_code, string description, string discount_type, decimal discount_value, DateTime start_date, DateTime end_date, decimal min_order_amount, int usage_limit, int used_count, string status)
        {
            this.Promotion_id = promotion_id;
            this.Promotion_code = promotion_code;
            this.Description = description;
            this.Discount_type = discount_type;
            this.Discount_value = discount_value;
            this.Start_date = start_date;
            this.End_date = end_date;
            this.Min_order_amount = min_order_amount;
            this.Usage_limit = usage_limit;
            this.Used_count = used_count;
            this.Status = status;
        }

        public int Promotion_id { get => promotion_id; set => promotion_id = value; }
        public string Promotion_code { get => promotion_code; set => promotion_code = value; }
        public string Description { get => description; set => description = value; }
        public string Discount_type { get => discount_type; set => discount_type = value; }
        public decimal Discount_value { get => discount_value; set => discount_value = value; }
        public DateTime Start_date { get => start_date; set => start_date = value; }
        public DateTime End_date { get => end_date; set => end_date = value; }
        public decimal Min_order_amount { get => min_order_amount; set => min_order_amount = value; }
        public int Usage_limit { get => usage_limit; set => usage_limit = value; }
        public int Used_count { get => used_count; set => used_count = value; }
        public string Status { get => status; set => status = value; }
    }
}
