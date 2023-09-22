using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TemplateTrack.API.Controllers.AssetBatchOperation;
using TemplateTrack.Core.Interface.BatchAssetOp;
using TemplateTrack.DataAccess.Model.BatchAsset;

namespace TemplateTrack.Test.TestCase.TestController
{

    public class AssetBatchControllerTests
    {

        [Fact]
        public async Task GetAllAsset_ReturnsOkResult()
        {
            // Arrange
            var expectedAssetBatches = new List<AssetBatch>();

            var mockBatchAssetService = new Mock<IBatchAsset>();
            mockBatchAssetService.Setup(service => service.GetAllBatchAsset())
                                 .ReturnsAsync(expectedAssetBatches);

            var controller = new AssetBatchController(null, mockBatchAssetService.Object);

            // Act
            var result = await controller.getAllAsset();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddAssetBatch_ReturnsOkResult()
        {
            // Arrange
            var assetBatchList = new List<AssetBatch>
            {
            new AssetBatch { BatchId = 1,AssetName="HD",BatchName="1",SerialNumber=1,TagId=1},
            new AssetBatch { BatchId = 1,AssetName="HD",BatchName="1",SerialNumber=1,TagId=5}

            };

            // Mock your IBatchAsset service
            var mockBatchAssetService = new Mock<IBatchAsset>();
            mockBatchAssetService.Setup(service => service.addAsseBatch(assetBatchList))
                                 .ReturnsAsync(assetBatchList.Count);

            // Create an instance of AssetBatchController and inject the mock service
            var controller = new AssetBatchController(null, mockBatchAssetService.Object);

            // Act
            var result = await controller.addAsseBatch(assetBatchList);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result); 

            var okResult = result as OkObjectResult;
            var returnValue = okResult.Value;

            // Assert that the return value matches the count of added AssetBatch objects
            Assert.Equal(assetBatchList.Count, returnValue);
        }

        [Fact]
        public async Task AddAssetBatchSize_ReturnsOkResult()
        {
            // Arrange
            var assetBatchList = new List<AssetBatch>
            {
            new AssetBatch {AssetName="HD",BatchName="1",SerialNumber=1,TagId=0},
            new AssetBatch {AssetName="HD",BatchName="1",SerialNumber=1,TagId=0}

            };

            // Mock your IBatchAsset service
            var mockBatchAssetService = new Mock<IBatchAsset>();
            mockBatchAssetService.Setup(service => service.BatchAsset(assetBatchList))
                                 .ReturnsAsync("SomeStringResult");

            // Create an instance of AssetBatchController and inject the mock service
            var controller = new AssetBatchController(null, mockBatchAssetService.Object);

            // Act
            var result = await controller.BatchAsset(assetBatchList);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Check_Tagid_0()
        {
            // Arrange
            var assetBatchList = new List<AssetBatch>
            {
            new AssetBatch {AssetName="HD",BatchName="1",SerialNumber=1,TagId=0},
            new AssetBatch {AssetName="HD",BatchName="1",SerialNumber=1,TagId=0}

            };

            // Mock your IBatchAsset service
            var mockBatchAssetService = new Mock<IBatchAsset>();
            mockBatchAssetService.Setup(service => service.BatchAsset(assetBatchList))
                                 .ReturnsAsync("SomeStringResult");

            // Create an instance of AssetBatchController and inject the mock service
            var controller = new AssetBatchController(null, mockBatchAssetService.Object);

            // Act
            var result = await controller.BatchAsset(assetBatchList);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

    }
}