namespace store_management.DTO
{
    public class order_itemsDTO
    {
        private int order_item_id; // id chi tiết đơn hàng
        private int order_id; // id đơn hàng
        private int product_id; // id sản phẩm
        private int quantity; // số lượng
        private decimal price; // giá
        private decimal subtotal; // tổng tiền

        public order_itemsDTO(int order_item_id, int order_id, int product_id, int quantity, decimal price, decimal subtotal)
        {
            this.Order_item_id = order_item_id;
            this.Order_id = order_id;
            this.Product_id = product_id;
            this.Quantity = quantity;
            this.Price = price;
            this.Subtotal = subtotal;
        }

        public int Order_item_id { get => order_item_id; set => order_item_id = value; }
        public int Order_id { get => order_id; set => order_id = value; }
        public int Product_id { get => product_id; set => product_id = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public decimal Price { get => price; set => price = value; }
        public decimal Subtotal { get => subtotal; set => subtotal = value; }
    }
}
