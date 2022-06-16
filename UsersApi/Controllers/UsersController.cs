using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.UserData;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserData _userData;

        public UsersController(IUserData userData)
        {
            _userData = userData;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _userData.GetUsers();
        }

        [HttpGet("FromCacheAsCollection")]
        public ActionResult<IEnumerable<User>> GetUsersFromCacheAsCollection()
        {
            return _userData.GetUsersFromCacheAsCollection();
        }

        [HttpGet("FromCacheAsSeperateEntities")]
        public ActionResult<IEnumerable<User>> GetUsersFromCacheAsSeperateEntites()
        {
            return _userData.GetUsersFromCacheAsSeperateEntities();
        }

        [HttpGet("FromCacheOnly")]
        public ActionResult<IEnumerable<User>> GetUsersFromCacheOnly()
        {
            return _userData.GetUsersFromCacheOnly();
        }

        [HttpGet("LoadIntoCache")]
        public ActionResult<IEnumerable<User>> LoadDataIntoCache()
        {
            return _userData.LoadDataIntoCache();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _userData.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _userData.UpdateUser(user);
            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            _userData.AddUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userData.GetUser(id);
            
            if (user == null)
            {
                return NotFound();
            }

            _userData.DeleteUser(user);
            return NoContent();
        }

        // GET : api/users/filter?firstName=""
        [HttpGet("filter")]
        public ActionResult<IEnumerable<User>> FilterByName(string firstName="")
        {
            return _userData.GetUsers().Where(user => user.FirstName == firstName).ToList();
        }
    }
}