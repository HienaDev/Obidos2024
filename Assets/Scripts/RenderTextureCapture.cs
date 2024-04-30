using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class RenderTextureCapture : MonoBehaviour
{

    [SerializeField] private RenderTexture captureTexture;
    public List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private GameObject image;

    public void ExportPhoto(string type)
    {
        byte[] bytes = toTexture2D(captureTexture).EncodeToPNG();
        var dirPath = Application.persistentDataPath + "/ExportPhoto";

        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        System.IO.File.WriteAllBytes(dirPath + $"/{type}_photo" + Random.Range(0, 1000000) + ".png", bytes);
        Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + dirPath);
    }

    private Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(1600, 900);//, TextureFormat.RGB24, false, true);
        RenderTexture.active = rTex;
        Rect rect = new Rect(0, 0, 1600, 900);
        tex.ReadPixels(rect, 0, 0);
        tex.Apply();

        sprites.Add(Sprite.Create(tex, rect, Vector2.zero));
        return tex;
    }

    private void Update()
    {
        image.GetComponent<Image>().sprite = sprites[sprites.Count-1];
    }
}
