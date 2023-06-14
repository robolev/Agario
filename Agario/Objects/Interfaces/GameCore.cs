using Engine;
namespace Agario.Agario.Objects.Interfaces;
using SFML.Window;

public abstract class GameCore
{
    protected Engine.Engine Engine { get; init; }
    
    protected GameCore()
    {
        Engine = new Engine.Engine();
		
        Engine.OnFrameStart += OnFrameStart;
        Engine.OnFrameEnd += OnFrameEnd;
		
        Engine.window.Closed += OnWindowClosed;
    }

    public virtual void Run()
    {
	    Initialize();
	    Engine.Run();
    }
    public virtual void Initialize()
    {
	    
    }

    protected virtual void OnFrameStart()
    {
		
    }

    protected virtual void OnFrameEnd()
    {
		
    }
	
    protected virtual void OnWindowClosed(object sender, EventArgs args)
    {
        Window window = (Window) sender;
        window.Close();
    }

    public  Engine.Engine GetEngine()
    {
	    
	    return Engine;
    }
}