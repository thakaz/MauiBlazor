using System.Security.Claims;

namespace MauiBlazor.Shared.Helper.Auth;

public class MyAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly I組織Repository _組織Repository;

    private bool IsAuthenticated
    {
        get
        {
            return _claimsPrincipal?.Identity?.IsAuthenticated ?? false;
        }
    }

    private ClaimsPrincipal _claimsPrincipal = new(new ClaimsIdentity());

    private Task<AuthenticationState> _authenticationState;

    public MyAuthenticationStateProvider(I組織Repository 組織Repository)
    {
        _組織Repository = 組織Repository;
        _authenticationState = Task.FromResult(new AuthenticationState(_claimsPrincipal));
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            if (IsAuthenticated)
            {
                return Task.FromResult(new AuthenticationState(_claimsPrincipal));
            }
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
        }
        catch (Exception)
        {
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));

            throw new NotImplementedException();
        }
    }

    public async Task<(bool isSuccess, string errorMessage)> ValidateLogin(string 組織コード, string password)
    {
        // ひとまず組織にログイン
        var 組織 = await _組織Repository.GetBy組織コードAsync(組織コード);
        if (組織 == null)
        {
            return (false, "組織が見つかりませんでした");
        }

        // 組織のパスワードと入力されたパスワードが一致するか
        if (PasswordHasher.VerifyPassword(password, 組織.パスワード ?? ""))
        {
            await NotifyUserAuthentication(組織.組織コード);
            return (true, string.Empty);
        }

        return (false, "パスワードが正しくありません");
    }


    public async Task NotifyUserAuthentication(string 組織コード)
    {

        //Roleはis管理組織で判定
        var role = ((await _組織Repository.GetBy組織コードAsync(組織コード))?.Is管理組織 ?? false) ? "admin" : "user";


        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, 組織コード),
            new Claim(ClaimTypes.Role, role),
        }, "apiauth");
        _claimsPrincipal = new ClaimsPrincipal(identity);
        _authenticationState = Task.FromResult(new AuthenticationState(_claimsPrincipal));

        NotifyAuthenticationStateChanged(_authenticationState);
    }


    public void NotifyUserLogout()
    {
        var identity = new ClaimsIdentity();
        _claimsPrincipal = new ClaimsPrincipal(identity);
        _authenticationState = Task.FromResult(new AuthenticationState(_claimsPrincipal));
        NotifyAuthenticationStateChanged(_authenticationState);
    }

    public string Get組織コード()
    {
        return _claimsPrincipal?.Identity?.Name ?? "";
    }



}
