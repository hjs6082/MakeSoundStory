using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CustomRandomDistributions;

[ExecuteInEditMode]
public class RandomDistribution : MonoBehaviour {

	// ���� �
	[SerializeField]
	AnimationCurve distributionCurve;

	// ���� ��� ġ��
	[SerializeField]
	AnimCurveRect curveRect;
	/// <summary>
	/// Returns a rectangle that wraps the distribution curve.
	/// </summary>
	public AnimCurveRect CurveRect {
		get {
			return curveRect;
		}
	}

	// ���� ���� ��� ����
	public enum RandomizeMode {BruteForce, Pregenerate};
	[SerializeField, HideInInspector]
	public RandomizeMode randomizeMode;
	[SerializeField, HideInInspector]
	public bool showOptions; // �ν�����â�� �ɼǵ��� ǥ�õǴ°�?
	
	// ������ ���е� ����, ���� ���� ��Ȯ (��� �� ����)
	[SerializeField, HideInInspector]
	public int prebakeResolution = 500;

	// �⺻���� ������ �ִϸ��̼� �
	AnimationCurve DefaultCurve () {
		return AnimationCurve.EaseInOut(0f, 0f, 100f, 100f);
	}

	// ������ �̸� ����� Ŭ����
	[SerializeField, HideInInspector]
	NumberBakery numberBakery = null;




	// Awake���� � �����Ͱ� ������Ʈ �Ǿ����� Ȯ��
	void Awake () {

		// �����Ϳ��� ��ũ��Ʈ�� �߰��� �� �⺻ � ����
		#if UNITY_EDITOR
		if (!Application.isPlaying && (distributionCurve == null)) {
			distributionCurve = DefaultCurve();
		}
		#endif

		UpdateCurveData();
	}
	
	/// <summary>
	/// ���� ������ ���Ǵ� ���� � ��ȯ
	/// </summary>
	/// <returns>���� ���� �</returns>
	public AnimationCurve GetDistributionCurve() {
		return distributionCurve;
	}

	/// <summary>
	/// ���� ������ ����� ���� � ����
	/// </summary>
	/// <param name="newDistributionCurve">���ο� ���� �</param>
	public void SetDistributionCurve(AnimationCurve newDistributionCurve) {
		distributionCurve = newDistributionCurve;
		UpdateCurveData();
	}


	// update the curve data if the curve or settings have changed. called by the custom inspector.
	void UpdateCurveData() {
		//Debug.Log ("Updating Curve Data of " + gameObject.name);

		// make sure the curve has at least two keyframes and fulfills other requirements
		CheckForValidCurve();

		// always calc the curve rect, it's needed in any case
		curveRect = new AnimCurveRect(distributionCurve);

		// either prebake numbers or clear bakery depending on mode
		if (randomizeMode == RandomizeMode.Pregenerate) {
			// instantiate number bakery
			numberBakery = new NumberBakery(distributionCurve, curveRect, prebakeResolution);
		}
		else { // brute force mode, clear number bakery
			numberBakery = null;			
		}
	}

	
	// Make sure that the curve is valid
	void CheckForValidCurve() {
		//Debug.Log ("Checking for valid curve");
		bool curveIsValid = true; // assume a valid curve, set to false if something is wrong
		
		// if the curve is null, not much curve there to check, set it to some default curve and skip the rest
		if (distributionCurve == null) distributionCurve = DefaultCurve();
		// curve is not null, 
		else {
			
			// check for at least some part of the curve being above zero
			AnimCurveRect tmpCurveRect = new AnimCurveRect(distributionCurve);
			if (tmpCurveRect.MaxY <= 0) {
				Debug.LogError("At least some part of the distribution curve has to be higher than zero.");
				curveIsValid = false;
			}
			
			// check for minimum amount of keyframes
			if (distributionCurve.keys.Length < 2) {
				Debug.LogError("A distribution curve needs at least two keyframes.");
				curveIsValid = false;
			}
		}
		
		if (!curveIsValid) Debug.LogError("Not a valid distribution curve.");
	}	


	/// <summary>
	/// Returns a random float value using weighted chances from the distribution curve.
	/// </summary>
	public float RandomFloat() {

		// return a float either from prebakes floats or brute force algorithm
		if (randomizeMode == RandomizeMode.Pregenerate) {
			return numberBakery.RandomFloat();
		}
		else {
			return BruteForceFloat();
		}
	}

	/// <summary>
	/// Returns a random integer value using weighted chances from the distribution curve.
	/// </summary>
	public int RandomInt() {
		
		// return a float either from prebakes floats or brute force algorithm
		if (randomizeMode == RandomizeMode.Pregenerate) {
			return numberBakery.RandomInt();
		}
		else {
			return Mathf.RoundToInt(BruteForceFloat());
		}
	}

	// brute force approach to finding a random float
	float BruteForceFloat() {
		float x, y, curveY = 0f;

		do {
			// pick a random point within the rectangle
			x = Random.Range(curveRect.MinX, curveRect.MaxX);
			y = Random.Range(0, curveRect.MaxY);
			// evaluate the curve at x of that point
			curveY = distributionCurve.Evaluate(x);
			// repeat until the chosen point is "under" the curve
		} while (curveY < y);

		// return the x coord of that point, it's good.
		return x;
	}

}
