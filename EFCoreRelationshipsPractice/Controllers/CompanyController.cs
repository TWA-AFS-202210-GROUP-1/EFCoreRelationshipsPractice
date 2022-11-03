using System.Collections.Generic;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreRelationshipsPractice.Controllers
{
  [ApiController]
  [Route("companies")]
  public class CompanyController : ControllerBase
  {
    private readonly CompanyService companyService;

    public CompanyController(CompanyService companyService)
    {
      this.companyService = companyService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> List()
    {
      var companyDtos = await this.companyService.GetAll();

      return Ok(companyDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDto>> GetById(int id)
    {
      var companyDto = await this.companyService.GetById(id);
      return Ok(companyDto);
    }

    [HttpPost]
    public async Task<ActionResult<CompanyDto>> Add(CompanyDto companyDto)
    {
      var id = await this.companyService.AddCompany(companyDto);

      return CreatedAtAction(nameof(GetById), new { id }, companyDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      await companyService.DeleteCompany(id);

      return this.NoContent();
    }
  }
}