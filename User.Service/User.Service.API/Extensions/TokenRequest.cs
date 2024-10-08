﻿using Newtonsoft.Json;

namespace User.Service.API.Extensions;

public class TokenRequest
{
    [JsonProperty("client_id")]
    public string ClientId { get; set; }

    [JsonProperty("client_secret")]
    public string ClientSecret { get; set; }

    [JsonProperty("audience")]
    public string Audience { get; set; }

    [JsonProperty("grant_type")]
    public string GrantType { get; set; }

    [JsonProperty("name")]
    public string Username { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("scope")]
    public string Scope { get; set; }
}
