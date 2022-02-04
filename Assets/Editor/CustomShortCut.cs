
#if UNITY_EDITOR_OSX

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomShortCut : Editor {

	[MenuItem("customShortCut/ActiveSelectionsObjs &%a")]
	static void ActiveSelectionsObjs() { 
		 
        //transforms是Selection类的静态字段，其返回的是选中的对象的Transform
        Transform[] transforms = Selection.transforms;

        
        //将字典中的信息打印出来
        foreach (Transform item in transforms)
        {
			if(item.gameObject) { 
			item.gameObject.SetActive(!item.gameObject.activeSelf);
			}
        }
    }
   
}


#endif
