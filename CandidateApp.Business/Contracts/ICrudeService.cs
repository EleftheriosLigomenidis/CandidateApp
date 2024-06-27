using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Business.Contracts
{
    public  interface ICrudeService<T> where T : class
    {

        List<T> GetAll();
        T Get(long id);
        T Create(T model);
        T Update(T model);
        void Delete(long id);
    }
}
