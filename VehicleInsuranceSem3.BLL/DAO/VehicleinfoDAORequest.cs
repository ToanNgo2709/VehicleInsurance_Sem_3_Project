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
    public class VehicleinfoDAORequest : ICrudFeature<VehicleinfoViewModel>
    {
        public InsuranceDbContext context = new InsuranceDbContext();

        public int Add(VehicleinfoViewModel newItem)
        {
            Vehicle_Info vehicle = new Vehicle_Info()
            {
                address = newItem.address,
                owner_name = newItem.ownername,
                version = newItem.version,
                frame_number = newItem.framenumber,
                engine_number = newItem.eginenumber,
                vehicle_number = newItem.vehiclenumber,
               brand_id = newItem.brandid,
               model_id = newItem.modelid,
               vehicle_condition = newItem.vehiclecondition,
               rate_by_condition = newItem.ratebycondition,
            };
            context.Vehicle_Info.Add(vehicle);
            context.SaveChanges();
            return 1;

        }

        public VehicleinfoViewModel GetByAllNumber(string frameNumber, string engineNumber, string vehicleNumber)
        {
            var q = context.Vehicle_Info
                .Where(v => v.frame_number.Equals(frameNumber) && v.engine_number.Equals(engineNumber) && v.vehicle_number.Equals(vehicleNumber))
                .Select(d => new VehicleinfoViewModel { id = d.id, address = d.address, ownername = d.owner_name, version = d.version, framenumber = d.frame_number, eginenumber = d.engine_number, vehiclenumber = d.vehicle_number, brandid = d.brand_id, modelid = d.model_id, ratebycondition = d.rate_by_condition, vehiclecondition = d.vehicle_condition }).OrderBy(d => d.id).FirstOrDefault();
            return q;
        }

        public void Delete(int id)
        {
            var q = context.Vehicle_Info.Where(d => d.id == id).FirstOrDefault();
            if (q !=null)
            {
                context.Vehicle_Info.Remove(q);
                context.SaveChanges();
            }
        }

        public List<VehicleinfoViewModel> GetAll()
        {
            var q = context.Vehicle_Info.Select(d => new VehicleinfoViewModel { id = d.id, address = d.address, brandid = d.brand_id, eginenumber = d.engine_number, framenumber = d.frame_number, modelid = d.model_id, ownername = d.owner_name, vehiclenumber = d.vehicle_number, version = d.version, ratebycondition = d.rate_by_condition, vehiclecondition = d.vehicle_condition }).ToList();
            return q;

        }

        public List<VehicleinfoViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public VehicleinfoViewModel GetEdit(int id)
        {
            var q = context.Vehicle_Info.Where(d => d.id == id).Select(d => new VehicleinfoViewModel { id = d.id, address = d.address, ownername = d.owner_name, version = d.version, framenumber = d.frame_number, eginenumber = d.frame_number, vehiclenumber = d.vehicle_number , brandid = d.brand_id , modelid = d.model_id , vehiclecondition = d.vehicle_condition , ratebycondition = d.rate_by_condition }).FirstOrDefault();
            return q;

        }

        public List<VehicleinfoViewModel> Gets(int page, int row)
        {
            var q = context.Vehicle_Info.Select(d => new VehicleinfoViewModel { id = d.id, address = d.address, ownername = d.owner_name, version = d.version, framenumber = d.frame_number, eginenumber = d.engine_number, vehiclenumber = d.vehicle_number  , brandid = d.brand_id , modelid =d.model_id , ratebycondition = d.rate_by_condition , vehiclecondition = d.vehicle_condition }).OrderBy(d => d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }


        public List<VehicleinfoViewModel> Search(int page, int row, string keyword)
        {
            var CountItem = context.Vehicle_Info.Where(d => d.address.ToLower().Contains(keyword.ToLower()) || d.owner_name.ToLower().Contains(keyword.ToLower()) || d.version.ToLower().Contains(keyword.ToLower()) || d.frame_number.ToLower().Contains(keyword.ToLower()) || d.engine_number.ToLower().Contains(keyword.ToLower()) || d.vehicle_number.ToLower().Contains(keyword.ToLower())).Count();
            var totalPage = CountItem / row;
            totalPage += (CountItem % row > 0 ? 1 : 0);
            HttpContext Context = HttpContext.Current;
            Context.Session["CountItemVecleinfo"] = CountItem;
            Context.Session["totalPage"] = totalPage;
            var q = context.Vehicle_Info.Where(d => d.address.ToLower().Contains(keyword.ToLower()) || d.owner_name.ToLower().Contains(keyword.ToLower()) || d.version.ToLower().Contains(keyword.ToLower()) || d.frame_number.ToLower().Contains(keyword.ToLower())
                || d.engine_number.ToLower().Contains(keyword.ToLower()) || d.vehicle_number.ToLower().Contains(keyword.ToLower())).Select(d => new VehicleinfoViewModel { id = d.id, ownername = d.owner_name, address = d.address, eginenumber = d.engine_number, framenumber = d.frame_number, vehiclenumber = d.vehicle_number, version = d.version , brandid = d.brand_id , modelid = d.model_id , ratebycondition = d.rate_by_condition , vehiclecondition = d.vehicle_condition }).OrderBy(d=>d.id).Skip((page - 1) * row).Take(row).ToList();
            return q;
        }

        public int Update(VehicleinfoViewModel updateItems)
        {
            try
            {
                var q = context.Vehicle_Info.Where(d => d.id == updateItems.id).FirstOrDefault();
                q.address = updateItems.address;
                q.owner_name = updateItems.ownername;
                q.version = updateItems.version;
                q.frame_number = updateItems.framenumber;
                q.engine_number = updateItems.eginenumber;
                q.vehicle_number = updateItems.vehiclenumber;
                q.vehicle_condition = updateItems.vehiclecondition;
                q.brand_id = updateItems.brandid;
                q.model_id = updateItems.modelid;
                q.rate_by_condition = updateItems.ratebycondition;

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
