using Labb3DatorGrafik.Component;
using Labb3DatorGrafik.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace Labb3DatorGrafik.System
{
    public class LightSystem : ISystem
    {
        LightComponent lightComp;
        public LightSystem() { }

        public void Draw(GameTime gameTime)
        {
            //rita ut ljuset
            var lightComponent = ComponentManager.Get().GetComponents<LightComponent>().Values.FirstOrDefault() as LightComponent;
            var cameraComp = ComponentManager.Get().GetComponents<CameraComponent>().Values.FirstOrDefault() as CameraComponent;
            CreateLightProjection(lightComponent, cameraComp);
        }

        private void CreateLightProjection(LightComponent lightComponent, CameraComponent cameraComp)
        {
            Matrix lightRot = Matrix.CreateLookAt(Vector3.Zero, -lightComp.LightDir, Vector3.Up);
            //skapa ljus grejerna
            Vector3[] frustumCorners = cameraComp.BoundingFrustum.GetCorners();
            for (int i = 0; i < frustumCorners.Length; i++) {
                frustumCorners[i] = Vector3.Transform(frustumCorners[i], lightRot);
            }
            BoundingBox boxLight = BoundingBox.CreateFromPoints(frustumCorners);
            Vector3 SizeOfBox = boxLight.Max - boxLight.Min;
            Vector3 halfOfBox = SizeOfBox * 0.5f;
            Vector3 lightPos = boxLight.Min + halfOfBox;
            lightPos.Z = boxLight.Min.Z;
           // lightPos = Vector3.Transform(lightPos, Matrix.Invert(lightRot)); denna har björn, kanske för att få det o bli stabilt på samma plats dock fuckar det för oss
            Matrix viewMatrixLight = Matrix.CreateLookAt(lightPos, lightPos - lightComp.LightDir, Vector3.Up);
            Matrix lightProjectionMatrix = Matrix.CreateOrthographic(SizeOfBox.X, SizeOfBox.Y, -SizeOfBox.Z, SizeOfBox.Y);
            Console.WriteLine("LightPos: "+ lightPos);
            lightComp.LightProjection = viewMatrixLight * lightProjectionMatrix;
        }

        public void Update(GameTime gameTime)
        {
            //förflytta ljuset konstant 
            var lightComponent = ComponentManager.Get().GetComponents<LightComponent>().FirstOrDefault();
            lightComp = lightComponent.Value as LightComponent;
            var rotationY = (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.00005f;
            var rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, rotationY);
            lightComp.LightDir = Vector3.Transform(lightComp.LightDir, rotation);

            Console.WriteLine("light dir" + lightComp.LightDir);
        }
    }
}
