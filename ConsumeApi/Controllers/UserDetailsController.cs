using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConsumeApi.Models;
using Newtonsoft.Json;
using System.Text;

namespace ConsumeApi.Controllers
{
    public class UserDetailsController : Controller
    {
        private readonly AppDBContext _context;

        public UserDetailsController(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> APIIndex()
        {

            List<UserDetails> users = new List<UserDetails>();
            using (var httpClient = new HttpClient())//handler
            {
                using (var response = await httpClient.GetAsync("http://localhost:5114/api/UserDetails"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<UserDetails>>(apiResponse);
                }

            }
            return View(users);
        }
        public async Task<IActionResult> CreateWithWebAPI()
        {
            List<UserType> UserTypeList = new List<UserType>();
            using (var httpClient = new HttpClient())//handler
            {
                using (var response = await httpClient.GetAsync("http://localhost:5114/api/UserTypes"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    UserTypeList = JsonConvert.DeserializeObject<List<UserType>>(apiResponse);
                }
            }
            List<SecurityQuestion> SQList = new List<SecurityQuestion>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5114/api/SecurityQuestions"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    SQList = JsonConvert.DeserializeObject<List<SecurityQuestion>>(apiResponse);
                }
            }

        
            ViewData["SqId"] = new SelectList(SQList, "SqId", "Question");
            ViewData["UserTypeId"] = new SelectList(UserTypeList, "UserTypeId", "UserTypeName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWithWebAPI([Bind("UserId,UserTypeId,FName,LName,UserName,Dob,Gender,SqId,SqAns")] UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                

                UserDetails user = new UserDetails();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(userDetails), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("http://localhost:5114/api/UserDetails", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<UserDetails>(apiResponse);
                    }
                }

                return RedirectToAction(nameof(APIIndex));
            }
           
            return View(userDetails);
        }

        public async Task<IActionResult> APIDetails(int? id)
        {
            
            UserDetails user = new UserDetails();
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("http://localhost:5114/api/UserDetails/"+id.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<UserDetails>(apiResponse);
                }
            }
            if (id == null || user == null || id == 0)
            {
                return NotFound();
            }
            return View(user);
        }
        public async Task<IActionResult> APIEdit(int? id)
        {
            UserDetails user = new UserDetails();
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("http://localhost:5114/api/UserDetails/" + id.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<UserDetails>(apiResponse);
                }
            }
            if (id == null || user == null || id == 0)
            {
                return NotFound();
            }
            List<UserType> UserTypeList = new List<UserType>();
            using (var httpClient = new HttpClient())//handler
            {
                using (var response = await httpClient.GetAsync("http://localhost:5114/api/UserTypes"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    UserTypeList = JsonConvert.DeserializeObject<List<UserType>>(apiResponse);
                }
            }
            List<SecurityQuestion> SQList = new List<SecurityQuestion>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5114/api/SecurityQuestions"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    SQList = JsonConvert.DeserializeObject<List<SecurityQuestion>>(apiResponse);
                }
            }


            ViewData["SqId"] = new SelectList(SQList, "SqId", "Question");
            ViewData["UserTypeId"] = new SelectList(UserTypeList, "UserTypeId", "UserTypeName");
            return View(user);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> APIEdit(int id, [Bind("UserId,UserTypeId,FName,LName,UserName,Dob,Gender,SqId,SqAns")] UserDetails userDetails)
        {
            if (id != userDetails.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    UserDetails user = new UserDetails();
                    using (var httpClient = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(userDetails), Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync("http://localhost:5114/api/UserDetails/"+id.ToString(), content))
                        {
                          
                        }
                    }

                    return RedirectToAction(nameof(APIIndex));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDetailsExists(userDetails.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            return View(userDetails);
        }

        public async Task<IActionResult> APIDelete(int? id)
        {
            UserDetails user = new UserDetails();
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("http://localhost:5114/api/UserDetails/" + id.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<UserDetails>(apiResponse);
                }
            }
            if (id == null || user == null || id == 0)
            {
                return NotFound();
            }
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("APIDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> APIDeleteConfirmed(int id)
        {
            using (var httpClient = new HttpClient())
            {
                if (id <= 0 )
                {
                    return NotFound();
                }
                using (var response = await httpClient.DeleteAsync("http://localhost:5114/api/UserDetails/Delete/" + id.ToString()))
                {
                    //httpClient.DeleteAsync("http://localhost:5114/api/UserDetails/" + id.ToString());
                    
                }
            }
            return RedirectToAction(nameof(APIIndex));
        }


        // GET: UserDetails
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.UserDetails.Include(u => u.SecurityQuestion).Include(u => u.UserType);
            return View(await appDBContext.ToListAsync());
        }

        // GET: UserDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserDetails == null)
            {
                return NotFound();
            }

            var userDetails = await _context.UserDetails
                .Include(u => u.SecurityQuestion)
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userDetails == null)
            {
                return NotFound();
            }

            return View(userDetails);
        }

        // GET: UserDetails/Create
        public IActionResult Create()
        {

            ViewData["SqId"] = new SelectList(_context.SecurityQuestions, "SqId", "Question");
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeName");
            return View();
        }


        // POST: UserDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       /* [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserTypeId,FName,LName,UserName,Dob,Gender,SqId,SqAns")] UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SqId"] = new SelectList(_context.SecurityQuestions, "SqId", "Question", userDetails.SqId);
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeName", userDetails.UserTypeId);
            return View(userDetails);
        }*/

        //Post Data using API

        
        // GET: UserDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserDetails == null)
            {
                return NotFound();
            }

            var userDetails = await _context.UserDetails.FindAsync(id);
            if (userDetails == null)
            {
                return NotFound();
            }
            ViewData["SqId"] = new SelectList(_context.SecurityQuestions, "SqId", "Question", userDetails.SqId);
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeName", userDetails.UserTypeId);
            return View(userDetails);
        }

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserTypeId,FName,LName,UserName,Dob,Gender,SqId,SqAns")] UserDetails userDetails)
        {
            if (id != userDetails.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDetailsExists(userDetails.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SqId"] = new SelectList(_context.SecurityQuestions, "SqId", "Question", userDetails.SqId);
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeName", userDetails.UserTypeId);
            return View(userDetails);
        }

        // GET: UserDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserDetails == null)
            {
                return NotFound();
            }

            var userDetails = await _context.UserDetails
                .Include(u => u.SecurityQuestion)
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userDetails == null)
            {
                return NotFound();
            }

            return View(userDetails);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserDetails == null)
            {
                return Problem("Entity set 'AppDBContext.UserDetails'  is null.");
            }
            var userDetails = await _context.UserDetails.FindAsync(id);
            if (userDetails != null)
            {
                _context.UserDetails.Remove(userDetails);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserDetailsExists(int id)
        {
          return (_context.UserDetails?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
