using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    //[Route("api/[controller]")] //If we are using [controller] and controller name changes in future,all the API endpoints of this controller gets changed which might be consumed by others
    [Route("api/VillaAPI")] //Hardcoding the controller name,doesn't changes the API endpoints even if our controller name changes in future
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        public ILogger<VillaDTO> _logger { get; }

        public VillaAPIController(ILogger<VillaDTO> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("Get all villa details");
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
                _logger.LogError("Error while fetching villa with Id: " + id);
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
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState); //returns validation message in response, commenting as [ApiController] provides same functionality built in 
            //}

            //add  Custom ModelState Validation
            if (VillaStore.VillaList.FirstOrDefault(x => x.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already exists!");
                return BadRequest(ModelState);
            }

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

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.VillaList.Remove(villa);
            return NoContent();
        }
        
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if(villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }

            var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);

            villa.Name = villaDTO.Name;
            villa.Occupancy = villaDTO.Occupancy;
            villa.Sqft = villaDTO.Sqft;
            return NoContent();
        }
        
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id,JsonPatchDocument<VillaDTO> patchDTO)
        {
            if(patchDTO == null || id == null)
            {
                return BadRequest();
            }

            var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            patchDTO.ApplyTo(villa,ModelState);
            if (!ModelState.IsValid) 
            { 
                return BadRequest();
            }
            return NoContent();
        }

    }
}
