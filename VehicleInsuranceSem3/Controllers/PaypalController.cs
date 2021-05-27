using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleInsuranceSem3.BLL.DAO;
using VehicleInsuranceSem3.BLL.ViewModel;
using VehicleInsuranceSem3.DAL.Model;
using VehicleInsuranceSem3.Models;

namespace VehicleInsuranceSem3.Controllers
{
    public class PaypalController : Controller
    {
        // GET: Paypal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Success()
        {   
            return View();
        }

        public ActionResult Failure()
        {
            return View();
        }

        //work with paypal payment
        private Payment payment;

        //create a payment using an APIContext
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            CheckoutInfo checkout = (CheckoutInfo)Session["checkoutInfo"];

            var lsItem = new ItemList() { items = new List<Item>() };
            lsItem.items.Add(new Item { name = checkout.CustomerPolicy.Policy.policy_number, currency = "USD", price = checkout.CustomerPolicy.total_payment.ToString(), quantity = "1", sku = "sku" });

            var payer = new Payer()
            {
                payment_method = "paypal",
                payer_info = new PayerInfo
                {
                    email = "" //personal account
                }

            };
            var redictUrl = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };
            var detail = new Details() { tax = "0", shipping = "0", subtotal = checkout.CustomerPolicy.total_payment.ToString() }; //subtotal: sum(price*quantity) if sum is incorrect, it will return an error 400.
            var amount = new Amount() { currency = "USD", details = detail, total = checkout.CustomerPolicy.total_payment.ToString() }; //total= tax + shipping + subtotal
            var transList = new List<Transaction>();
            transList.Add(new Transaction
            {
                description = "Payment",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = lsItem,

            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transList,
                redirect_urls = redictUrl
            };
            return this.payment.Create(apiContext);
        }
        //create execute payment method
        private Payment ExecutePayment(APIContext apiContext, string payerID, string paymentID)
        {
            var paymentExecute = new PaymentExecution() { payer_id = payerID };
            this.payment = new Payment() { id = paymentID };
            return this.payment.Execute(apiContext, paymentExecute);
        }
        //create method
        public ActionResult PaymentWithPaypal()
        {
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                string payerID = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerID))
                {
                    //create a payment
                    string baseUri = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paypal/PaymentWithPaypal?guid=";
                    string guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseUri + guid);

                    var link = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;
                    while (link.MoveNext())
                    {
                        Links links = link.Current;
                        if (links.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = links.href;
                        }
                    }
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executePayment = ExecutePayment(apiContext, payerID, Session[guid] as string);
                    if (executePayment.state.ToLower() != "approved")
                    {
                        return View("Failure");
                    }
                }
            }
            catch (PayPal.PaymentsException ex)
            {
                PaypalLogger.Log("Error: " + ex.Message);
                return View("Failure");
            }

            AddCustomerPolicy();
            return RedirectToAction("Success");
        }

        public void AddCustomerPolicy()
        {
            CheckoutInfo checkout = (CheckoutInfo)Session["checkoutInfo"];

            Vehicle_Info vehicleInfo = checkout.Vehicle;
            Customer_Policy customerPolicy = checkout.CustomerPolicy;

            VehicleinfoViewModel vehicle = new VehicleinfoViewModel()
            {
                address = vehicleInfo.address,
                brandid = vehicleInfo.brand_id,
                eginenumber = vehicleInfo.engine_number,
                framenumber = vehicleInfo.frame_number,
                modelid = vehicleInfo.model_id,
                ownername = vehicleInfo.owner_name,
                ratebycondition = vehicleInfo.rate_by_condition,
                vehiclecondition = vehicleInfo.vehicle_condition,
                vehiclenumber = vehicleInfo.vehicle_number,
                version = vehicleInfo.version
            };

            VehicleinfoDAORequest request1 = new VehicleinfoDAORequest();
            request1.Add(vehicle);

            VehicleinfoViewModel vehicle2 = request1.GetByAllNumber(vehicle.framenumber, vehicle.eginenumber, vehicle.vehiclenumber);

            CustomerpolicyViewModel cPolicy = new CustomerpolicyViewModel()
            {
                active = customerPolicy.active,
                createdate = customerPolicy.create_date,
                customeraddprove = customerPolicy.customer_add_prove,
                customerid = (int)customerPolicy.customer_id,
                policyenddate = customerPolicy.policy_end_date,
                policyid = (int)customerPolicy.policy_id,
                policystartdate = customerPolicy.policy_start_date,
                TotalPayment = customerPolicy.total_payment,
                vehicleid = vehicle2.id
            };

            CustomerpolicyDAORequest request2 = new CustomerpolicyDAORequest();
            request2.Add(cPolicy);

        }

    }
}