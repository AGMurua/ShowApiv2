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
    public class ShowManager
    {
        private readonly IRepository<ShowEntity> _context;
        private readonly IMapper _mapper;

        public ShowManager(BaseRepository<ShowEntity> context, IMapper mapper)
        {
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
    }
}
