Shader "Unlit/MultiplicationShader"
{
    Properties
    {
        _Points ("Number of Points", Range(0, 256)) = 3
        _Product ("Product", Range(1, 256)) = 2
    }
    SubShader
    {
        Tags 
        { 
            "Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
        }

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma target 3.0
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

            uniform int _Points;
            int _Product;
            float _MyTime;

            float DrawLine(float2 p1, float2 p2, float2 uv, float a)
            {
                float r = 0;
                float one_px = 0.001;
                
                // get dist between points
                float d = distance(p1, p2);
                
                // get dist between current pixel and p1
                float duv = distance(p1, uv);

                //if point is on line, according to dist, it should match current uv 
                r = 1 - floor(1 - (a*one_px) + distance(lerp(p1, p2, clamp(duv/d, 0, 1)), uv));
                    
                return r;
            }

            float DrawCircle(float2 p, float d, float2 uv)
            {
                return (distance(p, uv) <= d && distance(p, uv) >= d - 0.01) ? 1. : 0.;
            }

            float DivideCircle(int num, float2 uv, uint n)
            {
                float angle = (2*3.14) / num;
                float currentAngle = 0;
                
                float2 points[256];
                float p;
                
                for(int i = 0; i < num; i++) 
                {
                    points[i] = float2(sin(currentAngle) * 0.5, cos(currentAngle) * 0.5);
                    currentAngle += angle;                    
                     
                    p += DrawCircle(points[i], 0.005, uv);
                }

                float l;

                for(int k = 0; k < num; k++) 
                {
                    l += DrawLine(points[k], points[k * n % num], uv, 1);             
                }
                
                return  l;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                int num = lerp(1, 200, _MyTime);
                int num2 = lerp(1, 110, _MyTime);

                float2 center_uv = float2(i.uv.x - 0.5, i.uv.y - 0.5);

                float circle = DrawCircle(float2(0,0), 0.5, center_uv);

                float2 p1 = float2(0.25, -0.25);
                float2 p2 = float2(-0.25, 0.25);

                float linha = DrawLine(p1, p2, center_uv, 1);
                float dots = DrawCircle(p1, 0.005, center_uv) + DrawCircle(p2, 0.005, center_uv);
                
                dots = DivideCircle(num, center_uv, num2);

                float4 color = float4(0, dots, 0, 1);

                return color;
            }
            
            ENDCG
        }
    }
}
