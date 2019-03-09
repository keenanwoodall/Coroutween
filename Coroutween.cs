using System.Collections;
using UnityEngine;

namespace Beans.Unity.Tweening
{

	public static class Coroutween
	{

		public delegate void ProgressChanged<T> (T from, T to, float t) where T : struct;
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

		/// <summary>
		/// Generic tweening method that requires you apply the interpolation yourself.
		/// </summary>
		/// <typeparam name="T">The type of value to tween.</typeparam>
		/// <param name="from">The start value.</param>
		/// <param name="to">The target value.</param>
		/// <param name="duration">How long the tween will take.</param>
		/// <param name="onProgress">(from, to, t) The callback for when the tween progresses. This is where you interpolate between from and to based on t.</param>
		/// <returns>The coroutine that runs the tween.</returns>
		public static Coroutine To<T> (T from, T to, float duration, ProgressChanged<T> onProgress) where T : struct
		{
			return To (from, to, duration, onProgress, Ease.EaseType.CubicInOut);
		}
		/// <summary>
		/// Generic tweening method that requires you apply the interpolation yourself.
		/// </summary>
		/// <typeparam name="T">The type of value to tween.</typeparam>
		/// <param name="from">The start value.</param>
		/// <param name="to">The target value.</param>
		/// <param name="duration">How long the tween will take.</param>
		/// <param name="onProgress">(from, to, t) The callback for when the tween progresses. This is where you interpolate between from and to based on t.</param>
		/// <param name="ease">The type of easing preset to apply.</param>
		/// <returns>The coroutine that runs the tween.</returns>
		public static Coroutine To<T> (T from, T to, float duration, ProgressChanged<T> onProgress, Ease.EaseType ease) where T : struct
		{
			return To (from, to, duration, onProgress, Ease.GetEaseMethod (ease));
		}
		/// <summary>
		/// Generic tweening method that requires you apply the interpolation yourself.
		/// </summary>
		/// <typeparam name="T">The type of value to tween.</typeparam>
		/// <param name="from">The start value.</param>
		/// <param name="to">The target value.</param>
		/// <param name="duration">How long the tween will take.</param>
		/// <param name="onProgress">(from, to, t) The callback for when the tween progresses. This is where you interpolate between from and to based on t.</param>
		/// <param name="ease">The custom function in which to calculate easing. You can still use preset eases by passing Ease.EaseMethodNameHere as the methods used by the presets are public.</param>
		/// <returns></returns>
		public static Coroutine To<T> (T from, T to, float duration, ProgressChanged<T> onProgress, Ease.EaseMethod ease) where T : struct
		{
			return Tweener.StartCoroutine (ToRoutine (from, to, duration, onProgress, ease));
		}

		private static IEnumerator ToRoutine<T> (T from, T to, float duration, ProgressChanged<T> onProgress) where T : struct
		{
			if (duration == 0f)
			{
				onProgress (from, to, 1f);
				yield break;
			}

			var time = 0f;

			while (time < duration)
			{
				onProgress (from, to, time / duration);
				time += Time.deltaTime;
				yield return null;
			}

			onProgress (from, to, 1f);
			yield break;
		}

		private static IEnumerator ToRoutine<T> (T from, T to, float duration, ProgressChanged<T> onProgress, Ease.EaseMethod ease) where T : struct
		{
			return ToRoutine (from, to, duration, (a, b, t) => onProgress (a, b, ease (t)));
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