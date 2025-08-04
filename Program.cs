namespace AirGB;

public class Program
{
    public static void Main(string[] args)
    {
        var mainWindow = new MainWindow();
        mainWindow.Open(
            blockMainThread: true
        );
    }
}