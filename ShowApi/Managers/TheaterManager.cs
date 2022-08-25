using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;
        private readonly IRepository<TheaterEntity> _context;
        private readonly IMapper _mapper;
        private readonly RoomManager _roomManager;

        public TheaterManager(BaseRepository<TheaterEntity> context, IMapper mapper, RoomManager roomManager, IConfiguration config)
        {
            _config = config;
            _context = context;
            _mapper = mapper;
            _roomManager = roomManager;
            _context.Table = "theater";
        }

        public IList<TheaterDTO> GetAll()
        {
            return _mapper.Map<IList<TheaterDTO>>(_context.GetAll());
        }

        internal TheaterDTO GetById(string id)
        {
            return _mapper.Map<TheaterDTO>(_context.GetById(id));
        }


        internal TheaterDTO SaveTheater(TheaterCrudDTO dto)
        {
            return _mapper.Map<TheaterDTO>(_context.Save(_mapper.Map<TheaterEntity>(dto)));
            
        }

        internal object Delete(string id)
        {
            var test = _context.Delete(id);
            return new BaseResponse<PerformanceDTO>(_config.GetValue<string>("Response:Delete:Ok:Code"),
                                                    _config.GetValue<string>("Response:Delete:Ok:Message"));
        }

        internal object Update(string id, TheaterCrudDTO dto)
        {
            var entity = _context.GetById(id);
            var payload = new TheaterEntity
            {
                Id = entity.Id,
                Name = !string.IsNullOrWhiteSpace(dto.Name) ? dto.Name: entity.Name,
                Address = !string.IsNullOrWhiteSpace(dto.Address) ? dto.Address : entity.Address,
                Description = !string.IsNullOrWhiteSpace(dto.Description) ? dto.Description:entity.Description,
                Province = !string.IsNullOrWhiteSpace(dto.Province) ? dto.Province:entity.Province,
                Rooms = dto.Rooms is not null || dto.Rooms.Count > 0 ? dto.Rooms : entity.Rooms,
            };
            _context.Update(payload, id);
            return null;
        }
    }

}
