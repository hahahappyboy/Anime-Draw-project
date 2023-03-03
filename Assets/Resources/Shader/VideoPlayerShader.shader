Shader "Unlit/VideoPlayerShader"
{
    Properties
    {
        _MainTex ("主纹理", 2D) = "white" {}
        _MaskTex_Hor ("遮罩纹理水平", 2D) = "white" {}
        _TransRatio("透明倍率",float) = 1.5
    }
    SubShader
    {   
        
        Tags {"Queue"="Transparent"  "RenderType"="Transparent" }
        
        Pass
        {
            //半透明
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _MaskTex_Hor;
            float4 _MaskTex_Hor_ST;

            float _TransRatio;
            
            struct c2v
            {
                float4 vertex : POSITION;
                float4 texcoord_main : TEXCOORD0;
                float4 texcoord_mask : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv_main : TEXCOORD3;
                float2 uv_mask : TEXCOORD4;
                float4 vertex : SV_POSITION;
            };

          

            v2f vert (c2v v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv_main.xy = v.texcoord_main.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                o.uv_mask.xy = v.texcoord_mask.xy * _MaskTex_Hor_ST.xy + _MaskTex_Hor_ST.zw;
                return o;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col_main = tex2D(_MainTex, i.uv_main);
                fixed4 col_mask = tex2D(_MaskTex_Hor, i.uv_mask);
                col_main.a =  col_mask.a*_TransRatio;
                return col_main.rgba;
            }
            ENDCG
        }
    }
}