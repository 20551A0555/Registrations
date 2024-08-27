/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Registration_server_project.DataContext;
using Registration_server_project.Entities;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Kerberos;

namespace Registration_server_project.Controllers
{
    [Route("api/v1")]
    [ApiController]

    public class RegistrationDetailsController : ControllerBase
    {

        RegistrationDetail lst1 = new RegistrationDetail();

        private readonly RegistrationContext _context;

        public RegistrationDetailsController(RegistrationContext context)
        {
            _context = context;
        }

        // GET: api/RegistrationDetails


        /// <summary>
        /// GetRegistrationDetail
        /// </summary>
        /// <param name="Rollno"></param>
        /// <returns></returns>
        /// 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistrationDetailparams(int id)
        {
            var items = await _context.RegistrationDetails.Where(ids => ids.Id == id).Select(kk => kk).SingleOrDefaultAsync();
            return Ok(items);
        }

        [HttpGet(nameof(GetRegistrationDetail))]
        public async Task<ActionResult> GetRegistrationDetail(int id)
        {
            var items = await _context.RegistrationDetails.Where(ids => ids.Id == id).Select(kk => kk).SingleOrDefaultAsync();
            return Ok(items);
        }
        [HttpPost (nameof(Insertdata))]
        
        public async Task<IActionResult> Insertdata([FromBody] RegistrationDetail temporarylist)
        {
            try
            {
                var templist = new RegistrationDetail();
                templist.FirstName = temporarylist.FirstName;
                templist.LastName = temporarylist.LastName;
                templist.Email = temporarylist.Email;
                templist.City = temporarylist.City;
                templist.Password = temporarylist.Password;
                var registrationdetls = new List<RegistrationDetail>();
                //registrationdetls.(new RegistrationDetail { FirstName = "Hareram", LastName = "Kotla", Email = "Hare@gmail.com", City = "Hyderabad", Password = "123456ertyu" });
                var x =_context.RegistrationDetails.Add(new RegistrationDetail {Id=0, FirstName = templist.FirstName, LastName = templist.LastName, Email = templist.Email, City = templist.City, Password = templist.Password });
                _context.SaveChanges();
                if (x.Entity != null)
                {
                    return Ok(Request.Body);
                }
                else
                {
                    return Ok("fail");
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut(nameof(updatedetails))]
        public String updatedetails(String mail, String password)
        {
            //_context.RegistrationDetails.where
            var upd = _context.RegistrationDetails.Where(k => k.Email == mail).Select(j => j).FirstOrDefault();
            upd!.Password = password;
           
            _context.RegistrationDetails.Update(upd);

            _context.SaveChanges();
            return "Success";
        }
        [HttpDelete(nameof(deletedetails))]
        public String deletedetails(int id)
        {
            _context.RegistrationDetails.Where(tid => tid.Id == id).ExecuteDelete();
            return "Success";
        }
        
    }
}*/


using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Identity.Client.Kerberos;
using Microsoft.OpenApi.Any;
using Registration_server_project.dtos;
using Registration_server_project.Entities;
using System;
using System.Globalization;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace RegistrationServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegistrationDetailsController : ControllerBase
    {
        private readonly Registrationcontext _context;

        //public Registration_server_project.DataContext.RegistrationContext Context => _context;

        public RegistrationDetailsController(Registrationcontext context)
        {
            _context = context;
        }

        // POST: api/v1/RegistrationDetails/Insertdata
        [HttpPost(nameof(Insertdata))]
        public async Task<IActionResult> Insertdata([FromBody] registerdto registrationDetail)
        {
            if (!ModelState.IsValid)
            {
                return Ok(false);
            }

            try
            {
                var logindetails = new LoginDetail
                {
                    FirstName = registrationDetail.FirstName,
                    City = registrationDetail.City,
                };
                List<LoginDetail> l1 = new List<LoginDetail>();
                l1.Add(logindetails);
                var newRegistration = new RegistrationDetail
                {
                    FirstName = registrationDetail.FirstName,
                    LastName = registrationDetail.LastName,
                    Email = registrationDetail.Email,
                    City = registrationDetail.City,
                    Password = registrationDetail.Password,
                    LoginDetails = l1
                };

                _context.RegistrationDetails.Add(newRegistration);
                await _context.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }

        // Optional: GET: api/v1/RegistrationDetails/5
        [HttpGet("{mail}/{pass}")]
        public async Task<ActionResult<RegistrationDetail>> GetRegistrationDetail(String mail, String pass)
        {

            var registrationDetail = await _context.RegistrationDetails.Where(ml => ml.Email == mail && ml.Password == pass).Select(kk => kk).FirstOrDefaultAsync();

            if (registrationDetail == null)
            {
                return NotFound(false);
            }
            else
            {

                return Ok(registrationDetail);

            }
        }
        [HttpPut("{mailid}/{loc}/{pass}")]
        public async Task<IActionResult> updatedetails(String mailid, String loc, String pass)
        {
            var items = await _context.RegistrationDetails.Where(lst => lst.Email == mailid).Select(kk => kk).FirstOrDefaultAsync();
            if (items == null)
            {
                return NotFound(false);
            }
            else
            {
                items.City = loc;
                items.Password = pass;
                _context.RegistrationDetails.Update(items);
                _context.SaveChanges();
                return Ok(items);
            }
        }
        [HttpDelete("{maild}")]
        public async Task<IActionResult> deletedetails(String maild)
        {
            var items = await _context.RegistrationDetails.Where(lst => lst.Email == maild).Select(kk => kk).FirstOrDefaultAsync();
            if (items == null)
            {
                return NotFound(false);
            }
            else
            {
                _context.RegistrationDetails.Where(mid => mid.Email == maild).ExecuteDelete();
                return Ok(true);
            }
        }

        [HttpPut(nameof(AddOrUpdateBranch))]
        public IActionResult AddOrUpdateBranch([FromBody] reqDto req)
        {
            var plant = new Plant();
            List<Branch> branches = new List<Branch>();
            branches.Add(new Branch
            {
                BranchAddress = "Kakinada",
                BranchName = "Kdd dairies"

            });

            if (req.Type == "first")
            {
                plant = new Plant
                {
                    PlantAddress = req.PlantAddress,
                    PlantName = req.PlantName,
                    Branches = branches
                };

                _context.Plants.Add(plant);

                _context.SaveChanges();

            }

            else
            {
                plant = new Plant
                {
                    PlantId = 0,
                    PlantAddress = req.PlantAddress,
                    PlantName = req.PlantName,
                };


                var addd = _context.Plants.Add(plant);

                _context.SaveChanges();

                var branch = new Branch
                {
                    BranchId = 0,
                    PlantId = addd.Entity.PlantId
                };
                _context.Branches.Add(branch);

                _context.SaveChanges();
            }
            var respons = "success";
            var error = new
            {
                respons = respons,
            };
            return Ok(error.respons);
        }
        [HttpGet(nameof(Getlogindetails))]
        public IActionResult Getlogindetails()
        {
           var result=_context.LoginDetails.Select(dels=>dels).ToList();
            return Ok(result);
        }
        [HttpPost(nameof(UpdateLogindetails))]
        public IActionResult UpdateLogindetails([FromBody] registerdto req)
        {
            var Msg = new { 
                pass="updated",
                fail="Notupdated"
            };

            if (req != null)
            {
                List<LoginDetail> loglist = new List<LoginDetail>();
                var registerdata = _context.RegistrationDetails.Where(red => red.Email.Equals(req.Email)).Select(k => k).FirstOrDefault();
                if (registerdata != null) { 
                    var logindata=_context.LoginDetails.Where(log=>log.RegisterId.Equals(registerdata.Id)).Select(k => k).FirstOrDefault();
                    logindata.City=req.City;
                    loglist.Add(logindata);
                }
               registerdata.City=req.City;
               registerdata.LoginDetails=loglist;
               _context.RegistrationDetails.Update(registerdata);
               _context.SaveChanges();
                return Ok(Msg.pass);
            }
            else
            {
                return Ok(Msg.fail);
            }
        }
        //[HttpGet(nameof(GetUsernames))]
        //public IActionResult GetUsernames() 
        //    {
        //        var data = from R in _context.RegistrationDetails.Select(k => k)
        //                   join
        //                   L in _context.LoginDetails.Select(l => l) on R.Id equals L.Loginid
        //                   group R
        //                   by R.City;
        //        return Ok(data);

        //    }
        //}
    }
}