using ExpressTravelCustomerWebAPI.Data;
using ExpressTravelCustomerWebAPI.Models;

namespace ExpressTravelCustomerWebAPI.Services
{
    public interface ICustomerService
    {
        List<Contact> GetContacts(CustomersContext customerContext);
        Contact GetContactById(CustomersContext customerContext, int id);
        Contact InsertContact(CustomersContext customerContext, ContactInfo entity);
        Contact UpdateContact(CustomersContext customerContext, ContactInfo entity, int Id);
        bool DeleteContact(CustomersContext customerContext, int id);
        List<Reference> GetReference(CustomersContext customerContext);
    }
}
