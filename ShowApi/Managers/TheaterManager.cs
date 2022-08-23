using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using ShowApi.Data.Entities;
using ShowApi.Data.Repositories;
using ShowApi.Models;
using System;
using System.Collections.Generic;

namespace ShowApi.Managers
{
    public class TheaterManager
    {
        private readonly IRepository<TheaterEntity> _context;
        private readonly IMapper _mapper;
        private readonly RoomManager _roomManager;

        public TheaterManager(BaseRepository<TheaterEntity> context, IMapper mapper, RoomManager roomManager)
        {
            _context = context;
            _mapper = mapper;
            _roomManager = roomManager;
            _context.Table = "theater";
        }

        public IList<TheaterDTO> GetAll()
        {
            return _mapper.Map<IList<TheaterDTO>>(_context.GetAll());
        }

        internal object GetById(string id)
        {
            return _mapper.Map<TheaterDTO>(_context.GetById(id));
        }

        public void Post()
        {

        }

        internal void DataFeed(TheaterDTO dto)
        {

        }

        internal TheaterDTO SaveTheater(string name, IList<string> rooms)
        {
            var payload = new TheaterEntity()
            {
                Name = name,
            };
            if (rooms is not null && rooms.Count > 0)
            {
                payload.Rooms = _roomManager.FindRooms(rooms);
            }

            return _mapper.Map<TheaterDTO>(_context.Save(payload));
            
        }

        internal object SaveSection(string name, IList<string> seats, string roomId)
        {
            throw new NotImplementedException();
        }

        internal object SaveRoom(string name, IList<string> sections, string theaterId)
        {
            throw new NotImplementedException();
        }
    }

}
