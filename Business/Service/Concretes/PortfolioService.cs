using Business.CustomExceptions;
using Business.Service.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Concretes
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioService(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public void AddPortfolio(Portfolio portfolio)
        {
            if (portfolio == null) throw new EntityNullException("", "Entity exception");
            if (portfolio.PhotoFile == null) throw new FileNulException("", "File is null");
            if (portfolio.PhotoFile.Length > 2097152) throw new FileLengthException("PhotoFile", "Olcu 2 mbdan cox ola bilmez!!!!!!!!!!!");
            if (!portfolio.PhotoFile.ContentType.Contains("image/")) throw new ContentTypeException("PhotoFile", "Duzgun Format tap");
            string path= "C:\\Users\\ll novbe\\Desktop\\MVC_Agency\\MVC_Agency\\wwwroot\\Upload\\Portfolio\\" + portfolio.PhotoFile.FileName;
            using(FileStream stream = new FileStream(path, FileMode.Create))
            {
                portfolio.PhotoFile.CopyTo(stream);
            }
            if (!File.Exists(path)) throw new PhotoNotFoundException("PhotoFile", "File nott found !!!!!!");
            portfolio.ImgUrl = portfolio.PhotoFile.FileName;
            _portfolioRepository.Add(portfolio);
            _portfolioRepository.Commit();
        }

        public void DeletePortfolio(int id)
        {
            var portfolio = _portfolioRepository.Get(x => x.Id == id);
            
            if (portfolio == null) throw new EntityNullException("", "File tapilmir");
            string path = "C:\\Users\\ll novbe\\Desktop\\MVC_Agency\\MVC_Agency\\wwwroot\\Upload\\Portfolio\\" + portfolio.ImgUrl;
            FileInfo fileInfo = new FileInfo(path);
            fileInfo.Delete();
            _portfolioRepository.Delete(portfolio);
            _portfolioRepository.Commit();
        }

        public List<Portfolio> GetAllPortfolio(Func<Portfolio, bool>? func = null)
        {
            return _portfolioRepository.GetAll(func);
        }

        public Portfolio GetPortfolio(Func<Portfolio, bool>? func = null)
        {
            return _portfolioRepository.Get(func);
        }

        public void UpdatePortfolio(int id, Portfolio newPortfolio)
        {
            var oldPortfolio = _portfolioRepository.Get(x => x.Id == id);
            if(oldPortfolio == null) throw new EntityNullException("", "File tapilmir");
            if (newPortfolio.PhotoFile != null)
            {
                if (newPortfolio.PhotoFile.Length > 2097152) throw new FileLengthException("PhotoFile", "Olcu 2 mbdan cox ola bilmez!!!!!!!!!!!");
                if (!newPortfolio.PhotoFile.ContentType.Contains("image/")) throw new ContentTypeException("PhotoFile", "Duzgun Format tap");
                
                string oldpath = "C:\\Users\\ll novbe\\Desktop\\MVC_Agency\\MVC_Agency\\wwwroot\\Upload\\Portfolio\\" + oldPortfolio.ImgUrl;
                if (File.Exists(oldpath))
                {
                    FileInfo fileInfo = new FileInfo(oldpath);
                    fileInfo.Delete();
                }
                string newpath= "C:\\Users\\ll novbe\\Desktop\\MVC_Agency\\MVC_Agency\\wwwroot\\Upload\\Portfolio\\" + newPortfolio.PhotoFile.FileName;
                using(FileStream stream = new FileStream(newpath, FileMode.Create))
                {
                    newPortfolio.PhotoFile.CopyTo(stream);  
                }
                
                oldPortfolio.ImgUrl = newPortfolio.PhotoFile.FileName;
            }
            oldPortfolio.Title = newPortfolio.Title;
            oldPortfolio.Description = newPortfolio.Description;
            _portfolioRepository.Commit();
        }
    }
}
