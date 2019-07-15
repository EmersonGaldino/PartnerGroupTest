using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PartnerGroup.Domain.Models;
using PartnerGroup.Domain.Service;

namespace PartnerGroup.API.Controllers
{
    [Route("api/partnerGroup/[controller]")]
    [ApiController]
    public abstract class BaseController<T> : Controller where T : Entity
    {

        private IGenericService<T> _svc;


        public BaseController(IGenericService<T> svc)
        {
            _svc = svc;
        }

        [HttpGet("{id}")]
        public virtual IActionResult GetById(int id)
        {
            var result = _svc.GetSingle(id);

            if (!string.IsNullOrEmpty(result.ErrorMessage) && result.StatusCode == 500)
            {
                return StatusCode(500, result);

            }
            else if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                result.StatusCode = 400;
                return BadRequest(result);
            }

            result.StatusCode = 200;

            return Ok(result);
        }

        [HttpGet]
        public virtual IActionResult GetAll()
        {
            var result = _svc.GetAll();

            if (!string.IsNullOrEmpty(result.ErrorMessage) && result.StatusCode == 500)
            {
                return StatusCode(500, result);

            }          

            result.StatusCode = 200;

            return Ok(result);
        }



        [HttpDelete("{id}")]
        public virtual IActionResult Delete(int id)
        {

            var result = _svc.Delete(id);

            if (!string.IsNullOrEmpty(result.ErrorMessage) && result.StatusCode == 500)
            {
                return StatusCode(500, result);

            }
            else if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                result.StatusCode = 400;
                return BadRequest(result);
            }

            result.StatusCode = 200;

            return Ok(result);
        }

        [HttpPut("{id}")]
        public virtual IActionResult Uptade([FromBody] T obj, int id)
        {


            var result = _svc.Update(obj, id);

            if (!string.IsNullOrEmpty(result.ErrorMessage) && result.StatusCode == 500)
            {
                return StatusCode(500, result);

            }
            else if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                result.StatusCode = 400;
                return BadRequest(result);
            }

            result.StatusCode = 200;

            return Ok(result);

        }

        [HttpPost]
        public virtual IActionResult Add([FromBody] T obj)
        {
            var result = _svc.Create(obj);

            if (!string.IsNullOrEmpty(result.ErrorMessage) && result.StatusCode == 500)
            {
                return StatusCode(500, result);

            }
            else if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                result.StatusCode = 400;
                return BadRequest(result);
            }

            result.StatusCode = 200;

            return Ok(result);

        }

    }
}