using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project_Internal.Filters
{
    public interface IUserAccount
    {
        Task<IEnumerable<AddUserModel>>  GetUsers ();
        Task<AddUserModel> GetUserById (int Id);
        Task<int> AddUser (AddUserModel model);
        Task<int> DeleteUser(int id);
        Task<int> UpdateUser(AddUserModel model);
    }

     public   class AddUserModel {
   [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set;  }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class Permission {
        [Key,DatabaseGenerated( DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual AddUserModel? AddUserModel { get; set; }
        public string? UserType { get; set; }
    } 
}