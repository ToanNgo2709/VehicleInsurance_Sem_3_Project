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
    public class CompanyexpenseDAORequest : ICrudFeature<CompanyexpenseViewModel>

    {
        public InsuranceDbContext context = new InsuranceDbContext();

        public int Add(CompanyexpenseViewModel newItem)
        {
            Company_Expense company = new Company_Expense()
            {
                date = newItem.date,
                amount = newItem.amount,
                description = newItem.description,
                expense_type_id = (int)newItem.expensetypeid,
                customer_policy_id = newItem.customerpolicyid,
                id = newItem.id

            };
            context.Company_Expense.Add(company);
            context.SaveChanges();
            return 1;
        }

        public void Delete(int id)
        {
            var q = context.Company_Expense.Where(d => d.id == id).FirstOrDefault();
            if (q != null)
            {
                context.Company_Expense.Remove(q);
                context.SaveChanges();

            }

        }

        public List<CompanyexpenseViewModel> GetAll()
        {
            var q = context.Company_Expense.Select(d => new CompanyexpenseViewModel { id = d.id, amount = (decimal)d.amount, customerpolicyid = d.customer_policy_id, date = d.date, description = d.description, expensetypeid = d.expense_type_id }).ToList();
            return q;

        }

        public List<CompanyexpenseViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public CompanyexpenseViewModel GetEdit(int id)
        {
            var q = context.Company_Expense.Where(d => d.id == id).Select(d => new CompanyexpenseViewModel { id = d.id, amount = (decimal)d.amount, date = d.date, description = d.description, customerpolicyid = d.customer_policy_id, expensetypeid = d.expense_type_id }).FirstOrDefault();
            return q;

        }


        public List<CompanyexpenseViewModel> Gets(int page, int row)
        {
            var q = context.Company_Expense.Select(d => new CompanyexpenseViewModel { id = d.id, amount = (decimal)d.amount, date = d.date, description = d.description, customerpolicyid = d.customer_policy_id, expensetypeid = d.expense_type_id }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;

        }

        public List<CompanyexpenseViewModel> Search(int page, int row, string keyword)
        {
            var Counitem = context.Company_Expense.Where(d => d.description.ToLower().Contains(keyword.ToLower())).Count();
            var totalPage = Counitem / row;
            totalPage += (Counitem % row > 0 ? 1 : 0);
            HttpContext Context = HttpContext.Current;
            Context.Session["CountItemCompanyexpense"] = Counitem;
            Context.Session["totalPage"] = totalPage;
            var q = context.Company_Expense.Where(d => d.description.ToLower().Contains(keyword.ToLower())).Select(d => new CompanyexpenseViewModel { customerpolicyid = d.customer_policy_id, expensetypeid = d.expense_type_id, id = d.id, amount = (decimal)d.amount, date = d.date, description = d.description }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public int Update(CompanyexpenseViewModel updateItems)
        {
            try
            {
                var q = context.Company_Expense.Where(d => d.id == updateItems.id).FirstOrDefault();
                q.amount = (decimal)updateItems.amount;
                q.date = updateItems.date;
                q.description = updateItems.description;
                q.expense_type_id = (int)updateItems.expensetypeid;
                q.customer_policy_id = updateItems.customerpolicyid;



                context.SaveChanges();
                return 1;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);

                return 0;
            }
        }

        public List<CompanyexpenseViewModel> FilterExpenseByDate(DateTime startDate, DateTime endDate, int page, int row)
        {
            var q = context.Company_Expense
                .Where(c => c.date >= startDate && c.date <= endDate)
                .Select(d => new CompanyexpenseViewModel { id = d.id, amount = (decimal)d.amount, date = d.date, description = d.description, customerpolicyid = d.customer_policy_id, expensetypeid = d.expense_type_id }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

    }
}
