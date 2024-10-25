Shader "Custom/PlayerPulseShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _BaseIntensity ("Base Intensity", Float) = 2.0
        _PulseSpeed ("Pulse Speed", Float) = 2.0
        _PulseAmount ("Pulse Amount", Float) = 0.01
        _PulseIntensity ("Pulse Intensity", Float) = 0.3
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
            float _PulseIntensity;
            float _BaseIntensity;

            // 顶点着色器：扩展顶点
            v2f vert (appdata v)
            {
                v2f o;
                float pulse = (sin(_Time.y * _PulseSpeed) * _PulseAmount) + _PulseAmount;  // 使用Unity的时间变量 _Time
                o.pos = UnityObjectToClipPos(v.vertex + v.normal * pulse);
                o.norm = v.normal;
                return o;
            }

            sampler2D _MainTex;
            float4 _EmissionColor;

            // 片段着色器：改变颜色发光
            fixed4 frag (v2f i) : SV_Target
            {
                // maintain x1.5 of base color to have a constant bloom effect
                // add a color that pulses to it based on _PulseIntensity
                return fixed4((_EmissionColor.rgb*_BaseIntensity) + (_EmissionColor.rgb * (_PulseIntensity * sin(_Time.y * _PulseSpeed))), 1.0);  // 动态改变发光
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
