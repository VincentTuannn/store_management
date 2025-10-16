namespace store_management.DTO
{
    public class productsDTO
    {
        private int product_id; // id sản phẩm
        private int category_id; // id loại sản phẩm
        private int supplier_id; // id nhà cung cấp
        private string product_name; // tên sản phẩm
        private string barcode; // mã vạch
        private decimal price; // giá
        private string unit; // đơn vị tính
        private DateTime created_at; // thời gian tạo

        public productsDTO(int product_id, int category_id, int supplier_id, string product_name, string barcode, decimal price, string unit, DateTime created_at)
        {
            this.Product_id = product_id;
            this.Category_id = category_id;
            this.Supplier_id = supplier_id;
            this.Product_name = product_name;
            this.Barcode = barcode;
            this.Price = price;
            this.Unit = unit;
            this.Created_at = created_at;
        }

        public int Product_id { get => product_id; set => product_id = value; }
        public int Category_id { get => category_id; set => category_id = value; }
        public int Supplier_id { get => supplier_id; set => supplier_id = value; }
        public string Product_name { get => product_name; set => product_name = value; }
        public string Barcode { get => barcode; set => barcode = value; }
        public decimal Price { get => price; set => price = value; }
        public string Unit { get => unit; set => unit = value; }
        public DateTime Created_at { get => created_at; set => created_at = value; }
    }
}
