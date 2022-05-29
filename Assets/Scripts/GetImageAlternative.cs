using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using ZXing;

public class GetImageAlternative : MonoBehaviour
{

    [SerializeField]
    private ARCameraBackground aRCameraBackground;

    [SerializeField]
    private RenderTexture targetRennderTexture;

    [SerializeField]
    private TextMeshProUGUI qrCodeText;

    private Texture2D cameraImageTexture;
    private IBarcodeReader reader = new BarcodeReader();

    // Update is called once per frame
    void Update()
    {
        Graphics.Blit(null, targetRennderTexture, aRCameraBackground.material);
        cameraImageTexture = new Texture2D(targetRennderTexture.width,targetRennderTexture.height, TextureFormat.RGBA32,false);
        Graphics.CopyTexture(targetRennderTexture, cameraImageTexture);

        // Detect and decode the barcode inside the bitmap
        var result = reader.Decode(cameraImageTexture.GetPixels32(), cameraImageTexture.width, cameraImageTexture.height);

        // Do something with the result
        if (result != null)
        {
            qrCodeText.text = result.Text;
        }
    }
}
