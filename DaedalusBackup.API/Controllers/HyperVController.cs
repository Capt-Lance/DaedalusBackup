using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackupManagement.Domain;
using BackupManagement.Infrastructure.HyperV.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaedalusBackup.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HyperVController : ControllerBase
    {
        private readonly HyperVVirtualMachineRepository _hyperVRepo;
        public HyperVController(HyperVVirtualMachineRepository hyperVRepo)
        {
            _hyperVRepo = hyperVRepo;
        }
        [HttpGet("virtualmachines")]
        public IActionResult GetVMs()
        {
            IEnumerable<VirtualMachine> vms = _hyperVRepo.GetAll();
            return Ok(vms);
        }

    }
}