using FitnessProject.BusinessLogic.DBModel;
using FitnessProject.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProject.BusinessLogic.Core
{
     public class UserApi
     {
          internal PostResponse UserLoginAction(ULoginData data)
          {
               UDbTable result;
               var validate = new EmailAddressAttribute();
               
               if (validate.IsValid(data.Credential))
               {
                    using (var db = new FitnessDbContext())
                    {
                         result = db.Users.FirstOrDefault(u => u.Email == data.Credential && u.Password == data.Password);
                    }
                    if (result == null)
                    {
                         return new PostResponse { Status = false, StatusMsg = "The email or Password is Incorrect" };
                    }
                    using (var todo = new FitnessDbContext())
                    {
                         result.LastIp = data.LoginIp;
                         result.LastLogin = data.LoginDateTime;
                         todo.Entry(result).State = EntityState.Modified;
                         todo.SaveChanges();
                    }
                    return new PostResponse { Status = true };
               }
               else
               {
                    using (var db = new FitnessDbContext())
                    {
                         result = db.Users.FirstOrDefault(u => u.Username == data.Credential && u.Password == data.Password);
                    }
                    if (result == null)
                    {
                         return new PostResponse { Status = false, StatusMsg = "The username or Password is Incorrect" };
                    }
                    using (var todo = new FitnessDbContext())
                    {
                         result.LastIp = data.LoginIp;
                         result.LastLogin = data.LoginDateTime;
                         todo.Entry(result).State = EntityState.Modified;
                         todo.SaveChanges();
                    }

                    return new PostResponse { Status = true };
               }
          }

          internal PostResponse UserSignupAction(USignupData data)
          {
               UDbTable result;
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(data.Email))
               {
                    if (data.Password != data.ConfirmPassword)
                    {
                         return new PostResponse { Status = false, StatusMsg = "The Passwords don't match" };
                    }
                    if (data.Password == null || data.Email == null)
                    {
                         return new PostResponse { Status = false, StatusMsg = "The Email or Password is empty" };
                    }
                    if (data.Password != data.ConfirmPassword)
                    {
                         return new PostResponse { Status = false, StatusMsg = "The Password dont match " };
                    }
                    if (data.Password.Length < 8)
                    {
                         return new PostResponse { Status = false, StatusMsg = "Password min 8 characters" };
                    }
                    if (data.Username.Length < 5)
                    {
                         return new PostResponse { Status = false, StatusMsg = "Username min 5 characters" };
                    }
                    using (var db = new FitnessDbContext())
                    {
                         result = db.Users.FirstOrDefault(u => u.Email == data.Email);
                    }
                    if (result != null)
                    {
                         return new PostResponse { Status = false, StatusMsg = "The Email is already taken" };
                    }
                    using (var db = new FitnessDbContext())
                    {
                         result = db.Users.FirstOrDefault(u => u.Username == data.Username);
                    }
                    if (result != null)
                    {
                         return new PostResponse { Status = false, StatusMsg = "Please use a unique username" };
                    }
                   
                    result = new UDbTable
                    {
                         Username = data.Username,
                         Email = data.Email,
                         Password = data.Password,
                         Gender = data.Gender,
                         Age = data.Age,
                         Height = data.Height,
                         Weight = data.Weight,
                         WeightTarget = data.WeightTarget,
                         WorkoutPacketID = data.WorkoutPacketId,
                         LastIp = data.LoginIp,
                         LastLogin = data.LoginDateTime,
                         Level = 0
                    };
                    using (var db = new FitnessDbContext())
                    {
                         db.Users.Add(result);
                         db.SaveChanges();
                    }
                    return new PostResponse { Status = true };
               }
               else
               {
                    return new PostResponse { Status = false, StatusMsg = "Invalid email" };
               }
          }
     }
}
