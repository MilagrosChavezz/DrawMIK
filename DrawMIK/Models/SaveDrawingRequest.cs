using DrawMIK.DB.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DrawMIK.Models
{
    public class SaveDrawingRequest 
    {
        public List<Line> Lines { get; set; }
        public string DrawingName { get; set; }
    }
}
