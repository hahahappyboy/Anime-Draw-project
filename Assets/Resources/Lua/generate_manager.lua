
generate_manager = {}

local util = require 'xlua.util'
-- 生命周期awke
function generate_manager:awake()
    print("lua:awake")
    generate_manager.image = obj.transform:GetComponent("Image")
    generate_manager.CSharpGenerateManager = obj:GetComponent("GenerateManager")
end
--生命周期start
function generate_manager:start()
    print("luar:start")
end

--开启协程
function generate_manager:send()
    generate_manager.CSharpGenerateManager:StartCoroutine(generate_manager.send_web_request)
end

--协程
generate_manager.send_web_request = util.cs_generator(function()
    print("luar:StartCoroutine ")
    while true do
        local form = CS.UnityEngine.WWWForm()
        -- print(CS.CharacterChooseManager.instance)
        -- mask图像 
        local texture = CS.MaskDrawManager.instance:GetMaskTexture()
        --当前选中的角色
        local label_class = CS.CharacterChooseManager.instance:GetCurrentChooseCharacterID();
        -- 当前选择的模型
        local model_id = CS.SceneDataManager.GetInstance():GetModelID()
        -- mask图片
        -- local label_img = texture.EncodeToPNG()
        -- print(CS.UnityEngine.ImageConversion)
        local label_img = CS.UnityEngine.ImageConversion.EncodeToPNG(texture)
        -- 参数
        form:AddBinaryData("label_img",label_img)
        form:AddField("model_id", model_id)
        form:AddField("label_class", label_class)

        local www =  CS.UnityEngine.Networking.UnityWebRequest.Post(CS.Config.POST_HTTP_URL, form);
        --print(CS.Config.POST_HTTP_URL)
        -- 发送请求
        coroutine.yield(www:SendWebRequest())
        if www.isDone then
            if www.result == CS.UnityEngine.Networking.UnityWebRequest.Result.ConnectionError and  www.result == CS.UnityEngine.Networking.UnityWebRequest.Result.ProtocolError then
                CS.UnityEngine.Debug.LogWarning(www.error)
            else
                -- 得到回复
                local responseText = www.downloadHandler.text
                -- 再发送请求得到图片
                www = CS.UnityEngine.Networking.UnityWebRequestTexture.GetTexture(CS.Config.DOWNLOAD_HTTP_URL..responseText)
                coroutine.yield(www:SendWebRequest())
                if www.isDone then
                    if www.result == CS.UnityEngine.Networking.UnityWebRequest.Result.ConnectionError and  www.result == CS.UnityEngine.Networking.UnityWebRequest.Result.ProtocolError then
                        CS.UnityEngine.Debug.LogWarning(www.error)
                    else
                        texture = www.downloadHandler.texture
                        local rect = CS.UnityEngine.Rect(0,0,texture.width,texture.height)
                        local sprite = CS.UnityEngine.Sprite.Create(texture,rect,CS.UnityEngine.Vector2.zero)
                        generate_manager.image.sprite = sprite
                    end
                end
               

            end
            
        end
        
    end
end)

