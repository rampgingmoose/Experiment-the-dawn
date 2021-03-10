using UnityEditor;
using UnityEngine;

namespace SterlexDev
{
    public class DissolveShaderEditor : ShaderGUI
    {
        MaterialEditor _materialEditor;
        MaterialProperty[] _properties;


		private MaterialProperty _Fill = null;
		private MaterialProperty _Invert = null;
		private MaterialProperty _Worldcoordinates = null;
		private MaterialProperty _Fillcolor = null;
		private MaterialProperty _Borderwidth = null;
		private MaterialProperty _Bordercolor = null;
		private MaterialProperty _Bordernoisescale = null;
		private MaterialProperty _Bordernoisespeed = null;
		private MaterialProperty _Wave1_amplitude = null;
		private MaterialProperty _Wave1_frequency = null;
		private MaterialProperty _Wave2_amplitude = null;
		private MaterialProperty _Wave2_frequency = null;
		private MaterialProperty _Maintiling = null;
		private MaterialProperty _Albedo = null;
		private MaterialProperty _Normal = null;
		private MaterialProperty _Specular = null;
		private MaterialProperty _Smoothness = null;
		private MaterialProperty _Occlusion = null;
		private MaterialProperty _Bordertexture = null;

		public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
		{
			_properties = properties;
			_materialEditor = materialEditor;
			EditorGUI.BeginChangeCheck();
			DrawGUI();
		}

		void GetProperties()
        {
			_Fill = FindProperty("_Fill", _properties);
			_Invert = FindProperty("_Invertmask", _properties);
			_Worldcoordinates = FindProperty("_Worldcoordinates", _properties);
			_Fillcolor = FindProperty("_Fillcolor", _properties);
			_Borderwidth = FindProperty("_Borderwidth", _properties);
			_Bordercolor = FindProperty("_Bordercolor", _properties);
			_Bordernoisescale = FindProperty("_Noisescale", _properties);
			_Bordernoisespeed = FindProperty("_Noisespeed", _properties);
			_Wave1_amplitude = FindProperty("_Wave1amplitude", _properties);
			_Wave1_frequency = FindProperty("_Wave1frequency", _properties);
			_Wave2_amplitude = FindProperty("_Wave2amplitude", _properties);
			_Wave2_frequency = FindProperty("_Wave2Frequency", _properties);
			_Bordertexture = FindProperty("_Bordertexture", _properties);
			_Maintiling = FindProperty("_Maintiling", _properties);
			_Albedo = FindProperty("_Albedo", _properties);
			_Normal = FindProperty("_Normal", _properties);
			_Specular = FindProperty("_Specular", _properties);
			_Smoothness = FindProperty("_Smoothness", _properties);
			_Occlusion = FindProperty("_Occlusion", _properties);
		}

		static Texture2D Imagebanner= null;
		static GUIStyle Text = null;
		void ImageBanner()
		{
			    Imagebanner = Resources.Load<Texture2D>("Imagebanner");
				GUILayout.Space(4);
				var rect = GUILayoutUtility.GetRect(0, int.MaxValue, 80, 80);
			    EditorGUI.DrawPreviewTexture(rect, Imagebanner, null, ScaleMode.ScaleAndCrop);
				GUILayout.Space(4);
                Text = new GUIStyle
                {fontSize = 15};
                Text.normal.textColor = new Color(1f, 1f, 1f); 
		}

		void DrawGUI()
		{
			GetProperties();
			ImageBanner();
		    MainSettings();
			WaveSettings();
			TextureSettings();
		}

		void MainSettings()
		{
			var rect = GUILayoutUtility.GetRect(0, int.MaxValue, 20, 20);
			EditorGUI.LabelField(rect, "Main setting", Text);

			_materialEditor.SetDefaultGUIWidths();
			_materialEditor.ShaderProperty(_Fill, "Fill Quantity ");
			_materialEditor.ShaderProperty(_Fillcolor, "Fill Color");
			_materialEditor.ShaderProperty(_Bordercolor, "Border Color");
			_materialEditor.ShaderProperty(_Borderwidth, "Border Width");
			_materialEditor.TexturePropertySingleLine(new GUIContent("Border Texture"), _Bordertexture);
			_materialEditor.ShaderProperty(_Bordernoisescale, "Noise scale");
			_materialEditor.ShaderProperty(_Bordernoisespeed, "Noise speed");
			_materialEditor.ShaderProperty(_Invert, "Invert Dissolve");
			_materialEditor.ShaderProperty(_Worldcoordinates, "World Coordinates");


		}

		void WaveSettings()
		{
			var rect = GUILayoutUtility.GetRect(0, int.MaxValue, 30, 20);
			EditorGUI.LabelField(rect, "Wave Creator setting", Text);

			_materialEditor.SetDefaultGUIWidths();
			_materialEditor.ShaderProperty(_Wave1_amplitude, "First Axis Amplitude");
			_materialEditor.ShaderProperty(_Wave1_frequency, "First Axis Frequency");
			_materialEditor.ShaderProperty(_Wave2_amplitude, "Second Axis Amplitude");
			_materialEditor.ShaderProperty(_Wave2_frequency, "Second Axis Frequency");
			

		}
		void TextureSettings()
		{
			GUILayout.Space(4);
			var rect = GUILayoutUtility.GetRect(0, int.MaxValue, 30, 40);
			EditorGUI.LabelField(rect, "Texture setting", Text);
			
			_materialEditor.SetDefaultGUIWidths();
			
			_materialEditor.TexturePropertySingleLine(new GUIContent("Albedo"), _Albedo);
			_materialEditor.TexturePropertySingleLine(new GUIContent("Normal"), _Normal);
			_materialEditor.TexturePropertySingleLine(new GUIContent("Specular"), _Specular);
			_materialEditor.TexturePropertySingleLine(new GUIContent("Occlusion"), _Occlusion);
			_materialEditor.ShaderProperty(_Maintiling, "Main Texture Tiling");
			_materialEditor.ShaderProperty(_Smoothness, "Smoothness");

		}

    }


}
