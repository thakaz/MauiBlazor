using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazor.Shared.Helper.Auth;

public class MyAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly I組織Repository _組織Repository;

    private bool _isAuthenticated
    {
        get
        {
            return _claimsPrincipal?.Identity?.IsAuthenticated ?? false;
        }
    }

    private ClaimsPrincipal _claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

    private Task<AuthenticationState> _authenticationState;

    public MyAuthenticationStateProvider(I組織Repository 組織Repository)
    {
        _組織Repository = 組織Repository;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            if (_isAuthenticated)
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

    public async Task<bool> ValidateLogin(string 組織コード, string password)
    {
        //ひとまず組織にログイン
        var 組織 = await _組織Repository.GetBy組織コードAsync(組織コード) ?? throw new Exception("組織が見つかりませんでした");

        //組織のパスワードと入力されたパスワードが一致するか
        if (組織.パスワード == password)
        {
            NotifyUserAuthentication(組織.組織コード);
            return true;
        }

        return false;

    }

    public void NotifyUserAuthentication(string 組織コード)
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, 組織コード),
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
