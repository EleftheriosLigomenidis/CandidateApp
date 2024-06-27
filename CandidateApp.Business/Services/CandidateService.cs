using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using CandidateApp.Business.Contracts;
using CandidateApp.Business.Mappers;
using CandidateApp.Business.Models;
using CandidateApp.Business.Options;
using CandidateApp.Business.Utilities;
using CandidateApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;


namespace CandidateApp.Business.Services
{
    using data = Data.Models;
    using srv = Business.Models;
    public class CandidateService(ILogger<DegreeService> logger, ApplicationDbContext context,IOptions<AzureBlobStorageSettings> settings) : ICrudeService<Candidate>
    {
        private readonly ILogger<DegreeService> _logger = logger;
        private readonly ApplicationDbContext _context = context;
        private readonly AzureBlobStorageSettings _settings = settings.Value;
        public Candidate Create(Candidate model)
        {

            //if(model.CvBlob is not null)
            //{

            //    // await UploadFileAsync(model.CvBlob)
            //}
            data.Candidate entity = model.ToDbObject();
            _logger.LogTrace(Messages.CreatingEntity(nameof(model), model.ToString()));

            _context.Candidates.Add(entity);

            try
            {

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string errorMessage = Messages.CreateEntityFailed(nameof(srv.Candidate), model.ToString());
                _logger.LogError(ex, errorMessage);
                ex.Data.Add(Messages.UserMessage, errorMessage);
                throw;
            }
            var returnEntity = Get(entity.Id);
            return returnEntity;
        }

        public void Delete(long id)
        {
            _logger.LogTrace(Messages.FetchEntity(nameof(srv.Candidate), nameof(srv.Degree.Id),id.ToString()));

            var entity = _context.Candidates.Find(id);

            if (entity == null)
            {
                string errorMessage = Messages.EntityNotFound(nameof(srv.Candidate), id.ToString());
                _logger.LogError(errorMessage);
                throw new KeyNotFoundException(errorMessage);
            }

            _logger.LogTrace(Messages.DeletingEntity(nameof(srv.Candidate), id.ToString()));

            // could add changesNo field in db 
            // compare it with  an incoming changes No from the client 
            // if changes dont match den concurrency error
            _context.Candidates.Remove(entity);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string errorMessage = Messages.DeleteEntityFailed( nameof(srv.Candidate), id.ToString());
                _logger.LogError(ex, errorMessage);
                ex.Data.Add(Messages.UserMessage, errorMessage);
                throw;
            }
        }

        public Candidate Get(long id)
        {
            _logger.LogTrace(Messages.FetchEntity(nameof(srv.Candidate), nameof(srv.Candidate.Id), id.ToString()));

            data.Candidate? entity = _context.Candidates.Include(x => x.CandidateDegrees).ThenInclude(x => x.Degree)
                .FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                string errorMessage = Messages.EntityNotFound(nameof(srv.Candidate.Id), id.ToString());
                _logger.LogError(errorMessage);
                throw new KeyNotFoundException(errorMessage);
            }

            return CandidateMapper.ToBusinessObject(entity);
        }

        public List<Candidate> GetAll()
        {
            _logger.LogTrace(Messages.FetchCollection(nameof(srv.Candidate)));

            List<srv.Candidate> candidates = new();
            try
            {
                // could also use auto mapper
                candidates = [.. _context.Candidates.Include(x => x.CandidateDegrees)
                                .ThenInclude(x => x.Degree).AsNoTracking().Select(CandidateMapper.CandidateSelector)];
            }
            catch (Exception ex)
            {

                string errorMessage = Messages.FetchCollectionFailed(nameof(srv.Candidate));

                _logger.LogError(ex, errorMessage);

                ex.Data.Add(Messages.UserMessage, errorMessage);
                throw;
            }

            return candidates;
        }

        private async Task<string> UploadFileAsync(IFormFile file)
        {
            var blobServiceClient = new BlobServiceClient(_settings.ConnectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_settings.ContainerName);
            var blobClient = blobContainerClient.GetBlobClient(file.FileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
            }

            return blobClient.Uri.ToString();
        }

        public Candidate Update(Candidate model)
        {
            _logger.LogTrace(Messages.FetchEntity(nameof(srv.Candidate), nameof(srv.Candidate.Id), model.Id.ToString()));

            data.Candidate? entity = _context.Candidates.Find(model.Id);

            if (entity == null)
            {
                string errorMessage = Messages.EntityNotFound(nameof(srv.Candidate), model.Id.ToString());
                _logger.LogError(errorMessage);
                throw new KeyNotFoundException(errorMessage);
            }

            model.UpdateWith(entity);
            UpdateCandidateAttributes(model.Degrees, model.Id);
            _logger.LogTrace(Messages.UpdatingEntity(nameof(srv.Candidate), model.ToString()));

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string errorMessage = Messages.UpdateEntityFailed(nameof(srv.Candidate), model.ToString());
                _logger.LogError(ex, errorMessage);
                ex.Data.Add(Messages.UserMessage, errorMessage);
                throw;
            }

            var returnEntity = Get(entity.Id);
            return returnEntity;
        }

        private void UpdateCandidateAttributes(List<srv.Degree> degrees, long candidateId)
        {
            //fetch the associated candidate-degrees by candidateId

            var dbEntities = _context.CandidateDegrees.Include(x => x.Candidate).Where(x => candidateId == x.CandidateId).ToList();
            // Convert degree list to a dictionary for quick lookup
            var degreeDictionary = degrees.ToDictionary(a => a.Id, a => a);

            // Remove dbEntities that do not exist in the attributes list
            foreach (var entity in dbEntities)
            {
                if (!degreeDictionary.ContainsKey(entity.DegreeId))
                {
                    _context.CandidateDegrees.Remove(entity);
                }
            }

            List <data.CandidateDegree> dbAttrs = new List<data.CandidateDegree>();
            // Add degrees to dbEntities if they do not exist
            var dbEntityIds = new HashSet<long>(dbEntities.Select(e => e.DegreeId));
            foreach (var degree in degrees)
            {
                if (!dbEntityIds.Contains(degree.Id))
                {
                    var newObj = new data.CandidateDegree
                    {
                        DegreeId = degree.Id,
                        CandidateId = candidateId
                    };

                    dbAttrs.Add(newObj);

                }
            }

            _context.CandidateDegrees.AddRange(dbAttrs);

        }
    }
}
