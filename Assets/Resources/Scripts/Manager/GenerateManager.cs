using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GenerateManager : BaseMonoBehaviour {
    private Image image;
    //延迟发送请求的时间
    private float DELAY_POST_TIME = 4f;
    protected override void FetchComponent() {
        image = this.GetComponent<Image>();
    }

    protected override void GetResources() {
        //疫苗后开始发请求
        Invoke("Send", DELAY_POST_TIME);
        
    }

    protected override void InitZoneLayout() {
        
    }

    public void Send() {
        StartCoroutine(SendWebRequest());
    }
    
    /// <summary>
    /// 发送请求
    /// </summary>
    /// <returns></returns>
    IEnumerator  SendWebRequest() {
        while (true) {
            // Debug.Log("发送请求" );
            WWWForm form = new WWWForm();
            //mask图像
            Texture2D texture = MaskDrawManager.instance.GetMaskTexture();
            //当前选中的角色
            int label_class = CharacterChooseManager.instance.GetCurrentChooseCharacterID();
            //当前选择的模型
            string model_id = SceneDataManager.GetInstance().GetModelID();
            
            form.AddBinaryData("label_img",texture.EncodeToPNG());
            form.AddField("model_id", model_id);
            form.AddField("label_class", label_class);
            UnityWebRequest www = UnityWebRequest.Post(Config.POST_HTTP_URL, form);
            yield return www.SendWebRequest();
            if (www.isDone) {
                if (www.result == UnityWebRequest.Result.ConnectionError ||
                    www.result == UnityWebRequest.Result.ProtocolError) {
                    Debug.LogWarning(www.error);
                } else {
                    string responseText = www.downloadHandler.text;
                    //获取图像
                    www = UnityWebRequestTexture.GetTexture(Config.DOWNLOAD_HTTP_URL+responseText);
                    yield return www.SendWebRequest();
                    if (www.isDone) {
                        if (www.result == UnityWebRequest.Result.ConnectionError ||
                            www.result == UnityWebRequest.Result.ProtocolError) {
                            Debug.LogWarning(www.error);
                        } else {
                            texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                            Sprite sprite = Sprite.Create(texture, new Rect( 0 ,0, texture.width, texture.height), Vector2.zero);
                            image.sprite = sprite;
                        }
                    }
                }
            }
        }
    }

   
}
