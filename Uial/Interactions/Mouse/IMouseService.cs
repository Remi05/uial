using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uial.Interactions.Mouse
{
    public interface IMouseService
    {
        void Click();
        void DoubleClick();
        void Drag(int xOffset, int yOffset);
        void DragTo(int xPosition, int yPosition);
        void Move(int xOffset, int yOffset);
        void MoveTo(int xPosition, int yPosition);
        void Press();
        void Release();
    }
}
