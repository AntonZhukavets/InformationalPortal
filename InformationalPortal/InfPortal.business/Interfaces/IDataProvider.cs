using InfPortal.business.DTO;

namespace InfPortal.business.Interfaces
{
    public interface IDataProvider
    {
        void AddArticle(ArticleDTO article);
        void UpdateArticle(ArticleDTO article);
        void DeleteArticle(int? id);
        ArticleDTO GetArticle(int? id);
        ArticleDTO[] GetArticles();
        ArticleDTO[] GetArticlesByHeadingId(int? id);
        ArticleDTO[] GetArticlesByUserId(int? id);
        HeadingDTO[] GetHeadings();
        ArticleDTO[] GetArticlesByName(string name);
        string[] GetArticleNamesByInput(string name);

        int GetCountOfArticles();
    }
}
