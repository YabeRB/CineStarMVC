async function getPeliculas() {
  try {

    const snap = await getDocs(collection(db, "peliculas"));


    const contenedor = document.getElementById("contenido-interno");
    let html = `<br/><h1>Cartelera</h1><br/>`;

    snap.forEach(pelicula => {

      const p = pelicula.data();
      html += `
        <div class="contenido-pelicula">
          <div class="datos-pelicula">
            <h2>${p.Titulo}</h2>
            <p>${p.Sinopsis}</p>
            <br/>
            <div class="boton-pelicula"> 
              <a href="pelicula.html?id=${p.id}">
                <img src="img/varios/btn-mas-info.jpg" width="120" height="30" alt="Ver info"/>
              </a>
            </div>
            <div class="boton-pelicula"> 
              <a href="https://www.youtube.com/watch?v=${p.Link}" target="_blank">
                <img src="img/varios/btn-trailer.jpg" width="120" height="30"  alt="Ver trailer"/>
              </a>
            </div> 
          </div>
          <img src="img/pelicula/${p.id}.jpg" width="160" height="226"/><br/><br/>
        </div>
      `;
    });

    contenedor.innerHTML = html;
  } catch (error) {
    console.error("Error cargando películas:", error);
  }
};

getPeliculas();



