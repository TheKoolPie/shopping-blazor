using Shopping.Shared.Model.Serialization;
using System.Threading.Tasks;

namespace Shopping.Shared.Services.Interfaces
{
    public interface IShoppingRepoBackup
    {
        Task ImportDataJsonAsync(string filePath);
        Task ImportDataAsync(ShoppingDataSerializationModel data);
        Task ExportDataJsonAsync(string filePath);
    }
}
