const oidcConfig = {
    authority: process.env.REACT_APP_IDENTITY_URL,
    client_id: process.env.REACT_APP_IDENTITY_CLIENT_ID,
    redirect_uri: process.env.REACT_APP_REDIRECT_URL,
    response_type: "code",
    scope: "openid profile scope2",
    post_logout_redirect_uri: process.env.REACT_APP_REDIRECT_URL,
    response_mode: "query",
}

export default oidcConfig
