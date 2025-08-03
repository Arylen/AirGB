using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Windowing;

namespace AirGB.UI;

public abstract class AppWindow
{
    private IWindow window;
    private GL gl;
    private ImGuiController controller;
    private IInputContext inputContext;

    protected bool isWindowOpen;

    public abstract string WindowTitle { get; }
    public abstract Vector2D<int> DefaultSize { get; }

    protected AppWindow()
    {
        InitializeWindow();
    }

    private void InitializeWindow()
    {
        var options = WindowOptions.Default;

        options.Size = DefaultSize;
        options.Title = WindowTitle;

        window = Window.Create(options);


    }

    public void Open(bool blockMainThread = false)
    {
        if (isWindowOpen)
            return;
            
        if (blockMainThread)
        {
            window.Run();
        }
        else
        {
            Task.Run(
                () => window.Run()
            );
        }
    }

    private void OnLoad()
    {

    }

    private void OnUpdate(double deltaTime)
    {

    }

    private void OnRender(double deltaTime)
    {

    }

    private void OnClose()
    {

    }
}