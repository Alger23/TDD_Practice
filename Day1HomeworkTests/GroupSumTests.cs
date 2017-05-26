using Microsoft.VisualStudio.TestTools.UnitTesting;
using Day1Homework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpectedObjects;
using FluentAssertions;
using FluentAssertions.Common;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Day1Homework.Tests
{
    [TestClass()]
    public class GroupSumTests
    {
        private static List<Product> _data;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _data = new List<Product>
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

        [TestMethod]
        public void GroupSum_By_Cost_3_Items_Should_Return_Correct_Summary_Result()
        {
            // Arrange
            var property = "Cost";
            var length = 3;
            var expected = new [] {6,15,24,21 };

            var target = Substitute.For<IGroupHelper>();
            target.GroupSum(_data, property, length).Returns(new[] {6, 15, 24, 21});

            // Act
            var actual = target.GroupSum(_data, property, length);

            // Assert
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void GroupSum_By_Revenue_4_Items_Should_Return_Correct_Summary_Result()
        {
            // Arrange
            var property = "Revenue";
            var length = 4;
            var expected = new[] { 50, 66, 60 };

            var target = Substitute.For<IGroupHelper>();
            target.GroupSum(_data, property, length).Returns(new[] { 50, 66, 60 });

            // Act
            var actual = target.GroupSum(_data, property, length);

            // Assert
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void GroupSum_Property_NotFound_Should_Throw_Exception()
        {
            // Arrange
            var property = "name_not_exist";
            var length = 3;

            var target = Substitute.For<IGroupHelper>();
            target.GroupSum(_data, Arg.Is<string>(x => typeof(Product).GetProperties().All(p => p.Name != property)), length)
                .Throws(new ArgumentException(nameof(property)));

            // Act
            Action actual = () => target.GroupSum(_data, property, length);

            // Assert
            actual.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void GroupSum_With_String_PropertyType_Should_Throw_UnsupportedTypeException()
        {
            // Arrange
            var data = new List<Ball>
            {
                new Ball() {Id = 1, Cost = "10"},
                new Ball() {Id = 2, Cost = "20"},
            };
            var property = "Cost";
            var length = 3;

            var target = Substitute.For<IGroupHelper>();
            target.GroupSum(data, Arg.Is<string>(x => typeof(Ball).GetProperty(property).PropertyType == typeof(string)), length)
                .Throws(new UnsupportedTypeException(nameof(property)));

            // Act
            Action actual = () => target.GroupSum(data, property, length);

            // Assert
            actual.ShouldThrow<UnsupportedTypeException>();
        }

        [TestMethod]
        public void GroupSum_With_Null_Data_Should_Throuw_NullArgumentException()
        {
            // Arrange
            List<Ball> data = null;
            var property = "Cost";
            var length = 3;

            var target = Substitute.For<IGroupHelper>();
            target.GroupSum(data, Arg.Is<string>(x => typeof(Ball).GetProperty(property).PropertyType == typeof(string)), length)
                .Throws(new ArgumentNullException("Data"));

            // Act
            Action actual = () => target.GroupSum(data, property, length);

            // Assert
            actual.ShouldThrow<ArgumentNullException>();
        }
    }


    public interface IGroupHelper
    {
        int[] GroupSum<T>(IList<T> data, string property, int length);
    }

    public class Product
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public decimal Revenue { get; set; }
        public decimal SellPrice { get; set; }
    }
    public class Ball
    {
        public int Id { get; set; }
        public string Cost { get; set; }
    }

    public class UnsupportedTypeException : Exception
    {
        public UnsupportedTypeException(){}
        public UnsupportedTypeException(string message) : base(message) { }
        public UnsupportedTypeException(string message, Exception innerException) :base(message, innerException){ }
    }
}