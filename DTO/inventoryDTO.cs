namespace store_management.DTO
{
    public class inventoryDTO
    {
        private int inventory_id;
        private int product_id;
        private int quantity;
        private DateTime updated_at;

        public inventoryDTO(int inventory_id, int product_id, int quantity, DateTime updated_at)
        {
            this.Inventory_id = inventory_id;
            this.Product_id = product_id;
            this.Quantity = quantity;
            this.Updated_at = updated_at;
        }

        public int Inventory_id { get => inventory_id; set => inventory_id = value; }
        public int Product_id { get => product_id; set => product_id = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public DateTime Updated_at { get => updated_at; set => updated_at = value; }
    }
}
