using UnityEngine;
using UnityEditor;

public class PathCopy
{
    //      ���˽ű�����Editor Default Resources�ļ�����


    static string path;

    [MenuItem("GameObject/CopyPath",false,0)]
    static void CopyPath()
    {
        var current = Selection.activeTransform;
        path = current.name;
        GetPath(current);
        GUIUtility.systemCopyBuffer = path;
        Debug.Log("Copied");
    }
    

    static void GetPath(Transform obj)
    {
        if(obj.parent != null)
        {
            path = obj.parent.name + "/" + obj.name;
            GetPath(obj.parent);
        }
    }
}
