using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Business.Mappers
{
    using db = Data.Models;
    using srv = Business.Models;
    public static class DegreeMapper
    {
        public static readonly Expression<Func<db.Degree, srv.Degree>> DegreeSelector = (degree) => new srv.Degree
        {
            Id = degree.Id,
            Name = degree.Name,
        };

        /// <summary>
        /// maps a service obj to db obj
        /// </summary>
        /// <param name="srvObject">The service object</param>
        /// <returns></returns>
        public static db.Degree ToDbObject(this srv.Degree srvObject)
        {
            return new db.Degree
            {
                Id = srvObject.Id,
                Name = srvObject.Name,
            };
        }

        /// <summary>
        /// maps a db obj into  service obj 
        /// </summary>
        /// <param name="dbObject">The db object</param>
        /// <returns></returns>
        public static srv.Degree ToBusinessObject(db.Degree dbObject)
        {
            return new srv.Degree
            {

                Id = dbObject.Id,
                Name = dbObject.Name,
            };
        }

        /// <summary>
        /// Updates the target with the values of the source
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void UpdateWith(this srv.Degree source, db.Degree target)
        {
            target.Id = source.Id;
            target.Name = source.Name;
        }
    }
}
