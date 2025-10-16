namespace store_management.DTO
{
    public class categoriesDTO
    {
        private int category_id; // id loại sản phẩm
        private string category_name; // tên loại sản phẩm

        public categoriesDTO(int category_id, string category_name)
        {
            this.Category_id = category_id;
            this.Category_name = category_name;
        }

        public int Category_id { get => category_id; set => category_id = value; }
        public string Category_name { get => category_name; set => category_name = value; }
    }
}
