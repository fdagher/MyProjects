using IdentityModel;
using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OAuthOpenPlaygroundClient
{
    public partial class Form1 : Form
    {
        private string lastAccessToken;

        public Form1()
        {
            InitializeComponent();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPreferences frm = new frmPreferences();
            frm.ShowDialog(this);
        }

        private async void btnCode_Click(object sender, EventArgs e)
        {
            txtAuthCode.Text = await this.GetCode(txtClientIdCode.Text, txtScopesCode.Text, txtRedirectUriCode.Text);

            txtClientSecretCode.Enabled = true;
            btnGetTokenByCode.Enabled = true;
        }

        private async void btnGetTokenByCode_Click(object sender, EventArgs e)
        {
            var client = new TokenClient(Preferences.TokenEndpoint, "code.client", txtClientSecretCode.Text);

            var response = await client.RequestAuthorizationCodeAsync(txtAuthCode.Text, txtRedirectUriCode.Text);

            if (!string.IsNullOrEmpty(response.AccessToken))
            {
                txtAuthCodeResultToken.Text = response.AccessToken;
                txtAuthCodeResultTokenParsed.Text = ParseJwt(response.AccessToken);

                lastAccessToken = response.AccessToken;
            }
        }

        private async void btnGetTokenImplicit_Click(object sender, EventArgs e)
        {
            var state = Guid.NewGuid().ToString("N");

            var request = new AuthorizeRequest(Preferences.AuthorizationEndpoint);

            var url = request.CreateAuthorizeUrl(
                clientId: "implicit.client",
                responseType: "token",
                scope: txtScopesImplicit.Text,
                redirectUri: txtRedirectUriImplicit.Text,
                state: state);

            LaunchBrowser(url);

            Tuple<string, string, string> tuple = await this.GetResponseFromAuthorizationServer(txtRedirectUriImplicit.Text + "/", "implicit");

            txtAccessTokenImplicit.Text = "Check access token in the browser as the part after the # doesn't get trasfered to the server. Copy and paste here.";
        }

        private void txtAccessTokenImplicit_Leave(object sender, EventArgs e)
        {
            try
            {
                txtAccessTokenImplicitParsed.Text = ParseJwt(txtAccessTokenImplicit.Text);

                lastAccessToken = txtAccessTokenImplicit.Text;
            }
            catch { }
        }

        private void btnTokenCC_Click(object sender, EventArgs e)
        {
            // request token
            var tokenClient = new TokenClient(Preferences.TokenEndpoint, txtClientIdCC.Text, txtClientSecretCC.Text);
            var response = tokenClient.RequestClientCredentialsAsync(txtScopesCC.Text).Result;

            if (response.IsError)
            {
                txtAccessTokenCC.Text = response.Error;
                txtAccessTokenCCParsed.Text = "";
            }
            else
            {
                txtAccessTokenCC.Text = response.AccessToken;
                txtAccessTokenCCParsed.Text = ParseJwt(response.AccessToken);

                lastAccessToken = response.AccessToken;
            }
        }

        private void btnTokenRO_Click(object sender, EventArgs e)
        {
            // request token
            var tokenClient = new TokenClient(Preferences.TokenEndpoint, txtClientIdRO.Text, txtClientSecretRO.Text);
            var response = tokenClient.RequestResourceOwnerPasswordAsync(txtUsername.Text, txtPassword.Text, txtScopesRO.Text).Result;

            if (response.IsError)
            {
                txtAccessTokenRO.Text = response.Error;
                txtAccessTokenROParsed.Text = "";
            }
            else
            {
                txtAccessTokenRO.Text = response.AccessToken;
                txtAccessTokenROParsed.Text = ParseJwt(response.AccessToken);

                lastAccessToken = response.AccessToken;
            }
        }

        #region OpenID Connect Code

        private async void btnOidcGetCode_Click(object sender, EventArgs e)
        {
            txtOidcCode.Text = await this.GetCode(txtoidcClientIdCode.Text, txtoidcScopesCode.Text, txtoidcRedirectUriCode.Text);

            txtOidcClientSecretCode.Enabled = true;
            btnOidcExchangeCodeByToken.Enabled = true;
        }

        private async void btnOidcExchangeCodeByToken_Click(object sender, EventArgs e)
        {
            var client = new TokenClient(Preferences.TokenEndpoint, "code.oidc.client", txtClientSecretCode.Text);

            var response = await client.RequestAuthorizationCodeAsync(txtOidcCode.Text, txtoidcRedirectUriCode.Text);

            if (!string.IsNullOrEmpty(response.IdentityToken))
            {
                txtOidcTokenIdCode.Text = response.IdentityToken;
                txtOidcTokenIdCodeParsed.Text = ParseJwt(response.IdentityToken);
            }

            if (!string.IsNullOrEmpty(response.AccessToken))
            {
                txtOidcAccessTokenCode.Text = response.AccessToken;
                txtOidcAccessTokenCodeParsed.Text = ParseJwt(response.AccessToken);

                lastAccessToken = response.AccessToken;
            }

            if (!string.IsNullOrEmpty(response.RefreshToken))
            {
                txtOidcRefreshTokenCode.Text = response.RefreshToken;
                txtOidcRefreshTokenCodeParsed.Text = ParseJwt(response.RefreshToken);
            }

            btnOidcProfileCode.Enabled = true;
            btnOidcSignoutCode.Enabled = true;
        }

        private async void btnOidcProfileCode_Click(object sender, EventArgs e)
        {
            // discover endpoints from metadata
            //var disco = DiscoveryClient.GetAsync("http://localhost:5001").Result;
            //var client = new UserInfoClient(disco.EndSessionEndpoint);

            // request token
            var client = new UserInfoClient(Preferences.UserInfoEndpoint);
            UserInfoResponse response = await client.GetAsync(txtOidcAccessTokenCode.Text);

            if (!string.IsNullOrEmpty(response.Raw))
            {
                txtOidcProfileCode.Text = response.Raw;
            }
            //var client = new TokenClient(Preferences.TokenEndpoint, "code.oidc.client", txtClientSecretCode.Text);
        }

        private async void btnOidcSignoutCode_Click(object sender, EventArgs e)
        {
            var disco = DiscoveryClient.GetAsync("http://localhost:5001").Result;
            var client = new TokenRevocationClient(disco.RevocationEndpoint, txtoidcClientIdCode.Text, "secret");
            TokenRevocationResponse response = await client.RevokeAccessTokenAsync(txtOidcAccessTokenCode.Text);
            if (!string.IsNullOrEmpty(response.Raw))
            {
                txtOidcSignoutCode.Text = response.Raw;
            }
        }

        private async void btnOidcTokenHybrid_Click(object sender, EventArgs e)
        {
            var request = new AuthorizeRequest(Preferences.AuthorizationEndpoint);

            var url = request.CreateAuthorizeUrl(
                clientId: txtOidcClientIdHybrid.Text,
                responseType: OidcConstants.ResponseTypes.CodeIdTokenToken,
                responseMode: OidcConstants.ResponseModes.FormPost,
                scope: txtOidcScopesHybrid.Text,
                redirectUri: txtOidcRedirectUriHybrid.Text,
                state: CryptoRandom.CreateUniqueId(),
                nonce: CryptoRandom.CreateUniqueId());

            LaunchBrowser(url);

            Tuple<string, string, string> tuple = await GetResponseFromAuthorizationServer(txtOidcRedirectUriHybrid.Text + "/", "hybrid");

            txtOidcCodeHybrid.Text = tuple.Item1;
            txtOidcIdTokenHybrid.Text = tuple.Item2;
            txtOidcIdTokenHybridParsed.Text = ParseJwt(tuple.Item2);
            txtOidcAccessTokenHybrid.Text = tuple.Item3;
            txtOidcAccessTokenHybridParsed.Text = ParseJwt(tuple.Item3);

            lastAccessToken = txtOidcAccessTokenHybrid.Text;

            btnOidcGetProfileHybrid.Enabled = true;
        }

        private async void btnOidcGetProfileHybrid_Click(object sender, EventArgs e)
        {
            var client = new UserInfoClient(Preferences.UserInfoEndpoint);
            UserInfoResponse response = await client.GetAsync(txtOidcAccessTokenHybrid.Text);

            if (!string.IsNullOrEmpty(response.Raw))
            {
                txtOidcProfileHybrid.Text = response.Raw;
            }
        }
        #endregion

        #region Call API

        private async void btnCallApi_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lastAccessToken))
            {
                txtCallApi.Text = await this.CallAPI(lastAccessToken);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an authorization code from the IdP
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="scopes">The scopes.</param>
        /// <param name="redirectUri">The redirect uri.</param>
        /// <returns></returns>
        private async Task<string> GetCode(string clientId, string scopes, string redirectUri)
        {
            var state = Guid.NewGuid().ToString("N");
            var nonce = Guid.NewGuid().ToString("N");

            var request = new AuthorizeRequest(Preferences.AuthorizationEndpoint);

            var url = request.CreateAuthorizeUrl(
                clientId: clientId,
                responseType: "code",
                scope: scopes,
                redirectUri: redirectUri,
                state: state,
                nonce: nonce);

            LaunchBrowser(url);

            Tuple<string, string, string> tuple = await GetResponseFromAuthorizationServer(redirectUri + "/", "code");

            return tuple.Item1;
        }

        /// <summary>
        /// Launch the browser to login with the Idp
        /// </summary>
        /// <param name="url">The URL of the IdP</param>
        private void LaunchBrowser(string url)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(url);
            Process.Start(sInfo);
        }

        /// <summary>
        /// Opens a listener to a URL and wait for the Idp to send its response (code) after the login process
        /// </summary>
        /// <param name="url">The URL to lister to.</param>
        /// <param name="grant">If code or implicit flow are used</param>
        /// <returns></returns>
        private async Task<Tuple<string, string, string>> GetResponseFromAuthorizationServer(string url, string grant)
        {
            Tuple<string, string, string> tuple = new Tuple<string, string, string>(null, null, null);

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();

            HttpListenerContext ctx = await listener.GetContextAsync();

            if (grant == "code")
            {
                string rscode = ctx.Request.QueryString["code"];
                string rsscope = ctx.Request.QueryString["scope"];
                string rsstate = ctx.Request.QueryString["state"];

                tuple = new Tuple<string, string, string>(rscode, rsscope, rsstate);
            }
            else if (grant == "hybrid")
            {
                var request = ctx.Request;

                string text;
                using (var reader = new StreamReader(request.InputStream,
                                                     request.ContentEncoding))
                {
                    text = reader.ReadToEnd();
                }

                string rscode = text.Substring(0, text.IndexOf("&")).Replace("code=","");
                text = text.Substring(text.IndexOf("&") + 1);
                string rsidToken = text.Substring(0, text.IndexOf("&")).Replace("id_token=", "");
                text = text.Substring(text.IndexOf("&") + 1);
                string rsaccessToken = text.Substring(0, text.IndexOf("&")).Replace("access_token=", "");

                tuple = new Tuple<string, string, string>(rscode, rsidToken, rsaccessToken);
            }
            else
            {
                //string rstoken = ctx.Request.QueryString["access_token"];

                tuple = new Tuple<string, string, string>(null, null, null);
            }

            listener.Stop();

            return tuple;
        }

        /// <summary>
        /// Parse the JWT token to show the claims
        /// </summary>
        /// <param name="token">The JWT token to parse.</param>
        /// <returns></returns>
        private string ParseJwt(string token)
        {
            if (token == null || !token.Contains("."))
            {
                return token;
            }

            var parts = token.Split('.');
            var part = Encoding.UTF8.GetString(Base64Url.Decode(parts[1]));

            var jwt = JObject.Parse(part);
            return jwt.ToString();
        }

        /// <summary>
        /// Method to call the secure API
        /// </summary>
        /// <param name="accessToken"></param>
        private async Task<string> CallAPI(string accessToken)
        {
            // call api
            var client = new HttpClient();
            client.SetBearerToken(accessToken);

            var response = await client.GetAsync("http://localhost:5500/api/identity");

            if (!response.IsSuccessStatusCode)
            {
                return "Error Occured: " + response.StatusCode;
            }

            var content = response.Content.ReadAsStringAsync().Result;

            return content;
        }

        #endregion

    }
}
