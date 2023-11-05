using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

public class ServiceResponse<T> 
{
    public T ?Data{get;set;}
    public bool success{get;set;}=true;

    public string Message{get;set;}=string.Empty;
}