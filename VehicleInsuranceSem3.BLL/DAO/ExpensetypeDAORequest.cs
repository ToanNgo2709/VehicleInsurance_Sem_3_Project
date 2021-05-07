using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using VehicleInsuranceSem3.BLL.Repository;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.DAL.Model;

namespace VehicleInsuranceSem3.BLL.DAO
{
    public class ExpensetypeDAORequest : ICrudFeature<ExpensetypeViewModel>
    {
        public InsuranceDbContext context = new InsuranceDbContext();

        public int Add(ExpensetypeViewModel newItem)
        {
            Expense_Type expense = new Expense_Type()
            {
                name = newItem.name,
                active = newItem.active,
            };
            context.Expense_Type.Add(expense);
            context.SaveChanges();
            return 1;

        }

        public void Delete(int id)
        {
            var q = context.Expense_Type.Where(d => d.id == id).FirstOrDefault();
            if (q != null)
            {
                context.Expense_Type.Remove(q);
                context.SaveChanges();

            }

        }

        public List<ExpensetypeViewModel> GetAll()
        {
            var q = context.Expense_Type.Select(d => new ExpensetypeViewModel { id = d.id, name = d.name, active = d.active }).ToList();
            return q;
        }

        public List<ExpensetypeViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public ExpensetypeViewModel GetEdit(int id)
        {
            var q = context.Expense_Type.Where(d => d.id == id).Select(d => new ExpensetypeViewModel { id = d.id, name = d.name, active = d.active }).FirstOrDefault();
            return q;

        }

        public List<ExpensetypeViewModel> Gets(int page, int row)
        {
            var q = context.Expense_Type.Select(d => new ExpensetypeViewModel { id = d.id, name = d.name, active = d.active }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;

        }

        public List<ExpensetypeViewModel> Search(int page, int row, string keyword)
        {
            var CountItem = context.Expense_Type.Where(d => d.name.ToLower().Contains(keyword.ToLower())).Count();
            var totalPage = CountItem / row;
            totalPage += (CountItem % row > 0 ? 1 : 0);
            HttpContext Context = HttpContext.Current;
            Context.Session["CountItemexpensetype"] = CountItem;
            Context.Session["totalPage"] = totalPage;
            var q = context.Expense_Type.Where(d => d.name.ToLower().Contains(keyword.ToLower())).Select(d => new ExpensetypeViewModel { id = d.id, name = d.name, active = d.active }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q; 


        }

        public int Update(ExpensetypeViewModel updateItems)
        {
            try
            {
                var q = context.Expense_Type.Where(d => d.id == updateItems.id).FirstOrDefault();
                q.name = updateItems.name;
                q.active = updateItems.active;
                context.SaveChanges();
                return 1;

            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
                return 0;

                   
            }
        }
    }
}
