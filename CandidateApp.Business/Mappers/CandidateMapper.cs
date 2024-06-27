using CandidateApp.Data.Models;
using System.Linq.Expressions;


namespace CandidateApp.Business.Mappers
{
    using db = Data.Models;
    using srv = Business.Models;
    public static class CandidateMapper
    {
        public static readonly Expression<Func<db.Candidate, srv.Candidate>> CandidateSelector = (candidate) => new srv.Candidate
        {
            CreationTime = candidate.CreationTime,
            Firstname = candidate.Firstname,
            Lastname= candidate.Lastname,
            Email = candidate.Email,
            Id= candidate.Id,
            Mobile = candidate.Mobile,
            Degrees = candidate.CandidateDegrees.Select(x => x.Degree).Select(x => DegreeMapper.ToBusinessObject(x)).ToList(),
        };

 
        public static db.Candidate ToDbObject(this srv.Candidate srvObject)
        {
            var dbCandidate = new db.Candidate
            {
                CreationTime = srvObject.CreationTime,
                Firstname = srvObject.Firstname,
                Lastname = srvObject.Lastname,
                Email = srvObject.Email,
                Id = srvObject.Id,
                CvBlob = srvObject.CvBlob,
                Mobile = srvObject.Mobile,
           
            };
            dbCandidate.CandidateDegrees = CreateCandidateDegrees(srvObject, dbCandidate);
            return dbCandidate;

        }
        private static List<CandidateDegree> CreateCandidateDegrees(srv.Candidate srvObjec, db.Candidate dbObj)
        {
            List<CandidateDegree> candidateDegrees = new List<CandidateDegree>();

            foreach (var degree in srvObjec.Degrees)
            {
                CandidateDegree newObj = new ()
                {
                    Candidate = dbObj,
                    DegreeId = degree.Id
                };
                candidateDegrees.Add(newObj);
            }

            return candidateDegrees;
        }

        public static srv.Candidate ToBusinessObject(db.Candidate dbObject)
        {
            return new srv.Candidate
            {
                CreationTime = dbObject.CreationTime,
                Firstname = dbObject.Firstname,
                Lastname = dbObject.Lastname,
                Email = dbObject.Email,
                Id = dbObject.Id,
                Mobile = dbObject.Mobile,
                CvBlob = dbObject.CvBlob,
                Degrees = dbObject.CandidateDegrees.Select(x => x.Degree).Select(x => DegreeMapper.ToBusinessObject(x)).ToList(),
            };
        }

        public static void UpdateWith(this srv.Candidate source, db.Candidate target)
        {

            target.CreationTime = source.CreationTime;
            target.CvBlob = source.CvBlob;
            target.Firstname = source.Firstname;
            target.Lastname = source.Lastname;
            target.Email = source.Email;
            target.Id = source.Id;
            target.Mobile = source.Mobile;
        }
    }
}
