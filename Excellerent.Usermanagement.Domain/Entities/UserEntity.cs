using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.SharedModules.Seed;
using Excellerent.Usermanagement.Domain.Enums;
using Excellerent.Usermanagement.Domain.Models;
using System;

namespace Excellerent.Usermanagement.Domain.Entities
{
    public class UserEntity : BaseEntity<User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public UserStatus Status { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public virtual EmployeeEntity Employee { get; set; }
        public Guid EmployeeId { get; set; }
        public UserEntity(User model): base(model)
        {
            Guid = model.Guid;
            UserName = model.UserName;
            Password = model.Password;
            Email = model.Email;
            Tel = model.Tel;
            FirstName = model.FirstName;
            MiddleName = model.MiddleName;
            LastName = model.LastName;
            Status = model.Status;
            LastActivityDate = model.LastActivityDate;
            Employee = Employee == null?null: new EmployeeEntity(model.Employee);
            EmployeeId = model.EmployeeId;
        }
        public UserEntity()
        {

        }
        public override User MapToModel()
        {
            User model = new User();
            model.Guid = Guid;
            model.UserName = UserName;
            model.Password = Password;
            model.Email = Email;
            model.Tel = Tel;
            model.FirstName = FirstName;
            model.MiddleName = MiddleName;
            model.LastName = LastName;
            model.Status = Status;
            model.LastActivityDate = LastActivityDate;
            model.Employee = Employee == null? null: Employee.MapToModel();
            model.EmployeeId = EmployeeId;
            return model;
        }

        
        public override User MapToModel(User model)
        {
            model.Email = Email;
            model.Tel = Tel;
            model.FirstName = FirstName;
            model.MiddleName = MiddleName;
            model.LastName = LastName;
            model.Status = Status;
            return model;
        }
    }
}
