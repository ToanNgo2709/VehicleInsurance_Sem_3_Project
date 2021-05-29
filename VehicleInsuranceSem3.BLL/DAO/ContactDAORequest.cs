using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsuranceSem3.BLL.Repository;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.DAL.Model;

namespace VehicleInsuranceSem3.BLL.DAO
{
    public class ContactDAORequest : ICrudFeature<ContactViewModel>
    {
        public InsuranceDbContext context = new InsuranceDbContext();

        public int Add(ContactViewModel newItem)
        {
            Contact newContact = new Contact()
            {
                name = newItem.Name,
                email = newItem.Email,
                message = newItem.Message,
                website = newItem.Website
            };
            context.Contacts.Add(newContact);
            context.SaveChanges();
            return 1;
        }

        public void Delete(int id)
        {
            var item = context.Contacts.Where(c => c.id == id).FirstOrDefault();
            if (item != null)
            {
                context.Contacts.Remove(item);
                context.SaveChanges();
            }

        }

        public List<ContactViewModel> GetAll()
        {
            var list = context.Contacts.Select(c => new ContactViewModel
            {
                Id = c.id,
                Name = c.name,
                Email = c.email,
                Message = c.message,
                Website = c.website
            }).ToList();
            return list;
        }

        public List<ContactViewModel> GetById(int Id)
        {
            var q = context.Contacts
                .Where(c => c.id == Id)
                .Select(c => new ContactViewModel { Id = c.id, Name = c.name, Website = c.website, Email = c.email, Message = c.message })
                .OrderBy(d => d.Id)
                .ToList();
            return q;
          
        }

        public ContactViewModel GetContactById(int Id)
        {
            var q = context.Contacts
                .Where(c => c.id == Id)
                .Select(c => new ContactViewModel { Id = c.id, Name = c.name, Website = c.website, Email = c.email, Message = c.message })
                .OrderBy(d => d.Id)
                .FirstOrDefault();
            return q;

        }

        public ContactViewModel GetEdit(int id)
        {
            throw new NotImplementedException();
        }

        public List<ContactViewModel> Gets(int page, int row)
        {
            var q = context.Contacts.Select(c => new ContactViewModel { Id = c.id, Name = c.name, Website = c.website, Email = c.email, Message = c.message })
                .OrderBy(d => d.Id)
                .Skip((page - 1) * row)
                .Take(row)
                .ToList();
            return q;
        }

        public List<ContactViewModel> Search(int page, int row, string keyword)
        {
            throw new NotImplementedException();
        }

        public int Update(ContactViewModel updateItems)
        {
            throw new NotImplementedException();
        }
    }
}
