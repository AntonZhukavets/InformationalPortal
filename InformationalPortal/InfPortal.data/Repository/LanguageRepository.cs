using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using InfPortal.common.Logs;
using InfPortal.common.Exceptions;
using InfPortal.data.Entities;
using InfPortal.data.LanguageProxy;
using InfPortal.data.Interfaces;

namespace InfPortal.data.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
         LanguageServiceClient languageClient;
        const string errorArgument = "Parametr is invalid";
        public LanguageRepository()
        {
            languageClient = new LanguageServiceClient("BasicHttpBinding_ILanguageService");
        }
        public Language[] GetLanguages()
        {
            var languageList = new List<Language>();
            try
            {
                LanguageEntity[] languageEntities = languageClient.GetLanguages();
                if (languageEntities == null)
                {
                    return null;
                }
                foreach (var item in languageEntities)
                {
                    languageList.Add(new Language()
                    {
                        LanguageId = item.LanguageId,
                        LanguageName = item.LanguageName                       
                    });
                }
            }
            catch (FaultException<ServiceException> ex)
            {
                throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
            }
            return languageList.ToArray<Language>();
        }

        public bool AddLanguage(Language language)
        {
            bool resultOfOperation = false;
            if (language != null || string.IsNullOrEmpty(language.LanguageName))
            {
                try
                {
                    resultOfOperation = languageClient.AddLanguage(new LanguageEntity()
                    {
                        LanguageName = language.LanguageName                       
                    });
                }
                catch (FaultException<ServiceException> ex)
                {
                    throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
                }
                catch (FaultException<ArgumentException> ex)
                {
                    throw new ArgumentException(ex.Detail.Message);
                }
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
                    resultOfOperation = languageClient.DeleteLanguage(id);
                }
                catch (FaultException<ServiceException> ex)
                {
                    throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
                }
                catch (FaultException<ArgumentException> ex)
                {
                    throw new ArgumentException(ex.Detail.Message);
                }
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
                    resultOfOperation = languageClient.RestoreLanguage(id);
                }
                catch (FaultException<ServiceException> ex)
                {
                    throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
                }
                catch (FaultException<ArgumentException> ex)
                {
                    throw new ArgumentException(ex.Detail.Message);
                }
            }
            return resultOfOperation;
        }
    }
}
