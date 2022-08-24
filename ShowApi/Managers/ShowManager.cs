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
    public class ShowManager
    {
        private readonly IConfiguration _config;
        private readonly IRepository<ShowEntity> _context;
        private readonly IMapper _mapper;

        public ShowManager(BaseRepository<ShowEntity> context, IMapper mapper, IConfiguration config)
        {
            _config = config;
            _context = context;
            _mapper = mapper;
            _context.Table = "show";
        }

        internal IList<ShowDTO> GetAll()
        {
            return _mapper.Map<IList<ShowDTO>>(_context.GetAll());
        }

        internal string SaveNewShow(CrudShowDTO show)
        {
             _context.Save(_mapper.Map<ShowEntity>(show));
            return "";
        }

        internal ShowEntity GetById(string id)
        {
            return _context.GetById(id);
        }
        internal ShowDTO Update(CrudShowDTO dto, string id)
        {
            var entity = _context.GetById(id);
            var payload = new ShowEntity()
            {
                Cast = dto.Cast is not null && dto.Cast.Count > 0 ? dto.Cast : entity.Cast,
                Description = dto.Description ?? entity.Description,
                Genre = dto.Genre ?? entity.Genre,
                MPARating = dto.Genre ?? entity.Genre,
                Name = dto.Name ?? entity.Name,
                Id = id,
            };
            var result = _context.Update(payload, id);
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
