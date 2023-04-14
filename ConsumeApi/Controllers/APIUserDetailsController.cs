using ConsumeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace ConsumeApi.Controllers
{
    public class ApiUserDetailsController : Controller
    {
        // GET: ApiUserDetails
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

                        using (var response = await httpClient.PutAsync("http://localhost:5114/api/UserDetails/" + id.ToString(), content))
                        {

                        }
                    }

                    return RedirectToAction(nameof(APIIndex));
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                    
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
                if (id <= 0)
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

    }
}
