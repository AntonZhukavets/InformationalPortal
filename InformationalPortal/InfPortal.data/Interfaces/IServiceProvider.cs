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
        Article[] GetArticles();
        Article GetArticleById(int? id);
        void AddArticle(Article item);
        void UpdateArticle(Article item);
        void DeleteArticle(int? id);
        Heading[] GetHeadings();
        Article[] GetArticlesByHeadingId(int? id);
        Article[] GetArticlesByUserId(int? id);
        Article[] GetArticlesByName(string name);
        string[] GetArticleNamesByInput(string name);
        int GetCountOfArticles();
    }
}
