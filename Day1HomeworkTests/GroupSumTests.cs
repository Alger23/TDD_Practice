using System;
using System.Collections.Generic;
using System.Linq;
using Day1Homework;
using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day1HomeworkTests
{
    [TestClass]
    public class GroupSumTests
    {

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
        }

        [TestMethod]
        public void GroupSum_By_Cost_3_Items_Should_Return_Correct_Summary_Result()
        {
            // Arrange
            var products = GetProducts();
            var property = "Cost";
            var length = 3;
            var expected = new[] { 6, 15, 24, 21 };

            var target = new GroupHelper();

            // Act
            var actual = target.GroupSum(products, property, length).ToArray();

            // Assert
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void GroupSum_By_Revenue_4_Items_Should_Return_Correct_Summary_Result()
        {
            // Arrange
            var products = GetProducts();
            var property = "Revenue";
            var length = 4;
            var expected = new[] { 50, 66, 60 };

            IGroupHelper target = new GroupHelper();

            // Act
            var actual = target.GroupSum(products, property, length).ToArray();

            // Assert
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void GroupSum_By_CostFunc_3_Items_Should_Return_Correct_Summary_Result()
        {
            // Arrange
            var products = GetProducts();
            Func<Product, int> getCostFunc = o => (int) o.Cost;
            var length = 3;
            var expected = new[] { 6, 15, 24, 21 };

            var target = new GroupHelper();

            // Act
            var actual = target.GroupSum(products, getCostFunc, length).ToArray();

            // Assert
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void GroupSum_By_RevenueFunc_4_Items_Should_Return_Correct_Summary_Result()
        {
            // Arrange
            var products = GetProducts();
            Func<Product, int> getRevenue = o => (int)o.Revenue;
            var length = 4;
            var expected = new[] { 50, 66, 60 };

            IGroupHelper target = new GroupHelper();

            // Act
            var actual = target.GroupSum(products, getRevenue, length).ToArray();

            // Assert
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        public IList<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product{Id=1,Cost=1,Revenue=11,SellPrice=21},
                new Product{Id=2,Cost=2,Revenue=12,SellPrice=22},
                new Product{Id=3,Cost=3,Revenue=13,SellPrice=23},
                new Product{Id=4,Cost=4,Revenue=14,SellPrice=24},
                new Product{Id=5,Cost=5,Revenue=15,SellPrice=25},
                new Product{Id=6,Cost=6,Revenue=16,SellPrice=26},
                new Product{Id=7,Cost=7,Revenue=17,SellPrice=27},
                new Product{Id=8,Cost=8,Revenue=18,SellPrice=28},
                new Product{Id=9,Cost=9,Revenue=19,SellPrice=29},
                new Product{Id=10,Cost=10,Revenue=20,SellPrice=30},
                new Product{Id=11,Cost=11,Revenue=21,SellPrice=31}
            };
        }
    }
}