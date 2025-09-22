const getCines = async () => {

  const snap = await getDocs(collection(db, "cines"));

  const contenedor = document.getElementById("contenido-interno");
  contenedor.innerHTML = `<br /><h1>Nuestros Cines</h1><br />`;

  snap.forEach((cine) => {
    const c = cine.data();
    const html = `
        <div class="contenido-cine">
          <img src="/Content/img/cine/${c.id}.jpg" width="227" height="170" />
          <div class="datos-cine">
            <h4>${c.RazonSocial}</h4><br />
            <span>${c.Direccion} - ${c.Ciudad}<br /><br />Teléfono: ${c.Telefonos}</span>
          </div>
          <br />
          <a href="cine.html?&id=${c.id}">
            <img src="img/varios/ico-info2.png" width="150" height="40" />
          </a>
        </div>
      `;

    contenedor.innerHTML += html;
  });
};

getCines();
