using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using SoT;
using SoT.Data;
using SoT.Game.Engine;
using SoT.Game.Athena;
using SoT.Game.Athena.Service;

namespace SotEspCoreTest
{
    class Program : GameWindow
    {
        private SotCore core;

        public Program(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) { }

        static void Main(string[] args)
        {
            var gameWindowSettings = new GameWindowSettings();
            var nativeWindowSettings = new NativeWindowSettings { Size = new Vector2i(800, 600), Title = "Entity Renderer" };

            using (var game = new Program(gameWindowSettings, nativeWindowSettings))
            {
                game.Run();
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            core = new SotCore();
            if (!core.Prepare(true))
            {
                Close();
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            if (core == null || !core.Prepare(true))
            {
                Close();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            UE4Actor[] actors = core.GetActors();
            foreach (UE4Actor actor in actors)
            {
                DrawEntity(actor);
            }

            SwapBuffers();
        }

        private void DrawEntity(UE4Actor actor)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(1.0f, 1.0f, 1.0f); // White color

            // Assuming a simple box representation for each entity
            float size = 10.0f; // Adjust size as needed
            float x = actor.Position.X - size / 2.0f;
            float y = actor.Position.Y - size / 2.0f;

            GL.Vertex2(x, y);
            GL.Vertex2(x + size, y);
            GL.Vertex2(x + size, y + size);
            GL.Vertex2(x, y + size);

            GL.End();
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Keys.Escape)
            {
                Close();
            }
        }
    }
}
