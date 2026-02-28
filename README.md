# Pactica-5

Para ejecutar la API, primero se debe clonar el repositorio desde GitHub y abrir el proyecto en Visual Studio o en el editor de preferencia. 
Luego, verificar que la cadena de conexión en el archivo appsettings.json esté configurada correctamente para SQL Server LocalDB.
Después, abrir la Consola del Administrador de Paquetes y ejecutar el comando Update-Database para aplicar las migraciones y crear la base de datos automáticamente. 
Finalmente, ejecutar el proyecto presionando F5 o utilizando el comando dotnet run, lo que iniciará la aplicación y abrirá Swagger en el navegador.

Para probar las rutas usando Swagger, una vez abierta la interfaz, se pueden utilizar los diferentes endpoints disponibles en /api/usuarios.
El método GET permite obtener todos los usuarios o uno específico por ID; 
el método POST permite crear un nuevo usuario enviando los datos en formato JSON; 
el método PUT permite actualizar un usuario existente indicando su ID; y el método DELETE permite eliminar un usuario por ID.

<img width="1199" height="930" alt="image" src="https://github.com/user-attachments/assets/3fc55fe1-355a-40aa-8203-af2110b060d7" />
<img width="1218" height="869" alt="image" src="https://github.com/user-attachments/assets/07668368-3351-4f6e-995b-170a97eea141" />
<img width="1264" height="291" alt="image" src="https://github.com/user-attachments/assets/89583fcc-7824-4b22-89b5-11b111ee5cd6" />
<img width="1275" height="883" alt="image" src="https://github.com/user-attachments/assets/0e31631b-fd57-4b9c-83ba-3057189fc7df" />
<img width="1195" height="721" alt="image" src="https://github.com/user-attachments/assets/45b93dff-e1c9-4607-ae0a-55e6c701fef0" />
<img width="1408" height="849" alt="image" src="https://github.com/user-attachments/assets/d517c248-ea12-4ba7-82f9-41423d2b6acc" />

