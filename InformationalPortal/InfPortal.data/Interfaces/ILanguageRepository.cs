using InfPortal.data.Entities;

namespace InfPortal.data.Interfaces
{
    public interface ILanguageRepository
    {
        Language[] GetLanguages();
        bool AddLanguage(Language language);
        bool DeleteLanguage(int? id);
        bool RestoreLanguage(int? id);
    }
}
