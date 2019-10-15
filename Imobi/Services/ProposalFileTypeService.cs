using System.Threading.Tasks;
using Imobi.Services.Interfaces;

namespace Imobi.Services
{
    public class ProposalFileTypeService : IProposalFileTypeService
    {
        public async Task<string[]> GetFileTypes()
        {
            await Task.Delay(500);

            return new string[] { "Rg", "CPF", "CNH", "Holerite" };
        }
    }
}