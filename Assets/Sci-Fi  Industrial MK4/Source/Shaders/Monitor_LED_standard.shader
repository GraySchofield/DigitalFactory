// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X
Shader "MK4/MonitorLED_LWRP"
{
	Properties
	{
		_Emission("Emission", 2D) = "gray" {}
		_Composite("Composite", 2D) = "white" {}
		[HDR]_Color1("Color1", Color) = (0.3726415,0.954257,1,0)
		[HDR]_Color2("Color2", Color) = (0.09456214,0.1228874,0.2358491,0)
		_Albedo("Albedo", Color) = (0,0,0,0)
		_PixelTileX("Pixel Tile X", Range( 0.1 , 12)) = 1
		_PixelTileY("Pixel Tile Y", Range( 0.1 , 12)) = 1
		_PixelIntensity("Pixel Intensity", Range( 0 , 1)) = 0
		_Warp("Warp", Range( 0 , 1)) = 0.2
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Albedo;
		uniform float4 _Color2;
		uniform float4 _Color1;
		uniform sampler2D _Composite;
		uniform float _PixelTileX;
		uniform float _PixelTileY;
		uniform float _PixelIntensity;
		uniform sampler2D _Emission;
		uniform float _Warp;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = _Albedo.rgb;
			float2 panner51 = ( 1.0 * _Time.y * float2( 0,0.5 ) + i.uv_texcoord);
			float2 appendResult16 = (float2(( i.uv_texcoord.x * _PixelTileX ) , ( i.uv_texcoord.y * _PixelTileY )));
			float2 panner27 = ( 1.0 * _Time.y * float2( 2.1,-3.6 ) + i.uv_texcoord);
			float2 panner21 = ( 1.0 * _Time.y * float2( 2,1.7 ) + i.uv_texcoord);
			float2 panner23 = ( 1.0 * _Time.y * float2( -3.1,1.3 ) + i.uv_texcoord);
			float2 panner25 = ( 1.0 * _Time.y * float2( -3.1,-1.9 ) + i.uv_texcoord);
			float4 lerpResult2 = lerp( _Color2 , _Color1 , ( tex2D( _Composite, panner51 ).a + ( ( tex2D( _Composite, appendResult16 ).r * _PixelIntensity ) + tex2D( _Emission, ( ( ( tex2D( _Composite, panner27 ).b * ( ( tex2D( _Composite, panner21 ).g + tex2D( _Composite, panner23 ).g ) + ( 1.0 - tex2D( _Composite, panner25 ).b ) ) ) * _Warp * (0.0 + (_Warp - 0.0) * (0.3 - 0.0) / (1.0 - 0.0)) ) + i.uv_texcoord ) ) ) ));
			o.Emission = lerpResult2.rgb;
			o.Metallic = 0.0;
			o.Smoothness = 0.0;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16301
193;321;1208;497;1271.865;605.8057;1.760439;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;13;-3142.457,388.1973;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;25;-2881.231,124.4528;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-3.1,-1.9;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;23;-2873.993,-97.86044;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-3.1,1.3;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;6;-3306.266,-152.9703;Float;True;Property;_Composite;Composite;1;0;Create;True;0;0;False;0;None;0de3291707bd460448e9b6c6aa0dd15b;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;21;-2866.223,-271.272;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;2,1.7;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;20;-2665.544,-326.542;Float;True;Property;_TextureSample1;Texture Sample 1;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;22;-2667.347,-108.5812;Float;True;Property;_TextureSample2;Texture Sample 2;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;24;-2678.617,102.5352;Float;True;Property;_TextureSample3;Texture Sample 3;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;27;-2870.89,320.9155;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;2.1,-3.6;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;31;-2288.201,119.8037;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;28;-2321.394,-107.52;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;26;-2668.277,298.9979;Float;True;Property;_TextureSample4;Texture Sample 4;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;30;-2050.068,-15.71993;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-2224.536,843.9542;Float;False;Property;_PixelTileY;Pixel Tile Y;6;0;Create;True;0;0;False;0;1;8;0.1;12;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-2228.607,746.4704;Float;False;Property;_PixelTileX;Pixel Tile X;5;0;Create;True;0;0;False;0;1;4;0.1;12;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-1908.528,417.0692;Float;False;Property;_Warp;Warp;8;0;Create;True;0;0;False;0;0.2;0.15;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-1939.899,726.2467;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;37;-1584.421,311.9177;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;0.3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-1942.039,837.5337;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-1847.607,180.3863;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;16;-1769.758,746.5777;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-1380.918,209.0347;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-1403.981,940.7407;Float;False;Property;_PixelIntensity;Pixel Intensity;7;0;Create;True;0;0;False;0;0;0.15;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;38;-1463.583,-62.35028;Float;True;Property;_Emission;Emission;0;0;Create;True;0;0;False;0;None;None;False;gray;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;5;-1568.742,602.2837;Float;True;Property;_TextureSample0;Texture Sample 0;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;34;-1247.606,392.3935;Float;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-1016.327,376.0264;Float;True;Property;_Texture1;Texture1;0;0;Create;True;0;0;False;0;4ad751f70c7edd641920591facf638e2;4ad751f70c7edd641920591facf638e2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-930.8002,569.5924;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;51;-2896.524,-483.0774;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.5;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-690.8081,347.2625;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;50;-2662.182,-550.575;Float;True;Property;_TextureSample5;Texture Sample 5;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;3;-552.3028,-471.3845;Float;False;Property;_Color1;Color1;2;1;[HDR];Create;True;0;0;False;0;0.3726415,0.954257,1,0;0.3915094,0.6632353,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;52;-423.0665,264.2179;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;4;-549.3028,-275.6241;Float;False;Property;_Color2;Color2;3;1;[HDR];Create;True;0;0;False;0;0.09456214,0.1228874,0.2358491,0;0,0.01284065,0.06603771,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;55;205.1432,538.4794;Float;False;Constant;_Smoothness;Smoothness;9;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;54;229.7893,392.3629;Float;False;Constant;_Metallic;Metallic;9;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;2;-141.2657,272.7394;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;53;105.9442,-14.69637;Float;False;Property;_Albedo;Albedo;4;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;601.0539,291.2285;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;MK4/MonitorLED;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;25;0;13;0
WireConnection;23;0;13;0
WireConnection;21;0;13;0
WireConnection;20;0;6;0
WireConnection;20;1;21;0
WireConnection;22;0;6;0
WireConnection;22;1;23;0
WireConnection;24;0;6;0
WireConnection;24;1;25;0
WireConnection;27;0;13;0
WireConnection;31;0;24;3
WireConnection;28;0;20;2
WireConnection;28;1;22;2
WireConnection;26;0;6;0
WireConnection;26;1;27;0
WireConnection;30;0;28;0
WireConnection;30;1;31;0
WireConnection;14;0;13;1
WireConnection;14;1;7;0
WireConnection;37;0;36;0
WireConnection;15;0;13;2
WireConnection;15;1;12;0
WireConnection;29;0;26;3
WireConnection;29;1;30;0
WireConnection;16;0;14;0
WireConnection;16;1;15;0
WireConnection;35;0;29;0
WireConnection;35;1;36;0
WireConnection;35;2;37;0
WireConnection;5;0;6;0
WireConnection;5;1;16;0
WireConnection;34;0;35;0
WireConnection;34;1;13;0
WireConnection;1;0;38;0
WireConnection;1;1;34;0
WireConnection;18;0;5;1
WireConnection;18;1;19;0
WireConnection;51;0;13;0
WireConnection;17;0;18;0
WireConnection;17;1;1;0
WireConnection;50;0;6;0
WireConnection;50;1;51;0
WireConnection;52;0;50;4
WireConnection;52;1;17;0
WireConnection;2;0;4;0
WireConnection;2;1;3;0
WireConnection;2;2;52;0
WireConnection;0;0;53;0
WireConnection;0;2;2;0
WireConnection;0;3;54;0
WireConnection;0;4;55;0
ASEEND*/
//CHKSM=F67F1CAC0CC2953505730039A2CD66C69FF10256
