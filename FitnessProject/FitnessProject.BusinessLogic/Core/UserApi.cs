using System;
using System.Collections.Generic;
using FitnessProject.BusinessLogic.DBModel;
using FitnessProject.Domain.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using FitnessProject.Domain.Entities.Nutrition;
using FitnessProject.Domain.Entities.Session;
using FitnessProject.Helpers;
using Newtonsoft.Json;

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
        
        
        public PostResponse AddNutrition(int nutritionId, int nutritionQuantity, int userId)
          {
               UProgressDbTable result;
               NutritionDataMinimal json = new NutritionDataMinimal()
               {
                    Id = nutritionId,
                    Quantity = nutritionQuantity
               };
               var date = DateTime.Now.ToString("dd/MM/yyyy");
               using (var db = new FitnessDbContext())
               {
                    result = db.Progress.FirstOrDefault(u => u.UserId == userId && u.Date == date);

                    if (result == null)
                    {
                         result = new UProgressDbTable()
                         {
                              Date = date,
                              UserId = userId,
                              NutritionList = JsonConvert.SerializeObject(new List<NutritionDataMinimal> { json })
                         };

                         db.Progress.Add(result);
                    }
                    else
                    {
                         if (result.NutritionList == null)
                         {
                              result.NutritionList = JsonConvert.SerializeObject(new List<NutritionDataMinimal> { json });
                         }
                         var existingNutritionDataList = JsonConvert.DeserializeObject<List<NutritionDataMinimal>>(result.NutritionList);
                         existingNutritionDataList.Add(json);
                         result.NutritionList = JsonConvert.SerializeObject(existingNutritionDataList);
                    }

                    db.SaveChanges();
               }

               List<NutritionDataMinimal> nutritionDataList = new List<NutritionDataMinimal>();
               using (var db = new FitnessDbContext())
               {
                    result = db.Progress.FirstOrDefault(u => u.UserId == userId && u.Date == date);
                    nutritionDataList = JsonConvert.DeserializeObject<List<NutritionDataMinimal>>(result.NutritionList);
               }

               return new PostResponse { Status = true };
          }

          public PostResponse AddWorkout(int userId)
          {
               UProgressDbTable result;
               var date = DateTime.Now.ToString("dd/MM/yyyy");
               using (var db = new FitnessDbContext())
               {
                    result = db.Progress.FirstOrDefault(u => u.UserId == userId && u.Date == date);

                    if (result == null)
                    {
                         result = new UProgressDbTable()
                         {
                              Date = date,
                              NutritionList = null,
                              UserId = userId,
                              WorkoutDone = 1
                         };

                         db.Progress.Add(result);
                    }
                    else
                    {
                         var existingWorkoutList = result.WorkoutDone;
                         existingWorkoutList++;
                         result.WorkoutDone = existingWorkoutList;
                    }
                    try
                    {
                         db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                         foreach (var error in ex.EntityValidationErrors)
                         {
                              foreach (var validationError in error.ValidationErrors)
                              {
                                   Debug.WriteLine($"Property: {validationError.PropertyName}");
                                   Debug.WriteLine($"Error: {validationError.ErrorMessage}");
                              }
                         }
                    }
               }
               return new PostResponse { Status = true };
          }

          public PostResponse AddSleep(int sleepHours, int userId)
          {
               UProgressDbTable result;
               var date = DateTime.Now.ToString("dd/MM/yyyy");
               using (var db = new FitnessDbContext())
               {
                    result = db.Progress.FirstOrDefault(u => u.UserId == userId && u.Date == date);

                    if (result == null)
                    {
                         result = new UProgressDbTable()
                         {
                              Date = date,
                              UserId = userId,
                              SleepHours = sleepHours
                         };

                         db.Progress.Add(result);
                    }
                    else
                    {
                         result.SleepHours += sleepHours;
                    }

                    db.SaveChanges();

               }

               return new PostResponse { Status = true };
          }
          
          public List<UProgressData> RestoreProgressAction(int numberOfDays, int userId)
          {
              var dateLimitString = DateTime.Now.AddDays(-numberOfDays).ToString("dd/MM/yyyy");

              if (DateTime.TryParseExact(dateLimitString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateLimit))
              {
              }
              else
              {
                  throw new Exception("Invalid date format");
              }
              List<UProgressData> activity = new List<UProgressData>();
              using (var db = new FitnessDbContext())
              {
                  var progressList = db.Progress
                      .Where(u => u.UserId == userId)
                      .AsEnumerable()
                      .Where(u => 
                      {
                          bool success = DateTime.TryParseExact(u.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate);
                          return success && parsedDate >= dateLimit;
                      })
                      .ToList();

                  foreach (var item in progressList)
                  {
                      activity.Add(new UProgressData
                      {
                          Date = item.Date,
                          NutitionList = item.NutritionList,
                          WorkoutDone = item.WorkoutDone,
                          SleepHours = item.SleepHours
                      });
                  }
              }
              activity.Sort((x, y) =>
              {
                  var successX = DateTime.TryParseExact(x.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateX);
                  var successY = DateTime.TryParseExact(y.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateY);

                  switch (successX)
                  {
                      case true when successY:
                          return dateX.CompareTo(dateY);
                      case true:
                          return -1;
                      default:
                      {
                          return successY ? 1 : 0;
                      }
                  }
              });

              activity.Reverse();
              return activity;
          }
    }
}