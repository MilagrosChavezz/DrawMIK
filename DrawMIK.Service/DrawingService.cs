using DrawMIK.DB;
using DrawMIK.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrawMIK.Service
{
    public class DrawingService
    {
        public DrawMikContext _context;
        public DrawingService(DrawMikContext ctx)
        {
            _context = ctx;
        }

        public async Task SaveDraw(Game game)
        {
         

            await _context.Games.AddAsync(game);

            await _context.SaveChangesAsync();

        }

        public async Task<Game> FindDrawing(int id)
        {
            if (id == null) { 
            throw new System.ArgumentNullException(nameof(id));

            }
            return await _context.Games.FindAsync(id);
        }
       
        public async Task<List<Game>> GetDrawings()
        {
            return await _context.Games.ToListAsync();
        }

        public Game FindDrawing(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveLines(List<Line> lines)
        {
            foreach ( var line in lines) {

                await _context.Lines.AddAsync(line);
                
            }
            await _context.SaveChangesAsync();
        }
    }
}
