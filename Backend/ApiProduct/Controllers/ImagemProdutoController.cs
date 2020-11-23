using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiProduct.Models;

namespace ApiProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagemProdutoController : ControllerBase
    {
        private readonly produtodbContext _context;

        public ImagemProdutoController(produtodbContext context)
        {
            _context = context;
        }

        // GET: api/ImagemProduto
        [HttpGet]
        public IEnumerable<Imagemproduto> GetImagemproduto()
        {
            return _context.Imagemproduto;
        }

        // GET: api/ImagemProduto/5
        [HttpGet("{id}")]
        public IEnumerable<Imagemproduto> GetImagemproduto([FromRoute] int id)
        {
            List<Imagemproduto> resultadoList = new List<Imagemproduto>();

            IEnumerable<Imagemproduto> imagens = _context.Imagemproduto;

            foreach (Imagemproduto i in imagens)
            {
                if (i.CdProduto == id)
                {
                    resultadoList.Add(i);
                }
            }

            return resultadoList;
        }

        [HttpGet("NextId")]
        public async Task<IActionResult> GetNextId() =>
            Ok(_context.Imagemproduto.Count() + 1);

        // PUT: api/ImagemProduto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagemproduto([FromRoute] int id, [FromBody] Imagemproduto imagemproduto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != imagemproduto.CdImagem)
            {
                return BadRequest();
            }

            _context.Entry(imagemproduto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagemprodutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ImagemProduto
        [HttpPost]
        public async Task<IActionResult> PostImagemproduto([FromBody] Imagemproduto imagemproduto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Imagemproduto.Add(imagemproduto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImagemprodutoExists(imagemproduto.CdImagem))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetImagemproduto", new { id = imagemproduto.CdImagem }, imagemproduto);
        }

        // DELETE: api/ImagemProduto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImagemproduto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imagemproduto = await _context.Imagemproduto.FindAsync(id);
            if (imagemproduto == null)
            {
                return NotFound();
            }

            _context.Imagemproduto.Remove(imagemproduto);
            await _context.SaveChangesAsync();

            return Ok(imagemproduto);
        }

        private bool ImagemprodutoExists(int id)
        {
            return _context.Imagemproduto.Any(e => e.CdImagem == id);
        }
    }
}