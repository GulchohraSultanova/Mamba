using Bussiness.Abstracts;
using Bussiness.Exceptions;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Concreters
{
    public class OurTeamService : IOurTeamService
    {

        IOurTeamRepository _ourTeamRepository;
        IWebHostEnvironment _webHostEnvironment;
        public OurTeamService(IOurTeamRepository ourTeamRepository, IWebHostEnvironment webHostEnvironment)
        {
            _ourTeamRepository = ourTeamRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void Create(OurTeam team)
        {
            if (team.PhotoFile == null)
            {
                throw new NullException("Photofile", "Null ola bilmez");
            }
            if (!team.PhotoFile.ContentType.Contains("image/"))
            {
                throw new FileContentTypeException("PhotoFile", "Fayl formati duzgun deyil!");
            }
            string filename = team.PhotoFile.FileName;
            string path = _webHostEnvironment.WebRootPath + @"\Upload\Service\" + filename;
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                team.PhotoFile.CopyTo(fileStream);
            }
            team.ImgUrl = filename;
            _ourTeamRepository.Add(team);
            _ourTeamRepository.commit();


        }

        public void Delete(int id)
        {
            var oldTeam = _ourTeamRepository.Get(x => x.Id == id);
            string path = _webHostEnvironment.WebRootPath + @"\Upload\Service\"+ oldTeam.ImgUrl;
            FileInfo file = new FileInfo(path );
            if (file.Exists)
            {
                file.Delete();
            }
            _ourTeamRepository.Delete(oldTeam);
            _ourTeamRepository.commit();


        }

        public OurTeam Get(Func<OurTeam, bool>? func)
        {
            return _ourTeamRepository.Get(func);
        }

        public List<OurTeam> GetAll(Func<OurTeam, bool>? func = null)
        {
            return _ourTeamRepository.GetAll(func);
        }

        public void Update(int id, OurTeam team)
        {
            var oldTeam = _ourTeamRepository.Get(x => x.Id == id);
            if (oldTeam == null)
            {
                throw new NullException("", "Null ola bilmez");
            }
            if (team.PhotoFile != null)
            {
                if (!team.PhotoFile.ContentType.Contains("image/"))
                {
                    throw new FileContentTypeException("PhotoFile", "Fayl formati duzgun deyil!");
                }
                else
                {
                    string filename = team.PhotoFile.FileName;
                    string path = _webHostEnvironment.WebRootPath + @"\Upload\Service\" + filename;

                    using (FileStream fileStream = new FileStream(path, FileMode.Create))
                    {
                        team.PhotoFile.CopyTo(fileStream);
                    }
                    oldTeam.ImgUrl = filename;
                 

                }

            }
            else
            {
                team.ImgUrl = oldTeam.ImgUrl;
            }
            oldTeam.Name = team.Name;
            oldTeam.Position = team.Position;
            _ourTeamRepository.commit();

        }
    }
}
