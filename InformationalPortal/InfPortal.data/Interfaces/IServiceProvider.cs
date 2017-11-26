using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfPortal.data.Entities;

namespace InfPortal.data.Interfaces
{
    public interface IServiceProvider
    {       
        List<Article> GetArticles();
        Article GetArticleById(int? id);
        void AddArticle(Article item);
        void UpdateArticle(Article item);
        void DeleteArticle(int? id);
        List<Heading> GetHeadings();
        int GetCountOfArticles();
    }
}
