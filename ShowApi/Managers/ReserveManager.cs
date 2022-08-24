using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
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

        public ReserveManager(PerformanceRepository performanceRepository, TicketRepository context, IMapper mapper,IMemoryCache cache)
        {
            _performanceRepository = performanceRepository;
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }
        internal object GetAll()
        {
            throw new NotImplementedException();
        }

        internal object GetById(string id)
        {
            throw new NotImplementedException();
        }

        internal object Delete(string id)
        {
            throw new NotImplementedException();
        }

        internal object Update(object id)
        {
            throw new NotImplementedException();
        }

        internal object SaveTicket(ReserveDto ticket)
        {
            var performanceEntity = _performanceRepository.GetById(ticket.PerformanceId);
            var section = performanceEntity.Sections.FirstOrDefault(x => x.SectionId == ticket.SectionId);
            if (section == null)
                throw new Exception("No se encontro la sección");
            bool soldSeat = false;
            foreach (var item in ticket.Seats)
            {
                soldSeat = section.SoldSeats.Contains(item);
            }
            if (soldSeat)
                return new BaseResponse<string>();
            var theater = _cache.Get<IList<TheaterDTO>>("theater").First(x=>x.Id == performanceEntity.TeatherId);
            var show = _cache.Get<IList<ShowDTO>>("show").First(x=>x.Id == performanceEntity.ShowId);
            var room = _cache.Get<IList<RoomDTO>>("room").ToList().First(x => x.Id == performanceEntity.RoomId).Name;
            var seccion = _cache.Get<IList<SectionDTO>>("section").ToList().First(x => x.Id == ticket.SectionId).Name;
            var result = new TicketDTO
            {
                Date = performanceEntity.Date,
                Seats = ticket.Seats,
                Section = seccion,
                TheatherName = theater.Name,
                Adress = theater.Address,
                Room = room,
                Name = show.Name
            };
            _context.Save(_mapper.Map<TicketEntity>(result));
                performanceEntity.Sections.First(x => x.SectionId == ticket.SectionId).SoldSeats.ToList().AddRange(ticket.Seats);
            _performanceRepository.Update(performanceEntity, performanceEntity.Id);
            return null;

        }
    }
}
