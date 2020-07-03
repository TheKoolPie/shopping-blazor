using Moq;
using Shopping.Shared.Data.Abstractions;
using Shopping.Shared.Services.Implementations;
using Shopping.Shared.UnitTests.TestData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Xunit;

namespace Shopping.Shared.UnitTests.Services
{
    public class ShoppingRepoBackupTests : IDisposable
    {
        private DirectoryInfo GetCurrentDirectory
        {
            get
            {
                return new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).Directory;
            }
        }

        private string TargetFileName => "dataset.json";
        private string TargetFilePath => Path.Combine(GetCurrentDirectory.FullName, TargetFileName);

        public ShoppingRepoBackupTests()
        {
            if (File.Exists(TargetFilePath))
            {
                File.Delete(TargetFilePath);
            }
        }
        public void Dispose()
        {
            if (File.Exists(TargetFilePath))
            {
                File.Delete(TargetFilePath);
            }
        }

        [Fact]
        public async Task ExportDataJsonAsync_SerializeDataSet_FileExists()
        {
            var testData = TestDataRepo.GetDataSet();

            var repoMock = new Mock<IShoppingDataRepository>();
            repoMock.Setup(r => r.Categories.ToList()).Returns(testData.Data.Categories);

            var backupService = new ShoppingRepoBackup(repoMock.Object);

            await backupService.ExportDataJsonAsync(TargetFilePath);

            bool targetFileWasCreated = File.Exists(TargetFilePath);

            Assert.True(targetFileWasCreated);
        }
        [Fact]
        public async Task ImportDataJsonAsync_DeserializeData_DeserializedDataMatchesExported()
        {
            var testData = TestDataRepo.GetDataSet();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            var jsonString = JsonSerializer.Serialize(testData.Data, options);

            await File.WriteAllTextAsync(TargetFilePath, jsonString);

            var standardRepo = new StandardShoppingDataRepo();

            var backupService = new ShoppingRepoBackup(standardRepo);

            await backupService.ImportDataJsonAsync(TargetFilePath);

            Assert.Equal(testData.ExpectedCategoryCount, standardRepo.Categories.Count);
        }


    }
}
