# Hime

Hime is a stupid-simple web microframework for C#.
It basically aspires to do the same thing as Sinatra did for Ruby. Dead-simple prototyping with little to no effort.

Hime isn't near complete at all, it only has a few basic things implemented, but I'll probably forget this project existed in like a day or two, so it doesn't matter nearly as much.
Basically, don't run it in production, or even development. For your own safety.
Or do, I won't judge. Just don't blame me when your PC catches fire or something like this.

# How to do anything

1. Clone this repository
2. Link your .NET project against Hime. (Should work fine with both .NET Core and .NET Framework)
3. Create a new class deriving from HimeApplication.
4. Call `HimeRunner.Run` on a new instance of the class.
5. It should work. Hopefully.

# How do I create routes?

So far Hime only supports GET and POST (and even then it struggles).
Route declarations are done via attributes, like so:
```cs
[Get("/")]
public ActionResult Index(HimeContext ctx)
{
    return Ok("<b>Hello!</b>");
}
```

The Ok function automatically returns a 200 response, with the specified body.
If you want a custom response code, you can create your own ActionResult, like this:

```cs
[Post("/someroute")]
public ActionResult Route(HimeContext ctx)
{
   return new ActionResult
   {
        Code = 403,
        Content = Encoding.UTF8.GetBytes("Go away. You weren't supposed to be here.")
   };
}
```

# So what's missing?

Quite a lot, actually. You can't get the POST body, PUT/DELETE/PATCH aren't implemented yet, the HimeContext class has almost no information about the context, sessions, file sending, etc.
If you think this project is cool, please send in a pull request and help me out with making this. I'd greatly appreciate any kind of help with this.