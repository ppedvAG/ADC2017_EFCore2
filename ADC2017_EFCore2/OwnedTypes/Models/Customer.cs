namespace OwnedTypes.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Adress Adress { get; set; }
    }
}
