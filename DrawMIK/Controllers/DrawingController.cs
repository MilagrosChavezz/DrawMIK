
using DrawMIK.DB.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DrawMIK.Service;
using DrawMIK.Models;

namespace DrawMIK.Controllers
{
    public class DrawingController : Controller
    {
        private readonly DrawingService drawingService;

        public DrawingController(DrawingService _drawingService)
        {
            this.drawingService = _drawingService;
        }

        public Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
              
                Game game = drawingService.FindDrawing(id);
                game.Lines = game.Lines ?? new List<Line>();
                if (game != null)
                {
                   
                    return Task.FromResult<IActionResult>(View(game));  // Pasando el objeto Game a la vista
                }
                // Si no se encuentra el juego, puedes manejarlo de una forma adecuada
                ViewBag.ErrorMessage = "Game not found.";
                return Task.FromResult<IActionResult>(View());
               
            }
            return Task.FromResult<IActionResult>(View());  // Si no se pasa ningún id, retorna la vista vacía o con un mensaje.
        }

     
        [HttpPost]
        public async Task<IActionResult> SaveDrawing([FromBody] SaveDrawingRequest request) {
            if (request.Lines == null || request.DrawingName == null)
            {
                return BadRequest("Invalid input data");
            }

            Console.WriteLine($"Nombre del dibujo recibido: {request.DrawingName}");
            Console.WriteLine($"Líneas recibidas: {request.Lines}");

            Game game = new Game();
            
            game.GameName = request.DrawingName;

            await drawingService.SaveDraw(game);

            foreach (var line in request.Lines)
            {
                line.GameId = game.GameId;
            }

            await drawingService.SaveLines(request.Lines);


            return RedirectToAction("MyDrawings");
        }

        [HttpGet]
        public async Task<IActionResult> MyDrawings()
        {
            List<Game> games = await drawingService.GetDrawings();
            return View(games);
        }

       
    }
      
    
}
