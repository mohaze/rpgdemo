using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public static class AssetBundleUtil
    {
        public static List<FileInfo> GetAllRootFilePath()
        {
            List<string> pathList = new List<string>();
            //pathList.Add("G:/mainrpg/Assets/Topdown Kit/Model/Model Enemy/Wolf");
            pathList.Add("G:/mainrpg/Assets/Topdown Kit/Prefab/Enemy/Sample Monster");
            
            var dir = new DirectoryInfo(pathList[0]);
            FileInfo[] files = dir.GetFiles("*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                Debug.Log("file path ===" + file.Name  + file.Directory);
                //var list =  GetDependencies(file.Directory + "\\" +  file.Name);
            }
            return files.ToList();
        }
        
        //获取资源的依赖资源
        public static List<string> GetDependencies(string filePath)
        {
            //filePath = "Assets/Topdown Kit/Prefab/Enemy/Sample Monster/BabyWolf.prefab";
            //filePath = "G:/mainrpg/Assets/Topdown Kit/Prefab/Enemy/Sample Monster/BabyWolf.prefab";
            List<ResNode> list = new List<ResNode>();
            //资源本身
            list.Add(new ResNode(){});
            //此处要相对路径
            string[] dependency = AssetDatabase.GetDependencies(filePath);
            return dependency.ToList();
        }
    }
}