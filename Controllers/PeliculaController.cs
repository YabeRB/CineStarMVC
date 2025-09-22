using CineStarMVC.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CineStarMVC.Controllers
{
    public class PeliculaController : Controller
    {
        private readonly DB _db = new DB();

        public ActionResult verPeliculas(int? id)
        {
            DataTable peliculas = null;

            try
            {
                int idEstado = id ?? 1;

                peliculas = _db.ExecuteStoredProcedure("sp_getPeliculas",
                    new SqlParameter("@idEstado", idEstado));
            }
            catch (Exception)
            {
                ViewBag.Error = "No se pudieron cargar las películas. Intente más tarde.";
                peliculas = new DataTable();
            }

            return View(peliculas);
        }

        public ActionResult verPelicula(int idPelicula)
        {
            DataTable pelicula;

            try
            {
                pelicula = _db.ExecuteStoredProcedure("sp_getPelicula",
                    new SqlParameter("@id", idPelicula));
            }
            catch (Exception)
            {
                return HttpNotFound("Error al obtener la información de la película.");
            }

            if (pelicula == null || pelicula.Rows.Count == 0)
                return HttpNotFound();

            return View(pelicula.Rows[0]);
        }
    }
}
