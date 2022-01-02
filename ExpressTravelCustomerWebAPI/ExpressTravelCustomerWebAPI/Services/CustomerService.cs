using ExpressTravelCustomerWebAPI.Data;
using ExpressTravelCustomerWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpressTravelCustomerWebAPI.Services
{
    public class CustomerService : ICustomerService
    {
        public bool DeleteContact(CustomersContext customerContext, int id)
        {
            try
            {
                var contact = customerContext.Contacts.FirstOrDefault(c => c.ContactId == id);
                if (contact != null)
                {
                    customerContext.Entry(contact).State = EntityState.Deleted;
                    customerContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Contact GetContactById(CustomersContext customerContext, int id)
        {
            try
            {
                var contact = customerContext.Contacts.FirstOrDefault(c => c.ContactId == id);
                if (contact == null)
                    return null;
                else
                    return contact;
            }
            catch(Exception ex)
            {
                return null;
            }
        }


        public List<Contact> GetContacts(CustomersContext customerContext)
        {
            try
            {
                return customerContext.Contacts.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Reference> GetReference(CustomersContext customerContext)
        {
            try
            {
                return customerContext.References.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Contact InsertContact(CustomersContext customeContext, ContactInfo entity)
        {
            try
            {
                int id = 0;
                var value = customeContext.Contacts.OrderByDescending(u => u.ContactId).FirstOrDefault();
                if (value != null)
                {
                    id = value.ContactId;
                }
                Contact contact = new Contact();
                contact.ContactId = id + 1;
                contact.FirstName = entity.FirstName;
                contact.LastName = entity.LastName;
                contact.Dob = entity.Dob;
                contact.SalutationId = entity.SalutationId;
                contact.IsStaff = entity.IsStaff;

                customeContext.Contacts.Add(contact);
                customeContext.SaveChanges();

                return contact;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Contact UpdateContact(CustomersContext customeContext, ContactInfo entity, int Id)
        {
            try
            {
                var contact = customeContext.Contacts.FirstOrDefault(c => c.ContactId == Id);
                if (contact != null)
                {
                    contact.ContactId = Id;
                    contact.FirstName= entity.FirstName;
                    contact.LastName= entity.LastName;
                    contact.Dob = entity.Dob;
                    contact.SalutationId = entity.SalutationId;
                    contact.IsStaff = entity.IsStaff;

                    customeContext.Entry(contact).State = EntityState.Modified;
                    customeContext.SaveChanges();

                    return contact;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
