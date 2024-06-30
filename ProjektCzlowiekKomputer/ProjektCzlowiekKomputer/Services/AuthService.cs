using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Project.CrossCutting.Dtos;
using Project.CrossCutting.Dtos.CreateDto;
using Project.Data.Models;
using ProjektCzlowiekKomputer.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjektCzlowiekKomputer.Services
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<UserModel> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IShelveService _shelveService;
        private readonly IConfiguration _configuration;
        public AuthService(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IShelveService shelveService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _shelveService = shelveService;
        }
        public async Task<(int, string)> Registeration(RegistrationModel model, string role)
        {
            try
            {
                var userExists = await userManager.FindByNameAsync(model.Username);
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
                var createUserResult = await userManager.CreateAsync(user, model.Password);
                if (!createUserResult.Succeeded)
                    return (0, "User creation failed! Please check user details and try again.");

                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));

                await  userManager.AddToRoleAsync(user, role);

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
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
                return (0, "Invalid username");
            if (!await userManager.CheckPasswordAsync(user, model.Password))
                return (0, "Invalid password");

            var userRoles = await userManager.GetRolesAsync(user);
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


    }
}

