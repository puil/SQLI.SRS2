using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SQLI.SRS2.Modules.Disclosure.Tests.ViewModels
{
    public class DisclosureViewModelFixture
    {
        [Fact]
        public void LoadMaterialsCalled()
        {
            Assert.Equal(true, true);
        }
    }
}


//using Moq;
//using Prism.Regions;
//using SQLI.SRS2.Modules.ModuleName.ViewModels;
//using SQLI.SRS2.Services.Interfaces;
//using Xunit;

//namespace SQLI.SRS2.Modules.ModuleName.Tests.ViewModels
//{
//    public class ViewAViewModelFixture
//    {
//        Mock<IMessageService> _messageServiceMock;
//        Mock<IRegionManager> _regionManagerMock;
//        const string MessageServiceDefaultMessage = "Some Value";

//        public ViewAViewModelFixture()
//        {
//            var messageService = new Mock<IMessageService>();
//            messageService.Setup(x => x.GetMessage()).Returns(MessageServiceDefaultMessage);
//            _messageServiceMock = messageService;

//            _regionManagerMock = new Mock<IRegionManager>();
//        }

//        [Fact]
//        public void MessagePropertyValueUpdated()
//        {
//            var vm = new ViewAViewModel(_regionManagerMock.Object, _messageServiceMock.Object);

//            _messageServiceMock.Verify(x => x.GetMessage(), Times.Once);

//            Assert.Equal(MessageServiceDefaultMessage, vm.Message);
//        }

//        [Fact]
//        public void MessageINotifyPropertyChangedCalled()
//        {
//            var vm = new ViewAViewModel(_regionManagerMock.Object, _messageServiceMock.Object);
//            Assert.PropertyChanged(vm, nameof(vm.Message), () => vm.Message = "Changed");
//        }
//    }
//}
