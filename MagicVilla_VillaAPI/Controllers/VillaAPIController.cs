using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    //[Route("api/[controller]")] //If we are using [controller] and controller name changes in future,all the API endpoints of this controller gets changed which might be consumed by others
    [Route("api/VillaAPI")] //Hardcoding the controller name,doesn't changes the API endpoints even if our controller name changes in future
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDTO> GetVillas()
        {
            return VillaStore.VillaList;
        } 
        
        [HttpGet("{id:int}")] //Method expects explicitly "id" parameter of integer type,otherwise swagger won't work
        public VillaDTO GetVilla(int id)
        {
            return VillaStore.VillaList.FirstOrDefault(x => x.Id == id);
        }
    }
}
