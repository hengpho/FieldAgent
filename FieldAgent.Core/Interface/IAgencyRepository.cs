using FieldAgent.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Interface
{
    public interface IAgencyRepository
    {
        Response<Agency> Insert(Agency agency);
        Response Update(Agency agency);
        Response Delete(int agencyId);
        Response<Agency> Get(int agencyId);
        Response<List<Agency>> GetAll();
    }
}
