using System;
using System.ComponentModel;
using System.Web.Services;

namespace WebServices
{
	[WebService(Namespace = "http://four51.com/services/")]
	public class Four51OrderService : System.Web.Services.WebService
	{
		[WebMethod]
		public void ValidateAddToOrder(Order Order, out string ErrorMessage, out StatusCode StatusCode, out UnitPriceOverride[] UnitPriceOverrides)
		{
			
			ErrorMessage = "An error message to the end user can go here and will display if statuscode is set to fail";
			UnitPriceOverrides = new UnitPriceOverride[1];
			UnitPriceOverrides[0] = new UnitPriceOverride();
			UnitPriceOverrides[0].LineNumber = 1;
			UnitPriceOverrides[0].UnitPrice = (decimal)9.7;
			StatusCode = StatusCode.OK;
		}
		
		[WebMethod]
		public void ValidateCheckoutClicked(Order Order, out string ErrorMessage, out StatusCode StatusCode, out UnitPriceOverride[] UnitPriceOverrides)
		{

		        ErrorMessage = "diggity :" + Order.UserIPAddress;
			UnitPriceOverrides = new UnitPriceOverride[1];
			UnitPriceOverrides[0] = new UnitPriceOverride();
			UnitPriceOverrides[0].LineNumber = 1;
			UnitPriceOverrides[0].UnitPrice = (decimal)9.7;
			StatusCode = StatusCode.OK;
			
		}

		[WebMethod]
		public void ValidateShipping(Order Order, out string ErrorMessage, out StatusCode StatusCode, out UnitPriceOverride[] UnitPriceOverrides, out CostCenterOverride[] CostCenters)
		{
			CostCenters = new CostCenterOverride[1];
			CostCenters[0] = new CostCenterOverride();
			CostCenters[0].CostCenterOptions = new string[3];
			CostCenters[0].CostCenterOptions[0] = "cc1";
			CostCenters[0].CostCenterOptions[1] = "cc2";
			CostCenters[0].CostCenterOptions[2] = "cc3";
			UnitPriceOverrides = new UnitPriceOverride[1];
			UnitPriceOverrides[0] = new UnitPriceOverride();
			UnitPriceOverrides[0].LineNumber = 1;
			UnitPriceOverrides[0].UnitPrice = (decimal)9.7;
			//CostCenters = null;


			string discountCode = GetValueFromSpec("Order Type", Order.OrderFields);
			decimal discount = 0;
			switch (discountCode)
			{
				case "secret10percentcode":
					discount = (decimal)0.10;
					break;
				case "secret20percentcode":
					discount = (decimal)0.20;
					break;
			}
			if (discount > 0)
			{
				UnitPriceOverrides = new UnitPriceOverride[1];
				UnitPriceOverrides[0] = new UnitPriceOverride();
				UnitPriceOverrides[0].LineNumber = 1;
				UnitPriceOverrides[0].UnitPrice = Order.LineItems[0].UnitPrice -= Order.LineItems[0].UnitPrice * discount;
			}
			else
				UnitPriceOverrides = null;
			
			ErrorMessage = "";
			StatusCode = StatusCode.OK;
		}
		private string GetValueFromSpec(string name, Spec[] specs)
		{
			foreach (Spec spec in specs)
			{
				if (spec.Name == name)
					return spec.Value;
			}
			return null;
		}

		[WebMethod]
		public void ValidateBilling(Order Order, out string ErrorMessage, out StatusCode StatusCode)
		{
			ErrorMessage = "";
			StatusCode = StatusCode.OK;
		}

		[WebMethod]
		public void ValidateOrder(Order Order, out string ErrorMessage, out StatusCode StatusCode)
		{
			ErrorMessage = "What the!";
			StatusCode = StatusCode.Fail;
		}
		[WebMethod]
		public decimal CalculateTax(Order Order)
		{
			return (decimal)9.5;
		}
		[WebMethod]
		public decimal CalculateShipping(Order Order)
		{
			return (decimal)10.25;
		}
		[WebMethod]
		public string GeneratePOID(Order Order)
		{
			return "123456-5555";
		}
		public Four51OrderService()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code

		//Required by the Web Services Designer 
		private IContainer components = null;

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#endregion
	}
	public enum StatusCode
	{
		OK,
		Fail
	}

	public class CostCenterOverride
	{
		public int LineNumber;
		public string[] CostCenterOptions;
	}
	public class UnitPriceOverride
	{
		public int LineNumber;
		public decimal UnitPrice;
	}




	public struct Order
	{
		public string BuyerInteropID;
		public string cXMLPayloadID;
		public string OrderID;
		public string OrderType;
        public string UserIPAddress;
		public Payment Payment;
		public decimal ShippingTotal;
		public decimal TaxTotal;
		public decimal OrderTotal;
		public string Comments;
		public User FromUser;
		public Address BillingAddress;
		public LineItem[] LineItems;
		public Spec[] OrderFields;
		public Coupon Coupon;
	}
	public enum PaymentMethod
	{
		CreditCard, PurchaseOrder, Undetermined, BudgetAccount
	}
	public struct Payment
	{
		public PaymentMethod PaymentMethod;
		public CreditCard CreditCard;
		public string BudgetAccount;
	}
	public struct CreditCard
	{
		public string CardType;
		public string Number;
		public DateTime ExpirationDate;
		public string CardHolderName;
	}
	public struct LineItem
	{
		public bool LineItemToBeAdded;
		public string ProductInteropID;
		public string ProductID;
		public string VariantIdentifier;
		public int Quantity;
		public decimal UnitPrice;
		public string CostCenter;
		public string DateNeeded;
		public string ShipperName;
		public string ShipperAccount;
		public Address ShipAddress;
		public Spec[] ProductSpecs;
	}
	public struct Address
	{
		public string AddressName;
		public string AddressInteropID;
		public string FirstName;
		public string LastName;
		public string Street1;
		public string Street2;
		public string City;
		public string State;
		public string Zip;
		public string Country;
	}
	public struct User
	{
		public string UserName;
		public string Email;
		public string CompanyDuns;
		public string FirstName;
		public string LastName;
		public string Phone;
		public string InteropID;
	}
	public struct Spec
	{
		public string Name;
		public string Value;
	}	
	public struct Coupon
	{
		public string Code;
		public string InteropID;
		public decimal DiscountAmount;
	}

}