using AutoMapper;
using FN.Testing.Business.Contract.Entities;
using FN.Testing.DataLayer.Contract.Tables;

namespace FN.Testing.Business.Mapping
{
    public class BusinessMappingProfile : Profile
    {
        public BusinessMappingProfile()
        {
            CreateProfile();
        }

        private void CreateProfile()
        {
            CreateMap<Upload, UploadEntity>().ReverseMap();
            CreateMap<Upload, UploadedEntity>().ReverseMap();
        }
    }
}
