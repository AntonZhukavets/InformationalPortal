using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using InfPortal.common.Exceptions;
using InfPortal.data.Entities;
using InfPortal.data.Interfaces;
using InfPortal.data.UserProxy;

namespace InfPortal.data.Repository
{  
    internal class UserRepository : IUserRepository
    {
        UserServiceClient userClient;
        const string errorArgument = "Parametr is invalid";
        public UserRepository()
        {
            userClient = new UserServiceClient("BasicHttpBinding_IUserService");
        }
        public bool IsValidUser(string login, string password)
        {
            bool isValidUser = false;
            if (!string.IsNullOrEmpty(login) || !string.IsNullOrEmpty(password))
            {
                try
                {
                    isValidUser = userClient.IsValidUser(login, password);

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
            return isValidUser;
        }

        public User GetUserByLoginAndPassword(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                 throw new ArgumentException(errorArgument);
            }
            var user = new User();
            try
            {
                var userEntity = userClient.GetUserByLoginAndPassword(login, password);
                if (userEntity==null)
                {
                    return null;
                }
                user.Id = userEntity.Id;
                user.FirstName = userEntity.FirstName;
                user.LastName = userEntity.LastName;
                user.Login = userEntity.Login;
                user.Email = userEntity.Email;
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


        public bool AddUser(User user)
        {            
            if (user == null)
            {
                throw new ArgumentException(errorArgument);
            }
            bool resultOfRegistration = false;
            try
            {
                resultOfRegistration=userClient.Register(new UserEntity()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Login = user.Login,
                    Password = user.Password
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
            return resultOfRegistration;
        }


        public bool UpdateUser(User user)
        {
            bool resultOfUpdating = false;
            if (user == null)
            {
                throw new ArgumentException(errorArgument);
            }
            try
            {
                resultOfUpdating = userClient.UpdateUser(new UserEntity()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Login = user.Login,
                    Password = user.Password,
                    Id = user.Id
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
            return resultOfUpdating;
        }
        public User GetUserById(int? id)
        {            
            
            if (!id.HasValue || id==0)
            {
                throw new ArgumentException(errorArgument);
            }
            var user = new User();            
            try
            {
                var userEntity = userClient.GetUserById(id);
                if(userEntity==null)
                {
                    return null;
                }
                user.Id = userEntity.Id;
                user.FirstName = userEntity.FirstName;
                user.LastName = userEntity.LastName;
                user.Login = userEntity.Login;
                user.Email = userEntity.Email;                
                user.role = new Role()
                {
                    Id = userEntity.Role.Id,
                    Name = userEntity.Role.Name,
                    Description = userEntity.Role.Description
                };
                user.IsBlocked = userEntity.IsBlocked;
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


        public User[] GetAllUsers()
        {
            var users = new List<User>();                       
            try
            {
                UserEntity[] userEntities = userClient.GetAllUsers();
                if (userEntities==null)
                {
                    return null;
                }
                foreach (var item in userEntities)
                {
                    users.Add(new User()
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email,
                        Login = item.Login,
                        Password = item.Password,
                        role = new Role()
                        {
                            Id = item.Role.Id,
                            Name = item.Role.Name,
                            Description = item.Role.Description
                        },
                        IsBlocked = item.IsBlocked
                    });
                }
               
            }
            catch (FaultException<ServiceException> ex)
            {
                throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
            }
            catch (FaultException<ArgumentException> ex)
            {
                throw new ArgumentException(ex.Detail.Message);
            }
            return users.ToArray<User>();
        }


        public bool DeleteUser(int? id)
        {
            bool resultOfOperation = false;
            if (id.HasValue)
            {
                try
                {
                    resultOfOperation = userClient.DeleteUser(id);                    
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

        public bool ResumeUser(int? id)
        {
            bool resultOfOperation = false;
            if (id.HasValue)
            {
                try
                {
                    resultOfOperation = userClient.ResumeUser(id);
                }
                catch (FaultException<ServiceException> ex)
                {
                    throw new DataBaseConnectionException(ex.Detail.ErrorMessage);
                }

            }
            return resultOfOperation;
        }

        public bool MakeAdmin(int? id)
        {
            bool resultOfOperation = false;
            if (id != null)
            {
                try
                {
                    resultOfOperation = userClient.MakeAdmin(id);
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
