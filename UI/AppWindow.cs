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

    protected virtual void OnWindowLoad() { }
    protected virtual void OnWindowUpdate() { }
    protected virtual void OnWindowClosing() { }
    public virtual void DrawWindow() { }

    private void InitializeWindow()
    {
        var options = WindowOptions.Default;

        options.Size = DefaultSize;
        options.Title = WindowTitle;

        window = Window.Create(options);

        window.Load += OnLoad;
        window.Update += OnUpdate;
        window.Render += OnRender;
        window.Closing += OnClose;
    }

    private void OnLoad()
    {
        gl = GL.GetApi(window);
        inputContext = window.CreateInput();
        controller = new ImGuiController(gl, window, inputContext);
        OnWindowLoad();
    }

    private void OnUpdate(double deltaTime)
    {
        controller.Update((float)deltaTime);
        OnWindowUpdate();
    }

    private void OnRender(double deltaTime)
    {
        gl.Clear((uint)ClearBufferMask.ColorBufferBit);
        DrawWindow();
        controller.Render();
    }

    private void OnClose()
    {
        OnWindowClosing();

        controller?.Dispose();
        inputContext?.Dispose();
        gl?.Dispose();

        isWindowOpen = false;
    }

    public void Open(bool blockMainThread = false)
    {
        if (isWindowOpen)
            return;

        isWindowOpen = true;

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
}