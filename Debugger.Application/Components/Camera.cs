using System;
using System.Runtime.Intrinsics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Debugger.Application.Components
{
    public class Camera
    {

        public XnaVector2 Position { get; set; }
        public float Zoom { get; set; }
        private Vector3 _translationVector;
        private Vector3 _scaleVector;
        private Vector3 _centerVector;

        public Camera()
        {
            Zoom = 1.0f;
        }

        public void Follow(Vector2 target, float speed, float dt)
        {

            float frameIndependentSpeed = 1f - MathF.Exp(-speed * dt);

            Position = Vector2.Lerp(Position, target, frameIndependentSpeed);
        }

        public Matrix GetTransformationMatrix(GraphicsDevice graphicsDevice)
        {
            _translationVector.X = -Position.X;
            _translationVector.Y = -Position.Y;
            _scaleVector.X = Zoom;
            _scaleVector.Y = Zoom;
            _centerVector.X = graphicsDevice.Viewport.Width / 2.0f;
            _centerVector.Y = graphicsDevice.Viewport.Height / 2.0f;

            return Matrix.CreateTranslation(_translationVector) *
                   Matrix.CreateScale(_scaleVector) *
                   Matrix.CreateTranslation(_centerVector);
        }

    }

}