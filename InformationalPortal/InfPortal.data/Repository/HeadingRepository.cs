using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using InfPortal.common.Logs;
using InfPortal.common.Exceptions;
using InfPortal.data.Entities;
using InfPortal.data.HeadingProxy;
using InfPortal.data.Interfaces;

namespace InfPortal.data.Repository
{
    internal class HeadingRepository : IHeadingRepository
    {
        HeadingServiceClient headingClient;
        const string errorArgument = "Parametr is invalid";
        public HeadingRepository()
        {            
            headingClient = new HeadingServiceClient("BasicHttpBinding_IHeadingService");
        }
        public bool AddHeading(Heading heading)
        {
            bool resultOfOperation = false;
            if (heading != null || string.IsNullOrEmpty(heading.Name))
            {
                try
                {
                    resultOfOperation = headingClient.AddHeading(new HeadingEntity()
                    {
                        Name = heading.Name,
                        Description = heading.Description
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
        public bool EditHeading(Heading heading)
        {
            bool resultOfOperation = false;
            if (heading != null || string.IsNullOrEmpty(heading.Name))
            {
                try
                {
                    resultOfOperation = headingClient.EditHeading(new HeadingEntity()
                    {
                        Id=heading.Id,
                        Name=heading.Name,
                        Description=heading.Description
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

        public bool DeleteHeading(int? id)
        {
            bool resultOfOperation = false;
            if (id.HasValue)
            {
                try
                {
                    resultOfOperation = headingClient.DeleteHeading(id);                    
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

        public Heading[] GetHeadings()
        {
            var headingList = new List<Heading>();
            try
            {
                HeadingEntity[] headingEntities = headingClient.GetHeadings();
                if (headingEntities == null)
                {
                    return null;
                }
                foreach (var item in headingEntities)
                {
                    headingList.Add(new Heading()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description
                    });
                }
            }
            catch (FaultException<ServiceException> ex)
            {
                throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
            }
            return headingList.ToArray<Heading>();
        }
    }
}
