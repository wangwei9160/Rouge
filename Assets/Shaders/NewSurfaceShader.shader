Shader "Custom/GlowShader" 
{
    Properties 
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GlowColor ("Glow Color", Color) = (1, 0, 0, 1) // �ⷢ�����ɫ
        _GlowPower ("Glow Power", Range(0, 5)) = 1 // �ⷢ���ǿ��
    }
    SubShader 
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
 
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
            fixed4 _GlowColor;
            float _GlowPower;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                // ������ɫ
                fixed4 col = tex2D(_MainTex, i.uv);
                // ���ӱ�Ե����
                fixed4 glow = _GlowColor * _GlowPower;
                return col + glow;
            }
            ENDCG
        }
    }
}