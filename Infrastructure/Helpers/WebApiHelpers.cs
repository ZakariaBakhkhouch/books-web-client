using Application.Exceptions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using static Infrastructure.Helpers.Common;

namespace Infrastructure.Helpers
{
    public class WebApiHelpers
    {
        private readonly HttpClient _httpClient;
        public EventHandler<Exception> OnError;
        public EventHandler OnFailed;
        public EventHandler OnTimeout;
        public EventHandler OnUnAuthorized;

        public WebApiHelpers()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(GlobalSettings.Instance.BaseApiEndpoint) };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.Timeout = TimeSpan.FromMinutes(6);
        }

        #region servercalls

        public async Task<Tuple<CallStatus>> GetAsync(string action, string authenticationToken,
           bool throwExceptionOnError = false)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authenticationToken);
                var response = await _httpClient.GetAsync(action);
                if (response.IsSuccessStatusCode)
                {
                    return Tuple.Create(CallStatus.Success);
                }
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return Tuple.Create(CallStatus.Unauthorized);
                }

                if (response.StatusCode == HttpStatusCode.NotFound)
                    return Tuple.Create(CallStatus.NotFound);

                return Tuple.Create(CallStatus.Error);
            }
            catch (TimeoutException)
            {
                OnTimeout?.Invoke(new object(), new EventArgs());
            }
            catch (HttpRequestException)
            {
                if (throwExceptionOnError)
                    throw new NetworkErrorException();
                OnFailed?.Invoke(new object(), new EventArgs());
            }
            catch (Exception ex)
            {
                if (ex is ServiceAuthenticationException unauthorizedEx)
                    throw unauthorizedEx;
                OnError?.Invoke(new object(), ex);
            }

            return Tuple.Create(CallStatus.Exception);
        }

        public async Task<Tuple<CallStatus, T>> GetAsync<T>(string action, string authenticationToken,
            bool throwExceptionOnError = false)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authenticationToken);
                var response = await _httpClient.GetAsync(action);
                if (response.IsSuccessStatusCode)
                    return Tuple.Create(CallStatus.Success, await JsonDeserialize<T>(response));
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //if (throwExceptionOnError)
                    //    throw new ServiceAuthenticationException();
                    //OnUnAuthorized?.Invoke(new object(), new EventArgs());
                    return Tuple.Create(CallStatus.Unauthorized, default(T));
                }

                if (response.StatusCode == HttpStatusCode.NotFound)
                    return Tuple.Create(CallStatus.NotFound, await JsonDeserialize<T>(response));

                return Tuple.Create(CallStatus.Error, await JsonDeserialize<T>(response));
            }
            catch (TimeoutException)
            {
                OnTimeout?.Invoke(new object(), new EventArgs());
            }
            catch (HttpRequestException)
            {
                if (throwExceptionOnError)
                    throw new NetworkErrorException();
                OnFailed?.Invoke(new object(), new EventArgs());
            }
            catch (Exception ex)
            {
                if (ex is ServiceAuthenticationException unauthorizedEx)
                    throw unauthorizedEx;
                OnError?.Invoke(new object(), ex);
            }

            return Tuple.Create(CallStatus.Exception, default(T));
        }

        public async Task<Tuple<CallStatus, T>> PostAsync<T>(string action, object data)
        {
            try
            {
                //Create the request.
                var json = JsonConvert.SerializeObject(data);
                HttpContent content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await _httpClient.PostAsync(action, content);

                //Process the response.
                if (response.IsSuccessStatusCode)
                    return Tuple.Create(CallStatus.Success, await JsonDeserialize<T>(response));

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return Tuple.Create(CallStatus.Unauthorized, await JsonDeserialize<T>(response));
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return Tuple.Create(CallStatus.Error, await JsonBadRequestDeserialize<T>(response));
                }

                return Tuple.Create(CallStatus.Error, await JsonDeserialize<T>(response));
            }
            catch (TimeoutException)
            {
                OnTimeout?.Invoke(new object(), new EventArgs());
            }
            catch (HttpRequestException)
            {
                OnFailed?.Invoke(new object(), new EventArgs());
            }
            catch (Exception ex)
            {
                OnError?.Invoke(new object(), ex);
            }

            return Tuple.Create(CallStatus.Exception, default(T));
        }


        #endregion

        #region Serializers/Deserializers

        public async Task<T> JsonDeserialize<T>(HttpResponseMessage response)
        {
            try
            {
                // check f the response content is an image
                var fileType = response.Content.Headers.ContentType;
                var json = await response.Content.ReadAsStringAsync();
                var deserialized = JsonConvert.DeserializeObject<T>(json);
                return deserialized;
            }
            catch (Exception ex)
            {
                OnError?.Invoke(new object(), ex);
                return default(T);
            }
        }

        private async Task<T> JsonBadRequestDeserialize<T>(HttpResponseMessage response)
        {
            try
            {
                var json = await response.Content.ReadAsStringAsync();
                dynamic dynJson = JsonConvert.DeserializeObject(json);

                var deserialized = JsonConvert.DeserializeObject<T>(json);
                return deserialized;
            }
            catch (Exception ex)
            {
                OnError?.Invoke(new object(), ex);
                return default(T);
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        #endregion Serializers/Deserializers
    }
}
