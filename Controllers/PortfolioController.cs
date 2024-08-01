using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetAPI.Extensions;
using DotNetAPI.Interfaces;
using DotNetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPortfolioRepository _portfolioRepository;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var portfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(portfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);
            if (stock == null)
                return BadRequest("Stock Not Found");

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            if (userPortfolio.Any(x => x.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("Stock Already Exists");
            }

            var portfolio = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id
            };
            await _portfolioRepository.CreateAsync(portfolio);
            if (portfolio == null)
            {
                return StatusCode(500, "Could Not Add Stock");
            }
            else
                return Ok("Created Stock");
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var portfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            if (portfolio.Any(x => x.Symbol.ToLower() == symbol.ToLower()))
            {
                await _portfolioRepository.DeleteAsync(appUser, symbol);
                return Ok("Deleted Stock");
            }
            else
                return BadRequest("Stock Not Found");
        }
    }
}