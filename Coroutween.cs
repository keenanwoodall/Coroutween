﻿using System.Collections;
using UnityEngine;

namespace Beans.Unity.Tweening
{
	public static class Coroutween
	{
		public delegate void ProgressChanged (float t);
		public delegate void ProgressChanged<T> (T a, T b, float t);
		public delegate T Getter<out T> ();
		public delegate void Setter<in T> (T value);

		private class Coroutweener : MonoBehaviour { }
		private static Coroutweener tweener;
		private static Coroutweener Tweener
		{
			get
			{
				if (tweener == null)
					tweener = new GameObject ("Coroutweener").AddComponent<Coroutweener> ();
				return tweener;
			}
		}

		public static Coroutine To (float duration, Ease.EaseType ease, ProgressChanged onProgress)
		{
			return To (duration, Ease.GetEaseMethod (ease), onProgress);
		}
		public static Coroutine To (float duration, Ease.EaseMethod ease, ProgressChanged onProgress)
		{
			return Tweener.StartCoroutine (ToRoutine (duration, ease, onProgress));
		}

		public static Coroutine To<T> (T from, T to, float duration, Ease.EaseType ease, ProgressChanged<T> onProgress) where T : struct
		{
			return To (from, to, duration, Ease.GetEaseMethod (ease), onProgress);
		}
		public static Coroutine To<T> (T from, T to, float duration, Ease.EaseMethod ease, ProgressChanged<T> onProgress) where T : struct
		{
			return Tweener.StartCoroutine (ToRoutine (from, to, duration, ease, onProgress));
		}

		// float
		public static Coroutine To (Getter<float> getter, Setter<float> setter, float target, float duration, Ease.EaseType ease)
		{
			var from = getter ();
			return To (duration, ease, t => setter (Mathf.LerpUnclamped (from, target, t)));
		}
		public static Coroutine To (Getter<float> getter, Setter<float> setter, float target, float duration, Ease.EaseMethod ease)
		{
			var from = getter ();
			return To (duration, ease, t => setter (Mathf.LerpUnclamped (from, target, t)));
		}

		// Vector2
		public static Coroutine To (Getter<Vector2> getter, Setter<Vector2> setter, Vector2 target, float duration, Ease.EaseType ease)
		{
			var from = getter ();
			return To (duration, ease, t => setter (Vector2.LerpUnclamped (from, target, t)));
		}
		public static Coroutine To (Getter<Vector2> getter, Setter<Vector2> setter, Vector2 target, float duration, Ease.EaseMethod ease)
		{
			var from = getter ();
			return To (duration, ease, t => setter (Vector2.LerpUnclamped (from, target, t)));
		}

		// Vector3
		public static Coroutine To (Getter<Vector3> getter, Setter<Vector3> setter, Vector3 target, float duration, Ease.EaseType ease)
		{
			var from = getter ();
			return To (duration, ease, t => setter (Vector3.LerpUnclamped (from, target, t)));
		}
		public static Coroutine To (Getter<Vector3> getter, Setter<Vector3> setter, Vector3 target, float duration, Ease.EaseMethod ease)
		{
			var from = getter ();
			return To (duration, ease, t => setter (Vector3.LerpUnclamped (from, target, t)));
		}

		// Vector4
		public static Coroutine To (Getter<Vector4> getter, Setter<Vector4> setter, Vector4 target, float duration, Ease.EaseType ease)
		{
			var from = getter ();
			return To (duration, ease, t => setter (Vector4.LerpUnclamped (from, target, t)));
		}
		public static Coroutine To (Getter<Vector4> getter, Setter<Vector4> setter, Vector4 target, float duration, Ease.EaseMethod ease)
		{
			var from = getter ();
			return To (duration, ease, t => setter (Vector4.LerpUnclamped (from, target, t)));
		}

		// Quaternion
		public static Coroutine To (Getter<Quaternion> getter, Setter<Quaternion> setter, Quaternion target, float duration, Ease.EaseType ease)
		{
			var from = getter ();
			return To (duration, ease, t => setter (Quaternion.SlerpUnclamped (from, target, t)));
		}
		public static Coroutine To (Getter<Quaternion> getter, Setter<Quaternion> setter, Quaternion target, float duration, Ease.EaseMethod ease)
		{
			var from = getter ();
			return To (duration, ease, t => setter (Quaternion.SlerpUnclamped (from, target, t)));
		}

		private static IEnumerator ToRoutine (float duration, ProgressChanged onProgress)
		{
			if (duration == 0f)
			{
				onProgress (1f);
				yield break;
			}

			var time = 0f;

			while (time < duration)
			{
				onProgress (time / duration);
				time += Time.deltaTime;
				yield return null;
			}

			onProgress (1f);

			yield break;
		}

		private static IEnumerator ToRoutine (float duration, Ease.EaseMethod ease, ProgressChanged onProgress)
		{
			return ToRoutine (duration, t => onProgress (ease (t)));
		}

		private static IEnumerator ToRoutine<T> (T from, T to, float duration, Ease.EaseMethod ease, ProgressChanged<T> onProgress)
		{
			return ToRoutine (duration, t => onProgress (from, to, ease (t)));
		}
	}

	public static class Ease
	{
		public delegate float EaseMethod (float t);

		public enum EaseType
		{
			Linear,
			QuadIn,
			QuadOut,
			QuadInOut,
			CubicIn,
			CubicOut,
			CubicInOut,
			QuartIn,
			QuartOut,
			QuartInOut,
			QuintIn,
			QuintOut,
			QuintInOut,
			BounceIn,
			BounceOut,
			BounceInOut,
			ElasticIn,
			ElasticOut,
			ElasticInOut,
			CircularIn,
			CircularOut,
			CircularInOut,
			SinusIn,
			SinusOut,
			SinusInOut,
			ExponentialIn,
			ExponentialOut,
			ExponentialInOut
		}

		public static EaseMethod GetEaseMethod (EaseType ease)
		{
			switch (ease)
			{
				default:
					return Linear;
				case EaseType.QuadIn:
					return QuadIn;
				case EaseType.QuadOut:
					return QuadOut;
				case EaseType.QuadInOut:
					return QuadInOut;
				case EaseType.CubicIn:
					return CubicIn;
				case EaseType.CubicOut:
					return CubicOut;
				case EaseType.CubicInOut:
					return CubicInOut;
				case EaseType.QuartIn:
					return QuartIn;
				case EaseType.QuartOut:
					return QuartOut;
				case EaseType.QuartInOut:
					return QuartInOut;
				case EaseType.QuintIn:
					return QuintIn;
				case EaseType.QuintOut:
					return QuintOut;
				case EaseType.QuintInOut:
					return QuintInOut;
				case EaseType.BounceIn:
					return BounceIn;
				case EaseType.BounceOut:
					return BounceOut;
				case EaseType.BounceInOut:
					return BounceInOut;
				case EaseType.ElasticIn:
					return ElasticIn;
				case EaseType.ElasticOut:
					return ElasticOut;
				case EaseType.ElasticInOut:
					return ElasticInOut;
				case EaseType.CircularIn:
					return CircularIn;
				case EaseType.CircularOut:
					return CircularOut;
				case EaseType.CircularInOut:
					return CircularInOut;
				case EaseType.SinusIn:
					return SinusIn;
				case EaseType.SinusOut:
					return SinusOut;
				case EaseType.SinusInOut:
					return SinusInOut;
				case EaseType.ExponentialIn:
					return ExponentialIn;
				case EaseType.ExponentialOut:
					return ExponentialOut;
				case EaseType.ExponentialInOut:
					return ExponentialInOut;
			}
		}

		public static float Linear (float t) => t;
		public static float QuadIn (float t) => t * t;
		public static float QuadOut (float t) => t * (2f - t);
		public static float QuadInOut (float t) => t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;
		public static float CubicIn (float t) => t * t * t;
		public static float CubicOut (float t) => (t - 1f) * t * t + 1f;
		public static float CubicInOut (float t) => t < 0.5f ? 4f * t * t * t : (t - 1f) * (2f * t - 2f) * (2 * t - 2) + 1f;
		public static float QuartIn (float t) => t * t * t * t;
		public static float QuartOut (float t) => 1f - (t - 1f) * t * t * t;
		public static float QuartInOut (float t) => t < 0.5f ? 8f * t * t * t * t : 1f - 8f * (t - 1f) * t * t * t;
		public static float QuintIn (float t) => t * t * t * t * t;
		public static float QuintOut (float t) => 1f + (t - 1f) * t * t * t * t;
		public static float QuintInOut (float t) => t < 0.5f ? 16f * t * t * t * t * t : 1f + 16f * (t - 1f) * t * t * t * t;
		public static float BounceIn (float t) => 1f - BounceOut (1f - t);
		public static float BounceOut (float t) => t < 0.363636374f ? 7.5625f * t * t : t < 0.727272749f ? 7.5625f * (t -= 0.545454562f) * t + 0.75f : t < 0.909090936f ? 7.5625f * (t -= 0.8181818f) * t + 0.9375f : 7.5625f * (t -= 21f / 22f) * t + 63f / 64f;
		public static float BounceInOut (float t) => t < 0.5f ? BounceIn (t * 2f) * 0.5f : BounceOut (t * 2f - 1f) * 0.5f + 0.5f;
		public static float ElasticIn (float t) => -(Mathf.Pow (2, 10 * (t -= 1)) * Mathf.Sin ((t - (0.3f / 4f)) * (2 * Mathf.PI) / 0.3f));
		public static float ElasticOut (float t) => t == 1f ? 1f : 1f - ElasticIn (1f - t);
		public static float ElasticInOut (float t) => (t *= 2f) == 2f ? 1f : t < 1f ? -0.5f * (Mathf.Pow (2f, 10f * (t -= 1)) * Mathf.Sin ((t - 0.1125f) * (2f * Mathf.PI) / 0.45f)) : (Mathf.Pow (2f, -10f * (t -= 1f)) * Mathf.Sin ((t - 0.1125f) * (2f * Mathf.PI) / 0.45f) * 0.5f + 1f);
		public static float CircularIn (float t) => -(Mathf.Sqrt (1 - t * t) - 1);
		public static float CircularOut (float t) => Mathf.Sqrt (1f - (t = t - 1f) * t);
		public static float CircularInOut (float t) => (t *= 2f) < 1f ? -1f / 2f * (Mathf.Sqrt (1f - t * t) - 1f) : 0.5f * (Mathf.Sqrt (1 - (t -= 2) * t) + 1);
		public static float SinusIn (float t) => -Mathf.Cos (t * (Mathf.PI * 0.5f)) + 1f;
		public static float SinusOut (float t) => Mathf.Sin (t * (Mathf.PI * 0.5f));
		public static float SinusInOut (float t) => -0.5f * (Mathf.Cos (Mathf.PI * t) - 1f);
		public static float ExponentialIn (float t) => Mathf.Pow (2f, 10f * (t - 1f));
		public static float ExponentialOut (float t) => Mathf.Sin (t * (Mathf.PI * 0.5f));
		public static float ExponentialInOut (float t) => -0.5f * (Mathf.Cos (Mathf.PI * t) - 1f);
	}
}