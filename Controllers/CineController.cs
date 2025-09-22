using CineStarMVC.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CineStarMVC.Controllers
{
    public class CineController : Controller
    {
        private readonly DB _db = new DB();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult verCines()
        {
            DataTable cines = _db.ExecuteStoredProcedure("sp_getCines");
            return View(cines);
        }

        public ActionResult verCine(int id)
        {

            DataTable dtCine = CloneTable(_db.ExecuteStoredProcedure("sp_getCine",
                new SqlParameter("@id", id)));
            if (dtCine == null || dtCine.Rows.Count == 0)
                return HttpNotFound();

            DataTable dtTarifas = CloneTable(_db.ExecuteStoredProcedure("sp_getCineTarifas",
                new SqlParameter("@idCine", id)));
            NormalizeTarifas(dtTarifas);

            DataTable dtFunciones = CloneTable(_db.ExecuteStoredProcedure("sp_getCinePeliculas",
                new SqlParameter("@idCine", id)));
            NormalizeFunciones(dtFunciones);

            var model = new Tuple<DataTable, DataTable, DataTable>(dtCine, dtTarifas, dtFunciones);
            return View(model);
        }

        private DataTable CloneTable(DataTable original)
        {
            if (original == null) return null;
            DataTable clone = original.Clone();
            foreach (DataColumn col in clone.Columns)
                col.ReadOnly = false; 
            foreach (DataRow row in original.Rows)
                clone.ImportRow(row);
            return clone;
        }

        private void NormalizeTarifas(DataTable dtTarifas)
        {
            if (dtTarifas == null) return;

            if (!dtTarifas.Columns.Contains("Descripcion"))
                dtTarifas.Columns.Add("Descripcion", typeof(string));

            foreach (DataRow r in dtTarifas.Rows)
            {
                if (dtTarifas.Columns.Contains("DiasSemana"))
                    r["Descripcion"] = (r["DiasSemana"] ?? "").ToString().Trim();
                else if (r["Descripcion"] == DBNull.Value)
                    r["Descripcion"] = "";
            }

            if (dtTarifas.Columns.Contains("Precio"))
            {
                for (int i = 0; i < dtTarifas.Rows.Count; i++)
                {
                    var raw = (dtTarifas.Rows[i]["Precio"] ?? "").ToString();
                    if (raw.StartsWith("S/."))
                        raw = raw.Substring(3).Trim();
                    else if (raw.StartsWith("S/"))
                        raw = raw.Substring(2).Trim();
                    dtTarifas.Rows[i]["Precio"] = raw;
                }
            }
            else
            {
                dtTarifas.Columns.Add("Precio", typeof(string));
                foreach (DataRow r in dtTarifas.Rows)
                    r["Precio"] = "";
            }

            if (!dtTarifas.Columns.Contains("Fila"))
                dtTarifas.Columns.Add("Fila", typeof(string));

            for (int i = 0; i < dtTarifas.Rows.Count; i++)
                dtTarifas.Rows[i]["Fila"] = (i % 2 == 1) ? "impar" : "";
        }

        private void NormalizeFunciones(DataTable dtFunciones)
        {
            if (dtFunciones == null) return;

            if (!dtFunciones.Columns.Contains("Pelicula"))
                dtFunciones.Columns.Add("Pelicula", typeof(string));

            foreach (DataRow r in dtFunciones.Rows)
            {
                if (dtFunciones.Columns.Contains("Titulo"))
                    r["Pelicula"] = (r["Titulo"] ?? "").ToString().Trim();
                else if (r["Pelicula"] == DBNull.Value)
                    r["Pelicula"] = "";
            }

            if (!dtFunciones.Columns.Contains("Horarios"))
                dtFunciones.Columns.Add("Horarios", typeof(string));

            if (!dtFunciones.Columns.Contains("Fila"))
                dtFunciones.Columns.Add("Fila", typeof(string));

            for (int i = 0; i < dtFunciones.Rows.Count; i++)
                dtFunciones.Rows[i]["Fila"] = (i % 2 == 1) ? "impar" : "";
        }

        public ActionResult cartelera()
        {
            DataTable dtCartelera = _db.ExecuteStoredProcedure("sp_getPeliculas",
                new SqlParameter("@idEstado", 1));

            dtCartelera = CloneTable(dtCartelera); 
            return View("Peliculas", dtCartelera);
        }

        public ActionResult proximosEstrenos()
        {
            DataTable dtEstrenos = _db.ExecuteStoredProcedure("sp_getPeliculas",
                new SqlParameter("@idEstado", 2));

            dtEstrenos = CloneTable(dtEstrenos);
            return View("Peliculas", dtEstrenos); 
        }

    }
}
