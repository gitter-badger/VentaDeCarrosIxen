﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VentaDeCarrosIxen.Models;

namespace VentaDeCarrosIxen.Controllers
{
    public class CuentaController : Controller
    {
        public DB_VENTACARROS db = new DB_VENTACARROS();
        // GET: Cuenta
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            var usr = db.usuario.FirstOrDefault(u => u.correo == usuario.correo && u.contraseña == usuario.contraseña);
            if (usr!= null)
            {
                Session["idUsuario"] = usr.idUsuario;
                Session["nombreUsuario"] = usr.nombre;
                return VerificarSesion();
            }
            else
            {
                ModelState.AddModelError("", "Verifique sus credenciales: usuario o contraseña incorrectos");

            }
            return View();
        }
        public ActionResult Registro()
        {
            return View();
        }
    [HttpPost]
        public ActionResult Registro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var rol = db.rol.FirstOrDefault(r => r.idRol == 2);
                usuario.rol = rol;
                db.usuario.Add(usuario);
                db.SaveChanges();
                ViewBag.mensaje = "El usuario "+usuario.nombre+" ha sido registrado correctamente.";
            }
            return RedirectToAction("Login","Cuenta");
        }
    public ActionResult Logout()
    {
        Session.Remove("idUsuario");
        Session.Remove("nombreUsuario");
        return RedirectToAction("Login","Cuenta");
    }
    public ActionResult VerificarSesion()
    {
        if (Session["idUsuario"]!=null)
        {
            return RedirectToAction("../Home/Index");
        }
        else
        {
            return RedirectToAction("Login");
        }
    }
        
    }
}