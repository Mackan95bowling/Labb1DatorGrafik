using Labb3DatorGrafik.Component;
using Labb3DatorGrafik.Manager;
using Labb3DatorGrafik.Service;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3DatorGrafik.System
{
    public class ShadowSystem : ISystem
    {
        private RenderTarget2D renderTarget;
        public ShadowSystem()
        {
            renderTarget = new RenderTarget2D(GameService.Instance().graphics, 2048, 2048, false, SurfaceFormat.Single, DepthFormat.Depth24);
        }
        public void Draw(GameTime gameTime)
        {
            GameService.Instance().graphics.BlendState = BlendState.Opaque;

            GameService.Instance().graphics.DepthStencilState = DepthStencilState.Default;

            CreateShadowMap();
            DrawShadowMap();

        }

        private void CreateShadowMap()
        {
            GameService.Instance().graphics.SetRenderTarget(renderTarget);
            GameService.Instance().graphics.Clear(Color.CornflowerBlue);


            var light = ComponentManager.Get().GetComponents<LightComponent>().Values.FirstOrDefault() as LightComponent;

            var camera = ComponentManager.Get().GetComponents<CameraComponent>().Values.First() as CameraComponent;
            var ambient = ComponentManager.Get().GetComponents<AmbientComponent>().Values.First() as AmbientComponent;

            var models = ComponentManager.Get().GetComponents<ModelComponent>().Values;
           

            foreach (ModelComponent modelComp in models)
            {
                GameService.Instance().graphics.SetRenderTarget(null);
                DrawModel(modelComp, "CreateShadowMap", camera, light, ambient);

            }

        }

        public void DrawShadowMap()
        {
            GameService.Instance().graphics.Clear(Color.CornflowerBlue);
            GameService.Instance().graphics.BlendState = BlendState.Opaque;
            GameService.Instance().graphics.DepthStencilState = DepthStencilState.Default;
            GameService.Instance().graphics.RasterizerState = RasterizerState.CullCounterClockwise;
            GameService.Instance().graphics.SamplerStates[0] = new SamplerState
            {
                AddressU = TextureAddressMode.Clamp,
                AddressV = TextureAddressMode.Clamp,
                AddressW = TextureAddressMode.Clamp,
                Filter = TextureFilter.Linear,
                ComparisonFunction = CompareFunction.LessEqual,
                FilterMode = TextureFilterMode.Comparison
            };


            var models = ComponentManager.Get().GetComponents<ModelComponent>();
            var cameraComp = ComponentManager.Get().GetComponents<CameraComponent>();
            var camera = cameraComp.FirstOrDefault().Value as CameraComponent;
            var shadowRenderComp = ComponentManager.Get().GetComponents<ShadowRenderTargetComponent>();
            var shadowRender = shadowRenderComp.FirstOrDefault().Value as ShadowRenderTargetComponent;

            var lightComp = ComponentManager.Get().GetComponents<LightComponent>();
            var light = lightComp.FirstOrDefault().Value as LightComponent;

            var shadowMappingEffects = ComponentManager.Get().EntityComponent<ShadowMapEffect>(1);
            var fogComp = ComponentManager.Get().GetComponents<FogComponent>().Values.FirstOrDefault();
            var ambientComp = ComponentManager.Get().GetComponents<AmbientComponent>().Values.FirstOrDefault() as AmbientComponent;
            if (cameraComp == null || shadowRender == null || light == null || fogComp == null || ambientComp == null)
            {
                return;
            }
            foreach (ModelComponent modelComp in models.Values)
            {
                DrawModel(modelComp, "DrawWithShadowMap", camera, light, ambientComp);
            }
        }
        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void DrawModel(ModelComponent modelComp, string techniqueName, CameraComponent camera, LightComponent light, AmbientComponent ambient)
        {
            var model = modelComp.model;
            #region EffectParameters Enligt hur Simon och nick gör
            foreach (ModelMesh mesh in model.Meshes)

            {

                foreach (var meshPart in mesh.MeshParts)

                {
                    modelComp.ModelEffect.CurrentTechnique = modelComp.ModelEffect.Techniques[techniqueName];
                    if (techniqueName.Contains("DrawWithShadowMap"))

                    {

                        modelComp.ModelEffect.Parameters["ShadowMap"].SetValue(renderTarget);

                    }

                    modelComp.ModelEffect.Parameters["Texture"].SetValue(modelComp.texture);

                    modelComp.ModelEffect.Parameters["World"].SetValue(Matrix.CreateTranslation(modelComp.modelPosition));

                    modelComp.ModelEffect.Parameters["View"].SetValue(camera.view);

                    modelComp.ModelEffect.Parameters["Projection"].SetValue(camera.projection);

                    modelComp.ModelEffect.Parameters["DiffuseLightDirection"].SetValue(light.LightDir);

                    modelComp.ModelEffect.Parameters["LightViewProj"].SetValue(light.LightProjection);

                    modelComp.ModelEffect.Parameters["AmbientColor"].SetValue(ambient.AmbientColor);

                    modelComp.ModelEffect.Parameters["AmbientIntensity"].SetValue(ambient.Intensity);

                    modelComp.ModelEffect.Parameters["DiffuseLightDirection"].SetValue(light.DiffLightDir);

                    modelComp.ModelEffect.Parameters["DiffuseColor"].SetValue(light.DiffLightColor);

                    modelComp.ModelEffect.Parameters["DiffuseIntensity"].SetValue(light.DiffIntensity);

                    modelComp.ModelEffect.Parameters["CameraPosition"].SetValue(camera.cameraPosition);



                    modelComp.ModelEffect.Parameters["ShadowStrenght"].SetValue(0.5f);

                    modelComp.ModelEffect.Parameters["DepthBias"].SetValue(0.001f);

                    modelComp.ModelEffect.Parameters["ViewVector"].SetValue(Vector3.One);

                    modelComp.ModelEffect.Parameters["Shininess"].SetValue(0.9f);

                    modelComp.ModelEffect.Parameters["SpecularColor"].SetValue(Color.CornflowerBlue.ToVector4());

                    modelComp.ModelEffect.Parameters["SpecularIntensity"].SetValue(0.1f);



                    modelComp.ModelEffect.Parameters["FogStart"].SetValue(100f);

                    modelComp.ModelEffect.Parameters["FogEnd"].SetValue(150f);

                    modelComp.ModelEffect.Parameters["FogColor"].SetValue(Color.CornflowerBlue.ToVector4());

                    modelComp.ModelEffect.Parameters["FogEnabled"].SetValue(true);

                    foreach (var pass in modelComp.ModelEffect.CurrentTechnique.Passes)

                    {

                        pass.Apply();

                    }
                    /*Här ritas allt ut!!!! ska vara kvar såhär*/
                    GameService.Instance().graphics.SetVertexBuffer(meshPart.VertexBuffer);
                    GameService.Instance().graphics.Indices = meshPart.IndexBuffer;
                    GameService.Instance().graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, meshPart.VertexOffset, meshPart.StartIndex, meshPart.PrimitiveCount);
                }
            }
#endregion
        }
    }
}