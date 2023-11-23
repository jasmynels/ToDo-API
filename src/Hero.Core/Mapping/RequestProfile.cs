using AutoMapper;
using Core.Commands.ToDos;
using Core.Commands.Usuarios;
using Core.Entities.ToDos;
using Core.Entities.Usuarios;
using Core.Models.Requests;

namespace Core.Mapping
{
    public class ResquestProfile : Profile
    {
        public ResquestProfile()
        {

            CreateMap<CreateUsuarioCommand, Usuario>();
            CreateMap<CreateToDoCommand, ToDo>();
            CreateMap<UpdateToDoCommand, ToDo>();
            CreateMap<UpdateToDoRequest, ToDo>();
        }
    }
}