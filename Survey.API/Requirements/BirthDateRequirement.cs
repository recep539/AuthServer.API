﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace MiniApp1.API.Requirements
{
    public class BirthDateRequirement : IAuthorizationRequirement
    {
        public int Age { get; set; }
        public BirthDateRequirement(int age)
        {
            Age = age;
        }
    }
    public class BirthDayRequirementHandler : AuthorizationHandler<BirthDateRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BirthDateRequirement requirement)
        {
            var birthDate = context.User.FindFirst("birth-date");
            if (birthDate == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            var today = DateTime.Now;
            var age = today.Year - Convert.ToDateTime(birthDate.Value).Year;
            if (age >= requirement.Age)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }

    }
}
