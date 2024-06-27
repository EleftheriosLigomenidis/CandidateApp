//using CandidateApp.Business.Contracts;
//using CandidateApp.Business.Models;
//using Microsoft.AspNetCore.Http.HttpResults;

//namespace CandidateApp.Controllers
//{
//    public static class DegreeEndpoint
//    {
//        public static void AddDeggreeEndpoints(this WebApplication app)
//        {
//            app.MapGet("/degrees", (ICrudeService<Degree> degreeService) =>
//            {
//                var degrees = degreeService.GetAll();
//                return Results.Ok(degrees);
//            });

//            app.MapGet("/degrees/{id:long}", (ICrudeService<Degree> degreeService,long id) =>
//            {
//                var degrees = degreeService.Get(id);
//                return Results.Ok(degrees);
//            });

//            app.MapDelete("/degrees/{id:long}", (ICrudeService<Degree> degreeService, long id) =>
//            {
//                degreeService.Delete(id);
//                return Results.NoContent();
//            });

//            app.MapPost("/degrees", (ICrudeService<Degree> degreeService, Degree model) =>
//            {
//                var degrees = degreeService.Create(model);
//                return Results.Ok(degrees);
//            });

//            app.MapPut("/degrees", (ICrudeService<Degree> degreeService, Degree model) =>
//            {
//                var degrees = degreeService.Update(model);
//                return Results.Ok(degrees);
//            });
//        }
//    }
//}
