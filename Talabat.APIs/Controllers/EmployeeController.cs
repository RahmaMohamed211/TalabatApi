    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.core.Entities;
using Talabat.core.Repositiories;
using Talabat.core.Sepecifitction;

namespace Talabat.APIs.Controllers
{

    public class EmployeeController : ApiBaseController
    {
        private readonly IGenericRepository<Employee> empRepo;

        public EmployeeController(IGenericRepository<Employee> empRepo)
        {
            this.empRepo = empRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Employee>>> GetEmployees()
        {
            var spec = new EmployeeSpecification();
            var products = empRepo.GetAllwithSpecAsync(spec);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var spec = new EmployeeSpecification(id);
            var products = empRepo.GetByIdwithSpecAsync(spec);
            return Ok(products);
        }








    }


    
}
