
using ClassLibrary1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject1
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void WhenStockIsZeroAndFirstOwnerIsAvailable_Purchase_SendsSmsToOwner()
        {
            //arrange
            var mock = new Mock<ISmsGateway>();
            mock.Setup(x => x.SendSms("0712345678", It.IsAny<string>())).Returns(true);

            var vm = new VendingMachine(mock.Object);
            vm.Purchase();
            
            //act
            vm.Purchase();

            //assert
            mock.Verify(x => x.SendSms("0712345678", It.IsAny<string>()), Times.Exactly(1));
        }

        [TestMethod]
        public void WhenStockIsZeroAndFirstOwnerUnavailable_Purchase_SendsSmsToSecondOwner()
        {
            //arrange
            var mock = new Mock<ISmsGateway>();
            mock.Setup(x => x.SendSms("0712345678", It.IsAny<string>())).Returns(false);
            mock.Setup(x => x.SendSms("0722345678", It.IsAny<string>())).Returns(true);
            var vm = new VendingMachine(mock.Object);
            vm.Purchase();

            //act
            vm.Purchase();

            //assert
            mock.Verify(x => x.SendSms("0722345678", It.IsAny<string>()), Times.Exactly(1));
        }

        [ExpectedException(typeof(SmsNotAvailableException))]
        [TestMethod]
        public void WhenStockIsZeroAndBothOwnersUnavailable_Purchase_ThrowsException()
        {
            //arrange
            var mock = new Mock<ISmsGateway>();
            mock.Setup(x => x.SendSms("0712345678", It.IsAny<string>())).Returns(false);
            mock.Setup(x => x.SendSms("0722345678", It.IsAny<string>())).Returns(false);            
            var vm = new VendingMachine(mock.Object);
            vm.Purchase();

            //act
            vm.Purchase();                      
        }
    }
}
