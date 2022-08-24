using AutoMapper;
using Microsoft.Extensions.Configuration;
using ShowApi.Data.Entities;
using ShowApi.Data.Repositories;
using ShowApi.Models;
using System;
using System.Collections.Generic;

namespace ShowApi.Managers
{
    public class RoomManager
    {
        private readonly IConfiguration _config;
        private readonly IRepository<RoomEntity> _context;
        private readonly IMapper _mapper;
        private readonly SectionManager _sectionManager;

        public RoomManager(BaseRepository<RoomEntity> context, IMapper mapper, SectionManager sectionManager, IConfiguration config)
        {
            _config = config;
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

        internal RoomDTO GetById(string id)
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
                payload.Sections = sections;
            }

            return _mapper.Map<RoomEntity>(_context.Save(payload));

        }

        internal object Update(string id, string name, IList<string> sections)
        {
            var entity = _context.GetById(id);
            var payload = new RoomEntity()
            {
                Id = entity.Id,
                Name = name ?? entity.Name,
                Sections = sections is not null && sections.Count > 0 ? sections : entity.Sections
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
    }
}
