using AutoMapper;
using ShowApi.Data.Entities;
using ShowApi.Data.Repositories;
using ShowApi.Models;
using System;
using System.Collections.Generic;

namespace ShowApi.Managers
{
    public class RoomManager
    {
        private readonly IRepository<RoomEntity> _context;
        private readonly IMapper _mapper;
        private readonly SectionManager _sectionManager;

        public RoomManager(BaseRepository<RoomEntity> context, IMapper mapper, SectionManager sectionManager)
        {
            _context = context;
            _mapper = mapper;
            _sectionManager = sectionManager;
            _context.Table = "rooms";
        }
        public IList<RoomEntity> FindRooms(IList<string> rooms)
        {
            var result = new List<RoomEntity>();
            foreach (var item in rooms)
            {
                result.Add(_context.GetById(item));
            }
            return result;

        }

        internal object GetById(string id)
        {
            return _mapper.Map<RoomDTO>(_context.GetById(id));
        }

        internal IList<RoomDTO> GetAll()
        {
            return _mapper.Map<IList<RoomDTO>>(_context.GetAll());
        }


        internal RoomEntity SaveRoom(string name, IList<string> sections)
        {
            var payload = new RoomEntity()
            {
                Name = name,
            };
            if (sections is not null && sections.Count > 0)
            {
                payload.Sections = _sectionManager.FindSections(sections);
            }

            return _mapper.Map<RoomEntity>(_context.Save(payload));

        }

    }
}
