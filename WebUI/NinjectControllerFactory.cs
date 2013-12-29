using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;
using WebUI.Repositories;

namespace WebUI
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            _ninjectKernel.Bind<IReceiptRepository>().To<ReceiptRepository>();

            //    Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //    mock.Setup(m => m.Products)
            //        .Returns(new List<Product>
            //                    {
            //                        new Product { Name = "Football", Price = 25 },
            //                        new Product { Name = "Surf board", Price = 179 },
            //                        new Product { Name = "Running shoes", Price = 95 }
            //                    }.AsQueryable()
            //                );

            //    ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);


            //eg:
            //EmailSettings emailSettings = new EmailSettings
            //{
            //    //value is set in web.config file: ConfigurationManager.AppSettings["Email.WriteAsFile"] 
            //    WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            //};
            //_ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);
        }
    }
}