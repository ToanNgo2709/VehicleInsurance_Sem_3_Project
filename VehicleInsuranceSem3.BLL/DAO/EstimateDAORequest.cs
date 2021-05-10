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
    public class EstimateDAORequest : ICrudFeature<EstimateViewModel>
    {
        public InsuranceDbContext context = new InsuranceDbContext();
        public int Add(EstimateViewModel newItem)
        {
            Estimate estimate = new Estimate()
            {
                estimate_number = newItem.estimatenumber,
                vehicle_warranty = newItem.vehiclewarranty
            };
            context.Estimates.Add(estimate);
            context.SaveChanges();
            return 1;
            
        }

        public void Delete(int id)
        {
            var q = context.Estimates.Where(d => d.id == id).FirstOrDefault();
            if (q!=null)
            {
                context.Estimates.Remove(q);
                context.SaveChanges();

            }
        }

        public List<EstimateViewModel> GetAll()
        {
            var q = context.Estimates.Select(d => new EstimateViewModel { id = d.id, estimatenumber = d.estimate_number, vehiclewarranty = d.vehicle_warranty }).ToList();
            return q;

        }

        public List<EstimateViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public EstimateViewModel GetEdit(int id)
        {
            var q = context.Estimates.Where(d => d.id == id).Select(d => new EstimateViewModel { estimatenumber = d.estimate_number, vehiclewarranty = d.vehicle_warranty }).FirstOrDefault();
            return q;
        }

        public List<EstimateViewModel> Gets(int page, int row)
        {
            var q = context.Estimates.Select(d => new EstimateViewModel { estimatenumber = d.estimate_number, vehiclewarranty = d.vehicle_warranty, id = d.id }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;

        }

        public List<EstimateViewModel> Search(int page, int row, string keyword)
        {
            var CountItem = context.Estimates.Where(d => d.estimate_number.ToLower().Contains(keyword.ToLower())).Count();
            var totalPage = CountItem / row;
            totalPage += (CountItem % row > 0 ? 1 : 0);
            HttpContext Context = HttpContext.Current;
            Context.Session["CountItemEstimate"] = CountItem;
            Context.Session["totalPage"] = totalPage;
            var q = context.Estimates.Where(d => d.estimate_number.ToLower().Contains(keyword.ToLower())).Select(d => new EstimateViewModel { estimatenumber = d.estimate_number, vehiclewarranty = d.vehicle_warranty }).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public int Update(EstimateViewModel updateItems)
        {
            try
            {
                var q = context.Estimates.Where(d => d.id == updateItems.id).FirstOrDefault();
                q.estimate_number = updateItems.estimatenumber;
                q.vehicle_warranty = updateItems.vehiclewarranty;
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
