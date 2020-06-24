using UnityEngine;

public class ColourGenerator
{
    ColourSettings settings;
    Texture2D texture;
    const int textureResolution = 50;
    INoiseFilter biomeNoiseFilter;

    public void UpdateSettings(ColourSettings settings)
    {
        this.settings = settings;

        if (texture == null || texture.height != settings.biomeColourSettings.biomes.Length)
        {
            texture = new Texture2D(textureResolution * 2, settings.biomeColourSettings.biomes.Length, TextureFormat.RGBA32, false);
        }

        biomeNoiseFilter = NoiseFilterFactory.CreateNoiseFilter(settings.biomeColourSettings.noise);
    }

    public void UpdateElevation(MinMax elevationMinMax)
    {
        settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    public float BiomePercentFromPoint(Vector3 pointOnUnitSphere)
    {
        var heightPercent = (pointOnUnitSphere.y + 1) / 2f;
        heightPercent += (biomeNoiseFilter.Evaluate(pointOnUnitSphere) - settings.biomeColourSettings.noiseOffset) * settings.biomeColourSettings.noiseStrength;

        var biomeIndex = 0f;
        var numBiomes = settings.biomeColourSettings.biomes.Length;
        var blendRange = settings.biomeColourSettings.blendAmount / 2f + 0.001f;

        for (var i = 0; i < numBiomes; i++)
        {
            var dst = heightPercent - settings.biomeColourSettings.biomes[i].startHeight;
            var weight = Mathf.InverseLerp(-blendRange, blendRange, dst);

            biomeIndex *= (1 - weight);
            biomeIndex += i * weight;
        }

        return biomeIndex / Mathf.Max(1, numBiomes - 1);
    }

    public void UpdateColours()
    {
        var colours = new Color[texture.width * texture.height];

        var colourIndex = 0;
        foreach (var biome in settings.biomeColourSettings.biomes)
        {
            for (var i = 0; i < textureResolution * 2; i++)
            {
                Color gradientColour;
                if (i < textureResolution)
                {
                    gradientColour = settings.oceanColour.Evaluate(i / (textureResolution - 1f));
                } else
                {
                    gradientColour = biome.gradient.Evaluate((i-textureResolution) / (textureResolution - 1f));
                }

                var tintColor = biome.tint;

                colours[colourIndex] = gradientColour * (1 - biome.tintPercent) + tintColor * biome.tintPercent;

                colourIndex++;
            }
        }

        texture.SetPixels(colours);
        texture.Apply();
        settings.planetMaterial.SetTexture("_texture", texture);
    }
}
