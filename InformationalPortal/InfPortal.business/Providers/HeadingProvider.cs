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
    public class HeadingProvider : IHeadingProvider
    {
        private readonly IHeadingRepository headingRepository;
        private readonly ICacheProvider cacheProvider;
        private const string errorArgument = "Parametr is invalid";        
        private const string cacheKeyGetHeadings = "GetHeadings";
        
        public HeadingProvider(IHeadingRepository headingRepository, ICacheProvider cacheProvider)
        {
            this.headingRepository = headingRepository;
            this.cacheProvider = cacheProvider;
        }
        public bool AddHeading(HeadingDTO heading)
        {
            bool resultOfOperation = false;
            if (heading != null || string.IsNullOrEmpty(heading.Name))
            {
                try
                {
                    resultOfOperation = headingRepository.AddHeading(new Heading()
                    {
                        Name = heading.Name,
                        Description = heading.Description
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
                cacheProvider.Remove(cacheKeyGetHeadings);
            }
            return resultOfOperation;
        }

        public bool EditHeading(HeadingDTO heading)
        {
            bool resultOfOperation = false;
            if (heading != null || string.IsNullOrEmpty(heading.Name))
            {
                try
                {
                    resultOfOperation = headingRepository.EditHeading(new Heading()
                    {
                        Id = heading.Id,
                        Name = heading.Name,
                        Description = heading.Description
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
                cacheProvider.Remove(cacheKeyGetHeadings);
            }
            return resultOfOperation;
        }

        public bool DeleteHeading(int? id)
        {
            bool resultOfOperation = false;
            if (id.HasValue)
            {
                try
                {
                    resultOfOperation = headingRepository.DeleteHeading(id);

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
                cacheProvider.Remove(cacheKeyGetHeadings);
            }
            return resultOfOperation;
        }

        public HeadingDTO[] GetHeadings()
        {
            if (cacheProvider.Get(cacheKeyGetHeadings) != null)
            {
                return cacheProvider.Get(cacheKeyGetHeadings) as HeadingDTO[];
            }
            var headingDTOList = new List<HeadingDTO>();
            try
            {
                var headings = headingRepository.GetHeadings();
                if (headings == null)
                {
                    return null;
                }
                foreach (var item in headings)
                {
                    headingDTOList.Add(new HeadingDTO()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description
                    });
                }

            }
            catch (DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            cacheProvider.Insert(cacheKeyGetHeadings, headingDTOList.ToArray<HeadingDTO>());
            return headingDTOList.ToArray<HeadingDTO>();
        }
    }
}
