using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SpriteAssetGenerator
{
    private Material material;
    public async Task<TMP_SpriteAsset> CreateFromUrlsAsync(Dictionary<string, string> emojiMap, Material mat)
    {
        List<Sprite> sprites = new();
        List<string> names = new();
        material = mat;

        foreach (var emoj in emojiMap)
        {
            Texture2D texture = await DownloadTextureAsync(emoj.Value);
            Sprite sprite = CreateDefaultSprite();
            if (texture != null)
            {
                sprite = TextureToSprite(texture);
            }

            sprites.Add(sprite);
            names.Add(emoj.Key);

        }

        return GenerateSpriteAsset(sprites, names);
    }

    public static async Task<Texture2D> DownloadTextureAsync(string url)
    {
        Debug.Log(url);
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            var asyncOp = uwr.SendWebRequest();

            while (!asyncOp.isDone)
                await Task.Yield(); // Wait a frame and let Unity continue

            if (uwr.result == UnityWebRequest.Result.Success)
            {
                return DownloadHandlerTexture.GetContent(uwr);
            }
            else
            {
                Debug.Log("Failed to load texture: " + uwr.error);
                return null;
            }
        }
    }

    private Sprite TextureToSprite(Texture2D tex)
    {
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    }

    private TMP_SpriteAsset GenerateSpriteAsset(List<Sprite> sprites, List<string> names)
    {
        Texture2D atlas = new(2048, 2048);
        Rect[] rects = atlas.PackTextures(sprites.ConvertAll(s => s.texture).ToArray(), 4, 2048);

        TMP_SpriteAsset asset = ScriptableObject.CreateInstance<TMP_SpriteAsset>();
        asset.name = "RuntimeEmojiAsset";
        asset.spriteSheet = atlas;
        asset.spriteInfoList = new List<TMP_Sprite>();
        asset.material = material;

        for (int i = 0; i < sprites.Count; i++)
        {
            var r = rects[i];
            TMP_Sprite sprite = new TMP_Sprite
            {
                id = i,
                name = names[i],
                unicode = 0xE000 + i,
                x = r.x * atlas.width,
                y = r.y * atlas.height,
                width = r.width * atlas.width,
                height = r.height * atlas.height,
                xOffset = 0,
                yOffset = 100,
                xAdvance = r.width * atlas.width,
                scale = 1f,
                sprite = sprites[i]
            };

            asset.spriteInfoList.Add(sprite);
        }

        int index = 0;
        foreach (var r in asset.spriteCharacterTable)
        {
            r.glyphIndex = (uint)index;
            index++;
        }

        material.mainTexture = asset.spriteSheet;

        asset.UpdateLookupTables();

       
        Debug.Log(asset.spriteCharacterTable.Count);
        return asset;
    }

    private Sprite CreateDefaultSprite()
    {
        Texture2D texture = new Texture2D(2, 2);
        Color defaultColor = Color.gray;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                texture.SetPixel(x, y, defaultColor);
            }
        }

        texture.Apply();

        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
