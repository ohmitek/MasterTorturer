using UnityEngine;
using System.Collections;

public class PlayManager : MonoBehaviour 
{
	public Animator[] playerGroup; 
	private string[] animClipNameGroup;
	private int currentNumber;

	// Use this for initialization
	void Start () {

		animClipNameGroup = new string[] {
            "(01) Idle_1",
            "(02) Look to the right_1",
            "(03) Look to the left_1",
            "(04) Cheer_1",
            "(05) Pour water",
            "(06) Cheer_2",
            "(07) Death_1",
            "(08) Death_2",
            "(09) Look down",
            "(10) Point",
            "(11) Use a knife",
            "(12) Pray",
            "(13) Examine a document",
            "(14) Write",
            "(15) Kneel",
            "(16) Look into a mirror",
            "(17) Hitting a desk_1",
            "(18) Drowse_1",
            "(19) Stretching_1",
            "(20) Whispering(Left)",
            "(21) Whispering(Right)",
            "(22) Despair_1",
            "(23) Judge hammer",
            "(24) Turn the pages(Slowly)",
            "(25) Turn the pages(Quickly)",
            "(26) Turn the pages(Mini Book)",
            "(27) Newspaper",
            "(28) Touching hair_1",
            "(29) Touching hair_2",
            "(30) Wiping glasses",
            "(31) Wipe the desk",
            "(32) Cell phone_1",
            "(33) Cell phone_2"

		};

		currentNumber = 0;


		playerGroup = GameObject.Find ("PlayerGroup").transform.GetComponentsInChildren<Animator>();

		for (int i = 0; i < playerGroup.Length; i++)
		{
			playerGroup[i].speed = 1f;
			playerGroup[i].Play(animClipNameGroup[currentNumber]);
		}
	}
	

	void OnGUI()
	{
		// GUI 옵션
		GUIStyle textStyle = new GUIStyle();
		textStyle.fontSize = 15;
		textStyle.normal.textColor = Color.white;
		textStyle.hover.textColor = Color.red;


		//좌측이동
		if (GUI.Button(new Rect(50,50,50,50),"<"))
		{
			currentNumber--;

			if(currentNumber < 0 )
			{
				currentNumber = animClipNameGroup.Length - 1;
			}

			for(int i = 0; i < playerGroup.Length; i++)
			{
				playerGroup[i].speed = 1f;
				playerGroup[i].Play(animClipNameGroup[currentNumber]);
			}

		}
		//우측이동
		if(GUI.Button(new Rect(160,50,50,50),">"))
		{
			currentNumber++;

			if(currentNumber == animClipNameGroup.Length)
			{
				currentNumber = 0;
			}

			for(int i = 0; i < playerGroup.Length; i++)
			{
				playerGroup[i].speed = 1f;				
				playerGroup[i].Play(animClipNameGroup[currentNumber]);
			}
		}

		GUI.Label (new Rect(240, 50, 200,100), animClipNameGroup[currentNumber].ToString(), textStyle);
		
		// 현재/전체개수
		int totalCnt = animClipNameGroup.Length;
		string showMesssage = "("+(currentNumber+1) +"/" + totalCnt+")";
		GUI.Label(new Rect(240, 66, 200, 100), showMesssage);
	}
}
