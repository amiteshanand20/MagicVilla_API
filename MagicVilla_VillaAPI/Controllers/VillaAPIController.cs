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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.VillaList);
        } 
        
        [HttpGet("{id:int}", Name = "GetVilla")] //Method expects explicitly "id" parameter of integer type,otherwise swagger won't work
        [ProducesResponseType(StatusCodes.Status200OK)] //Display possible reponse status code on Swagger UI
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa =  VillaStore.VillaList.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);   
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
        {
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if(villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDTO.Id =  VillaStore.VillaList.OrderByDescending( x => x.Id).FirstOrDefault().Id + 1;
            
            VillaStore.VillaList.Add(villaDTO); 
            return CreatedAtRoute("GetVilla",new {id = villaDTO.Id },villaDTO);   // returns URL/Route of newly created record (see "location" field of response headers on Swagger UI)
        }
    }
}
