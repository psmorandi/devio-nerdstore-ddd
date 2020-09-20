namespace PsmjCo.NerdStore.Catalogo.Application.AutoMapper
{
    using Domain;
    using global::AutoMapper;
    using ViewModels;

    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            this.CreateMap<Produto, ProdutoViewModel>()
                .ForMember(d => d.Largura, o => o.MapFrom(s => s.Dimensoes.Largura))
                .ForMember(d => d.Altura, o => o.MapFrom(s => s.Dimensoes.Altura))
                .ForMember(d => d.Profundidade, o => o.MapFrom(s => s.Dimensoes.Profundidade));
            ;
            this.CreateMap<Categoria, CategoriaViewModel>();
        }
    }

    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            this.CreateMap<ProdutoViewModel, Produto>()
                .ConstructUsing(
                    p =>
                        new Produto(
                            p.Nome,
                            p.Descricao,
                            p.Ativo,
                            p.Valor,
                            p.CategoriaId,
                            p.DataCadastro,
                            p.Imagem,
                            new Dimensoes(p.Altura, p.Largura, p.Profundidade)));

            this.CreateMap<CategoriaViewModel, Categoria>()
                .ConstructUsing(c => new Categoria(c.Nome, c.Codigo));
        }
    }
}