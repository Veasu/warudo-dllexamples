using System;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;

namespace veasu.mouse
{
  [NodeType(Id = "com.veasu.mouseclicknode", Title = "On Mouse Click", Category = "Mouse Nodes")]
  public class OnMouseButtonClickNode : Warudo.Core.Graphs.Node
  {
    [DataInput]
    public mouseButtons mouseButton = mouseButtons.Left;

    public enum mouseButtons : ushort {
      Left = 0x01,
      Right = 0x02,
      Middle = 0x04,
      Side1 = 0x05,
      Side2 = 0x06
    }

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern short GetAsyncKeyState(UInt16 virtualKeyCode);

    [FlowOutput]
    public Continuation OnTrigger;

    public override void OnUpdate() {
      if (GetAsyncKeyState((ushort)mouseButton) != 0)
      {
        InvokeFlow(nameof(OnTrigger));
      }
    }
  }
}
