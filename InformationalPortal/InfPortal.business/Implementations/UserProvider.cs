using InfPortal.business.DTO;
using InfPortal.business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfPortal.data.Interfaces;
using InfPortal.data.Entities;
using InfPortal.data.Implementations;
using InfPortal.common.Exceptions;

namespace InfPortal.business.Implementations
{
    public class UserProvider:IUserProvider
    {
        const string errorArgument = "Parametrs is invalid";
        private IUserServiceProvider userServiceProvider;
        public UserProvider()
        {
            this.userServiceProvider = new UserServiceProvider();
        }
        public void Registration(UserDTO user)
        {
            try
            {
                userServiceProvider.Registration(new User()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Login = user.Login,
                    Password = user.Password,
                    Age = user.Age
                });
            }
            catch(DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
        }

        public bool IsValidUser(string login, string password)
        {
            bool isValidUser = false;
            try
            {
                isValidUser=userServiceProvider.IsValidUser(login, password);
            }
            catch(DataBaseConnectionException ex )
            {
                throw new DataBaseConnectionException(ex.Message);
            }
            return isValidUser;        }


        public UserDTO GetUserByLogin(string login)
        {
            var userDTO = new UserDTO();
            try
            {
                var user = userServiceProvider.GetUserByLogin(login);
                if (user != null)
                {
                
                    userDTO.Id = user.Id;
                    userDTO.FirstName = user.FirstName;
                    userDTO.LastName = user.LastName;
                    userDTO.Email = user.Email;
                    userDTO.Password = user.Password;
                    userDTO.Login = user.Login;
                    userDTO.Age = user.Age;
                    userDTO.role = new RoleDTO()
                    {
                        Id = user.role.Id,
                        Name = user.role.Name,
                        Description = user.role.Description
                    };
                }
            }
            catch (DataBaseConnectionException ex)
            {                
                throw new DataBaseConnectionException(ex.Message);
            }
            return userDTO;
        }


        public void UpdateUser(UserDTO user)
        {
            try
            {
                userServiceProvider.UpdateUser(new User()
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
            catch (DataBaseConnectionException ex)
            {
                throw new DataBaseConnectionException(ex.Message);
            }
        }


        public UserDTO GetUserById(int? id)
        {
            var userDTO = new UserDTO();
            int parsedId;
            if (!int.TryParse(id.ToString().Trim(), out parsedId))
            {
                throw new ArgumentException(errorArgument);
            }
            try
            {
                var user = userServiceProvider.GetUserById(parsedId);
                if (user != null)
                {
                    userDTO.Id = user.Id;
                    userDTO.FirstName = user.FirstName;
                    userDTO.LastName = user.LastName;
                    userDTO.Email = user.Email;
                    userDTO.Password = user.Password;
                    userDTO.Login = user.Login;
                    userDTO.Age = user.Age;
                    userDTO.role = new RoleDTO()
                    {
                    Id = user.role.Id,
                    Name = user.role.Name,
                    Description = user.role.Description
                    };
                }
            }
            catch (DataBaseConnectionException ex)
            {                
                throw new DataBaseConnectionException(ex.Message);
            }
            return userDTO;
        }
    }
}
