using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using DefaultNamespace;


public  class EditorManager : Editor {

		private EditorManager() { }
		private static EditorManager _EditorManager = null;



		public static EditorManager Ins()
		{
			if (_EditorManager == null)
			{
				
			}
			return _EditorManager;
		}
		
		[MenuItem("Tools/打ab包")]
		static public void OfflineData()
		{
			//BuildPipeline.BuildAssetBundles
			AssetBundleTool.Start();
			Debug.Log("scuessed!");
			
			

		}
		

}
