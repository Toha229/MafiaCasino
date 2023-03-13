using AutoMapper;
using DAL;
using DAL.Models;
using DAL.Models.VM;
using DAL.Repository;
using DAL.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLogic.Services
{
    public class UserService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRewardsRopository _rewardsRopository;

		public UserService(IConfiguration configuration, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IRewardsRopository rewardsRopository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _rewardsRopository = rewardsRopository;
		}

		public async Task<List<UserVM>> GetRichesAsync(int usersCount)
		{
			using (var _context = new AppDbContext())
			{
				var res = await _context.Users.OrderByDescending(u => u.Cash).Select(u => _mapper.Map<User, UserVM>(u)).Take(usersCount).ToListAsync();
				return res;
			}
		}

		public async Task<ServiceResponse> GetUserProfileAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return new ServiceResponse
				{
					Success = false,
					Message = "User not found."
				};
			}

			var mappedUder = _mapper.Map<User, UserProfileVM>(user);
			mappedUder.Rewards = await _rewardsRopository.GetRewardsAsync(userId);

			return new ServiceResponse
			{
				Success = true,
				Message = "User profile loaded.",
				Payload = mappedUder
			};
		}

		public async Task<ServiceResponse> LoginUserAsync(LoginUserVM model)
        {
            User user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Incorrect email."
                };
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User logged in successfully."
                };
            }

            if (result.IsNotAllowed)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Confirm your email please."
                };
            }

            if (result.IsLockedOut)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Your account is locked. Connect with administrator or wait some time."
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "Something incorrect."
            };
        }

        public async Task<ServiceResponse> LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
            return new ServiceResponse
            {
                Success = true,
                Message = "User logged out."
            };
        }

        public async Task<ServiceResponse> RegisterUserAsync(RegisterUserVM model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Psswords do not match.",
                };
            }

            var mappedUser = _mapper.Map<RegisterUserVM, User>(model);
            mappedUser.Deffault();
            var result = await _userManager.CreateAsync(mappedUser, model.Password);
            await _rewardsRopository.SetRewardProgress(mappedUser.Id, "Civilian", 1);
            if (result.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User successfully created.",
                };
            }


            List<IdentityError> errorList = result.Errors.ToList();
            string errors = "";

            foreach (var error in errorList)
            {
                errors = errors + error.Description.ToString();
            }

            return new ServiceResponse
            {
                Success = false,
                Message = errors
            };
        }
    }
}
