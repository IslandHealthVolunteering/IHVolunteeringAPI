﻿using System.Linq;
using IHVolunteerAPIData.Models;
using Microsoft.AspNetCore.Mvc;
using IHVolunteerAPI.Security;
using Microsoft.EntityFrameworkCore;
using System;

namespace IHVolunteerAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        readonly IHVolunteerAPIContext Context;

        public UserController(IHVolunteerAPIContext context)
            => Context = context;

        [HttpGet]
        public IActionResult GetUser()
        {
            var users = Context.User.ToList();

            return Ok(users);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            var userToCreate = user;

            try
            {
                Context.Add(userToCreate);
                Context.SaveChanges();
            }
            catch(DbUpdateException dbe)
            {
                Console.WriteLine("An error occurred when updating the entries " + dbe);

                return BadRequest("Please make sure you have created login creds with this email address");
            }

            return Created("/api/user", "User " + userToCreate.Email + " successfully created");
        }
    }
}
