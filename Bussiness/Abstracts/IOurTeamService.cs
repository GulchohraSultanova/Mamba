using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstracts
{
   public  interface IOurTeamService
    {
        void Create(OurTeam team);
        void Delete(int id);
        void Update(int id,OurTeam team);
        OurTeam Get(Func<OurTeam, bool> ? func=null);
        List<OurTeam> GetAll(Func<OurTeam,bool> ? func=null);
    }
}
