﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CasualClans.Data;
using CasualClans.Data.Models;
using CasualClans.Models.ApplicationUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CasualClans.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;
        private readonly IConfiguration _configuration;

        public ProfileController(UserManager<ApplicationUser> userManager,
            IApplicationUser userService,
            IUpload uploadService, 
            IConfiguration configuration)
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
            _configuration = configuration;
        }

        public IActionResult Detail(string Id)
        {
            var user = _userService.GetById(Id);
            var userRoles = _userManager.GetRolesAsync(user).Result;

            var model = new ProfileModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserRating = user.Rating.ToString(),
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                MemberSince = user.MemberSince,
                IsAdmin = userRoles.Contains("Admin")
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            var userId = _userManager.GetUserId(User);

            var connectionString = _configuration.GetConnectionString("AzureStorageAccount");

            var container = _uploadService.GetBlobContainer(connectionString, "profile-images");

            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            var filename = contentDisposition.FileName.Trim('"');

            var blockBlob = container.GetBlockBlobReference(filename);

            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            await _userService.SetProfileImage(userId, blockBlob.Uri);

            return RedirectToAction("Detail", "Profile", new { id = userId });
            //connect to azure storage container
            //Get blob container

            //parse content dispostion response header

            //get reference to block blob
            //on that block blob upload file

            //set users profile image to the URI
            //redirect to users profile
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var profiles = _userService.GetAll()
                .OrderByDescending(user => user.Rating)
                .Select(u => new ProfileModel
                {
                    Email = u.Email,
                    UserName = u.UserName,
                    ProfileImageUrl = u.ProfileImageUrl,
                    UserRating = u.Rating.ToString(),
                    MemberSince = u.MemberSince
                });

            var model = new ProfileListModel
            {
                Profiles = profiles
            };

            return View(model);
        }
    }
}