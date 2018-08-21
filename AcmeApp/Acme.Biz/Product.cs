using Acme.Common;
using static Acme.Common.LoggingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{   /// <summary>
    /// Manages products carried in inventory.
    /// </summary>
    public class Product
    {
        public const  double InchesPerMeter = 39.37;
        public readonly decimal MinimumPrice;
        

        #region Constructors
        public Product()
        {
            Console.WriteLine("Product instance created");
            //this.productVendor = new Vendor();
            this.MinimumPrice = .96m;
        }
        public Product(int productId,
                        string productName,
                        string description) : this()
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Description = description;
            if (ProductName.StartsWith("Bulk"))
                {
                this.MinimumPrice = 9.99m;
            }

            Console.WriteLine("Product instance has a name: " +
                                ProductName);
        }
        #endregion

        #region Properties
        private DateTime? availabilityDate;

        public DateTime? AvailabilityDate
        {
            get { return availabilityDate; }
            set { availabilityDate = value; }
        }

        private string productName;

        public string ProductName
        {
            get {
                //Remove exces spaces from the beginning and ending of productName
                //We use the "?" nullconditional operator to set formattedValue to "null" when productname is "null".
                var formattedValue = productName?.Trim();  
                return formattedValue;
            }
            set { productName = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        
        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        private Vendor productVendor;

        public Vendor ProductVendor
        {
            get {
                //Check wether backing fiels is null
                if (productVendor == null)
                    //Lazy loading
                    {
                    productVendor = new Vendor();
                    }
                return productVendor;
            }
            set { productVendor = value; }
        }

        #endregion

        public string SayHello()
        {
            //var vendor = new Vendor();
            //vendor.SendWelcomeEmail("Message from Product");

            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("new product", this.ProductName, "sales@gmail.com");

            var result = LogAction("saying hello");

            return "Hello " + ProductName +
                " (" + ProductId + "): " +
                Description + " Available on: " + AvailabilityDate?.ToShortDateString();
        }

    }
}
