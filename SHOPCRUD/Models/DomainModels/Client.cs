namespace SHOPCRUD.Models.DomainModels
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public double TelNubmer { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
