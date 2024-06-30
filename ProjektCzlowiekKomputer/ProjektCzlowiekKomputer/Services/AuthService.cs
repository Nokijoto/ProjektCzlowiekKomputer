using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.CrossCutting.Common;
using Project.CrossCutting.Dtos;
using Project.CrossCutting.Dtos.CreateDto;
using Project.Data;
using Project.Data.Models;
using ProjektCzlowiekKomputer.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjektCzlowiekKomputer.Services
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IShelveService _shelveService;
        private readonly IConfiguration _configuration;
        private readonly ProjectDbContext _db;
        public AuthService(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IShelveService shelveService, ProjectDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _shelveService = shelveService;
            _db = db;
        }
        public async Task<(int, string)> Registeration(RegistrationModel model, string role)
        {
            try
            {
                var userExists = await _userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                    return (0, "User already exists");

                UserModel user = new UserModel()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username,
                    Name = model.Name,
                    UserGuid = Guid.NewGuid()   
                };
                var createUserResult = await _userManager.CreateAsync(user, model.Password);
                if (!createUserResult.Succeeded)
                    return (0, "User creation failed! Please check user details and try again.");

                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));

                await _userManager.AddToRoleAsync(user, role);

                List<CreateShelveDto> shelveDto = new List<CreateShelveDto>
                {
                    new CreateShelveDto { Name = "Read", Description = "Books to read" },
                    new CreateShelveDto { Name = "Reading", Description = "Books currently reading" },
                    new CreateShelveDto { Name = "Favorite", Description = "Favorite books" },
                    new CreateShelveDto { Name = "Want to read", Description = "Books want to read" }
                };
                foreach (var shelve in shelveDto)
                {
                    await _shelveService.AddShelve(shelve, user.UserGuid);
                }

                return (1, "User created successfully!");
            }
            catch (Exception e)
            {
               if(e.InnerException.Message!=null)
                {
                    return (0, e.InnerException.Message);
                }
                return (0, e.Message);
            }
        }

        public async Task<(int, string)> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
                return (0, "Invalid username");
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                return (0, "Invalid password");

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim("userGuid",user.UserGuid.ToString()),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            string token = GenerateToken(authClaims);
            return (1, token);
        }


        public async Task<(bool Succeeded, string NewPassword)> ResetPasswordAsync(ResetPasswordModel model)
        {
            try
            {
                var userEmail = await _userManager.FindByEmailAsync(model.Email);
                var userUsername = await _userManager.FindByNameAsync(model.UserName);

                if (userEmail == null || userUsername == null || userUsername.Id != userEmail.Id)
                {
                    return (false, null);
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(userEmail);
                var newPassword = GenerateRandomPassword();
                var resetPasswordResult = await _userManager.ResetPasswordAsync(userEmail, token, newPassword);
                return resetPasswordResult.Succeeded ? (true, newPassword) : (false, null);
            }
            catch (Exception e)
            {
                return (false, null);
            }
        }



        private string GenerateRandomPassword()
        {
            const int length = 12;
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@$?_-";
            var random = new Random();
            return new string(Enumerable.Repeat(validChars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<CrudOperationResult<UpdateProfileModel>> UpdateProfile(UpdateProfileModel model, Guid guid)
        {
            try
            {
                var user = await _db.Users.SingleOrDefaultAsync(x => x.UserGuid == guid);
                if (user == null)
                {
                    return new CrudOperationResult<UpdateProfileModel>
                    {
                        Message = "User not found in the database",
                        Status = CrudOperationResultStatus.Failure,
                        Result = null
                    };
                }
                user.Name = model.Name ?? user.Name;
                user.PhoneNumber = model.PhoneNumber ?? user.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return new CrudOperationResult<UpdateProfileModel>
                    {
                        Message = "Error updating user",
                        Status = CrudOperationResultStatus.Failure,
                        Result = null
                    };
                }

                var updatedProfile = new UpdateProfileModel
                {
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };

                return new CrudOperationResult<UpdateProfileModel>
                {
                    Message = "User updated successfully",
                    Status = CrudOperationResultStatus.Success,
                    Result = updatedProfile
                };
            }
            catch (Exception e)
            {
                
                return new CrudOperationResult<UpdateProfileModel>
                {
                    Message = $"An error occurred while updating the user profile {e.Message}",
                    Status = CrudOperationResultStatus.Failure,
                    Result = null
                };
            }
        }

    }
}

