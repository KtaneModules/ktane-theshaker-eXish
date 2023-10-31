using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShakerScript : MonoBehaviour
{
	private void Start()
	{
		Id = IdTracker++;
		missingNo = new int[]
		{
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17,
			18,
			19
		}.Shuffle().Take(4).ToArray<int>();
		missingNo = (from x in missingNo
		orderby x
		select x).ToArray();
		Debug.LogFormat("[The Shaker #{0}] The missing balls are: {1}", new object[]
		{
			Id,
			missingNo.Join(", ")
		});
		missingNo = (from x in missingNo
		orderby missingNo.Product().GetDigit(Array.IndexOf(missingNo, x)), x
		select x).ToArray();
		Debug.Log(missingNo.Join(", "));
		Answers = new bool?[4];
		Debug.LogFormat("[The Shaker #{0}] The expected solutions are: {1}", new object[]
		{
			Id,
			(from x in new int[]
			{
				0,
				1,
				2,
				3
			}
			select GetSolution(missingNo[x]).ToString()).Join(", ")
		});
		KMBombModule module = Module;
		module.OnActivate = (KMBombModule.KMModuleActivateEvent)Delegate.Combine(module.OnActivate, new KMBombModule.KMModuleActivateEvent(Activate));
		KMSelectable[] buttons = Buttons;
		for (int i = 0; i < buttons.Length; i++)
		{
			KMSelectable kmselectable = buttons[i];
			KMSelectable real = kmselectable;
			KMSelectable kmselectable2 = kmselectable;
			kmselectable2.OnInteract = (KMSelectable.OnInteractHandler)Delegate.Combine(kmselectable2.OnInteract, new KMSelectable.OnInteractHandler(delegate()
			{
				Audio.PlaySoundAtTransform(PressBeep.name, real.transform);
				real.AddInteractionPunch(0.2f);
				ToggleButton(real);
				return false;
			}));
		}
		KMSelectable submit = Submit;
		submit.OnInteract = (KMSelectable.OnInteractHandler)Delegate.Combine(submit.OnInteract, new KMSelectable.OnInteractHandler(delegate()
		{
			Submit.AddInteractionPunch(1f);
			StartCoroutine(CheckAnswer());
			return false;
		}));
	}

	private IEnumerator CheckAnswer()
	{
		if (_solving)
		{
			yield break;
		}
		if (_solved)
		{
			Audio.PlaySoundAtTransform(WeirdPress.name, transform);
			ToggleButton(Buttons[1]);
			ToggleButton(Buttons[2]);
			ToggleButton(Buttons[3]);
			ToggleButton(Buttons[0]);
			yield return new WaitForSeconds(0.25f);
			ToggleButton(Buttons[1]);
			ToggleButton(Buttons[2]);
			ToggleButton(Buttons[3]);
			ToggleButton(Buttons[0]);
			yield break;
		}
		if ((from x in Answers
		where x != null
		select x).Count() < 3)
		{
			int tempAnswer = 0;
			if (Buttons[0].GetComponentInChildren<Spinner>() != null)
			{
				tempAnswer += 2;
			}
			if (Buttons[1].GetComponentInChildren<Spinner>() != null)
			{
				tempAnswer += 8;
			}
			if (Buttons[2].GetComponentInChildren<Spinner>() != null)
			{
				tempAnswer++;
			}
			if (Buttons[3].GetComponentInChildren<Spinner>() != null)
			{
				tempAnswer += 4;
			}
			string text = "[The Shaker #{0}] Submitted a {1} for input #{2}.";
			object[] array = new object[3];
			array[0] = Id;
			array[1] = tempAnswer;
			array[2] = (from x in Answers
			where x != null
			select x).Count() + 1;
			Debug.LogFormat(text, array);
			if (tempAnswer == GetSolution(missingNo[(from x in Answers
			where x != null
			select x).Count()]))
			{
				Answers[(from x in Answers
				where x != null
				select x).Count()] = new bool?(true);
			}
			else
			{
				Answers[(from x in Answers
				where x != null
				select x).Count()] = new bool?(false);
			}
			Audio.PlaySoundAtTransform(WeirdPress.name, transform);
			ToggleButton(Buttons[1]);
			ToggleButton(Buttons[2]);
			ToggleButton(Buttons[3]);
			ToggleButton(Buttons[0]);
			yield return new WaitForSeconds(0.25f);
			ToggleButton(Buttons[1]);
			ToggleButton(Buttons[2]);
			ToggleButton(Buttons[3]);
			ToggleButton(Buttons[0]);
			yield break;
		}
		_solving = true;
		Audio.PlaySoundAtTransform(CheckChime.name, transform);
		ToggleButton(Buttons[1]);
		yield return new WaitForSeconds(0.5f);
		ToggleButton(Buttons[3]);
		yield return new WaitForSeconds(0.5f);
		ToggleButton(Buttons[2]);
		yield return new WaitForSeconds(0.5f);
		ToggleButton(Buttons[0]);
		yield return new WaitForSeconds(0.5f);
		ToggleButton(Buttons[1]);
		yield return new WaitForSeconds(0.5f);
		ToggleButton(Buttons[3]);
		yield return new WaitForSeconds(0.5f);
		ToggleButton(Buttons[2]);
		yield return new WaitForSeconds(0.5f);
		ToggleButton(Buttons[0]);
		yield return new WaitForSeconds(0.5f);
		ToggleButton(Buttons[1]);
		ToggleButton(Buttons[2]);
		yield return new WaitForSeconds(0.5f);
		ToggleButton(Buttons[1]);
		ToggleButton(Buttons[2]);
		ToggleButton(Buttons[3]);
		ToggleButton(Buttons[0]);
		yield return new WaitForSeconds(0.5f);
		ToggleButton(Buttons[1]);
		ToggleButton(Buttons[2]);
		ToggleButton(Buttons[3]);
		ToggleButton(Buttons[0]);
		yield return new WaitForSeconds(0.5f);
		ToggleButton(Buttons[1]);
		ToggleButton(Buttons[2]);
		ToggleButton(Buttons[3]);
		ToggleButton(Buttons[0]);
		yield return new WaitForSeconds(0.5f);
		ToggleButton(Buttons[3]);
		ToggleButton(Buttons[0]);
		yield return null;
		int answer = 0;
		if (Buttons[0].GetComponentInChildren<Spinner>() != null)
		{
			answer += 2;
		}
		if (Buttons[1].GetComponentInChildren<Spinner>() != null)
		{
			answer += 8;
		}
		if (Buttons[2].GetComponentInChildren<Spinner>() != null)
		{
			answer++;
		}
		if (Buttons[3].GetComponentInChildren<Spinner>() != null)
		{
			answer += 4;
		}
		Debug.LogFormat("[The Shaker #{0}] Submitted a {1}.", new object[]
		{
			Id,
			answer
		});
		if (answer == GetSolution(missingNo[3]))
		{
			Answers[3] = new bool?(true);
		}
		else
		{
			Answers[3] = new bool?(false);
		}
		_solving = false;
		if ((from x in Answers
		where x == true
		select x).Count() == 4)
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
			Module.HandlePass();
			_solved = true;
		}
		else
		{
			Module.HandleStrike();
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.Strike, transform);
			Answers = new bool?[4];
		}
	}

	private void Activate()
	{
		Parent = Instantiate(CageTemplate, GetPhysicsPosition(), transform.rotation);
		BombTransform = gameObject;
		StartCoroutine(SpawnBalls());
	}

	private void FixedUpdate()
	{
		Spinner.transform.Rotate(new Vector3(360f * Time.fixedDeltaTime, 0f, 0f));
		if (Parent != null)
		{
			Parent.GetComponentInChildren<Spinner>().transform.Rotate(new Vector3(360f * Time.fixedDeltaTime, 0f, 0f));
		}
		if (Parent != null && BombTransform != null)
		{
			Parent.transform.rotation = BombTransform.transform.rotation;
		}
		for (int i = 0; i < Balls.Count; i++)
		{
			DisplayBalls[i].transform.localPosition = Balls[i].transform.localPosition;
			DisplayBalls[i].transform.rotation = Balls[i].transform.rotation;
		}
	}

	private IEnumerator SpawnBalls()
	{
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < 16; i++)
		{
			Balls.Add(Instantiate(PhysicsBallTemplate, Parent.transform));
			DisplayBalls.Add(Instantiate(DisplayBallTemplate, Scale));
			DisplayBalls[i].GetComponent<MeshRenderer>().material.mainTexture = NumberTextures[(from x in new int[]
			{
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12,
				13,
				14,
				15,
				16,
				17,
				18,
				19
			}
			where !missingNo.Contains(x)
			select x).ElementAt(i)];
			DisplayBalls[i].GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.8f, 1f), Random.Range(0.8f, 1f), Random.Range(0.8f, 1f));
		}
		foreach (GameObject gameObject in Balls)
		{
			gameObject.transform.localScale = Parent.transform.lossyScale;
		}
		for (;;)
		{
			foreach (GameObject gameObject2 in Balls)
			{
				if (Vector3.Distance(gameObject2.transform.position, Parent.transform.position) > 10f)
				{
					gameObject2.transform.position = Parent.transform.position;
				}
			}
			yield return new WaitForSeconds(1f);
		}
	}

	private bool ReferenceEquals(object Other)
	{
		return Id == ((ShakerScript)Other).Id;
	}

	private void OnDestroy()
	{
		if (Parent == null)
		{
			return;
		}
		Destroy(Parent);
		PhysicsCount--;
	}

	private Vector3 GetPhysicsPosition()
	{
		Vector3 vector = new Vector3(100f, 100f, 100f);
		int num = ++PhysicsCount;
		vector.x += (15 * (num % 10));
		vector.y += (15 * (num % 100 - num % 10));
		vector.z += (15 * (num % 1000 - num % 100));
		return vector * -1f;
	}

	private void ToggleButton(KMSelectable Button)
	{
		Spinner componentInChildren = Button.GetComponentInChildren<Spinner>();
		if (componentInChildren != null)
		{
			Destroy(componentInChildren.gameObject);
		}
		else
		{
			Instantiate(LightTemplate, Button.transform).GetComponentInChildren<Light>().range *= transform.lossyScale.x;
		}
	}

	private int GetSolution(int val)
	{
		switch (val)
		{
		case 0:
			return 15;
		case 1:
			return 6;
		case 2:
			return 14;
		case 3:
			return 0;
		case 4:
			return 7;
		case 5:
			return 15;
		case 6:
			return 1;
		case 7:
			return 8;
		case 8:
			return 5;
		case 9:
			return 2;
		case 10:
			return 9;
		case 11:
			return 7;
		case 12:
			return 3;
		case 13:
			return 11;
		case 14:
			return 9;
		case 15:
			return 4;
		case 16:
			return 12;
		case 17:
			return 3;
		case 18:
			return 5;
		case 19:
			return 13;
		default:
			return 0;
		}
	}

	public GameObject PhysicsBallTemplate;

	public GameObject CageTemplate;

	public GameObject DisplayBallTemplate;

	public GameObject LightTemplate;

	public GameObject Spinner;

	public KMBombModule Module;

	public KMAudio Audio;

	public AudioClip CheckChime;

	public AudioClip PressBeep;

	public AudioClip WeirdPress;

	public Transform Scale;

	public Texture[] NumberTextures;

	public KMSelectable[] Buttons;

	public KMSelectable Submit;

	private bool _solved;

	private bool _solving;

	private List<GameObject> Balls = new List<GameObject>();

	private List<GameObject> DisplayBalls = new List<GameObject>();

	public int Id;

	private static int IdTracker = 1;

	private GameObject Parent;

	private GameObject BombTransform;

	private int[] missingNo;

	private bool?[] Answers;

	private static int PhysicsCount;
}