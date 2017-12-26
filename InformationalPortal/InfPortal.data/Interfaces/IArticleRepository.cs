using System;
using InfPortal.data.Entities;


namespace InfPortal.data.Interfaces
{
    public interface IArticleRepository
    {
        bool AddArticle(Article article);
        bool EditArticle(Article article);
        bool DeleteArticle(int? id);
        Article GetFullArticleById(int? id);
        Article[] GetArticlesPreViewByHeadingId(int? id);
        Article[] GetArticlesPreViewByHeadingIdAndDatePeriod(DateTime dateFrom, DateTime dateTo, int? id);
        Article[] GetArticlesPreViewByUserId(int? id);
        Article[] GetArticlesPreViewByName(string name);
        Article[] GetArticlesPreView();
    }
}
