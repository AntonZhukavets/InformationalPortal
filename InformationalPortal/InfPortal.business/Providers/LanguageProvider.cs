using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InfPortal.business.DTO;
using InfPortal.business.Interfaces;
using InfPortal.common.Exceptions;
using InfPortal.common.Logs;
using InfPortal.data.Entities;
using InfPortal.data.Interfaces;
using InfPortal.data.Repository;

namespace InfPortal.business.Providers
{
    public class LanguageProvider: ILanguageProvider
    {
        private readonly ILanguageRepository languageRepository;
        private readonly ICacheProvider cacheProvider;
        private const string errorArgument = "Parametr is invalid";
        private const string cacheKeyGetLanguages = "GetLanguages";
        public LanguageProvider(ILanguageRepository languageRepository, ICacheProvider cacheProvider)
        {
            this.languageRepository = languageRepository;
            this.cacheProvider = cacheProvider;
        }

        public bool AddLanguage(LanguageDTO language)
        {
            bool resultOfOperation = false;
            if (language != null || string.IsNullOrEmpty(language.LanguageName))
            {
                try
                {
                    resultOfOperation = languageRepository.AddLanguage(new Language()
                    {
                        LanguageName = language.LanguageName                       
                    });
                }
                catch (DataBaseConnectionException ex)
                {
                    throw new DataBaseConnectionException(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
            if (resultOfOperation)
            {
                cacheProvider.Remove(cacheKeyGetLanguages);
            }
            return resultOfOperation;
        }

        public bool DeleteLanguage(int? id)
        {
            bool resultOfOperation = false;
            if (id.HasValue)
            {
                try
                {
                    resultOfOperation = languageRepository.DeleteLanguage(id);

                }
                catch (DataBaseConnectionException ex)
                {
                    throw new DataBaseConnectionException(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
            if (resultOfOperation)
            {
                cacheProvider.Remove(cacheKeyGetLanguages);
            }
            return resultOfOperation;
        }

        public bool RestoreLanguage(int? id)
        {
            bool resultOfOperation = false;
            if (id.HasValue)
            {
                try
                {
                    resultOfOperation = languageRepository.RestoreLanguage(id);

                }
                catch (DataBaseConnectionException ex)
                {
                    throw new DataBaseConnectionException(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
            if (resultOfOperation)
            {
                cacheProvider.Remove(cacheKeyGetLanguages);
            }
            return resultOfOperation;
        }

        public LanguageDTO[] GetLanguages()
        {
            if (cacheProvider.Get(cacheKeyGetLanguages) != null)
            {
                return cacheProvider.Get(cacheKeyGetLanguages) as LanguageDTO[];
            }
            var languageDTOList = new List<LanguageDTO>();
            try
            {
                var languages = languageRepository.GetLanguages();
                if (languages == null)
                {
                    return null;
                }
                foreach (var item in languages)
                {
                    languageDTOList.Add(new LanguageDTO()
                    {
                        LanguageId = item.LanguageId,
                        LanguageName = item.LanguageName                       
                    });
                }

            }
            catch (DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            cacheProvider.Insert(cacheKeyGetLanguages, languageDTOList.ToArray<LanguageDTO>());
            return languageDTOList.ToArray<LanguageDTO>();
        }
    }
}
