using AutoMapper;
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

        public SectionManager(BaseRepository<SectionEntity> context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _context.Table = "section";
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

        internal SectionDTO SaveSection(string name, IList<string> seat)
        {
            var payload = new SectionEntity
            {
                Name = name,
                Seat = seat
            };
            return _mapper.Map<SectionDTO>(_context.Save(payload));
        }

        internal SectionDTO UpdateSection(SectionDTO dto)
        {
            var entity = _context.GetById(dto.Id);
            var payload = new SectionEntity
            {
                Name = dto.Name ?? entity.Name,
                Seat = dto.Seat ?? entity.Seat,
                Id = entity.Id,
            };

            return _mapper.Map<SectionDTO>(_context.Save(payload));
        }
    }
}
