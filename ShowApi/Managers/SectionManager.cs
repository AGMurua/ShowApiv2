using AutoMapper;
using Microsoft.Extensions.Configuration;
using ShowApi.Data.Entities;
using ShowApi.Data.Repositories;
using ShowApi.Models;
using System;
using System.Collections.Generic;

namespace ShowApi.Managers
{
    public class SectionManager
    {
        private readonly IRepository<SectionEntity> _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public SectionManager(BaseRepository<SectionEntity> context, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _context.Table = "section";
            _config = config;
        }
        public IList<SectionEntity> FindSections(IList<string> sections)
        {
            var result = new List<SectionEntity>();
            foreach (var item in sections)
            {
                result.Add(_context.GetById(item));
            }
            return result;
        }

        internal object GetById(string id)
        {
            return _mapper.Map<SectionDTO>(_context.GetById(id));
        }

        internal object GetAll()
        {
            return _mapper.Map<IList<SectionDTO>>(_context.GetAll());
        }

        internal SectionDTO SaveSection(string name, int seats)
        {

            var payload = new SectionEntity
            {
                Name = name,
                Seat = generateSeats(seats)
            };
            return _mapper.Map<SectionDTO>(_context.Save(payload));
        }

       
        internal SectionDTO UpdateSection(string name, int numberOfSeat,string id)
        {
            var entity = _context.GetById(id);
            var payload = new SectionEntity
            {
                Name = name ?? entity.Name,
                Seat = numberOfSeat != 0 ? generateSeats(numberOfSeat) : entity.Seat,
                Id=entity.Id
                
            };
            _context.Update(payload, id);
            return null;
        }

        internal object Delete(string id)
        {
            var test = _context.Delete(id);
            return new BaseResponse<PerformanceDTO>(_config.GetValue<string>("Response:Delete:Ok:Code"),
                                                    _config.GetValue<string>("Response:Delete:Ok:Message"));
        }

        private IList<string> generateSeats(int seats)
        {
            var seatsList = new List<string>();
            for (int i = 0; i < seats; i++)
            {
                seatsList.Add((i + 1).ToString());
            }
            return seatsList;
        }
    }
}
