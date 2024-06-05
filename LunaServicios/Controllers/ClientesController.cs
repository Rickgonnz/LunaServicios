using LunaServicios.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using Repository;
using Tools;

namespace LunaServicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IOptions<MyConfig> _config;
        private readonly IUnitOfWork _unitOfWork;

        public ClientesController(IOptions<MyConfig> config, IUnitOfWork unitOfWork)
        {
            _config = config;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            IEnumerable<Cliente> clientes = from d in _unitOfWork.Cliente.Get()
                                            select new Cliente
                                            {
                                                ClienteId = d.ClienteId,
                                                Nombre = d.Nombre,
                                                Rut = d.Rut,
                                                Direccion = d.Direccion,
                                                Telefono = d.Telefono
                                            };
            if (clientes == null || !clientes.Any())
            {
                return NotFound("No existen clientes");
            }

            return Ok(clientes);
        }

        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            var cliente = _unitOfWork.Cliente.Get(id);
            if (cliente == null)
            {
                return NotFound("El cliente no existe.");
            }

            return Ok(cliente);
        }

        [HttpPost("Add")]
        public IActionResult Add(Cliente clientePost)
        {
            try
            {
                if (clientePost == null)
                {
                    return BadRequest("Los datos del cliente son nulos");
                }

                var cliente = new Cliente();
                cliente.ClienteId = clientePost.ClienteId;
                cliente.Nombre = clientePost.Nombre;
                cliente.Rut = clientePost.Rut;
                cliente.Direccion = clientePost.Direccion;
                cliente.Telefono = clientePost.Telefono;

                _unitOfWork.Cliente.Add(cliente);
                _unitOfWork.Save();

                var successMessage = new { message = "cliente de nombre : " + clientePost.Nombre + " ID : " + clientePost.ClienteId + " ha sido creado con éxito" };

                //Archivo en Log el cliente que se creó
                Log.GetInstance(_config.Value.PathLog).Save("cliente de nombre : " + clientePost.Nombre + " ID : " + clientePost.ClienteId + " ha sido creado con éxito");
                return Ok(successMessage);
            }
            catch (Exception ex)
            {
                Log.GetInstance(_config.Value.PathLog).Save("Ocurrió un error al crear el cliente. Detalles: " + ex.Message.ToString());
                return StatusCode(500, "Ocurrió un error al crear el cliente. Por favor, inténtelo de nuevo más tarde.");
            }

        }

        [HttpPost("Delete")]
        public IActionResult Delete(int clienteId)
        {
            try
            {
                var cliente = _unitOfWork.Cliente.Get(clienteId);
                if (cliente == null)
                {
                    return NotFound("El cliente no existe.");
                }
                else
                {
                    _unitOfWork.Cliente.Delete(clienteId);
                    _unitOfWork.Cliente.Save();

                    var successMessage = new { message = "cliente de nombre : " + cliente.Nombre + " ID : " + cliente.ClienteId + " ha sido eliminado con éxito" };
                    //Archivo en Log el cliente que se elimino
                    Log.GetInstance(_config.Value.PathLog).Save("cliente de nombre : " + cliente.Nombre + " ID : " + cliente.ClienteId + " ha sido eliminado con éxito");
                    return Ok(successMessage);
                }
            }
            catch (Exception ex)
            {
                Log.GetInstance(_config.Value.PathLog).Save("Ocurrió un error al eliminar el cliente. Detalles: " + ex.Message.ToString());
                return StatusCode(500, "Ocurrió un error al eliminar el cliente. Por favor, inténtelo de nuevo más tarde.");
            }

        }

        [HttpPost("Update")]
        public IActionResult Update(Cliente clientePost)
        {
            try
            {
                var cliente = _unitOfWork.Cliente.Get(clientePost.ClienteId);
                if (cliente == null)
                {
                    return NotFound("El cliente no existe.");
                }
                else
                {
                    cliente.Nombre = clientePost.Nombre;
                    cliente.Rut = clientePost.Rut;
                    cliente.Direccion = cliente.Direccion;
                    cliente.Telefono = cliente.Telefono;
                    _unitOfWork.Cliente.Update(cliente);
                    _unitOfWork.Save();

                    var successMessage = new { message = "cliente ID : " + cliente.ClienteId + " ha sido actualizado con éxito" };
                    //Archivo en Log el cliente que se actualizó
                    Log.GetInstance(_config.Value.PathLog).Save("cliente de nombre : " + cliente.Nombre + " ID : " + cliente.ClienteId + " ha sido actualizado con éxito");
                    return Ok(successMessage);

                }
            }
            catch (Exception ex)
            {
                Log.GetInstance(_config.Value.PathLog).Save("Ocurrió un error al actualizar el cliente. Detalles: " + ex.Message.ToString());
                return StatusCode(500, "Ocurrió un error al actualizar el cliente. Por favor, inténtelo de nuevo más tarde.");
            }
        }

    }
}
