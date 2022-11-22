using System.Diagnostics;
using Microsoft.AspNetCore.Http.Features;

internal class MyDiagnosticListener : IObserver<DiagnosticListener>
{
    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(DiagnosticListener value)
    {
        if (value.Name == "Microsoft.AspNetCore")
        {
            value.Subscribe(new KeyValueObserver());
        }
    }
}

internal class KeyValueObserver : IObserver<KeyValuePair<string, object>>
{
    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(KeyValuePair<string, object> kvp)
    {
        if (kvp.Key == "Microsoft.AspNetCore.Hosting.HttpRequestIn.Start")
        {
            var context = kvp.Value as HttpContext;
            
            var bodyControlFeature = context.Features.Get<IHttpBodyControlFeature>();
            bodyControlFeature.AllowSynchronousIO = true;
            
            var stream = new StreamReader(context.Request.Body);
            var body = stream.ReadToEnd();
            body.ToString();
        }
    }
}