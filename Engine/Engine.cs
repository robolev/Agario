﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Agario;
using Engine.Config;


namespace Engine
{
    public class Engine
    {
        public List<IDrawable> drawables = new List<IDrawable>();
        public List<IUpdatable> updatables = new List<IUpdatable>();

        public RenderWindow window;
        
        public Action OnFrameStart;
        public Action OnFrameEnd;
        
        public static Engine Instance { get; private set; }

        public Engine()
        {
            window = new RenderWindow(new VideoMode(EngineConfig.WindowWidth, EngineConfig.WindowHeight), "Moving Circle", Styles.Default, new ContextSettings { AntialiasingLevel = 8 });
            window.SetFramerateLimit(EngineConfig.FrameRateLimit);
            Instance = this;
        }

        public void Run()
        {
            Clock clock = new Clock();

            while (window.IsOpen)
            {
                float deltaTime = clock.Restart().AsSeconds();
           
                window.DispatchEvents();

                OnFrameStart.Invoke();
                
                Update(deltaTime);
                
                Render();
                
                OnFrameEnd.Invoke();
                
                window.Display();
            }
        }

        public void RegisterActor(IDrawable? drawable = null, IUpdatable? updatable = null)
        {
            if (drawable != null && !drawables.Contains(drawable))
            {
                drawables.Add(drawable);
            } 

            if (updatable != null && !updatables.Contains(updatable))
            {
                updatables.Add(updatable);
            }
        }
        

        public void Update(float deltaTime)
        {
            foreach (var updatable in updatables)
            {
                updatable.Update(deltaTime);
            }
        }

        public void Render()
        {
            window.Clear(Color.Black);
            drawables.Sort((drawable, drawable1) => drawable.ZIndex.CompareTo(drawable1.ZIndex));
            
            foreach (var drawable in drawables)
            {
                drawable.Draw(window);
            }
            
        }
    }
}
