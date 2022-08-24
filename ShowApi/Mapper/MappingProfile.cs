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
            CreateMap<TheaterDTO, TheaterEntity>();
            CreateMap<RoomEntity, RoomDTO>();
            CreateMap<ShowEntity, ShowDTO>();
            CreateMap<CrudShowDTO, ShowEntity>();
            CreateMap<SectionEntity, SectionDTO>();
            CreateMap<SectionDTO, SectionEntity>();
            CreateMap<PerformanceDTO, PerformanceEntity>();
            CreateMap<PerformanceEntity, PerformanceDTO>();
            CreateMap<PerformanceEntity, PerformanceCrudDTO>();
            CreateMap<PerformanceCrudDTO, PerformanceEntity>();
            CreateMap<PerformanceSectionPriceDTO, SectionByPrice>();
            CreateMap<SectionByPrice, PerformanceSectionPriceDTO>();
            CreateMap<TheaterCrudDTO, TheaterEntity>();
            CreateMap<TheaterEntity, TheaterCrudDTO>();

        }
    }
}
