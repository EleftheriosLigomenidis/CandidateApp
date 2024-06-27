using CandidateApp.Business.Contracts;
using CandidateApp.Business.Models;
using CandidateApp.Business.Services;

namespace CandidateApp.Extentions
{
    public static class BusinessServiceDependencies
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ICrudeService<Degree>,DegreeService>();
            services.AddScoped<IDegreeService,DegreeService>();
            services.AddScoped<ICrudeService<Candidate>,CandidateService>();
        }
    }
}
