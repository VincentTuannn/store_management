namespace store_management.DTO
{
    public class ordersDTO
    {
        private int order_id; // id đơn hàng
        private int customer_id; // id khách hàng
        private int user_id; // id người dùng
        private int promo_id; // id khuyến mãi
        private DateTime order_date; // ngày đặt hàng
        private string status; // trạng thái
        private decimal total_amount; // tổng số tiền
        private decimal discount_amount; // số tiền giảm giá

        public ordersDTO(int order_id, int customer_id, int user_id, int promo_id, DateTime order_date, string status, decimal total_amount, decimal discount_amount)
        {
            this.Order_id = order_id;
            this.Customer_id = customer_id;
            this.User_id = user_id;
            this.Promo_id = promo_id;
            this.Order_date = order_date;
            this.Status = status;
            this.Total_amount = total_amount;
            this.Discount_amount = discount_amount;
        }

        public int Order_id { get => order_id; set => order_id = value; }
        public int Customer_id { get => customer_id; set => customer_id = value; }
        public int User_id { get => user_id; set => user_id = value; }
        public int Promo_id { get => promo_id; set => promo_id = value; }
        public DateTime Order_date { get => order_date; set => order_date = value; }
        public string Status { get => status; set => status = value; }
        public decimal Total_amount { get => total_amount; set => total_amount = value; }
        public decimal Discount_amount { get => discount_amount; set => discount_amount = value; }
    }
}
