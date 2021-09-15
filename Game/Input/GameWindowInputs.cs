using System;
using OpenTK;

namespace Sim.Input
{
    public static class GameWindowInputs
    {
        public static InputContext CreateInputContext(GameWindow window)
        {
            var context = new InputContext();
            context.RegisterAction("toggle_fullscreen", new FullScreenInputAction(window));
            context.RegisterAction("close_game", new CloseInputAction(window));
            return context;
        }
    }

    public class FullScreenInputAction : InputAction
    {
        private readonly GameWindow window;
        private Size cachedSize;

        public FullScreenInputAction(GameWindow window)
        {
            this.window = window;
            this.cachedSize = window.ClientSize;
        }

        public void OnTrigger()
        {
            if (window.WindowState == WindowState.Fullscreen)
            {
                window.WindowBorder = WindowBorder.Resizable;
                window.WindowState = WindowState.Normal;
                window.ClientSize = cachedSize;
            }
            else
            {
                cachedSize = window.ClientSize;
                window.WindowBorder = WindowBorder.Hidden;
                window.WindowState = WindowState.Fullscreen;
            }
        }
    }

    public class CloseInputAction : InputAction
    {
        private readonly GameWindow window;

        public CloseInputAction(GameWindow window)
        {
            this.window = window;
        }

        public void OnTrigger()
        {
            window.Exit();
        }
    }
}