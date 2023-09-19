using APISecurityBasic.Logging;
using APISecurityBasic.Models;
using APISecurityBasic.Models.DTO;
using APISecurityBasic.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace APISecurityBasic.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        // private readonly ILogging _logger;  
       // private readonly ApplicationDBContext _db;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        // public VillaAPIController(ApplicationDBContext db, IMapper mapper)
        // {
        //     _db = db;
        //     _mapper = mapper;
        ////     _logger= logger;
        // }
        public VillaAPIController(IVillaRepository dbVilla, IMapper mapper)
        {
              _dbVilla = dbVilla;
             _mapper = mapper;
            //     _logger= logger;
        }
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            //  _logger.Log("Getting all villa", "");
            IEnumerable<Villa> villa = await _dbVilla.GetAllAsync();  
            return Ok(_mapper.Map<List<VillaDTO>>(villa));
        }
        [Authorize(Roles = "admin")]
        [HttpGet("{id:int}", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id) {
            if(id == 0)
            {
               // _logger.Log("Get villa from error id: "+ ids,"error");
                return BadRequest();
            }

            // var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id==id);
            var villa = await _dbVilla.GetAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDTO>(villa));
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO villaDTO)
        {
            //if (villaDTO == null)
            //{
            //    return BadRequest(villaDTO);
            //}

            //if (VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower()== (villaDTO.Name.ToLower()))
            //{
            //    ModelState.AddModelError("CustomerError", "Villa already exist");
            //    return BadRequest(ModelState);
            //}

            
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);    
            }
            //villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id+1;
            //VillaStore.villaList.Add(villaDTO); 

            Villa model = _mapper.Map<Villa>(villaDTO);
            //Villa model = new()
            //{
            //    Amentity = villaDTO.Amentity,
            //    Details = villaDTO.Details,
            //    Id = villaDTO.Id,
            //    ImageUrl = villaDTO.ImageUrl,
            //    Name = villaDTO.Name,
            //    Rate = villaDTO.Rate,
            //    Occupany = villaDTO.Occupany,
            //    Sqft = villaDTO.Sqft
            //};
          //  _db.Villas.Add(model);  
            //_db.SaveChanges();
            await _dbVilla.CreateAsync(model);  
            return CreatedAtRoute("GetVilla", new {id = villaDTO.Id}, villaDTO);
        }

        [HttpDelete("{id:int}", Name ="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> DeleteVIlla(int id )
        {
            if (id == 0)
            {
                return BadRequest();
            }

           var villa = await _dbVilla.GetAsync(u => u.Id == id);    
            if (villa == null)
            {
                return NotFound();
            }
            await _dbVilla.RemoveAsync(villa);
            return  NoContent();    
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVIlla(int id, [FromBody] VillaUpdateDTO villaDTO)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            if (villaDTO==null)
            {
                return BadRequest();
            }

            var  villa = _dbVilla.GetAsync(u => u.Id == id);

            //Villa model = new()
            //{
            //    Amentity = villaDTO.Amentity,
            //    Details = villaDTO.Details,
            //    Id = villaDTO.Id,
            //    ImageUrl = villaDTO.ImageUrl,
            //    Name = villaDTO.Name,
            //    Rate = villaDTO.Rate,
            //    Occupany = villaDTO.Occupany,
            //    Sqft = villaDTO.Sqft
            //};
            Villa model = _mapper.Map<Villa>(villaDTO);
            await _dbVilla.UpdateAsync(model);
          


            return NoContent();
        }
        [HttpPatch("{id:int}", Name = "PatchVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePattilaVIlla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var villa = await _dbVilla.GetAsync(u=>u.Id==id, tracked: false);
            VillaUpdateDTO villaUpdate = _mapper.Map<VillaUpdateDTO>(villa);

            if (villa == null)
            {
                return BadRequest();
            }
            //VillaDTO model = new()
            //{
            //    Amentity = villa.Amentity,
            //    Details = villa.Details,
            //    Id = villa.Id,
            //    ImageUrl = villa.ImageUrl,
            //    Name = villa.Name,
            //    Rate = villa.Rate,
            //    Occupany = villa.Occupany,
            //    Sqft = villa.Sqft
            //};
            patchDTO.ApplyTo(villaUpdate, ModelState);
            Villa model = _mapper.Map<Villa>(patchDTO);
            await _dbVilla.UpdateAsync(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
