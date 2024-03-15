using Moq;
using NUnit.Framework;
using simple_online_book_catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimOnlineBook.Models.models.DomainModel
{
    [TestFixture]
    public class AutherDomainModelTest
    {
        [Test]
        public void autherName_TestPrperety_ShouldReturnString() 
        {
            //Arrange
            var autherObj = new Authors();
            var moqObj = new Mock<Authors>();

            //Act
            moqObj.Setup(x => x.Name).Returns(autherObj.Name);

            //Assert
            Assert.That(moqObj.Object.Name, Is.EqualTo(autherObj.Name));
        }

    }
}
