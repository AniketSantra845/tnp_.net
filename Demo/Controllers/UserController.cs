﻿using Demo.api;
using Demo.Controllers.Json;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Demo.Controllers
{
    public class UserController : Controller
    {
        private readonly IEmailSender emailSender;
        HttpClient client;
        private readonly IHttpContextAccessor context;
        public UserController(IHttpContextAccessor httpContextAccessor, IEmailSender emailSender)
        {
            Webapi wb = new Webapi();
            client = wb.response();
            context = httpContextAccessor;
            this.emailSender = emailSender;

        }
        public IActionResult Role()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Role(int id)
        {
            if (id == 1)
            {
                return RedirectToAction("Login", new { id = 1 });
            }   
            else if (id == 2)
            {
                return RedirectToAction("Login", new { id = 2 });
            }
            else
            {
                TempData["error"] = "press any button";
                return RedirectToAction("Role");
            }
        }
        public IActionResult Login(int id)
        {
            if (id == 1)
            {
                TempData["r"] = id;
                return View();
            }
            else if (id == 2)
            {
                TempData["r"] = id;
                return View();
            }
            else
            {
                TempData["error"] = "choose any role to sign in.";
                return RedirectToAction("Role");
            }
        }

        [HttpPost]
        public IActionResult Login(User model, int id)
        {
            if (id == 1 || id == 2)
            {
                if (ModelState.IsValid)
                {
                    String data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync(client.BaseAddress + "getlogin&role=" + id, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        String result = response.Content.ReadAsStringAsync().Result;
                        var logindetails = JsonDecode.FromJson(result);
                        Debug.WriteLine(logindetails);
                        var success = logindetails.Success;
                        if (success)
                        {
                            int role = Convert.ToInt32(logindetails.userinfo.role);
                            if (id == role)
                            {
                                if (role == 1)
                                {
                                    int userid = Convert.ToInt32(logindetails.userinfo.id);
                                    int sessionid = Convert.ToInt32(logindetails.student.session_id);
                                    int studentid = Convert.ToInt32(logindetails.student.id);
                                    string studentemailid = logindetails.userinfo.email;
                                    String studentName = logindetails.student.first_name;
                                    context.HttpContext.Session.SetInt32("role", role);
                                    context.HttpContext.Session.SetInt32("userid", userid);
                                    context.HttpContext.Session.SetInt32("sessionid", sessionid);
                                    context.HttpContext.Session.SetInt32("studentid", studentid);
                                    context.HttpContext.Session.SetString("studentemail", studentemailid);
                                    return RedirectToAction("Index", "Home");
                                }
                                else if (role == 2)
                                {
                                    int userid = Convert.ToInt32(logindetails.userinfo.id);
                                    int sessionid = Convert.ToInt32(logindetails.sessioninfo.id);
                                    string coordinatormailid = logindetails.userinfo.email;
                                    context.HttpContext.Session.SetInt32("role", role);
                                    context.HttpContext.Session.SetInt32("userid", userid);
                                    context.HttpContext.Session.SetInt32("sessionid", sessionid);
                                    context.HttpContext.Session.SetString("coordinatormail", coordinatormailid);
                                    return RedirectToAction("Index", "Home");
                                }
                            }
                            else
                            {

                            }
                            {
                                if (id == 1)
                                {
                                    TempData["error"] = "Enter the credentials of a student!";
                                    TempData["r"] = id;
                                    return RedirectToAction("Login");
                                }
                                else if (id == 2)
                                {
                                    TempData["error"] = "Enter the credentials of a co-ordinator!";
                                    TempData["r"] = id;
                                    return RedirectToAction("Login");
                                }
                            }
                        }
                        else
                        {
                            TempData["error"] = "Wrong id or password";
                            TempData["r"] = id;
                            return RedirectToAction("Login");
                        }
                    }
                }
                return View(model);
            }
            else { TempData["error"] = "Please select role to sign in!"; return RedirectToAction("Role"); }

        }
        public IActionResult Logout()
        {
            context.HttpContext.Session.Remove("role");
            context.HttpContext.Session.Remove("userid");
            context.HttpContext.Session.Remove("sessionid");
            context.HttpContext.Session.Remove("studentid");
            context.HttpContext.Session.Remove("studentemail");
            return RedirectToAction("Login");
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                Random rnd = new Random();
                var otp = rnd.Next(11111, 99999);
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "check_user&otp=" + otp, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(result);
                    if (JsonDecode.FromJson(result).Success)
                    {
                        MailRequest mail = new MailRequest();
                        mail.ToEmail = model.email;
                        mail.Subject = "Password Reset.";
                        mail.Body = "Your OTP is: " + otp;
                        try
                        {
                            await emailSender.SendEmailAsync(mail);
                            TempData["success"] = "otp sent, check your inbox or spam.";
                            return RedirectToAction("ConfirmPassword");
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("email", "This email is not registered, enter a valid email id.");
                        return View(model);
                    }
                }

            }
            return View(model);
        }
        public IActionResult ConfirmPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ConfirmPassword(ConfirmPassword confirmPassword)
        {
            Debug.WriteLine(confirmPassword.otp);
            Debug.WriteLine(confirmPassword.password);
            Debug.WriteLine(confirmPassword.cpassword);
            if (ModelState.IsValid)
            {
                return RedirectToAction("ConfirmPassword");
            }
            return View(confirmPassword);
        }
    }
}
