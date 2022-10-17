namespace CleanArchitecture.Functional.Tests.Common;
public class ApiResponse {
    public HttpResponseMessage Response { get; }

    public ApiResponse( HttpResponseMessage response ) {
        Response = response;
    }
}

public class ApiResponse<T> : ApiResponse {
    public T Value { get; }

    public ApiResponse( T value, HttpResponseMessage response ) : base( response ) {
        Value = value;
    }
}
