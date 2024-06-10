<h1>CryptoApp API RESTful</h1>
<hr>
CryptoApp es una API que funciona a través de una web en Vue y que gestiona usuarios y criptomonedas. Los usuarios pueden realizar operaciones como ingresar y retirar dinero, comprar y vender criptomonedas, y visualizar sus transacciones y criptomonedas compradas. Además, un administrador tiene acceso para gestionar todas las operaciones relacionadas con las criptomonedas.
<hr>
<ul>
  <li><b>Docker: </b>con docker compose levantamos la api, la base de datos SQL Server y la web en Vue para poder usarla contenerizada.</li>
  <li><b>Azure: </b>la api y la base de datos SQL Server también están creadas en la nube de Azure: <a href="https://cryptoapppro.azurewebsites.net/swagger/index.html">CryptoApp API</a></li>
</ul>
<hr>
<h1>Imágenes de Docker Hub</h1>
<li><b>Imagen de la API: </b>docker pull mariozgz/cryptoapp:1.0</li>
<li><b>Imagen de la Web: </b>docker pull mariozgz/cryptoapp-vue:1.0</li>
