using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace DrawIt23
{
    public class DrawItSignal: Hub
    {
        public void UpdateCanvas(int x, int y, int lastx, int lasty, int size, string color)
        {
            
            Clients.All.updateDot(x, y, lastx, lasty, size, color);
        }
        public void ClearCanvas()
        {
            Clients.All.clearCanvas();
        }

    }
}