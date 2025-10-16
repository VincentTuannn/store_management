namespace store_management.DTO
{
    public class suppliersDTO
    {
        private int supplier_id; // id nhà cung cấp
        private string name; // tên nhà cung cấp
        private string phone; // số điện thoại
        private string address; // địa chỉ
        private string email; // email

        public suppliersDTO(int supplier_id, string name, string phone, string address, string email)
        {
            this.Supplier_id = supplier_id;
            this.Name = name;
            this.Phone = phone;
            this.Address = address;
            this.Email = email;
        }

        public int Supplier_id { get => supplier_id; set => supplier_id = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public string Email { get => email; set => email = value; }
    }
}
