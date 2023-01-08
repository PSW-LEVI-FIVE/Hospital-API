using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalLibrary.Advertisement;
using HospitalLibrary.Advertisement.Dtos;
using HospitalLibrary.Advertisement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Public
{
    [Route("api/public/advertisement")]
    [ApiController]
    public class AdvertisementController:ControllerBase
    {
        private IAdvertisementService _advertisementService;
        
        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }
        
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllAdverisement()
        {
            IEnumerable<Advertisement> advertisements = await _advertisementService.GetAll();
            IEnumerable<AdvertisementDTO> advertisementDTOs = Array.Empty<AdvertisementDTO>();
            foreach (Advertisement advertisement in advertisements)
            {
                AdvertisementDTO advertisementDTO = new AdvertisementDTO(advertisement.Title, advertisement.Text, advertisement.PictureUrl);
                advertisementDTOs = advertisementDTOs.Append(advertisementDTO);
            }
            return Ok(advertisementDTOs);
        }
    }
}