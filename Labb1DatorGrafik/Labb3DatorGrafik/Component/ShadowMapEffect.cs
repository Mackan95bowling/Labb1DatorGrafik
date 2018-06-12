using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3DatorGrafik.Component
{
    public class ShadowMapEffect : IComponent
    {
       public AmbientComponent AmbientComponent { get; set; }
        public CameraComponent camera { get; set; }
        public Effect effect { get; set; }
        public FogComponent fog { get; set; }
        public LightComponent light { get; set; }
        public RenderTarget2D ShadowRenderTarget { get; set; }
        public Matrix world { get; set; }
        public bool ShadowMap { get; set; }
        public string TechniqueName { get; set; }
        public void AddEffect(Effect effects, Texture2D texture) {

            effect.Parameters["Texture"].SetValue(effects.Parameters["Texture"].GetValueTexture2D());
            if (effect.Parameters["Texture"] == null || effect.Parameters["Texture"].GetValueTexture2D() == null)
            {
                effect.Parameters["Texture"].SetValue(texture);
            }
        }

        public void Apply()
        {
            effect.CurrentTechnique = effect.Techniques[TechniqueName];
            effect.Parameters["World"].SetValue(world);
            effect.Parameters["View"].SetValue(camera.view);
            effect.Parameters["Projection"].SetValue(camera.projection);
            effect.Parameters["LightDirection"].SetValue(light.LightDir);
            effect.Parameters["LightViewProj"].SetValue(light.LightProjection);
            effect.Parameters["ShadowStrenght"].SetValue(10f);
            effect.Parameters["DepthBias"].SetValue(0.001f);

            if (!ShadowMap)
            {
                effect.Parameters["ShadowMap"].SetValue(ShadowRenderTarget);
            }

            effect.Parameters["AmbientColor"].SetValue(AmbientComponent.AmbientColor);
            effect.Parameters["AmbientIntensity"].SetValue(AmbientComponent.Intensity);

            effect.Parameters["ViewVector"].SetValue(Vector3.One);
            effect.Parameters["DiffuseLightDirection"].SetValue(light.DiffLightDir); // todo
            effect.Parameters["DiffuseColor"].SetValue(light.DiffLightColor);
            effect.Parameters["DiffuseIntensity"].SetValue(light.DiffIntensity);



            effect.Parameters["CameraPosition"].SetValue(camera.cameraPosition);


            effect.Parameters["FogStart"].SetValue(fog.FogStart);
            effect.Parameters["FogEnd"].SetValue(fog.FogEnd);
            effect.Parameters["FogColor"].SetValue(fog.Color);
            effect.Parameters["FogEnabled"].SetValue(fog.Enabled);



            effect.Parameters["Shininess"].SetValue(0.9f);
            effect.Parameters["SpecularColor"].SetValue(Color.MediumVioletRed.ToVector4());
            effect.Parameters["SpecularIntensity"].SetValue(0.1f);
            foreach (var effectPass in effect.CurrentTechnique.Passes)
            {
                effectPass.Apply();
            }
            //effect.CurrentTechnique.Passes[0].Apply();
        }
    }
}
