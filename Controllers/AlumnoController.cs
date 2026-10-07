using Newtonsoft.Json;
using POOI_T2_CASTILLO_SIERRA.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace POOI_T2_CASTILLO_SIERRA.Controllers
{
    public class AlumnoController : Controller
    {
        static string lista = @"[]";

        public ActionResult Index()
        {
            try
            {

                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(lista) ?? new List<Alumno>();
                return View(temporal);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(new List<Alumno>());
            }
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(Alumno alumno)
        {
            try
            {
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(lista) ?? new List<Alumno>();


                if (temporal.Exists(a => a.dni == alumno.dni))
                {
                    ViewBag.Mensaje = "El DNI ingresado ya se encuentra registrado.";
                    return View(alumno);
                }

                temporal.Add(alumno);


                lista = JsonConvert.SerializeObject(temporal, Formatting.Indented);

                ViewBag.Mensaje = "Guardado de manera exitosa";
                return View(alumno);
            }
            catch (JsonException ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        public ActionResult Detalles(string id)
        {

            List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(lista) ?? new List<Alumno>();

            Alumno alumnoEncontrado = temporal.Find(a => a.dni == id);

            return View(alumnoEncontrado);
        }

        public ActionResult Actualizar(string id)
        {
            List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(lista) ?? new List<Alumno>();

            Alumno alumnoEncontrado = temporal.Find(a => a.dni == id);

            return View(alumnoEncontrado);
        }

        [HttpPost]
        public ActionResult Actualizar(string dniOriginal, Alumno alumno)
        {
            try
            {

                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(lista) ?? new List<Alumno>();

                if (temporal.Exists(a => a.dni == alumno.dni && a.dni != dniOriginal))
                {
                    ViewBag.Mensaje = "El DNI ingresado ya pertenece a otro alumno.";
                    return View(alumno);
                }
                int index = temporal.FindIndex(a => a.dni == dniOriginal);

                temporal[index] = alumno;

                lista = JsonConvert.SerializeObject(temporal, Formatting.Indented);

                ViewBag.Mensaje = "Alumno actualizado";
                return View(alumno);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(alumno);
            }
        }

        public ActionResult Eliminar(string id)
        {
            try
            {
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(lista) ?? new List<Alumno>();
                int index = temporal.FindIndex(a => a.dni == id);

                if (index >= 0)
                {
                    temporal.RemoveAt(index);
                    lista = JsonConvert.SerializeObject(temporal, Formatting.Indented);
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
    }
}