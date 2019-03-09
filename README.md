# Coroutween
A 💩 tweening library for Unity - only made for the pun, don't actually use it.

### How to use it
Make sure you have `using Beans.Unity.Tweening;` at the top of your script

After that, you have a few options for how to tween stuff.
In the following examples I'll show how to change the FOV of a camera with each approach.

1. The easiest way is to use premade tweens like this:
```cs
camera.FieldOfViewTo (to: 25f, duration, EaseType.ElasticOut);
```
2. The next way lets you tween any supported value (`int`, `float`, `Vector2/3/4`, `Quaternion`, `Color`)
```cs
Coroutween.To (from: camera.fieldOfView, to: 25f, duration, EaseType.ElasticOut, x => camera.fieldOfView = x);
```
3. The final way gives you complete control by simply providing a callback with the tween's eased progress.
```cs
var from = camera.fieldOfView;
var to = 25f;
Coroutween.CreateInterpolater (duration, EaseType.ElasticOut, t => camera.fieldOfView = Mathf.LerpUnclamped (from, to, t));
```

All of these examples produce the same result:

![1](https://i.imgur.com/Gca8XFf.gif)

---

### Custom Easing
Every tween method that has an `EaseType` will also have an overload with an `EaseMethod` which is a callback that lets you calculate your own easing. If, for example, you wanted to use an animation curve instead of a preset easing type, you could pass `curve.Evaluate` instead of some `EaseType`.

Here's small example of how you could use it:
```cs
public float duration = 1f;
public AnimationCurve positionCurve = new AnimationCurve ();

private void Update ()
{
    if (Input.GetKeyDown (KeyCode.Space))
        transform.PositionTo (transform.position + Vector3.up, duration, positionCurve.Evaluate);
}
```
![2](https://i.imgur.com/fkako6q.gif)

---

### Physics?
If you are a little clever, you could even use tweens with physics
```cs
public Transform target;
public float duration = 1f;
public Rigidbody rb;

private Vector3 targetPosition;

private IEnumerator Start ()
{
    while (true)
    {
        yield return Coroutween.To (transform.position, target.position, duration, EaseType.ElasticOut, x => targetPosition = x);
    }
}

private void FixedUpdate ()
{
    rb.MovePosition (targetPosition);
}
```
![3](https://i.imgur.com/m2iwh3I.gif)
