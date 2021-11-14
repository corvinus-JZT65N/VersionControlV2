using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyak8.Entities
{
    class Present : Toy
    {
        public SolidBrush BoxColor { get; private set; }
        public SolidBrush RibbonColor { get; private set; }
        public Present(Color ribbon, Color box)
        {
            RibbonColor = new SolidBrush(ribbon);
            BoxColor = new SolidBrush(box);
        }
        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(BoxColor, 0, 0, Width, Width);
            g.FillRectangle(RibbonColor, 0, Width/5*2, Width, Width / 5);
            g.FillRectangle(RibbonColor, Width/5*2, 0, Width / 5, Width);
        }
    }
}
