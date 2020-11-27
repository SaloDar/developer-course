using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Domain.Types;
using DeveloperCourse.SecondTask.Identity.API.Interfaces;
using DeveloperCourse.SecondTask.Identity.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DeveloperCourse.SecondTask.Identity.API.Services
{
    public class DbFillerService : IHostedService
    {
        #region Fields

        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region Constructors

        public DbFillerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #endregion

        #region Implemented Methods

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var serviceScope = _serviceProvider.CreateScope();

            var roleManager = serviceScope.ServiceProvider.GetRequiredService<IRoleManager>();

            await FillRoles(roleManager);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region Private Methods

        private async Task FillRoles(IRoleManager roleManager)
        {
            foreach (UserRole role in typeof(UserRole).GetEnumValues())
            {
                if (await roleManager.RoleExistsAsync(role))
                {
                    continue;
                }

                var roleEntity = new Role(role);

                await roleManager.CreateAsync(roleEntity);
            }
        }

        #endregion
    }
}