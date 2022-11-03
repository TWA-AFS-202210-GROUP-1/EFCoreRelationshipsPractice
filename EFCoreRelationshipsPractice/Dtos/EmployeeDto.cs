using EFCoreRelationshipsPractice.Model;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class EmployeeDto
    {

        public EmployeeDto()
        {
        }

        public EmployeeDto(EmployeeEntity employeeEntity)
        {
            this.Name = employeeEntity.Name;
            this.Age = employeeEntity.Age;
        }

        public string Name { get; set; }
        public int Age { get; set; }

        public EmployeeEntity ToEntity()
        {
            return new EmployeeEntity()
            {
                Name = this.Name,
                Age = this.Age,
            };
    }
}
}