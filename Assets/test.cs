using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof (UnityEngine.EventSystems.EventTrigger))]
public class test : MonoBehaviour {
	void test2 () {
		Button btn = this.gameObject.GetComponent<Button> ();
		EventTrigger _trigger = btn.GetComponent<EventTrigger> ();
		EventTrigger.Entry en1 = new EventTrigger.Entry ();
		EventTrigger.Entry en2 = new EventTrigger.Entry ();
		EventTrigger.Entry en3 = new EventTrigger.Entry ();
 
		// 鼠标点击事件
		en1.eventID = EventTriggerType.PointerClick;
		// 鼠标进入事件 
		en2.eventID = EventTriggerType.PointerEnter;
		// 鼠标滑出事件 
		en3.eventID = EventTriggerType.PointerExit;
 
		en1.callback = new EventTrigger.TriggerEvent ();
		en1.callback.AddListener (Func1);
		_trigger.triggers.Add (en1);

		en2.callback = new EventTrigger.TriggerEvent ();
		en2.callback.AddListener (Func2);
		_trigger.triggers.Add (en2);

		en3.callback = new EventTrigger.TriggerEvent ();
		en3.callback.AddListener (Func3);
		_trigger.triggers.Add (en3);
	}

	private void Func1 (BaseEventData pointData) {
		Debug.Log ("鼠标点击了!");
	}
	private void Func2 (BaseEventData pointData) {
		Debug.Log ("鼠标进入了!");
	}
	private void Func3 (BaseEventData pointData) {
		Debug.Log ("鼠标滑出了!");
	}

	void test1 () {

		// AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/ugui");
		// Debug.Log(ab);
		// Sprite sss = ab.LoadAsset<Sprite>("Assets/GameData/UGUI/test3.png");
		// // for (int i = 0; i < ob.Length; i++)
		// // {
		// // 	Debug.Log(ob[i].GetType());
		// // }

		// // loadSprite(ob);
		// Debug.Log();
		// SpriteRenderer sp = image.GetComponent<SpriteRenderer>();
		// sp.sprite = sss;

	}

	// void loadSprite(Object[] ob){
	// 	SpriteRenderer sp = image.GetComponent<SpriteRenderer>();
	// 	SpriteRenderer sp1 = image1.GetComponent<SpriteRenderer>();
	// 	sp.sprite = ob[1] as Sprite;
	// 	sp1.sprite = ob[3] as Sprite;

	// }

	// // Update is called once per frame
	// void Update () {

	// }
}