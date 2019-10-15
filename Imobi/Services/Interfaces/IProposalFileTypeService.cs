using System.Threading.Tasks;

namespace Imobi.Services.Interfaces
{
    public interface IProposalFileTypeService
    {
        Task<string[]> GetFileTypes();
    }
}