using AutoMapper;
using ShowApi.Data.Entities;
using ShowApi.Models;
using System.Collections.Generic;

namespace ShowApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<TheaterEntity, TheaterDTO>();
            CreateMap<RoomEntity, RoomDTO>();
            CreateMap<SectionEntity, SectionDTO>();
            CreateMap<ShowEntity, ShowDTO>();
            CreateMap<TheaterDTO, TheaterEntity>();
            //CreateMap<IList<TheaterEntity>, IList<TheaterDTO>>();
        }
    }
}
