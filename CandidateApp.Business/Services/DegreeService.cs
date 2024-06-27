using CandidateApp.Business.Contracts;
using CandidateApp.Business.Exceptions;
using CandidateApp.Business.Mappers;
using CandidateApp.Business.Models;
using CandidateApp.Business.Utilities;
using CandidateApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;


namespace CandidateApp.Business.Services
{
    using data = Data.Models;
    using srv = Business.Models;
    public class DegreeService(ILogger<DegreeService> logger, ApplicationDbContext context) : ICrudeService<Degree>,IDegreeService
    {
        private readonly ILogger<DegreeService> _logger = logger;
        private readonly ApplicationDbContext _context = context;

        public Degree Create(Degree model)
        {
            data.Degree entity = model.ToDbObject();
            _logger.LogTrace(Messages.CreatingEntity(nameof(model),model.ToString()));

            _context.Degrees.Add(entity);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string errorMessage = Messages.CreateEntityFailed(nameof(srv.Degree),model.ToString());
                _logger.LogError(ex, errorMessage);
                ex.Data.Add(Messages.UserMessage, errorMessage);
                throw;
            }
            var returnEntity = Get(entity.Id);
            return returnEntity;
        }

        public void Delete(long id)
        {
            _logger.LogTrace(Messages.FetchEntity(nameof(srv.Degree), nameof(srv.Degree.Id), id.ToString()));
            var entity = _context.Degrees.Include(x => x.CandidateDegrees).FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                string errorMessage = Messages.EntityNotFound(nameof(srv.Degree), id.ToString());
                _logger.LogError(errorMessage);
                throw new KeyNotFoundException(errorMessage);
            }

            _logger.LogTrace(Messages.DeletingEntity(nameof(srv.Degree), id.ToString()));

            if (entity.IsUsed())
            {
                string errorMessage = Messages.EntityReferenced(nameof(srv.Degree), id.ToString(),nameof(data.CandidateDegree));
                _logger.LogError(errorMessage);
                throw new ReferentialIntegrityException(errorMessage);
            }


            // could add changesNo field in db 
            // compare it with  an incoming changes No from the client 
            // if changes dont match den concurrency error
            _context.Degrees.Remove(entity);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string errorMessage = Messages.DeleteEntityFailed( nameof(srv.Degree), id.ToString());
                _logger.LogError(ex, errorMessage);
                ex.Data.Add(Messages.UserMessage, errorMessage);
                throw;
            }
        }

        public Degree Get(long id)
        {
            _logger.LogTrace(Messages.FetchEntity(nameof(srv.Degree), nameof(srv.Degree.Id), id.ToString()));

            data.Degree? entity = _context.Degrees
                .FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                string errorMessage = Messages.EntityNotFound(nameof(srv.Degree.Id), id.ToString());
                _logger.LogError(errorMessage);
                throw new KeyNotFoundException(errorMessage);
            }

            return DegreeMapper.ToBusinessObject(entity);
        }

        public List<Degree> GetAll()
        {
            _logger.LogTrace(Messages.FetchCollection(nameof(srv.Degree)));
           
            List<srv.Degree> degrees = new();
            try
            {
                // could also use auto mapper
                degrees = [.. _context.Degrees.AsNoTracking().Select(DegreeMapper.DegreeSelector)];
            }
            catch (Exception ex)
            {

                string errorMessage = Messages.FetchCollectionFailed( nameof(srv.Degree));

                _logger.LogError(ex, errorMessage);

                ex.Data.Add(Messages.UserMessage, errorMessage);
                throw;
            }

            return degrees;
        }


        public Degree Update(Degree model)
        {
            _logger.LogTrace(Messages.FetchEntity( nameof(srv.Degree), nameof(srv.Degree.Id), model.Id.ToString()));
            
            data.Degree? entity = _context.Degrees.Find(model.Id);

            if (entity == null)
            {
                string errorMessage = Messages.EntityNotFound(nameof(srv.Degree), model.Id.ToString());
                _logger.LogError(errorMessage);
                throw new KeyNotFoundException(errorMessage);
            }

            model.UpdateWith(entity);

            _logger.LogTrace(Messages.UpdatingEntity(nameof(srv.Degree), model.ToString()));

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string errorMessage = Messages.UpdateEntityFailed(nameof(srv.Degree), model.ToString());
                _logger.LogError(ex, errorMessage);
                ex.Data.Add(Messages.UserMessage, errorMessage);
                throw;
            }

            var returnEntity = Get(entity.Id);
            return returnEntity;
        }
    }
}
