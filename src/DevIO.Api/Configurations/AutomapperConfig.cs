using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Business.Models;

namespace DevIO.Api.Configurations
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig() 
        {
            CreateMap<Fornecedor, FornecedorViewModel>()
                .ReverseMap();

            CreateMap<Endereco, EnderecoViewModel>()
                .ReverseMap();

            CreateMap<ProdutoViewModel, Produto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.NomeFornecedor, opt => opt.MapFrom(src => src.Fornecedor!.Nome));
        }
    }
}
