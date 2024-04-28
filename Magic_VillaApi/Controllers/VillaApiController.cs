using Magic_VillaApi.Data;
using Magic_VillaApi.Models;
using Magic_VillaApi.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Magic_VillaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillaApiController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }

        [HttpGet("{id}", Name = "GetVilla")]
        //[ProducesResponseType(200)] -- Show Responses block which type of response back
        //[ProducesResponseType(400)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return villa;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<VillaDTO> AddVilla([FromBody] VillaDTO villa)
        {
            if (VillaStore.villaList.FirstOrDefault(x => x.Name.ToLower() == villa.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Name already exist");
                return BadRequest(ModelState);
            }
            //if (!ModelState.IsValid) [ApiController] is written in controller that's why its not working
            //{
            //    return BadRequest(ModelState);
            //}
            if (villa == null)
            {
                return BadRequest();
            }
            if (villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villa.Id = VillaStore.villaList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villa);
            return CreatedAtRoute(nameof(GetVilla), new { id = villa.Id }, villa);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villa)
        {
            if (villa == null)
            {
                return BadRequest();
            }
            var checkVilla = VillaStore.villaList.FirstOrDefault(x => x.Id == villa.Id);
            if (checkVilla == null)
            {
                return BadRequest();
            }

            checkVilla.Name = villa.Name;
            checkVilla.Occpancy = villa.Occpancy;
            checkVilla.Sqrft = villa.Sqrft;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePatchVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            patchDTO.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
