using AutoMapper;
using CarGlassDesafioTecnico.Dto.Usuarios;

namespace CarGlassDesafioTecnico.Infra.Mappers.CarGlassDesafioTecnicoProfile
{
    public class UsuariosProfile : Profile
    {
        public UsuariosProfile()
        {
            CreateMap<Domain.Entities.Usuario, UsuarioCreateDto>().ReverseMap();
            CreateMap<Domain.Entities.Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Domain.Entities.Usuario, UsuarioUpdateDto>().ReverseMap();
            CreateMap<UsuarioCreateDto, Domain.Entities.Usuario>().ReverseMap();
            CreateMap<UsuarioDto, Domain.Entities.Usuario>().ReverseMap();
            CreateMap<UsuarioUpdateDto, Domain.Entities.Usuario>().ReverseMap();

        }
    }
}
