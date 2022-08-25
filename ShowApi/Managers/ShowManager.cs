using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memory;

        public ShowManager(BaseRepository<ShowEntity> context, IMapper mapper, IConfiguration config, IMemoryCache cache)
        {
            _config = config;
            _context = context;
            _mapper = mapper;
            _memory = cache;
            _context.Table = "show";
        }

        internal object GetAll()
        {
            var cache = _memory.Get("show");
            if (cache is null)
            {
                var result = _mapper.Map<IList<ShowDTO>>(_context.GetAll());
                _memory.Set("show", result);
                return result;
            }
            return cache;
        }

        internal ShowDTO SaveNewShow(CrudShowDTO show)
        {
            var result = _mapper.Map<ShowDTO>(_context.Save(_mapper.Map<ShowEntity>(show)));
            RefreshCache();
            return result;
        }

        internal ShowDTO GetById(string id)
        {
            return _mapper.Map<ShowDTO>(_context.GetById(id));
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
            RefreshCache();
            return null;
        }

        internal object Delete(string id)
        {
            var test = _context.Delete(id);
            RefreshCache();
            return new BaseResponse<PerformanceDTO>(_config.GetValue<string>("Response:Delete:Ok:Code"),
                                                    _config.GetValue<string>("Response:Delete:Ok:Message"));
        }

        private void RefreshCache()
        {
            var result = _mapper.Map<IList<ShowDTO>>(_context.GetAll());
            _memory.Set("show", result);
        }
    }
}
