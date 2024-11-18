Shader "Custom/ColorAdjustmentShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RedShift ("Red Channel Shift", Float) = 1.0
        _GreenShift ("Green Channel Shift", Float) = 1.0
        _BlueShift ("Blue Channel Shift", Float) = 1.0
    }
    SubShader
    {
        Tags {"Queue"="Transparent"}
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _RedShift, _GreenShift, _BlueShift;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col.r *= _RedShift;
                col.g *= _GreenShift;
                col.b *= _BlueShift;
                return col;
            }
            ENDCG
        }
    }
}