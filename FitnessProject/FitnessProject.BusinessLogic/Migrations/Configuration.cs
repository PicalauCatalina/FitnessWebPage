using System.Collections.Generic;
using FitnessProject.BusinessLogic.DBModel;
using FitnessProject.Domain.Entities.Nutrition;
using FitnessProject.Domain.Entities.Workout;

namespace FitnessProject.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<FitnessDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "FitnessProject.BusinessLogic.DBModel.FitnessDbContext";
        }

        protected override void Seed(FitnessDbContext context)
        {
            if (!context.Nutrition.Any())
            {
                var items = new List<NutritionDbTable>
                {
                    new NutritionDbTable
                    {
                        Name = "Mar", Protein = 0.3, Carbohydrate = 13, Fat = 0.2,
                        EnergyValue = 0.3 * 4 + 13 * 4 + 0.2 * 9
                    },
                    new NutritionDbTable
                    {
                        Name = "Orez", Protein = 2.6, Carbohydrate = 69, Fat = 0.3,
                        EnergyValue = 2.6 * 4 + 69 * 4 + 0.3 * 9
                    },
                    new NutritionDbTable
                    {
                        Name = "Carne de pui", Protein = 20.6, Carbohydrate = 0, Fat = 5.6,
                        EnergyValue = 20.6 * 4 + 0 + 5.6 * 9
                    },
                    new NutritionDbTable
                    {
                        Name = "Cartofi", Protein = 2, Carbohydrate = 17, Fat = 0.1,
                        EnergyValue = 2 * 4 + 17 * 4 + 0.1 * 9
                    },
                    new NutritionDbTable
                    {
                        Name = "Hrisca", Protein = 8, Carbohydrate = 72, Fat = 1.3,
                        EnergyValue = 8 * 4 + 72 * 4 + 1.3 * 9
                    },
                    new NutritionDbTable
                    {
                        Name = "Banane", Protein = 1.3, Carbohydrate = 22, Fat = 0.3,
                        EnergyValue = 1.3 * 4 + 22 * 4 + 0.3 * 9
                    },
                    new NutritionDbTable
                    {
                        Name = "Branza de vaca", Protein = 14, Carbohydrate = 2, Fat = 18,
                        EnergyValue = 14 * 4 + 2 * 4 + 18 * 9
                    },
                    new NutritionDbTable
                    {
                        Name = "Iaurt", Protein = 4.3, Carbohydrate = 4.7, Fat = 3.3,
                        EnergyValue = 4.3 * 4 + 4.7 * 4 + 3.3 * 9
                    },
                    new NutritionDbTable
                        { Name = "Peste", Protein = 20, Carbohydrate = 0, Fat = 5, EnergyValue = 20 * 4 + 0 + 5 * 9 },
                    new NutritionDbTable
                    {
                        Name = "Carne de vita", Protein = 21, Carbohydrate = 0, Fat = 3.1,
                        EnergyValue = 21 * 4 + 0 + 3.1 * 9
                    },
                    new NutritionDbTable
                    {
                        Name = "Carne de porc", Protein = 20, Carbohydrate = 0, Fat = 9.1,
                        EnergyValue = 20 * 4 + 0 + 9.1 * 9
                    }
                };

                items.ForEach(c => context.Nutrition.Add(c));
                context.SaveChanges();
            }

            if (!context.Workout.Any())
            {
                var item = new WorkoutDbTable
                {
                    PacketName = "Antrenament de baza",
                    Json = @"
                              {
                                ""Json"": [
                                  {
                                    ""Day"": ""Monday"",
                                    ""Exercise"": [
                                      {
                                        ""Name"": ""Impins cu elasticul"",
                                        ""Reps"": 12,
                                        ""Sets"": 3,
                                        ""GifLink"": ""https://64.media.tumblr.com/699805fcccf6cd4e145a49de211469f7/tumblr_mxdxb5n6ju1ro3593o1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Picioare"",
                                        ""Reps"": 15,
                                        ""Sets"": 5,
                                        ""GifLink"": ""https://64.media.tumblr.com/33574e89c95d9f6dd3bee79e9a585efb/tumblr_mxdxb5n6ju1ro3593o4_r1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Abdomen oblic"",
                                        ""Reps"": 8,
                                        ""Sets"": 4,
                                        ""GifLink"": ""https://64.media.tumblr.com/e8ae2d108cb5410f8eb13aa345865281/tumblr_mxdxb5n6ju1ro3593o6_r1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Abdomen lateral"",
                                        ""Reps"": 12,
                                        ""Sets"": 5,
                                        ""GifLink"": ""https://64.media.tumblr.com/82445f343ecce110c12e0952c96e5f0a/tumblr_mxdxb5n6ju1ro3593o9_r1_500.gifv""
                                      }
                                    ]
                                  },
                                  {
                                    ""Day"": ""Tuesday"",
                                    ""Exercise"": [
                                      {
                                        ""Name"": ""Impins cu elasticul"",
                                        ""Reps"": 12,
                                        ""Sets"": 3,
                                        ""GifLink"": ""https://64.media.tumblr.com/699805fcccf6cd4e145a49de211469f7/tumblr_mxdxb5n6ju1ro3593o1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Picioare"",
                                        ""Reps"": 15,
                                        ""Sets"": 5,
                                        ""GifLink"": ""https://64.media.tumblr.com/33574e89c95d9f6dd3bee79e9a585efb/tumblr_mxdxb5n6ju1ro3593o4_r1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Abdomen oblic"",
                                        ""Reps"": 8,
                                        ""Sets"": 4,
                                        ""GifLink"": ""https://64.media.tumblr.com/e8ae2d108cb5410f8eb13aa345865281/tumblr_mxdxb5n6ju1ro3593o6_r1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Abdomen lateral"",
                                        ""Reps"": 12,
                                        ""Sets"": 5,
                                        ""GifLink"": ""https://64.media.tumblr.com/82445f343ecce110c12e0952c96e5f0a/tumblr_mxdxb5n6ju1ro3593o9_r1_500.gifv""
                                      }
                                    ]
                                  },
                                  {
                                    ""Day"": ""Wednesday"",
                                    ""Exercise"": [
                                      {
                                        ""Name"": ""Impins cu elasticul"",
                                        ""Reps"": 12,
                                        ""Sets"": 3,
                                        ""GifLink"": ""https://64.media.tumblr.com/699805fcccf6cd4e145a49de211469f7/tumblr_mxdxb5n6ju1ro3593o1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Picioare"",
                                        ""Reps"": 15,
                                        ""Sets"": 5,
                                        ""GifLink"": ""https://64.media.tumblr.com/33574e89c95d9f6dd3bee79e9a585efb/tumblr_mxdxb5n6ju1ro3593o4_r1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Abdomen oblic"",
                                        ""Reps"": 8,
                                        ""Sets"": 4,
                                        ""GifLink"": ""https://64.media.tumblr.com/e8ae2d108cb5410f8eb13aa345865281/tumblr_mxdxb5n6ju1ro3593o6_r1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Abdomen lateral"",
                                        ""Reps"": 12,
                                        ""Sets"": 5,
                                        ""GifLink"": ""https://64.media.tumblr.com/82445f343ecce110c12e0952c96e5f0a/tumblr_mxdxb5n6ju1ro3593o9_r1_500.gifv""
                                      }
                                    ]
                                  },
                                  {
                                    ""Day"": ""Thursday"",
                                    ""Exercise"": [
                                      {
                                        ""Name"": ""Impins cu elasticul"",
                                        ""Reps"": 12,
                                        ""Sets"": 3,
                                        ""GifLink"": ""https://64.media.tumblr.com/699805fcccf6cd4e145a49de211469f7/tumblr_mxdxb5n6ju1ro3593o1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Picioare"",
                                        ""Reps"": 15,
                                        ""Sets"": 5,
                                        ""GifLink"": ""https://64.media.tumblr.com/33574e89c95d9f6dd3bee79e9a585efb/tumblr_mxdxb5n6ju1ro3593o4_r1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Abdomen oblic"",
                                        ""Reps"": 8,
                                        ""Sets"": 4,
                                        ""GifLink"": ""https://64.media.tumblr.com/e8ae2d108cb5410f8eb13aa345865281/tumblr_mxdxb5n6ju1ro3593o6_r1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Abdomen lateral"",
                                        ""Reps"": 12,
                                        ""Sets"": 5,
                                        ""GifLink"": ""https://64.media.tumblr.com/82445f343ecce110c12e0952c96e5f0a/tumblr_mxdxb5n6ju1ro3593o9_r1_500.gifv""
                                      }
                                    ]
                                  },
                                  {
                                    ""Day"": ""Friday"",
                                    ""Exercise"": [
                                      {
                                        ""Name"": ""Impins cu elasticul"",
                                        ""Reps"": 12,
                                        ""Sets"": 3,
                                        ""GifLink"": ""https://64.media.tumblr.com/699805fcccf6cd4e145a49de211469f7/tumblr_mxdxb5n6ju1ro3593o1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Picioare"",
                                        ""Reps"": 15,
                                        ""Sets"": 5,
                                        ""GifLink"": ""https://64.media.tumblr.com/33574e89c95d9f6dd3bee79e9a585efb/tumblr_mxdxb5n6ju1ro3593o4_r1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Abdomen oblic"",
                                        ""Reps"": 8,
                                        ""Sets"": 4,
                                        ""GifLink"": ""https://64.media.tumblr.com/e8ae2d108cb5410f8eb13aa345865281/tumblr_mxdxb5n6ju1ro3593o6_r1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Abdomen lateral"",
                                        ""Reps"": 12,
                                        ""Sets"": 5,
                                        ""GifLink"": ""https://64.media.tumblr.com/82445f343ecce110c12e0952c96e5f0a/tumblr_mxdxb5n6ju1ro3593o9_r1_500.gifv""
                                      }
                                    ]
                                  },
                                  {
                                    ""Day"": ""Saturday"",
                                    ""Exercise"": [
                                      {
                                        ""Name"": ""Impins cu elasticul"",
                                        ""Reps"": 12,
                                        ""Sets"": 3,
                                        ""GifLink"": ""https://64.media.tumblr.com/699805fcccf6cd4e145a49de211469f7/tumblr_mxdxb5n6ju1ro3593o1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Picioare"",
                                        ""Reps"": 15,
                                        ""Sets"": 5,
                                        ""GifLink"": ""https://64.media.tumblr.com/33574e89c95d9f6dd3bee79e9a585efb/tumblr_mxdxb5n6ju1ro3593o4_r1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Abdomen oblic"",
                                        ""Reps"": 8,
                                        ""Sets"": 4,
                                        ""GifLink"": ""https://64.media.tumblr.com/e8ae2d108cb5410f8eb13aa345865281/tumblr_mxdxb5n6ju1ro3593o6_r1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Abdomen lateral"",
                                        ""Reps"": 12,
                                        ""Sets"": 5,
                                        ""GifLink"": ""https://64.media.tumblr.com/82445f343ecce110c12e0952c96e5f0a/tumblr_mxdxb5n6ju1ro3593o9_r1_500.gifv""
                                      }
                                    ]
                                  },
                                  {
                                    ""Day"": ""Sunday"",
                                    ""Exercise"": [
                                      {
                                        ""Name"": ""Impins cu elasticul"",
                                        ""Reps"": 12,
                                        ""Sets"": 3,
                                        ""GifLink"": ""https://64.media.tumblr.com/699805fcccf6cd4e145a49de211469f7/tumblr_mxdxb5n6ju1ro3593o1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Picioare"",
                                        ""Reps"": 15,
                                        ""Sets"": 5,
                                        ""GifLink"": ""https://64.media.tumblr.com/33574e89c95d9f6dd3bee79e9a585efb/tumblr_mxdxb5n6ju1ro3593o4_r1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Abdomen oblic"",
                                        ""Reps"": 8,
                                        ""Sets"": 4,
                                        ""GifLink"": ""https://64.media.tumblr.com/e8ae2d108cb5410f8eb13aa345865281/tumblr_mxdxb5n6ju1ro3593o6_r1_500.gifv""
                                      },
                                      {
                                        ""Name"": ""Abdomen lateral"",
                                        ""Reps"": 12,
                                        ""Sets"": 5,
                                        ""GifLink"": ""https://64.media.tumblr.com/82445f343ecce110c12e0952c96e5f0a/tumblr_mxdxb5n6ju1ro3593o9_r1_500.gifv""
                                      }
                                    ]
                                  }
                                ]
                              }"
                };
                context.Workout.Add(item);
                context.SaveChanges();
            }
        }
    }
}
