using InfPortal.business.DTO;

namespace InfPortal.business.Interfaces
{
    public interface ILanguageProvider
    {
        bool AddLanguage(LanguageDTO language);
        bool DeleteLanguage(int? id);
        bool RestoreLanguage(int? id);
        LanguageDTO[] GetLanguages();
    }
}
