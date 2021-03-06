﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PettiInn.DAL.Entities.EF5;
using PettiInn.DAL.Manager.Core.Managers;
using PettiInn.SOA.DTO;

namespace PettiInn.DAL.Manager.EF5.Managers
{
    internal class HotelManager : ManagerBase<Hotel>, IHotelManager
    {
        public NHResult<Hotel> Create(HotelDTO dto)
        {
            var hotel = new Hotel
            {
                Name = dto.Name,
                Address = dto.Address,
                Location = dto.Location,
                Sort = dto.Sort
            };

            var mManager = new RoomTypeManager();
            var roomTypes = mManager.GetByIds(dto.RoomTypes.Select(m => m.Id), true);
            hotel.RoomTypes = roomTypes.ToList();

            var result = base.SaveOrUpdate(hotel);

            return result;
        }

        public NHResult<Hotel> Update(HotelDTO dto)
        {
            var hotel = base.GetById(dto.Id);

            hotel.Name = dto.Name;
            hotel.Address = dto.Address;
            hotel.Location = dto.Location;
            hotel.Sort = dto.Sort;

            var mManager = new RoomTypeManager();
            var roomTypes = mManager.GetByIds(dto.RoomTypes.Select(m => m.Id), true);

            hotel.RoomTypes.Clear();

            foreach (var r in roomTypes)
            {
                hotel.RoomTypes.Add(r);
            }

            var result = base.SaveOrUpdate(hotel);
            return result;
        }

        public override NHResult<Hotel> Delete(int id)
        {
            var result = new NHResult<Hotel>();
            var hotel = base.GetById(id);

            if (hotel.RoomBookings.Count > 0)
            {
                result.Errors.Add("该酒店已有订单，无法删除");
            }

            if (result.IsValid)
            {
                hotel.Prices.Clear();
                hotel.Rooms.Clear();
                hotel.RoomTypes.Clear();

                result = base.Delete(id);
            }

            return result;
        }

        internal override DbSet<Hotel> DBSet
        {
            get { return base.DBContext.Hotel; }
        }
    }
}
