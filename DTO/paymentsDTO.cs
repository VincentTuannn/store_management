namespace store_management.DTO
{
    public class paymentsDTO
    {
        private int payment_id; // id thanh toán
        private int order_id; // id đơn hàng
        private decimal amount; // số tiền
        private DateTime payment_date; // ngày thanh toán
        private string payment_method; // phương thức thanh toán

        public paymentsDTO(int payment_id, int order_id, decimal amount, DateTime payment_date, string payment_method)
        {
            this.Payment_id = payment_id;
            this.Order_id = order_id;
            this.Amount = amount;
            this.Payment_date = payment_date;
            this.Payment_method = payment_method;
        }

        public int Payment_id { get => payment_id; set => payment_id = value; }
        public int Order_id { get => order_id; set => order_id = value; }
        public decimal Amount { get => amount; set => amount = value; }
        public DateTime Payment_date { get => payment_date; set => payment_date = value; }
        public string Payment_method { get => payment_method; set => payment_method = value; }
    }
}
