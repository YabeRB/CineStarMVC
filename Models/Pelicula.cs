namespace CineStarMVC.Models
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Clasificacion { get; set; }
        public string Formato { get; set; }
        public string Sinopsis { get; set; }
        public string Imagen { get; set; }
        public string TrailerUrl { get; set; }
    }
}
