using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;

namespace CleanArchitecture.Functional.Tests.Common;
public static class ApiRequester {
    public static async Task<ApiResponse<T>> PostAsync<T>(
        WebApplicationFactory<Program> server, string url, object request ) {
        var client = server.CreateClient();

        string json = JsonConvert.SerializeObject( request );
        var httpContent = new StringContent(
            json, System.Text.Encoding.UTF8, "application/json" );

        var response = await client.PostAsync( url, httpContent );
        var content = await response.Content.ReadAsStringAsync();
        var apiResult = JsonConvert.DeserializeObject<T>( content );

        return new ApiResponse<T>( apiResult, response );
    }

    public static async Task<ApiResponse> PostAsync(
        WebApplicationFactory<Program> server, string url, object request ) {
        var client = server.CreateClient();

        string json = JsonConvert.SerializeObject( request );
        var httpContent = new StringContent(
            json, System.Text.Encoding.UTF8, "application/json" );

        var response = await client.PostAsync( url, httpContent );
        var content = await response.Content.ReadAsStringAsync();

        return new ApiResponse( response );
    }

    public static async Task<ApiResponse<T>> PostAsync<T>(
        WebApplicationFactory<Program> server, string url ) {
        var client = server.CreateClient();

        string json = JsonConvert.SerializeObject( "" );
        var httpContent = new StringContent(
            json, System.Text.Encoding.UTF8, "application/json" );

        var response = await client.PostAsync( url, httpContent );
        var content = await response.Content.ReadAsStringAsync();
        var apiResult = JsonConvert.DeserializeObject<T>( content );

        return new ApiResponse<T>( apiResult, response );
    }

    public static async Task<ApiResponse<T>> PutAsync<T>(
        WebApplicationFactory<Program> server, string url, object request ) {
        var client = server.CreateClient();

        string json = JsonConvert.SerializeObject( request );
        var httpContent = new StringContent(
            json, System.Text.Encoding.UTF8, "application/json" );

        var response = await client.PutAsync( url, httpContent );
        var content = await response.Content.ReadAsStringAsync();
        var apiResult = JsonConvert.DeserializeObject<T>( content );

        return new ApiResponse<T>( apiResult, response );
    }

    public static async Task<ApiResponse> PutAsync(
        WebApplicationFactory<Program> server, string url, object request ) {
        var client = server.CreateClient();

        string json = JsonConvert.SerializeObject( request );
        var httpContent = new StringContent(
            json, System.Text.Encoding.UTF8, "application/json" );

        var response = await client.PutAsync( url, httpContent );
        var content = await response.Content.ReadAsStringAsync();

        return new ApiResponse( response );
    }

    public static async Task<ApiResponse<T>> GetAsync<T>(
        WebApplicationFactory<Program> server, string url ) {
        var client = server.CreateClient();
        var response = await client.GetAsync( url );
        var content = await response.Content.ReadAsStringAsync();
        var apiResult = JsonConvert.DeserializeObject<T>( content );

        return new ApiResponse<T>( apiResult, response );
    }

    public static async Task<ApiResponse> GetAsync(
        WebApplicationFactory<Program> server, string url ) {
        var client = server.CreateClient();
        var response = await client.GetAsync( url );
        var content = await response.Content.ReadAsStringAsync();

        return new ApiResponse( response );
    }

    public static async Task<ApiResponse<T>> DeleteAsync<T>(
        WebApplicationFactory<Program> server, string url ) {
        var client = server.CreateClient();
        var response = await client.DeleteAsync( url );
        var content = await response.Content.ReadAsStringAsync();
        var apiResult = JsonConvert.DeserializeObject<T>( content );

        return new ApiResponse<T>( apiResult, response );
    }

    public static async Task<ApiResponse> DeleteAsync(
        WebApplicationFactory<Program> server, string url ) {
        var client = server.CreateClient();
        var response = await client.DeleteAsync( url );
        var content = await response.Content.ReadAsStringAsync();

        return new ApiResponse( response );
    }
}
