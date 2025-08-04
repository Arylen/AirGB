using AirGB.UI;
using ImGuiNET;
using Silk.NET.Maths;

namespace AirGB;

public class MainWindow : AppWindow
{
    public override string WindowTitle => "AirGB";

    public override Vector2D<int> DefaultSize => new Vector2D<int>(800, 600);

    public override void DrawWindow()
    {
        DrawMainMenuBar();
        if (ImGui.Begin("MainWindow"))
        {
            ImGui.Text("Meow");
        }

        ImGui.End();
    }

    private void DrawMainMenuBar()
    {
        
    }
}