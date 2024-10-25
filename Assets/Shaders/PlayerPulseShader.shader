Shader "Custom/PlayerPulseShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _PulseSpeed ("Pulse Speed", Float) = 2.0
        _PulseAmount ("Pulse Amount", Float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 norm : TEXCOORD0;
            };

            float _PulseSpeed;
            float _PulseAmount;

            // 顶点着色器：扩展顶点
            v2f vert (appdata v)
            {
                v2f o;
                float pulse = sin(_Time.y * _PulseSpeed) * _PulseAmount;  // 使用Unity的时间变量 _Time
                o.pos = UnityObjectToClipPos(v.vertex + v.normal * pulse);
                o.norm = v.normal;
                return o;
            }

            sampler2D _MainTex;
            float4 _EmissionColor;

            // 片段着色器：改变颜色发光
            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(_EmissionColor.rgb * (0.5 + 0.5 * sin(_Time.y * _PulseSpeed)), 1.0);  // 动态改变发光
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
