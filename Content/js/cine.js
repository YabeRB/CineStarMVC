const getCine = async () => {
  const params = new URLSearchParams(window.location.search);
  const idUrl = params.get("id"); 

  try {

    const q = query(collection(db, "cines"), where("id", "==", idUrl));
    const querySnap = await getDocs(q);

    if (!querySnap.empty) {
      const cineDoc = querySnap.docs[0]; 
      const cine = cineDoc.data();


      const contenedor = document.getElementById("contenido-interno");

      contenedor.innerHTML = `
        <h2>${cine.RazonSocial}</h2>
        <div class="cine-info">
          <div class="cine-info datos">
            <p>${cine.Direccion} - ${cine.Ciudad}</p>
            <p>Teléfono: ${cine.Telefonos} anexo 865</p>
            <br/>
            <div class="tabla"> 
            <div class="fila"> 
            <div class="celda-titulo">Lunes y Miércoles</div> 
            <div class="celda">S/. 4.00</div>
            </div>
            <div class="fila impar"> 
            <div class="celda-titulo">Martes</div>
            <div class="celda">S/. 3.50</div> 
            </div> 
            <div class="fila"> 
            <div class="celda-titulo">Jueves y Viernes</div> 
            <div class="celda">S/. 6.50</div> 
            </div> 
            <div class="aviso">
              <p>A partir del 1ro de julio de 2016, Cinestar Multicines realizará el cobro de la comisión de S/. 1.00
                 adicional al tarifario vigente, a los usuarios que compren sus entradas por el aplicativo de Cine Papaya
                 para Cine Star Comas, Excelsior, Las Américas, Benavides, Breña, San Juan, UNI, Aviación, Sur, Porteño,
                 Tumbes y Tacna.</p>
            </div>
          </div>
          <img src="img/cine/${cine.id}.2.jpg"/>
          <br/><br/><h4>Los horarios de cada función están sujetos a cambios sin previo aviso.</h4><br/>
          <div class="cine-info peliculas">
            <div class="tabla">
              <div class="fila">
                <div class="celda-cabecera">Películas</div>
                <div class="celda-cabecera">Horarios</div>
              </div>
            </div>
          </div>
        </div>
        <div>
          <img style="float:left;" src="img/cine/${cine.id}.3.jpg" alt="Imagen del cine"/>
          <span class="tx_gris">
            Precios de los juegos: desde S/1.00 en todos los Cine Star.<br/>
            Horario de atención de juegos es de 12:00 m hasta las 10:30 pm. 
            <br/><br/>
            Visitános y diviértete con nosotros. 
            <br/><br/>
            <b>CINESTAR</b>, siempre pensando en ti. 
          </span>		
        </div>
      `;
    } else {
      console.error("No se encontró el cine con id =", idUrl);
    }
  } catch (error) {
    console.error("Error al obtener el cine:", error);
  }
};

getCine();
