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
        private readonly BaseRepository<RoomEntity> _context;
        private readonly IMapper _mapper;

        public RoomManager(BaseRepository<RoomEntity> context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IList<RoomEntity> FindRooms(IList<string> rooms)
        {
            var result = new List<RoomEntity>();
            foreach (var item in rooms)
            {
                result.Add(_context.GetById(item, "rooms"));
            }
            return result;

        }

        internal object GetById(string id)
        {
            return _mapper.Map<RoomDTO>(_context.GetById(id, "rooms"));
        }

        internal object GetAll()
        {
            return _mapper.Map<RoomDTO>(_context.GetAll("rooms"));
        }
    }
}
