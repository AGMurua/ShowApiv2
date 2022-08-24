using AutoMapper;
using Microsoft.Extensions.Configuration;
using ShowApi.Data.Entities;
using ShowApi.Data.Repositories;
using ShowApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IList<PerformanceDTO> GetByShowId(string id)
        {
            return _mapper.Map<IList<PerformanceDTO>>(_context.GetByShowId(id));
        }

        public BaseResponse<PerformanceDTO> SaveNewPerformance(PerformanceCrudDTO dto, bool samePrice, decimal? price = null)
        {
            var payload = _mapper.Map<PerformanceEntity>(dto);
            var result = new BaseResponse<PerformanceDTO>();
            if (samePrice && price == null)
            {
                return new BaseResponse<PerformanceDTO>(_config.GetValue<string>("Response:Save:Bad:Code"),
                                                        _config.GetValue<string>("Response:Save:Bad:Message"));
            }
            if (samePrice && price != null)
            {
                payload = setSamePrice(price, payload, dto);
            }
            else
            {
                payload = setAvergaPrices(dto, payload);
            }

            result.Data = _mapper.Map<PerformanceDTO>(_context.Save(payload));
            return result;
        }

        private PerformanceEntity setAvergaPrices(PerformanceCrudDTO dto, PerformanceEntity payload)
        {
            payload.LowestPrice = (decimal)dto.Sections.Min(x => x.Price);
            payload.HighestPrice = (decimal)dto.Sections.Max(x => x.Price);
            return payload;
        }

        private PerformanceEntity setSamePrice(decimal? price, PerformanceEntity payload, PerformanceCrudDTO dto)
        {
            foreach (var item in payload.Sections)
            {
                item.Price = (decimal)price;
            }
            payload.LowestPrice = (decimal)price;
            payload.HighestPrice = (decimal)price;
            return payload;
        }

        public BaseResponse<PerformanceDTO> Delete(string id)
        {
            var test = _context.Delete(id);
            var test2 = _config.GetValue<string>("Response");
            return new BaseResponse<PerformanceDTO>(_config.GetValue<string>("Response:Delete:Ok:Code"),
                                                    _config.GetValue<string>("Response:Delete:Ok:Message"));
        }
    }
}
