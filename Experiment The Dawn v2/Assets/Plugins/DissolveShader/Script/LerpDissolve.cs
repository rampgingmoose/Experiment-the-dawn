using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpDissolve : MonoBehaviour
{
	public float minFillValue;
	public float maxFillValue;
	public float LerpSpeed;
	public Material[] mat;
	private float fill = 0f;
	private float range;
	

	void Start()
	{
		GetValue();
		range = maxFillValue - minFillValue;
	}
	void Update()
	{
		fill = (range / 2f) * (Mathf.Sin(LerpSpeed * Time.time) + 1f) + minFillValue;
		GetValue();
	}
	private void GetValue()
	{
		for (int i = 0; i < mat.Length; i++)
		{
			mat[i].SetFloat("_Fill", fill);
		}
	}
}
