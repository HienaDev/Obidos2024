using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureCapture : MonoBehaviour
{

    [SerializeField] private RenderTexture captureTexture;

    public void ExportPhoto()
    {
        byte[] bytes = toTexture2D(captureTexture).EncodeToPNG();
        var dirPath = Application.persistentDataPath + "/ExportPhoto";

        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        System.IO.File.WriteAllBytes(dirPath + "/Photo" + Random.Range(0, 1000000) + ".png", bytes);
        Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + dirPath);
    }

    private Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(1600, 900, TextureFormat.RGB24, false, true);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
