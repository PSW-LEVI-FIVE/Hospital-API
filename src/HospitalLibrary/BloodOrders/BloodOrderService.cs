using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalLibrary.BloodOrders.Dtos;
using HospitalLibrary.BloodOrders.Interfaces;
using HospitalLibrary.Doctors;
using HospitalLibrary.Shared.Interfaces;
using OpenQA.Selenium;

namespace HospitalLibrary.BloodOrders
{
    
    public class BloodOrderService : IBloodOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public BloodOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BloodOrder> Create(BloodOrder bloodOrder)
        {
            _unitOfWork.BloodOrderRepository.Add(bloodOrder);
            _unitOfWork.BloodOrderRepository.Save();
            return bloodOrder;
        }

        public async Task<List<ShowBloodOrderDto>> GetAllBloodOrders()
        {
            IEnumerable<BloodOrder> bloodOrders = await _unitOfWork.BloodOrderRepository.GetAll();
            List<ShowBloodOrderDto> showList = FillDtoList(bloodOrders);
            return showList;
        }

        private List<ShowBloodOrderDto> FillDtoList(IEnumerable<BloodOrder> bloodOrders)
        {
            List<ShowBloodOrderDto> showList = new List<ShowBloodOrderDto>();
            foreach (var order in bloodOrders)
            {
                Doctor doctor = _unitOfWork.DoctorRepository.GetOne(order.DoctorId);
                ShowBloodOrderDto dto = new ShowBloodOrderDto(order, doctor);
                showList.Add(dto);
            }
            return showList;
        }
    }
}