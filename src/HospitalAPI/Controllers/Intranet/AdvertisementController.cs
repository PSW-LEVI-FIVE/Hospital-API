using System;
using System.Threading.Tasks;
using HospitalLibrary.Advertisement;
using HospitalLibrary.Advertisement.Dtos;
using HospitalLibrary.Advertisement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.Intranet
{
    [Route("api/intranet/advertisement")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private IAdvertisementService _advertisementService;
        
        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }
        
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [Route("create")]
        public async Task<IActionResult> CreateAdvertisement([FromBody] CreateAdvertisementDto createAdvertisementDto)
        {
            Advertisement advertisement = new Advertisement(createAdvertisementDto.Title, createAdvertisementDto.Text,
                createAdvertisementDto.PictureUrl, DateTime.Now);
            _advertisementService.Create(advertisement);
            return Ok(createAdvertisementDto);
        }
    }
}