using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PartnerGroup.Domain.Models;
using PartnerGroup.Domain.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PartnerGroup.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace PartnerGroup.Test.PartnerGroup.API.Controllers
{
    [TestClass]
    public class MarcaControllerTest
    {
        private Mock<IGenericService<Marca>> _svc;
        private MarcaController _controller;

        [TestInitialize]
        public void Setup()
        {
            _svc = GetMarcaSvcMock();
            _controller = new MarcaController(_svc.Object);
        }

        [TestMethod]
        public void CreateNoErrors()
        {
            var result = _controller.Add(It.IsAny<Marca>());
            var okObjectResult = result as OkObjectResult;
            var apiResult = okObjectResult.Value as APIReturn;
            var marca = JsonConvert.DeserializeObject<Marca>(apiResult.Content);


            Assert.IsNotNull(okObjectResult);
            Assert.IsNotNull(marca);
            Assert.IsTrue(apiResult.StatusCode == 200);

        }

        [TestMethod]
        public void UpdateNoErrors()
        {
            var result = _controller.Uptade(It.IsAny<Marca>(), It.IsAny<int>());
            var okObjectResult = result as OkObjectResult;
            var apiResult = okObjectResult.Value as APIReturn;

            Assert.IsNotNull(okObjectResult);
            Assert.IsNotNull(apiResult);
            Assert.IsTrue(apiResult.StatusCode == 200);

        }


        [TestMethod]
        public void DeleteNoErrors()
        {
            var result = _controller.Delete(It.IsAny<int>());
            var okObjectResult = result as OkObjectResult;
            var apiResult = okObjectResult.Value as APIReturn;

            Assert.IsNotNull(okObjectResult);
            Assert.IsNotNull(apiResult);
            Assert.IsTrue(apiResult.StatusCode == 200);

        }


        [TestMethod]
        public void GetAllNoErrors()
        {
            var result = _controller.GetAll();
            var okObjectResult = result as OkObjectResult;
            var apiResult = okObjectResult.Value as APIReturn;
            var list = JsonConvert.DeserializeObject<List<Marca>>(apiResult.Content);

            Assert.IsNotNull(okObjectResult);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(apiResult.StatusCode == 200);

        }

        [TestMethod]
        public void GetByIdNoErrors()
        {
            var result = _controller.GetById(It.IsAny<int>());
            var okObjectResult = result as OkObjectResult;
            var apiResult = okObjectResult.Value as APIReturn;
            var marca = JsonConvert.DeserializeObject<Marca>(apiResult.Content);


            Assert.IsNotNull(okObjectResult);
            Assert.IsNotNull(marca);
            Assert.IsTrue(apiResult.StatusCode == 200);

        }

        private Mock<IGenericService<Marca>> GetMarcaSvcMock()
        {
            Mock<IGenericService<Marca>> _mock = new Mock<IGenericService<Marca>>();



            _mock.Setup(x => x.Create(It.IsAny<Marca>())).Returns(
                new APIReturn
                {
                    Content = JsonConvert.SerializeObject(new Marca
                    {
                        Id = 1,
                        Nome = "TestCreate"
                    }),
                    StatusCode = 200,
                    Message = "Criado com Sucesso"
                }
                );

            _mock.Setup(x => x.Delete(It.IsAny<int>())).Returns(
                new APIReturn
                {
                    Content = true,
                    StatusCode = 200,
                    Message = "Deletado com Sucesso"
                }
                );

            _mock.Setup(x => x.Update(It.IsAny<Marca>(), It.IsAny<int>())).Returns(
                new APIReturn
                {
                    Content = JsonConvert.SerializeObject(new Marca
                    {
                        Id = 1,
                        Nome = "TestUptade"
                    }),
                    StatusCode = 200,
                    Message = "Atualizado com Sucesso"
                }
                );


            _mock.Setup(x => x.GetSingle(It.IsAny<int>())).Returns(
                new APIReturn
                {
                    Content = JsonConvert.SerializeObject(new Marca
                    {
                        Id = 1,
                        Nome = "TestGetSingle"
                    }),
                    StatusCode = 200,
                    Message = "Registro retornado com Sucesso"
                }
                );


            _mock.Setup(x => x.GetAll()).Returns(
                new APIReturn
                {
                    Content = JsonConvert.SerializeObject(new List<Marca> {
                    new Marca
                    {
                        Id = 1,
                        Nome = "TestUptade"
                    }}),
                    StatusCode = 200,
                    Message = "Atualizado com Sucesso"
                }
                );



            return _mock;
        }


        private Marca GetValidMarca()
        {
            Marca marca = new Marca();
            marca.Id = 1;
            marca.Nome = "Teste";

            return marca;
        }
    }
}
