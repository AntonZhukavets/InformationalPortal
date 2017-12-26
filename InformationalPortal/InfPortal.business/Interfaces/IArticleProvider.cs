using System;
using InfPortal.business.DTO;


namespace InfPortal.business.Interfaces
{
    public interface IArticleProvider
    {
        bool AddArticle(ArticleDTO article);
        bool EditArticle(ArticleDTO article);
        bool DeleteArticle(int? id);
        ArticleDTO GetFullArticleById(int? id);
        ArticleDTO[] GetArticlesPreView();
        ArticleDTO[] GetArticlesPreViewByHeadingId(int? id);
        ArticleDTO[] GetArticlesPreViewByUserId(int? id);
        ArticleDTO[] GetArticlesPreViewByName(string name);
        ArticleDTO[] GetArticlesPreViewByHeadingIdAndDatePeriod(DateTime dateFrom, DateTime dateTo, int? id);
    }
}
