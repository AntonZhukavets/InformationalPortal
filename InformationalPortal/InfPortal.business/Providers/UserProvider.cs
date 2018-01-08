using System;
using System.Collections.Generic;
using System.Text;
using InfPortal.business.DTO;
using InfPortal.business.Interfaces;
using InfPortal.common.Exceptions;
using InfPortal.data.Entities;
using InfPortal.data.Repository;
using InfPortal.data.Interfaces;

namespace InfPortal.business.Providers
{
    public class UserProvider : IUserProvider
    {
        const string errorArgument = "Parametrs is invalid";
        private IUserRepository userRepository;
        private ICacheProvider cacheProvider;
        public UserProvider(IUserRepository userRepository, ICacheProvider cacheProvider)
        {
            this.userRepository = userRepository;
            this.cacheProvider = cacheProvider;
        }
        public bool Registration(UserDTO user)
        {
            bool resultOfRegistration = false;
            if (user != null)
            {
                try
                {
                    resultOfRegistration = userRepository.AddUser(new User()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Login = user.Login,
                        Password = user.Password
                    });
                }
                catch (DataBaseConnectionException ex)
                {
                    throw new DataBaseConnectionException(ex.Message);
                }
            }
            return resultOfRegistration;
        }

        public bool IsValidUser(string login, string password)
        {
            bool isValidUser = false;
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    isValidUser = userRepository.IsValidUser(login, password);
                }
                catch (DataBaseConnectionException ex)
                {
                    throw new DataBaseConnectionException(ex.Message);
                }
            }
            return isValidUser;
        }


        public UserDTO GetUserByLoginAndPassword(string login, string password)
        {
            if(string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(errorArgument);
            }
            var userDTO = new UserDTO();
            try
            {
                var user = userRepository.GetUserByLoginAndPassword(login, password);
                if (user == null)
                { 
                    return null;
                }
                userDTO.Id = user.Id;
                userDTO.FirstName = user.FirstName;
                userDTO.LastName = user.LastName;
                userDTO.Email = user.Email;
                userDTO.Password = user.Password;
                userDTO.Login = user.Login;
                userDTO.role = new RoleDTO()
                {
                    Id = user.role.Id,
                    Name = user.role.Name,
                    Description = user.role.Description
                };            
            }
            catch (DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            return userDTO;
        }


        public bool UpdateUser(UserDTO user)
        {
            bool resultOfUpdating = false;
            if (user != null)
            {
                try
                {
                    resultOfUpdating = userRepository.UpdateUser(new User()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Login = user.Login,
                        Password = user.Password,
                        Id = user.Id
                    });
                }
                catch (DataBaseConnectionException ex)
                {
                    throw new DataBaseConnectionException(ex.Message);
                }
            }
            return resultOfUpdating;
        }


        public UserDTO GetUserById(int? id)
        {
            if (!id.HasValue)
            {
                throw new ArgumentException(errorArgument);
            }
            try
            {
                var user = userRepository.GetUserById(id);
                if (user == null)
                { 
                    return null; 
                }
                var userDTO = new UserDTO();
                userDTO.Id = user.Id;
                userDTO.FirstName = user.FirstName;
                userDTO.LastName = user.LastName;
                userDTO.Email = user.Email;
                userDTO.Password = user.Password; 
                userDTO.Login = user.Login;
                userDTO.role = new RoleDTO()
                {
                    Id = user.role.Id,
                    Name = user.role.Name,
                    Description = user.role.Description
                };
                userDTO.IsBlocked = user.IsBlocked;
                return userDTO;                
            }
            catch (DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }            
        }


        public UserDTO[] GetAllUsers()
        {
            try
            {
                var users = userRepository.GetAllUsers();
                if (users == null)
                {
                    return null;
                }
                var usersDTO = new List<UserDTO>();
                foreach (var item in users)
                {
                    usersDTO.Add(new UserDTO()
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email,
                        Login = item.Login,
                        Password = item.Password,
                        role = new RoleDTO()
                        {
                            Id = item.role.Id,  
                            Name = item.role.Name,
                            Description = item.role.Description
                        },
                        IsBlocked = item.IsBlocked
                    });
                }
                return usersDTO.ToArray();
                
            }
            catch (DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }            
        }


        public bool DeleteUser(int? id)
        {
            bool resultOfOperation = false;
            if (id.HasValue)
            {
                try
                {
                    resultOfOperation = userRepository.DeleteUser(id);
                }
                catch (DataBaseConnectionException ex)
                {
                    throw new DataBaseConnectionException(ex.Message);
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
                    resultOfOperation = userRepository.ResumeUser(id);
                }
                catch (DataBaseConnectionException ex)
                {
                    throw new DataBaseConnectionException(ex.Message);
                }
            }
            return resultOfOperation;
        }

        public bool MakeAdmin(int? id)
        {
            bool resultOfOperation = false;
            if (id.HasValue)
            {
                try
                {
                    resultOfOperation = userRepository.MakeAdmin(id);
                }
                catch (DataBaseConnectionException ex)
                {
                    throw new DataBaseConnectionException(ex.Message);
                }
            }
            return resultOfOperation;
        }

    }
}
