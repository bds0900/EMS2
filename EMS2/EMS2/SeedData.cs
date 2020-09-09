using EMS2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS2
{
    public class SeedData
    {
        public static void Seed()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<EMSContext>(options =>
            {
                options.UseInMemoryDatabase("EMSDB");
            });
            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<EMSContext>();
                    var patient=context.Patients.FindAsync("1234567890KV").Result;
                    if(patient==null)
                    {
                        patient = new Patient
                        {
                            HCN = "1234567890KV",
                            LastName = "Doosan",
                            FirstName = "Beak",
                            DateBirth = new DateTime(1990, 9, 15),
                            Sex = "M",
                            AddressLine1 = "300C Bluevlae North Street",
                            AddressLine2 = "",
                            City = "Waterloo",
                            Province = "ON",
                            PostalCode = "N2P 1C5",
                            PhoneNumber = "226-888-8888"
                        };
                        context.AddAsync(patient);
                        context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
