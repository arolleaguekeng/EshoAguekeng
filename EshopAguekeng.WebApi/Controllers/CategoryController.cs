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
    public class CategorysController : ApiController
    {
        private readonly CategoryRepository categoryRepository;
        public CategorysController()
        {
            categoryRepository = new CategoryRepository();
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var category = categoryRepository.Get(id);
            if (category == null)
                return NotFound();
            return base.Ok(MapCategory(category));
        }

        [HttpGet]
        public IHttpActionResult Get(string name)
        {
            var category = categoryRepository.Get(name);
            if (category == null)
                return NotFound();
            return base.Ok(MapCategory(category));
        }



        [HttpGet]
        public IHttpActionResult Find(string value)
        {
            var searchValue = value?.ToLower() ?? string.Empty; 

            var category = categoryRepository.Find
                (
                    x=>
                    x.Name.ToLower().Contains(searchValue)
                );
            return Ok(category.Select(x=>MapCategory(x)).ToArray());
        }
        public IHttpActionResult Post(CategoryModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();
                var category = new Category
                    (
                        0,
                        model.Name,
                        model.UserId
                    );
                 category = categoryRepository.Add(category);
                return base.Ok(MapCategory(category));
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


        public IHttpActionResult Put(CategoryModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();
                var category = new Category
                    (
                        model.Id,
                        model.Name,
                        model.UserId
                    );
                category = categoryRepository.Set(category);
                return base.Ok(MapCategory(category));
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

         
        public IHttpActionResult Delete(int id)
        {

            var category = categoryRepository.Delete(id);
            return base.Ok(MapCategory(category));
        }

        private CategoryModel MapCategory(Category category)
        {
            return new CategoryModel
            (
                category.Id,
                category.Name,
                category.UserId,
                new UserModel
                (
                    category.User.Id,
                    category.User.Username,
                    category.User.Fullname,
                    category.User.Role
                )
            );
        }
    }
}
