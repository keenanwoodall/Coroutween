# Coroutween
A 💩 tweening library for Unity - only made for the pun, don't actually use it.

### How to use it
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
If, for example, you wanted to use an animation curve you could do something like this:
```cs
public AnimationCurve curve = AnimationCurve.EaseInOut (0f, 0f, 1f, 1f);

private void Start ()
{
    // Increases object's scale to twice it's size along an animation curve.
    Coroutween.To (() => transform.localScale, x => transform.localScale = x, Vector3.one * 2f, 5f, curve.Evaluate);
}
```
