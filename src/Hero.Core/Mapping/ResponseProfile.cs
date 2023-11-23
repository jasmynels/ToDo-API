using AutoMapper;
using Core.Entities.ToDos;
using Core.Entities.Usuarios;
using Core.Models.Responses;

namespace Core.Mapping
{
    public class ResponseProfile : Profile
    {
        public ResponseProfile()
        {
            CreateMap<Usuario, UsuarioResponse>();
            CreateMap<ToDo, ToDoResponse>();
        }
    }
}