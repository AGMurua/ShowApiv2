using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ShowApi.Data.Entities;
using ShowApi.Data.Repositories;
using ShowApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowApi.Managers
{
    public class PerformanceManager
    {
        private readonly PerformanceRepository _context;
        private readonly IMapper _mapper;
        private readonly SectionManager _sectionManager;
        private readonly IConfiguration _config;
        private readonly IMemoryCache _memory;
        private readonly ShowManager _showManager;
        private readonly TheaterManager _theaterManager;
        private readonly RoomManager _roomManager;

        public PerformanceManager(PerformanceRepository context, IMapper mapper, SectionManager sectionManager,
                                IConfiguration config, IMemoryCache memory, TheaterManager theaterManager, RoomManager roomManager,
                                ShowManager showManager)
        {
            _context = context;
            _mapper = mapper;
            _sectionManager = sectionManager;
            _context.Table = "performance";
            _config = config;
            _memory = memory;
            _showManager = showManager;
            _theaterManager = theaterManager;
            _roomManager = roomManager;
        }


        public IList<PerformanceDTO> GetAll()
        {
            var cache = _memory.Get<IList<PerformanceDTO>>("performance");
            if (cache is null)
            {
                var result = _mapper.Map<IList<PerformanceDTO>>(_context.GetAll());
                _memory.Set("performance", result);
                return result;
            }
            return cache;
        }

        public PerformanceDTO GetById(string id)
        {
            return _mapper.Map<PerformanceDTO>(_context.GetById(id));
        }

        internal IList<PerformanceDTO> GetByFilter(decimal? minPrice, decimal? maxPrice,
                                    DateTime? minDate, DateTime? maxDate, IList<string> cast, string genre)
        {
            var performances = _memory.Get<IList<PerformanceDTO>>("performance");
            if (performances is null)
                performances = this.GetAll();
            if (minPrice is not null)
                performances = performances.Where(x => x.HighestPrice >= minPrice).ToList();
            if (maxPrice is not null)
                performances = performances.Where(x => x.LowestPrice <= maxPrice).ToList();
            if(minDate is not null)
                performances = performances.Where(x => x.Date >= minDate).ToList();
            if (maxDate is not null)
                performances = performances.Where(x => x.Date <= maxDate).ToList();
            if (cast is not null && cast.Count > 0)
            {
                foreach (var item in cast)
                {
                    performances = performances.Where(x => _showManager.GetById(x.ShowId).Cast.Contains(item)).ToList();
                }
            }
            if (genre is not null)
                performances = performances.Where(x => _showManager.GetById(x.ShowId).Genre == genre).ToList();

            return performances;
        }

        public IList<PerformanceDTO> GetByShowId(string id)
        {
            return _mapper.Map<IList<PerformanceDTO>>(_context.GetByShowId(id));
        }

        public BaseResponse<PerformanceDTO> SaveNewPerformance(PerformanceCrudDTO dto, bool samePrice, decimal? price = null)
        {
            var theaterData = findTheather(dto.TeatherId);
            var roomData = findRoom(dto.RoomId);
            var showData = findShow(dto.ShowId);
            var payload = new PerformanceEntity
            {
                Date = dto.Date,
                TeatherName = theaterData.Name,
                Adress = theaterData.Address,
                ShowId = dto.ShowId,
                RoomName = roomData.Name,
                ShowName = showData.Name,
            };
            var result = new BaseResponse<PerformanceDTO>();
            if (samePrice && price == null)
            {
                return new BaseResponse<PerformanceDTO>(_config.GetValue<string>("Response:Save:Bad:Code"),
                                                        _config.GetValue<string>("Response:Save:Bad:Message"));
            }
            payload.Sections = findSections(dto.Sections);
            if (samePrice && price != null)
            {
                payload = setSamePrice(price, payload, dto);
            }
            else
            {
                payload = setAvergaPrices(dto, payload);
            }
            result.Data = _mapper.Map<PerformanceDTO>(_context.Save(payload));
            RefreshCache();
            return result;
        }

        private ShowDTO findShow(string showId)
        {
            return _showManager.GetById(showId);
        }

        internal object Update(string id, PerformanceCrudDTO dto)
        {
            RefreshCache();
            throw new NotImplementedException();
        }
        internal BaseResponse<PerformanceDTO> Delete(string id)
        {
            var test = _context.Delete(id);
            RefreshCache();
            return new BaseResponse<PerformanceDTO>(_config.GetValue<string>("Response:Delete:Ok:Code"),
                                                    _config.GetValue<string>("Response:Delete:Ok:Message"));
        }

        private IList<SectionByPrice> findSections(IList<PerformanceSectionPriceDTO> sections)
        {
            var sectionParse = new List<SectionByPrice>();
            foreach (var item in sections)
            {
                var result = new SectionByPrice();
                var sectionData = _sectionManager.GetById(item.SectionId);
                result.Seats = sectionData.Seat;
                result.Price = (decimal)item.Price;
                result.SectionId = item.SectionId;
                result.SectionName = sectionData.Name;
                result.SoldSeats = new List<string>();
                sectionParse.Add(result);
            }
            return sectionParse;
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
        private RoomDTO findRoom(string roomId)
        {
            return _roomManager.GetById(roomId);
        }

        private TheaterDTO findTheather(string teatherId)
        {
            return _theaterManager.GetById(teatherId);
        }
        private void RefreshCache()
        {
            var result = _mapper.Map<IList<PerformanceDTO>>(_context.GetAll());
            _memory.Set("performance", result);
        }


    }
}
