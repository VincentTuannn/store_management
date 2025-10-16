namespace store_management.DTO
{
    public class customersDTO
    {
        private int customer_id; // id khách hàng
        private string name; // tên khách hàng
        private string email; // email
        private string phone; // số điện thoại
        private string address; // địa chỉ
        private DateTime created_at; // thời gian tạo

        public customersDTO(int customer_id, string name, string email, string phone, string address, DateTime created_at)
        {
            this.Customer_id = customer_id;
            this.Name = name;
            this.Email = email;
            this.Phone = phone;
            this.Address = address;
            this.Created_at = created_at;
        }

        public int Customer_id { get => customer_id; set => customer_id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public DateTime Created_at { get => created_at; set => created_at = value; }
    }
}
