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
        public ShadowSystem()
        {
        }
        public void Draw(GameTime gameTime) {
            CreateShadowMap();
            DrawShadowMap();

        }

        private void CreateShadowMap()
        {
            throw new NotImplementedException();
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

            var shadowMappingEffects = ComponentManager.Get().GetComponents<ShadowMapEffect>().FirstOrDefault();
            var fogComp = ComponentManager.Get().GetComponents<FogComponent>().Values.FirstOrDefault();
            var ambientComp = ComponentManager.Get().GetComponents<AmbientComponent>().Values.FirstOrDefault();
            if (cameraComp == null || shadowRender == null || light == null || fogComp == null || ambientComp == null)
            {
                return;
            }
            foreach (var modelComp in models)
            {
                var model = modelComp.Value as ModelComponent;
             
                ShadowMapEffect ShadowMappingEffect;
                
                    ShadowMappingEffect.AmbientComponent = ambientComp;
                    ShadowMappingEffect.camera = cameraComp;
                    ShadowMappingEffect.fog = fogComp;
                    ShadowMappingEffect.light = light;
                    ShadowMappingEffect.ShadowRenderTarget = shadowRender.;
                    DrawModel(modelComp, false, shadowMappingEffect);
                

            }
        }
            public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void DrawModel(ModelComponent modelComp, bool v, ShadowMapEffect shadowMappingEffect)
        {
            var model = modelComp.model;

            string TechniqueName = v ? "CreateShadowMap" : "DrawWithShadowMap";
            Matrix[] transforms = new Matrix[model.Bones.Count];

            throw new NotImplementedException();
        }
    }
