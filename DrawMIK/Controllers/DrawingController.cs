
using DrawMIK.DB.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DrawMIK.Service;

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
        public async Task<IActionResult> SaveDrawing([FromBody] List<Line> lines,String drawingName) {
            Console.WriteLine($"Nombre del dibujo recibido: {drawingName}");
            Console.WriteLine($"Líneas recibidas: {lines}");

            Game game = new Game();
            game.Lines = lines;
            game.GameName = drawingName;

            foreach (var line in lines)
            {
                line.GameId = game.GameId;
            }
            await drawingService.SaveLines(lines);
             await drawingService.SaveDraw( game);

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
