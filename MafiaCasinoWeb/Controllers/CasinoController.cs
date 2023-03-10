using AutoMapper;
using BusinessLogic.Services;
using Casino;
using DAL;
using DAL.Models;
using DAL.Models.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace MafiaCasinoWeb.Controllers
{
    [Authorize]
    public class CasinoController : Controller
    {
        private readonly UserService _userService;

        public CasinoController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Riches()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserVM>());
            var mapper = config.CreateMapper();

            List<UserVM> mappedUsers = await _userService.GetRichesAsync(10);
            return View(mappedUsers);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Slots()
        {
            return View(SlotsList.GetAllSlots());
        }

        public async Task<IActionResult> HatSlot()
        {
            return View();
        }
    }
}
