using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PartnerGroup.Domain.Models;
using PartnerGroup.Domain.Service;

namespace PartnerGroup.API.Controllers
{
    public class PatrimonioController : BaseController<Patrimonio>
    {
        public PatrimonioController(IGenericService<Patrimonio> svc) : base(svc)
        {
        }
    }
}