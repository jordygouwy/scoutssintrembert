using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using ScoutsWebsite.Models;
using ScoutsWebsite.IRepositories;
using System.Drawing;

namespace ScoutsWebsite.Controllers
{
    public class AccountController : Controller
    {
        private ILeaderRepository _repoLeader;
        private IPostRepository _repoPost;
        private ICalendarRepository _repoCalendar;

        public AccountController(ILeaderRepository repoLeader, IPostRepository repoPost, ICalendarRepository repoCalendar)
        {
            _repoLeader = repoLeader;
            _repoPost = repoPost;
            _repoCalendar = repoCalendar;
        }

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool validlogin = false;
                if (!string.IsNullOrEmpty(model.UserName) && model.UserName.ToUpper().Equals("LEIDING"))
                {
                    if (!string.IsNullOrEmpty(model.Password) && model.Password.Equals("LeidingSintRembert"))
                    {
                        validlogin = true;
                    }
                }
                else if (!string.IsNullOrEmpty(model.UserName) && model.UserName.ToUpper().Equals("JORDY"))
                {
                    if (!string.IsNullOrEmpty(model.Password) && model.Password.Equals("jor123%*"))
                    {
                        validlogin = true;
                    }
                }

                if (validlogin)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName.ToUpper(), model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public void ShowImage(Guid leaderid)
        {
            Response.Clear();
            byte[] _buf = _repoLeader.GetLeaderPhoto(leaderid);
            if (_buf != null)
            {
                Response.ContentType = "application/octet-stream";
                //Response.ContentType = "image/gif";
                Response.BinaryWrite(_buf);
            }
            // Response.AddHeader("Content-disposition", string.Format("attachment; filename={0}{1}", description, Path.GetExtension(file)));

            Response.End();
        }

        #region POSTS
        public ActionResult AdminPosts()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(_repoPost.GetPosts());
            }
            else
            {
                return base.RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AdminPostDelete(Guid? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id.HasValue)
                {
                    _repoPost.DeletePost(id.Value);
                }
                return base.RedirectToAction("AdminPosts", "Account");
            }
            else
            {
                return base.RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AdminPostEdit(Guid? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                PostDetailItem item = null;
                if (id.HasValue)
                {
                    item = _repoPost.GetPost(id.Value);
                }

                if (item == null)
                {
                    item = new PostDetailItem();
                }
                return View(item);
            }
            else
            {
                return base.RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AdminPostEdit(PostDetailItem model)
        {
            if (User.Identity.IsAuthenticated)
            {
                //    Image img = null;
                //    img.GetThumbnailImage(
                if (ModelState.IsValid)
                {
                    _repoPost.AddOrUpdatePost(model);
                    return base.RedirectToAction("AdminPosts", "Account");
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return base.RedirectToAction("Index", "Home");
            }
        }
        #endregion

        #region CALENDAR
        public ActionResult AdminCalendar()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(_repoCalendar.GetCalendarItems());
            }
            else
            {
                return base.RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AdminCalendarDelete(Guid? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id.HasValue)
                {
                    _repoCalendar.DeleteCalendar(id.Value);
                }
                return base.RedirectToAction("AdminCalendar", "Account");
            }
            else
            {
                return base.RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AdminCalendarEdit(Guid? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                CalendarDetailItem item = null;
                if (id.HasValue)
                {
                    item = _repoCalendar.GetCalendarItem(id.Value);
                }

                if (item == null)
                {
                    item = new CalendarDetailItem();
                }
                return View(item);
            }
            else
            {
                return base.RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AdminCalendarEdit(CalendarDetailItem model)
        {
            if (User.Identity.IsAuthenticated)
            {
                //    Image img = null;
                //    img.GetThumbnailImage(
                if (ModelState.IsValid)
                {
                    _repoCalendar.AddOrUpdateCalendar(model);
                    return base.RedirectToAction("AdminCalendar", "Account");
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return base.RedirectToAction("Index", "Home");
            }
        }
        #endregion

        #region LEADERS
        public ActionResult AdminLeaders()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(_repoLeader.GetLeaders());
            }
            else
            {
                return base.RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AdminLeaderDelete(Guid? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id.HasValue)
                {
                    _repoLeader.DeleteLeader(id.Value);
                }
                return base.RedirectToAction("AdminLeaders", "Account");
            }
            else
            {
                return base.RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AdminLeaderEdit(Guid? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                LeaderDetailItem item = null;
                if (id.HasValue)
                {
                    item = _repoLeader.GetLeader(id.Value);
                }

                if (item == null)
                {
                    item = new LeaderDetailItem();
                }
                return View(item);
            }
            else
            {
                return base.RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AdminLeaderEdit(LeaderDetailItem model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    _repoLeader.AddOrUpdateLeader(model);
                    return base.RedirectToAction("AdminLeaders", "Account");
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return base.RedirectToAction("Index", "Home");
            }
        }
        #endregion



        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
