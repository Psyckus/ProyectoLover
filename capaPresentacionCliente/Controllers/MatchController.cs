using capaPresentacionCliente.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace capaPresentacionCliente.Controllers
{
    public class MatchController : Controller
    {
        private readonly tiusr3pl_loverEntities1 _dbContext;

        public MatchController(tiusr3pl_loverEntities1 dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: Match
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<match1> Create(int user1Id, int user2Id)
        {
            var existingMatch = _dbContext.match1.FirstOrDefault(m => (m.cliente1 == user1Id && m.cliente2 == user2Id) || (m.cliente1 == user2Id && m.cliente2 == user1Id));

            if (existingMatch != null)
            {
                // Si ya existe una coincidencia, no hacer nada.
                return Ok();
            }

            // Crear un nuevo registro de coincidencia en la base de datos.
            var newMatch = new match1
            {
                cliente1 = user1Id,
                cliente2 = user2Id
            };
            _dbContext.match1.Add(newMatch);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        private match1 Ok()
        {
            throw new NotImplementedException();
        }
    }
}