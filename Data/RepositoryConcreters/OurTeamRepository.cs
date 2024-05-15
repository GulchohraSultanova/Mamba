using Core.Models;
using Core.RepositoryAbstracts;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryConcreters
{
    public class OurTeamRepository : IOurTeamRepository
    {
        AppDbContext _appDbContext;

        public OurTeamRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(OurTeam ourTeam)
        {
           _appDbContext.OurTeams.Add(ourTeam);
        }

        public int commit()
        {
           return _appDbContext.SaveChanges();
        }

        public void Delete(OurTeam ourTeam)
        {
            _appDbContext.OurTeams.Remove(ourTeam);
        }

        public OurTeam Get(Func<OurTeam, bool>? func)
        {
            return func==null? _appDbContext.OurTeams.FirstOrDefault():
                _appDbContext.OurTeams.FirstOrDefault(func);
        }

        public List<OurTeam> GetAll(Func<OurTeam, bool>? func)
        {
            return func==null? _appDbContext.OurTeams.ToList():
                _appDbContext.OurTeams.Where(func).ToList();
        }
    }
}
