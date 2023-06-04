﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Elearning.Models;
using Elearning.Models.Enums;

namespace Elearning.Controllers;

public class BaseController : Controller
{
    protected AuthorizedUser AuthorizedUser { get; private set; }
    protected bool IsAuthorized
    {
        get
        {
            return AuthorizedUser != null;
        }
    }
    protected bool IsAdmin
    {
        get
        {
            return IsAuthorized && AuthorizedUser.PersonType == PersonType.ADMIN;
        }
    }
    protected bool IsUser
    {
        get
        {
            return IsAuthorized && AuthorizedUser.PersonType == PersonType.USER;
        }
    }

    protected IActionResult RedirectToHomePage(AuthorizedUser authorizedUser = null)
    {
       
        if (IsAdmin)
        {
            return RedirectToAction("Index", "Admin");
        }
        else if (IsUser)
        {
            return RedirectToAction("Index", "User");
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }

    public BaseController()
    {

    }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        if (HttpContext?.Session != null)
            AuthorizedUser = AuthorizedUser.GetBySession(HttpContext.Session);
    }
}