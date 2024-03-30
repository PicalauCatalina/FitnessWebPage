﻿using System;
using FitnessProject.BusinessLogic.DBModel;
using FitnessProject.Domain.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FitnessProject.Domain.Entities.Session;
using FitnessProject.Helpers;

namespace FitnessProject.BusinessLogic.Core
{
    public class UserApi
    {
        internal PostResponse UserLoginAction(ULoginData data)
        {
            UDbTable result;
            var validate = new EmailAddressAttribute();
            var hashPassword = LoginHelper.HashGen(data.Password);

            if (validate.IsValid(data.Credential))
            {
                using (var db = new FitnessDbContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Email == data.Credential && u.Password == hashPassword);
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
                    result = db.Users.FirstOrDefault(u => u.Username == data.Credential && u.Password == hashPassword);
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

                var hashPassword = LoginHelper.HashGen(data.Password);

                result = new UDbTable
                {
                    Username = data.Username,
                    Email = data.Email,
                    Password = hashPassword,
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

            return new PostResponse { Status = false, StatusMsg = "Invalid email" };
        }

        public HttpCookie UserGenCookies(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            UDbTable result;
            using (var db = new FitnessDbContext())
            {
                result = db.Users.FirstOrDefault(u => u.Email == loginCredential || u.Username == loginCredential);
            }

            using (var db = new FitnessDbContext())
            {
                SessionsDbTable current = db.Sessions.FirstOrDefault(s => s.UserId == result.Id);

                if (current != null)
                {
                    current.Cookie = apiCookie.Value;
                    current.ExpirationDate = DateTime.Now.AddDays(1);
                    using (var up = new FitnessDbContext())
                    {
                        db.Entry(current).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    var session = new SessionsDbTable();
                    session.Cookie = apiCookie.Value;
                    session.ExpirationDate = DateTime.Now.AddDays(1);
                    session.UserId = result.Id;
                    db.Sessions.Add(session);
                    db.SaveChanges();
                }
            }

            return apiCookie;
        }

        public UserMinimal GetUserDataByCookie(string cookie)
        {
            SessionsDbTable cData;
            using (var db = new FitnessDbContext())
            {
                cData = db.Sessions.FirstOrDefault(s => s.Cookie == cookie);
                if (cData == null)
                {
                    return null;
                }
            }

            UDbTable uData;
            using (var db = new FitnessDbContext())
            {
                uData = db.Users.FirstOrDefault(u => u.Id == cData.UserId);
                if (uData == null)
                {
                    return null;
                }
            }

            return new UserMinimal
            {
                Id = uData.Id,
                Username = uData.Username,
                Email = uData.Email,
                Gender = uData.Gender,
                Age = uData.Age,
                Height = uData.Height,
                Weight = uData.Weight,
                WeightTarget = uData.WeightTarget,
                WorkoutPacketId = uData.WorkoutPacketID,
                Level = uData.Level
            };
        }
    }
}