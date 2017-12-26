using InfPortal.business.DTO;

namespace InfPortal.business.Interfaces
{
    public interface IDataProvider
    {
        bool AddArticle(ArticleDTO article);
        bool EditArticle(ArticleDTO article);
        bool DeleteArticle(int? id);
        bool AddHeading(HeadingDTO heading);
        bool EditHeading(HeadingDTO heading);
        bool DeleteHeading(int? id);
        ArticleDTO GetFullArticleById(int? id);
        ArticleDTO[] GetArticlesPreView();
        ArticleDTO[] GetArticlesPreViewByHeadingId(int? id);
        ArticleDTO[] GetArticlesPreViewByUserId(int? id);
        HeadingDTO[] GetHeadings();
        ArticleDTO[] GetArticlesPreViewByName(string name);
        string[] GetArticleNamesByInput(string name);
        int GetCountOfArticles();
    }
}
