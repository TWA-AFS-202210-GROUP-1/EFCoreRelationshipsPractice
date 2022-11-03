﻿using EFCoreRelationshipsPractice.Model;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace EFCoreRelationshipsPractice.Dtos
{
  public class CompanyDto
  {
    public CompanyDto()
    {
    }

    public CompanyDto(CompanyEntity companyEntity)
    {
      Name = companyEntity.Name;
    }

    public string Name { get; set; }

    public ProfileDto? Profile { get; set; }

    public List<EmployeeDto>? Employees { get; set; }

    public CompanyEntity ToEntity()
    {
      return new CompanyEntity()
      {
        Name = Name,
      };
    }
  }
}