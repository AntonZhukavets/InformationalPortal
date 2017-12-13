using InfPortal.common.Exceptions;
using InfPortal.data.Entities;
using InfPortal.data.Interfaces;
using InfPortal.data.UserProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InfPortal.data.Implementations
{

    public class UserServiceProvider : IUserServiceProvider
    {
        UserServiceClient userClient;
        const string errorArgument = "Parametr is invalid";
        public UserServiceProvider()
        {
            userClient = new UserServiceClient("BasicHttpBinding_IUserService");
        }
        public bool IsValidUser(string login, string password)
        {
            bool isValidUser=false;

            try
            {
                isValidUser = userClient.IsValidUser(login, password);
                
            }
            catch (FaultException<ServiceException> ex)
            {
                throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
            }
            return isValidUser;
        }

        public User GetUserByLogin(string login)
        {
            var user = new User();
            try
            {
                var userEntity = userClient.GetUserByLogin(login);
                user.Id = userEntity.Id;
                user.FirstName = userEntity.FirstName;
                user.LastName = userEntity.LastName;
                user.Login = userEntity.Login;
                user.Email = userEntity.Email;
                user.Age = userEntity.Age;
                user.role = new Role()
                {
                    Id = userEntity.Role.Id,
                    Name = userEntity.Role.Name,
                    Description = userEntity.Role.Description
                };
            }
            catch (FaultException<ServiceException> ex)
            {
               throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
            }
            return user;
        }


        public void Registration(User user)
        {
            try
            {
                userClient.Register(new UserEntity()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Login = user.Login,
                    Password = user.Password,
                    Age = user.Age
                });
            }
            catch(FaultException<ServiceException> ex)
            {
                throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
            }
        }


        public void UpdateUser(User user)
        {
            try
            {
                userClient.UpdateUser(new UserEntity()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Login = user.Login,
                    Password = user.Password,
                    Age = user.Age,
                    Id=user.Id
                });
            }
            catch (FaultException<ServiceException> ex)
            {
                throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
            }
        }


        public User GetUserById(int? id)
        {
            var user = new User();
            int parsedId;
            if (!int.TryParse(id.ToString().Trim(), out parsedId))
            {
                throw new ArgumentException(errorArgument);
            }
            try
            {
                UserEntity userEntity = userClient.GetUserById(parsedId);
                user.Id = userEntity.Id;
                user.FirstName = userEntity.FirstName;
                user.LastName = userEntity.LastName;
                user.Login = userEntity.Login;
                user.Email = userEntity.Email;
                user.Age = userEntity.Age;
                user.role = new Role()
                {
                    Id = userEntity.Role.Id,
                    Name = userEntity.Role.Name,
                    Description = userEntity.Role.Description
                };
            }
            catch (FaultException<ServiceException> ex)
            {
                throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
            }
            catch (FaultException<ArgumentException> ex)
            {
                throw new ArgumentException(ex.Detail.Message);
            }
            return user;
        }
    }
}

