using AutoMapper;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StoresController(IStoreRepository storeRepository, IMapper mapper) : Controller
    {
        private readonly IStoreRepository _storeRepository = storeRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var store = await _storeRepository.GetAllStoreAsync();
                var storeDtos = _mapper.Map<IEnumerable<StoreDto>>(store);
                return Ok(storeDtos);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Error al obtener el listado de tiendas" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var store = await _storeRepository.GetStoreByIdAsync(id);
                if (store == null) return NotFound(new { message = "La tienda especificada no existe" });
                return Ok(_mapper.Map<StoreDto>(store));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Error al obtener la tienda especificada" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(StoreDto dto)
        {
            try
            {
                var store = _mapper.Map<Store>(dto);
                await _storeRepository.AddStoreAsync(store);
                return CreatedAtAction(nameof(Get), new { id = store.Id }, dto);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Error al crear la tienda" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, StoreDto dto)
        {
            if (id != dto.Id) return BadRequest(new { message = "El ID de la ruta no coincide con el ID de la tienda" });

            var existingStore = await _storeRepository.GetStoreByIdAsync(id);
            if (existingStore == null) return NotFound(new { message = "Tienda no encontrada" });

            existingStore.Name = dto.Name;
            existingStore.Address = dto.Address;
            existingStore.Active = dto.Active;

            try
            {
                await _storeRepository.UpdateStoreAsync(existingStore);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Error al actualizar la tienda" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _storeRepository.DeleteStoreAsync(id);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Error al eliminar la tienda" });
            }
        }
    }
}
