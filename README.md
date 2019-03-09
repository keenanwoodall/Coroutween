# Coroutween
A shitty tweening library for Unity

### Getting Started
Make sure you have `using Beans.Unity.Tweening;` at the top of your script

After that you have a few options for how to tween stuff.
In the following examples I'll show how to change the position of a transform with each approach.

1. The easiest way lets you determine a getter and setter for supported types and takes care of everything else.

```cs
Coroutween.To (() => transform.position, x => transform.position = x, target.position, duration, EaseType.CubicInOut);
```

2. The next way lets you tween any value type as long as you know how to linearly interpolate it.

```cs
Coroutween.To 
(
	transform.position, 
	target.position, 
	duration, 
	EaseType.CubicInOut, 
	(a, b, t) => transform.position = Vector3.LerpUnclamped (a, b, t)
);
```

3. The final way gives you complete control over interpolation but you have to keep track of the start and end values on your own.
```cs
var from = transform.position;
var to = target.position;
Coroutween.To (duration, EaseType.CubicInOut, t => transform.position = Vector3.LerpUnclamped (from, to, t));
```

Each variation is also overloaded with a version that lets you supply a custom easing function.
If, for example, you wanted to make your own cubic-in easing you could do something like this:
```cs
private float someFloat;

private void Start ()
{
	Coroutween.To (() => someFloat, x => someFloat = x, 100f, 5f, CustomCubicEasing);
}

private float CustomCubicEasing (float t)
{
	return t * t * t;
}
```
