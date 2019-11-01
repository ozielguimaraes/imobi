using Imobi.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imobi.Services
{
    public class MockProposal : IDataStore<ProposalDto>
    {
        private List<ProposalDto> items;

        public MockProposal()
        {
            items = new List<ProposalDto>
            {
                new ProposalDto
                {
                    Number = 98898,
                    Client = "Oziel Guimarães de Paula",
                    Venture= "Jd America",
                    Tower = "Norte",
                    Unity = "833",
                    Conclusion= new Conclusion{
                        PercentageCompletion= 84
                    },
                    Movements = new List<string>{
                        "Montar Pasta - Análise de Documentos",
                        "Montar Pasta - Entrar em Contato"
                    }
                },
                new ProposalDto
                {
                    Number= 123456,
                    Client = "João Felipe Júnior",
                    Venture= "Itaim Bibi",
                    Tower = "Centro",
                    Unity= "658",
                    Conclusion= new Conclusion{
                     PercentageCompletion= 38
                    },
                    Movements = new List<string>{
                        "Montar Pasta - Análise de Documentos",
                        "Montar Pasta - Entrar em Contato"
                    }
                },
                new ProposalDto
                {
                    Number= 123456,
                    Client = "João Felipe Júnior",
                    Venture= "Itaim Bibi",
                    Tower = "Centro",
                    Unity= "658",
                    Conclusion= new Conclusion{
                     PercentageCompletion= 82
                    },
                    Movements = new List<string>{
                        "Montar Pasta - Análise de Documentos",
                        "Montar Pasta - Entrar em Contato"
                    }
                },
                new ProposalDto
                {
                    Number = 937163,
                    Client = "Mário de Souza Costa",
                    Venture= "Itaim Bibi",
                    Tower = "Centro",
                    Unity = "658",
                    Conclusion= new Conclusion{
                     PercentageCompletion= 50
                    },
                    Movements = new List<string>{
                        "Montar Pasta - Análise de Documentos",
                        "Montar Pasta - Entrar em Contato"
                    }
                },
                new ProposalDto
                {
                    Number = 123456,
                    Client = "João Felipe Júnior",
                    Venture= "Itaim Bibi",
                    Tower = "Centro",
                    Unity = "658",
                    Conclusion= new Conclusion{
                     PercentageCompletion = 38
                    },
                    Movements = new List<string>{
                        "Montar Pasta - Análise de Documentos",
                        "Montar Pasta - Entrar em Contato"
                    }
                }
            };
        }

        public async Task<bool> AddItemAsync(ProposalDto item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ProposalDto item)
        {
            var oldItem = items.FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ProposalDto> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault());
        }

        public async Task<IEnumerable<ProposalDto>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}