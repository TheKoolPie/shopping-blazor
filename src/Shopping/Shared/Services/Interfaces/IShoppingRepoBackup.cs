using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Services.Interfaces
{
    public interface IShoppingRepoBackup
    {
        Task ImportDataJsonAsync(string filePath);
        Task ExportDataJsonAsync(string filePath);
    }
}
