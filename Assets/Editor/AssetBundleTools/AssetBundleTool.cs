using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public static class AssetBundleTool
    {
        
        private const string AssetBundlesOutputPath = "Assets/StreamingAssets";
        public static void Start()
        {
            if (!Directory.Exists(AssetBundlesOutputPath))
            {
                Directory.CreateDirectory(AssetBundlesOutputPath);
            }
            //获取所有要打包的文件名（全路径）
            List<FileInfo> fileList =  AssetBundleUtil.GetAllRootFilePath();
            //组织存储了依赖关系的dic<资源名字-resnode>
            var resNodeDic = GenNodes(fileList);

            //组织dic<包名-包含的资源>
			
            //根据BuildSetting里面所激活的平台进行打包  
            BuildPipeline.BuildAssetBundles(AssetBundlesOutputPath, 0, EditorUserBuildSettings.activeBuildTarget);
        }

        public static Dictionary<string, ResNode> GenNodes(List<FileInfo> fileList)
        {
            Dictionary<string, ResNode> resNodeDic = new Dictionary<string, ResNode>();
            //List<ResNode> list = new List<ResNode>();
            foreach (var fileInfo in fileList)
            {
                string rootName = "Assets/Topdown Kit/Prefab/Enemy/Sample Monster/BabyWolf.prefab";//方便调试先用全路径当文件名，后面改成md5
                var resRoot = new ResNode(){fullName = rootName};
                //根节点加入dic(根节点不检查重复)
                resNodeDic[rootName] = resRoot;
                GenChildrenNodes(resRoot, resNodeDic);


            }
            return resNodeDic;
        }

        //深度遍历依赖的资源
        public static void GenChildrenNodes(ResNode rootNode, Dictionary<string, ResNode> resNodeDic)
        {
            List<string> depList = AssetBundleUtil.GetDependencies(rootNode.fullName); //GetDependencies(file.Directory + "\\" +  file.Name);
            foreach (var depPath in depList)
            {
                //如果是本身
                if (depPath == rootNode.fullName)
                    continue;
                string depName = depPath;
                rootNode.children.Add(depName);
                if (!resNodeDic.TryGetValue(depName, out ResNode node))
                {
                    node = new ResNode(){fullName = depName};
                    resNodeDic[depName] = node;
                }
                node.parents.Add(rootNode.fullName);
                GenChildrenNodes(node, resNodeDic);
            }
        }
        
    }
}