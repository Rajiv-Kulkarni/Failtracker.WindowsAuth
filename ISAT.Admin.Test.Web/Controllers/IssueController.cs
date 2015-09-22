using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using ISAT.Admin.Test.Web.Data;
using ISAT.Admin.Test.Web.Domain;
using ISAT.Admin.Test.Web.Filters;
using ISAT.Admin.Test.Web.Infrastructure;
using ISAT.Admin.Test.Web.Infrastructure.Alerts;
using ISAT.Admin.Test.Web.Models.Issue;

namespace ISAT.Admin.Test.Web.Controllers
{
    //[HandleError]
    public class IssueController : FailTrackerController
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public IssueController(ApplicationDbContext context,
            ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;

            ViewBag.UserName = (_currentUser.Me != null) ? _currentUser.Me.MyName : "";
        }

        [ChildActionOnly]
        public ActionResult YourIssuesWidget()
        {
            var models = _context.Issues.Where(i => i.AssignedTo_Id == _currentUser.Me.Id)
                .Project().To<IssueSummaryViewModel>();

            return PartialView(models.ToArray());
        }

        [ChildActionOnly]
        public ActionResult CreatedByYouWidget()
        {
            var models = _context.Issues.Where(i => i.Creator_Id == _currentUser.Me.Id)
                .Project().To<IssueSummaryViewModel>();

            return PartialView(models.ToArray());
        }

        [ChildActionOnly]
        public ActionResult AssignmentStatsWidget()
        {
            var stats = _context.Users.Project().To<ApplicationUser>().Project().To<AssignmentStatsViewModel>();

            return PartialView(stats.ToArray());
        }

        public ActionResult New()
        {
            var form = new NewIssueForm();
            return View(form);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Created issue")]
        [AuthorizeAccess(ApplicationRoles.Admin)]
        public ActionResult New(NewIssueForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var assignedToUser = _context.Users.Project().To<ApplicationUser>().Single(u => u.Id == form.AssignedToUserName);//MattQuestion: Why is AssignedToUsername is an Id and not the UserName?

            _context.Issues.Add(new Issue(_currentUser.Me, assignedToUser, form.IssueType, form.Subject, form.Body));

            _context.SaveChanges();

            
            return RedirectToAction<HomeController>(c => c.Index())
                .WithSuccess("Issue created!");
        }

        [Log("Viewed issue {id}")]
        [HandleError]
        [AuthorizeAccess(ApplicationRoles.Admin, ApplicationRoles.User)]
        public ActionResult View(int id) //MattQuestion: How to handle error when user tries to browse to this action directly without any value specified for id? I am currently using an Error view, displayed globally through web.config.
        {
            //MattQuestion: How to handle unexpected and any spurious runtime errors
            try
            {
                var model = _context.Issues
                    .Project().To<IssueDetailsViewModel>()
                    .SingleOrDefault(i => i.IssueID == id);

                if (model == null)
                {
                    return RedirectToAction<HomeController>(c => c.Index())
                        .WithError("Unable to find the issue.  Maybe it was deleted?");
                }

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction<HomeController>(c => c.Index())
                    .WithError("Unexpected error: Please contact the administrators.");
            }
        }

        [Log("Started to edit issue {id}")]
        //[Authorize(Roles = "User")]
        [AuthorizeAccess(ApplicationRoles.Admin, ApplicationRoles.User)]
        public ActionResult Edit(int id)
        {
            var form = _context.Issues
                .Project().To<EditIssueForm>()
                .SingleOrDefault(i => i.IssueID == id);

            if (form == null)
            {
                return RedirectToAction<HomeController>(c => c.Index())
                    .WithError("Unable to find the issue.  Maybe it was deleted?");
            }

            return View(form);
        }

        [HttpPost, Log("Saving changes")]
        //[Authorize(Roles="Admin")]
        [AuthorizeAccess(ApplicationRoles.Admin)]
        public ActionResult Edit(EditIssueForm form) //assignedtousername is id?
        {
            if (!ModelState.IsValid)
            {
                return JsonValidationError();
            }

            var issue = _context.Issues.SingleOrDefault(i => i.IssueID == form.IssueID);

            if (issue == null)
            {
                return JsonError("Cannot find the issue specified.");
            }

            var assignedToUser = _context.Users.Single(u => u.Id == form.AssignedToUserName);//MattQuestion: why is AssignedToUserName not a user name but a user id?

            issue.Subject = form.Subject;
            issue.AssignedTo_Id = assignedToUser.Id;
            issue.Body = form.Body;
            issue.IssueType = form.IssueType;

            _context.SaveChanges();
            return RedirectToAction<HomeController>(c => c.Index())
                .WithSuccess("Issue edited!");

            //return JsonSuccess(form);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Deleted issue {id}")]
        //[Authorize(Roles = "Admin")]
        [AuthorizeAccess(ApplicationRoles.Admin)]
        public ActionResult Delete(int id)
        {
            var issue = _context.Issues.Find(id);

            if (issue == null)
            {
                return RedirectToAction<HomeController>(c => c.Index())
                    .WithError("Unable to find the issue.  Maybe it was deleted?");
            }

            _context.Issues.Remove(issue);

            _context.SaveChanges();

            return RedirectToAction<HomeController>(c => c.Index())
                .WithSuccess("Issue deleted!");
        }
    }
}