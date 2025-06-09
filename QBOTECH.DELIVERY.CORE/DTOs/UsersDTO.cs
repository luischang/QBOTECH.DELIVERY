using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBOTECH.DELIVERY.CORE.DTOs
{
    public class UsersDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
    public class UsersCreateDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
    public class UsersCreateResponseDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;        
        public string CountryCode { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
    public class UsersUpdateDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
    //Get users DTO without password
    public class UsersListDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

    }

    // UsersSignInDTO
    public class UsersSignInDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class UsersResponseDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Token { get; set; }

    }

}
