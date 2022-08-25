using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ShowApi.Data.Entities;
using ShowApi.Data.Repositories;
using ShowApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShowApi.Managers
{
    public class ReserveManager
    {
        private readonly PerformanceRepository _performanceRepository;
        private readonly TicketRepository _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _config;

        public ReserveManager(PerformanceRepository performanceRepository, TicketRepository context,
                            IMapper mapper, IMemoryCache cache, IConfiguration config)
        {
            _performanceRepository = performanceRepository;
            _performanceRepository.Table = "performance";
            _context = context;
            _context.Table = "ticket";
            _mapper = mapper;
            _cache = cache;
            _config = config;
        }
        internal object GetAll(string userId, string profile)
        {
            if (profile == "admin")
                return _mapper.Map<IList<TicketDTO>>(_context.GetAll());
            return _mapper.Map<IList<TicketDTO>>(_context.GetByUserId(userId));
        }

        internal BaseResponse<TicketDTO> SaveTicket(ReserveCrudDto ticket, string userId, string userName)
        {
            var result = new BaseResponse<TicketDTO>();
            var performanceEntity = _performanceRepository.GetById(ticket.PerformanceId);
            var section = performanceEntity.Sections.FirstOrDefault(x => x.SectionId == ticket.SectionId);
            if (section == null)
                throw new Exception("No se encontro la sección");
           
            foreach (var item in ticket.Seats)
            {
                var soldSeat = section.SoldSeats.Contains(item);
                var existSeat = section.Seats.Contains(item);
                if (!existSeat)
                    return new BaseResponse<TicketDTO>("409", "Las butacas para esa seccion no existen");
                if (soldSeat)
                    return new BaseResponse<TicketDTO>("409", "Las butacas ya estan ocupados");
            }
            var reserve = new TicketDTO
            {
                Date = performanceEntity.Date,
                Seats = ticket.Seats,
                TheatherName = performanceEntity.TeatherName,
                Adress = performanceEntity.Adress,
                Room = performanceEntity.RoomName,
                Name = performanceEntity.ShowName,
                Section = section.SectionName,
                UserId = userId,
                Username = userName
            };
            result.Data = _mapper.Map<TicketDTO>(_context.Save(_mapper.Map<TicketEntity>(reserve)));
            foreach (var item in ticket.Seats)
            {
                performanceEntity.Sections.First(x => x.SectionId == ticket.SectionId).SoldSeats.Add(item);
            }
            _performanceRepository.Update(performanceEntity, performanceEntity.Id);
            RefreshCache();
            return result;
        }

        public void RefreshCache()
        {
            var result = _mapper.Map<IList<PerformanceDTO>>(_performanceRepository.GetAll());
            _cache.Set("performance", result);
        }
    }
}
