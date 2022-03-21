using EshoAguekeng.Repository;
using EshopAguekeng.Models;
using EshopAguekeng.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EshopAguekeng.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private readonly UserRepository userRepository;
        public UsersController()
        {
            userRepository = new UserRepository();
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var user = userRepository.Get(id);
            if (user == null)
                return NotFound();
            return MapUser(user);
        }



        [HttpGet]
        public IHttpActionResult Get(string username)
        {
            var user = userRepository.Get(username);
            if (user == null)
                return NotFound();
            return MapUser(user);
        }

        [HttpGet]
        public IHttpActionResult Login(string username , string password)
        {
            var user = userRepository.Get(username,password);
            if (user == null)
                return NotFound();
            return MapUser(user);
        }
        public IHttpActionResult Post([FromBody] UserModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();
                var user = new User
                    (
                        0,
                        model.Username,
                        model.Fullname,
                        model.Role,
                        model.Password);
                user = userRepository.Add(user);
                return MapUser(user);
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DuplicateWaitObjectException)
            {
                return Conflict();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public IHttpActionResult Put(UserModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();
                var user = new User
                    (
                        0,
                        model.Username,
                        model.Fullname,
                        model.Role,
                        model.Password);
                user = userRepository.Set(user);
                return MapUser(user);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DuplicateWaitObjectException)
            {
                return Conflict();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        private IHttpActionResult MapUser(User user)
        {
            return Ok
                (
                    new UserModel
                    (
                         user.Id,
                         user.Username,
                         user.Fullname,
                         user.Password
                    )
                );
        }
    }
}
