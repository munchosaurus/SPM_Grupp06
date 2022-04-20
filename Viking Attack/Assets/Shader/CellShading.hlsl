void CellShading_float(in float3 Normal, in float CellRampSmoothness, in float3 ClipSpacePos, in float3 WorldPos, in float4 CellRampTinting,
in float CellRampOffset, out float3 CellRampOutput, out float3 Direction)
{

	// set the shader graph node previews
	#ifdef SHADERGRAPH_PREVIEW
		CellRampOutput = float3(0.5,0.5,0);
		Direction = float3(0.5,0.5,0);
	#else

		// grab the shadow coordinates
		#if SHADOWS_SCREEN
			half4 shadowCoord = ComputeScreenPos(ClipSpacePos);
		#else
			half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
		#endif 

		// grab the main light
		#if _MAIN_LIGHT_SHADOWS_CASCADE || _MAIN_LIGHT_SHADOWS
			Light light = GetMainLight(shadowCoord);
		#else
			Light light = GetMainLight();
		#endif

		// dot product for cellramp
		half d = dot(Normal, light.direction) * 0.5 + 0.5;
		
		// cellramp in a smoothstep
		half cellRamp = smoothstep(CellRampOffset, CellRampOffset+ CellRampSmoothness, d );
		// multiply with shadows;
		cellRamp *= light.shadowAttenuation;
		// add in lights and extra tinting
		CellRampOutput = light.color * (cellRamp + CellRampTinting) ;
		// output direction for rimlight
		Direction = light.direction;
	#endif

}