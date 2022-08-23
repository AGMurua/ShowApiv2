using AutoMapper;
using Microsoft.Extensions.Configuration;
using ShowApi.Data.Entities;
using ShowApi.Data.Repositories;
using ShowApi.Models;
using System.Collections.Generic;

namespace ShowApi.Managers
{
    public class PerformanceManager
    {
        private readonly PerformanceRepository _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public PerformanceManager(PerformanceRepository context, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _context.Table = "performance";
            _config = config;
        }

        public IList<PerformanceDTO> GetAll()
        {
            return _mapper.Map<IList<PerformanceDTO>>(_context.GetAll());
        }

        public PerformanceDTO GetById(string id)
        {
            return _mapper.Map<PerformanceDTO>(_context.GetById(id));
        }

        public IList<PerformanceDTO> GetByShowId (string id)
        {
           return  _mapper.Map<IList<PerformanceDTO>>(_context.GetByShowId(id));
        }

        public PerformanceDTO SaveNewPerformance(PerformanceCrudDTO dto)
        {
            return _mapper.Map<PerformanceDTO>(_context.Save(_mapper.Map<PerformanceEntity>(dto)));
        }

        public BaseResponse Delete(string id)
        {
            var test = _context.Delete(id);
            var test2 = _config.GetValue<string>("Response");
            return new BaseResponse(_config.GetValue<string>("Response:Delete:Code"), _config.GetValue<string>("Response:Delete:Message"));
        }
    }
}
