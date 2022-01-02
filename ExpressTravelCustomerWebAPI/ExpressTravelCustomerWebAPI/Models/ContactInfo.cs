namespace ExpressTravelCustomerWebAPI.Models
{
    public class ContactInfo
    {
        public int SalutationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public bool IsStaff { get; set; }
    }
}
