using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoryAbstracts
{
   public  interface IOurTeamRepository
    {
        void Add(OurTeam ourTeam);
        void Delete(OurTeam ourTeam);
        OurTeam Get(Func<OurTeam,bool>? func);
        List<OurTeam> GetAll(Func<OurTeam,bool> ? func);
        int commit();
    }
}
