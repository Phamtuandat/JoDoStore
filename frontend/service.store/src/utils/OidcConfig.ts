import { OidcClientSettings } from "oidc-client"

const oidcConfig: OidcClientSettings = {
    authority: process.env.REACT_APP_IDENTITY_URL,
    client_id: process.env.REACT_APP_IDENTITY_CLIENT_ID,
    redirect_uri: process.env.REACT_APP_REDIRECT_URL,
    response_type: "code",
    scope: "openid profile scope2",
    post_logout_redirect_uri: process.env.REACT_APP_REDIRECT_URL,
    response_mode: "query",
}
/* /src/utils/authConst.js */

export const IDENTITY_CONFIG = {
    authority: process.env.REACT_APP_IDENTITY_URL, //(string): The URL of the OIDC provider.
    client_id: process.env.REACT_APP_IDENTITY_CLIENT_ID, //(string): Your client application's identifier as registered with the OIDC provider.
    redirect_uri: process.env.REACT_APP_REDIRECT_URL, //The URI of your client application to receive a response from the OIDC provider.
    login: process.env.REACT_APP_IDENTITY_URL + "/login",
    automaticSilentRenew: false, //(boolean, default: false): Flag to indicate if there should be an automatic attempt to renew the access token prior to its expiration.
    loadUserInfo: false, //(boolean, default: true): Flag to control if additional identity data is loaded from the user info endpoint in order to populate the user's profile.
    silent_redirect_uri: process.env.REACT_APP_REDIRECT_URL, //(string): The URL for the page containing the code handling the silent renew.
    post_logout_redirect_uri: process.env.REACT_APP_REDIRECT_URL, // (string): The OIDC post-logout redirect URI.
    audience: "https://example.com", //is there a way to specific the audience when making the jwt
    responseType: "id_token token", //(string, default: 'id_token'): The type of response desired from the OIDC provider.
    grantType: "password",
    scope: "openid example.api", //(string, default: 'openid'): The scope being requested from the OIDC provider.
    webAuthResponseType: "id_token token",
}

export const METADATA_OIDC = {
    issuer: "https://identityserver",
    jwks_uri: process.env.REACT_APP_IDENTITY_URL + "/.well-known/openid-configuration/jwks",
    authorization_endpoint: process.env.REACT_APP_IDENTITY_URL + "/connect/authorize",
    token_endpoint: process.env.REACT_APP_IDENTITY_URL + "/connect/token",
    userinfo_endpoint: process.env.REACT_APP_IDENTITY_URL + "/connect/userinfo",
    end_session_endpoint: process.env.REACT_APP_IDENTITY_URL + "/connect/endsession",
    check_session_iframe: process.env.REACT_APP_IDENTITY_URL + "/connect/checksession",
    revocation_endpoint: process.env.REACT_APP_IDENTITY_URL + "/connect/revocation",
    introspection_endpoint: process.env.REACT_APP_IDENTITY_URL + "/connect/introspect",
}
export default oidcConfig
