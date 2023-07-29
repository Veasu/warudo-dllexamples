using UnityEngine;
using Warudo.Core.Attributes;
using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace veasu.mouse
{
  [NodeType(Id = "com.veasu.mouseposnode", Title = "Mouse Position ", Category = "Mouse Nodes")]
  public class MousePosNode : Warudo.Core.Graphs.Node
  {
    [DataOutput]
    [Label("MouseX")]
    private object getMouseX() {
      return mouseX;
    }

    [DataOutput]
    [Label("MouseY")]
    private object getMouseY() {
      return  mouseY;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public static implicit operator Point(POINT point)
        {
            return new Point(point.X, point.Y);
        }
    }

    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out POINT lpPoint);

    public static Point GetCursorPosition()
    {
        POINT lpPoint;
        GetCursorPos(out lpPoint);
        return lpPoint;
    }

    private float mouseX;
    private float mouseY;
    
    public override void OnUpdate() {
      Point mousePos = GetCursorPosition();
      mouseX = Math.Clamp(mousePos.X, 0, Screen.currentResolution.width);
      if (mousePos.X < Screen.currentResolution.width && mousePos.X > 0){
        mouseY = Math.Clamp(mousePos.Y, 0, Screen.currentResolution.height);
      }
    }
  }

}
