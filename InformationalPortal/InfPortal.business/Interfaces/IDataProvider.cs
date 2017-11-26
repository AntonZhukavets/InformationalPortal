using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfPortal.business.DTO;

namespace InfPortal.business.Interfaces
{
    public interface IDataProvider
    {
        void AddArticle(ArticleDTO article);
        void UpdateArticle(ArticleDTO article);
        void DeleteArticle(int? id);
        ArticleDTO GetArticle(int? id);
        List<ArticleDTO> GetArticles();
        List<HeadingDTO> GetHeadings();
        void Dispose();
        int GetCountOfArticles();
    }
}
