﻿using EFCoreRelationshipsPractice.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreRelationshipsPracticeTest.ControllerTest
{
    public class ControllerTestBase : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {
        public ControllerTestBase(CustomWebApplicationFactory<Program> factory)
        {
            Factory = factory;
        }

        protected CustomWebApplicationFactory<Program> Factory { get; }

        public void Dispose()
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<CompanyDbContext>();

            context.Employees.RemoveRange(context.Employees);
            context.Companies.RemoveRange(context.Companies);

            context.SaveChanges();
        }

        protected HttpClient GetClient()
        {
            return Factory.CreateClient();
        }
    }
}