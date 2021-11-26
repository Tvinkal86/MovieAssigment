using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo.Database;
using demo.Models;
using demo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private List<Movie> _movies = new List<Movie>
        {
            new Movie { Id = 1, Name = "Titanic", DirectorName = "XYZ", Actor = "test", Actress = "test", TotalCast= 2 },
            new Movie { Id = 2, Name = "Fast And Furious", DirectorName = "ABC", Actor = "twinkle", Actress = "test", TotalCast = 3 }
        };

        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet]
        [Route("list")]
        public List<Movie> Get()
        {
            return _movies.ToList();
        }

        [HttpGet("{id}")]
        [Route("get")]
        public List<Movie> Get(int id)
        {
            return _movies.Where(t => t.Id == id).ToList();
        }

        [HttpPost]
        [Route("save")]
        public bool Post(Movie movie)
        {
            _movies.Add(movie);
            //_movies.SaveChanges();
            return true;
        }

        [HttpPut]
        [Route("udpate")]
        public bool Put(Movie movie)
        {
            var ifExist = _movies.Where(t => t.Id == movie.Id).FirstOrDefault();
            if (ifExist == null)
                return false;

            ifExist.Name = movie.Name;
            ifExist.DirectorName = movie.DirectorName;
            ifExist.Actor = movie.Actor;
            ifExist.Actress = movie.Actress;
            ifExist.TotalCast = movie.TotalCast;
            //_movies.SaveChanges();
            return true;
        }

        [HttpDelete]
        [Route("delete")]
        public bool Delete(int id)
        {
            var ifExist = _movies.Where(t => t.Id == id).FirstOrDefault();
            if (ifExist == null)
                return false;
            _movies.Remove(ifExist);
            return true;
        }
    }
}
