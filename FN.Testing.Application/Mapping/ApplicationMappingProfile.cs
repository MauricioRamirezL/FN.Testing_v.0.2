using AutoMapper;
using FN.Testing.Application.Contract.Models;
using FN.Testing.Business.Contract.Entities;

namespace FN.Testing.Application.Mapping
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateProfile();
        }

        private void CreateProfile()
        {
            CreateMap<UploadEntity, UploadModel>().ReverseMap();
        }
    }
}
