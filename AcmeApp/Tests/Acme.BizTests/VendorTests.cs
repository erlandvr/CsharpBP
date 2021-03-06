﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Common;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class VendorTests
    {
        [TestMethod()]
        public void SendWelcomeEmail_ValidCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "ABC Corp";
            var expected = "Message sent: Hello ABC Corp";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_EmptyCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "";
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_NullCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = null;
            string expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PlaceOrderTest()
        {
            //Arrange
            var vendor = new Vendor();
            var product = new Product(1, "Saw", "got teeth");
            var expected = new OperationResult(true, "Order from acme, Inc\r\nProduct: Tools-0001\r\nQuantity: 12" + "\r\nInstructions: standard delivery");

            //Act
            var actual = vendor.PlaceOrder(product, 12);

            //Assert
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);

        }

        [TestMethod()]
        public void PlaceOrder_3Parameters()
        {
            //Arrange
            var vendor = new Vendor();
            var product = new Product(1, "Saw", "got teeth");
            var expected = new OperationResult(true, "Order from acme, Inc\r\nProduct: Tools-0001\r\nQuantity: 12" + "\r\nDeliver By: 10/25/2030" + "\r\nInstructions: standard delivery");

            //Act
            var actual = vendor.PlaceOrder(product, 12, new DateTimeOffset(2030, 10, 25, 0, 0, 0, new TimeSpan(-7, 0, 0)));

            //Assert
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);

        }

        [TestMethod()]
        public void PlaceOrder_NoDeliveryDate()
        {
            //Arrange
            var vendor = new Vendor();
            var product = new Product();
            var expected = new OperationResult(true, "Order from acme, Inc\r\nProduct: Tools-0001\r\nQuantity: 12" + "\r\nInstructions: Deliver to Suite");

            //Act
            var actual = vendor.PlaceOrder(product, 12, instructions: "Deliver to Suite");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlaceOrderNullProduct_Exception()
        {
            //Arrange
            var vendor = new Vendor();

            //Act
            var actual = vendor.PlaceOrder(null, 12);

            //Assert
            //Expected exception
        }

        [TestMethod()]
        public void PlaceOrderTest_withAddress()
        {
            //Arrange
            var vendor = new Vendor();
            var product = new Product();
            var expected = new OperationResult(true, "Test with address");

            //Act
            var actual = vendor.PlaceOrder(
                product, 12,
                Vendor.IncludeAddress.Yes,
                Vendor.SendCopy.No);

            //Assert
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            //Arrange
            var vendor = new Vendor();
            vendor.VendorId = 1;
            vendor.CompanyName = "ABC Corp";
            var expected = "Vendor: ABC Corp";

            //Act
            var actual = vendor.ToString();
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PrepareDirectionsTest()
        {
            //Arrange
            var vendor = new Vendor();
            var expected = @"Insert \r\n to define a new line";

            //Act
            var actual = vendor.PrepareDirections();
            Console.WriteLine(actual);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}