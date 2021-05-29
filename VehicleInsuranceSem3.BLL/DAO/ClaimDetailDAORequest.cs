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
  public  class ClaimDetailDAORequest :ICrudFeature<ClaimDetailViewModel>
    {
        public InsuranceDbContext context = new InsuranceDbContext();

        public int Add(ClaimDetailViewModel newItem)
        {
            Claim_Detail cc = new Claim_Detail()
            {
                claimable_amount = newItem.claimableamount,
                claim_number = newItem.claimnumber,
                customer_policy_id = newItem.customerpolicyid,
                date_accident = (DateTime)newItem.dateaccident,
                insured_amount = newItem.insuredamount,
                place_accident = newItem.placeaccident,
                id  = newItem.id

            };
            context.Claim_Detail.Add(cc);
            context.SaveChanges();
            return 1;
           

        }

        public void Delete(int id)
        {
            var q = context.Claim_Detail.Where(d => d.id == id).FirstOrDefault();
            if (q !=null)
            {
                context.Claim_Detail.Remove(q);
                context.SaveChanges();

            }
        }

        public List<ClaimDetailViewModel> GetAll()
        {
            var q = context.Claim_Detail.Select(d => new ClaimDetailViewModel { id = d.id,claimableamount = d.claimable_amount, claimnumber = d.claim_number, customerpolicyid = d.customer_policy_id, dateaccident = d.date_accident, insuredamount = d.insured_amount, placeaccident = d.place_accident }).ToList();
            return q;
        }

        public ClaimDetailViewModel GetClaimById(int id)
        {
            var q = context.Claim_Detail
                .Where(c => c.id == id)
                .Select(d => new ClaimDetailViewModel { id = d.id, claimableamount = d.claimable_amount, claimnumber = d.claim_number, customerpolicyid = d.customer_policy_id, dateaccident = d.date_accident, insuredamount = d.insured_amount, placeaccident = d.place_accident }).FirstOrDefault();
            return q;
        }

        public List<ClaimDetailViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public ClaimDetailViewModel GetEdit(int id)
        {
            var q = context.Claim_Detail.Where(d => d.id == id).Select(d => new ClaimDetailViewModel {id = d.id, dateaccident = d.date_accident, insuredamount = d.insured_amount, placeaccident = d.place_accident, claimableamount = d.claimable_amount, claimnumber = d.claim_number, customerpolicyid = d.customer_policy_id }).FirstOrDefault();
            return q;
        }

        public List<ClaimDetailViewModel> Gets(int page, int row)
        {
            var q = context.Claim_Detail.Select(d => new ClaimDetailViewModel {  id = d.id,claimableamount = d.claimable_amount, claimnumber = d.claim_number, customerpolicyid = d.customer_policy_id, dateaccident = d.date_accident, insuredamount = d.insured_amount, placeaccident = d.place_accident }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public List<ClaimDetailViewModel> Search(int page, int row, string keyword)
        {
            var CountItem = context.Claim_Detail.Where(d => d.claim_number.ToLower().Contains(keyword.ToLower())).Count();
            var TotalPage = CountItem / row;
            TotalPage += (CountItem % row > 0 ? 1 : 0);
            HttpContext Context = HttpContext.Current;
            Context.Session["CounteItemClaimDetail"] = CountItem;
            Context.Session["TotalPage"] = TotalPage;
            var q = context.Claim_Detail.Where(d => d.claim_number.ToLower().Contains(keyword.ToLower())).Select(d => new ClaimDetailViewModel { id =d.id,claimableamount = d.claimable_amount, claimnumber = d.claim_number, customerpolicyid = d.customer_policy_id, dateaccident = d.date_accident, insuredamount = d.insured_amount, placeaccident = d.place_accident }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();

            return q;
        }

        public int Update(ClaimDetailViewModel updateItems)
        {
            try
            {
                var q = context.Claim_Detail.Where(d => d.id == updateItems.id).FirstOrDefault();
                q.customer_policy_id = updateItems.customerpolicyid;
                q.claim_number = updateItems.claimnumber;
                q.claimable_amount = updateItems.claimableamount;
                q.date_accident = (DateTime)updateItems.dateaccident;
                q.insured_amount = updateItems.insuredamount;
                q.place_accident = updateItems.placeaccident;

                context.SaveChanges();
                return 1;

            }
            catch (EntityException ex)
            {

                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        public List<ClaimDetailViewModel> FilterClaimDetailByDate(DateTime start, DateTime end, int page, int row)
        {
            var q = context.Claim_Detail
                .Where(c => c.date_accident >= start && c.date_accident <= end)
                .Select(d => new ClaimDetailViewModel { id = d.id, claimableamount = d.claimable_amount, claimnumber = d.claim_number, customerpolicyid = d.customer_policy_id, dateaccident = d.date_accident, insuredamount = d.insured_amount, placeaccident = d.place_accident }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public List<ClaimDetailViewModel> CheckPolicyExist(int customePolicyID)
        {
            var q = context.Claim_Detail
                .Where(c => c.customer_policy_id == customePolicyID)
                .Select(d => new ClaimDetailViewModel { id = d.id, claimableamount = d.claimable_amount, claimnumber = d.claim_number, customerpolicyid = d.customer_policy_id, dateaccident = d.date_accident, insuredamount = d.insured_amount, placeaccident = d.place_accident }).OrderBy(d => d.id).ToList();
            return q;
        }
    }

}
