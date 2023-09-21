using System;
using System.Runtime.InteropServices;
using UnityEngine;

[Serializable]
public class VKPlayWebEventArgsBase : EventArgs
{
    /// <summary>
    /// "ok" если операция успешна.
    /// </summary>
    public string status;

    /// <summary>
    /// Внутренний код ошибки партнера.
    /// </summary>
    public int errcode;

    /// <summary>
    /// Описание кода ошибки партнера.
    /// </summary>
    public string errmsg;
}

[Serializable]
public class GetLoginStatusCallbackData : VKPlayWebEventArgsBase
{
    /// <summary>
    /// 0 - пользователь не авторизован;
    /// <br/>
    /// 1 - пользователь авторизован, не зарегистрирован;
    /// <br/>
    /// 2 - пользователь авторизован, зарегистрирован;
    /// <br/>
    /// 3 - пользователь авторизован, зарегистрирован, и совершил покупку игры
    /// (только для премиум и платных игр с поддержкой внутриигровых транзакций).
    /// </summary>
    public int loginStatus;
}

[Serializable]
public class UserInfoCallbackData : VKPlayWebEventArgsBase
{
    /// <summary>
    /// ID пользователя на платформе.
    /// </summary>
    public int uid;

    /// <summary>
    /// MD5 хэш от почты пользователя.
    /// </summary>
    public string hash;
}

[Serializable]
public class UserProfileCallbackData : VKPlayWebEventArgsBase
{
    /// <summary>
    /// ID пользователя на платформе;
    /// </summary>
    public int uid;

    /// <summary>
    /// Ник пользователя;
    /// </summary>
    public string nick;

    /// <summary>
    /// Ссылка (URL) на изображение для аватара пользователя;
    /// </summary>
    public string avatar;

    /// <summary>
    /// Год рождения пользователя;
    /// </summary>
    public string birthyear;

    /// <summary>
    /// Пол пользователя, "male" или "female";
    /// </summary>
    public string sex;

    /// <summary>
    /// Уникальный строковый идентификатор пользователя.
    /// </summary>
    public string slug;
}

[Serializable]
public class PaymentFrameUrlCallbackData : VKPlayWebEventArgsBase
{
    /// <summary>
    /// Ссылка (URL) на платежный фрейм.
    /// </summary>
    public string url;
}

[Serializable]
public class GetAuthTokenCallbackData : VKPlayWebEventArgsBase
{
    /// <summary>
    /// ID пользователя в платформе;
    /// </summary>
    public int uid;

    /// <summary>
    /// Авторизационный токен.
    /// </summary>
    public string hash;
}

[Serializable]
public class PaymentReceivedCallbackData : VKPlayWebEventArgsBase
{
    /// <summary>
    /// ID пользователя в платформе;
    /// </summary>
    public int uid;
}

[Serializable]
public class PaymentWindowClosedCallbackData : VKPlayWebEventArgsBase
{
    // Пусто.
}

[Serializable]
public class ConfirmWindowClosedCallbackData : VKPlayWebEventArgsBase
{
    // Пусто.
}

[Serializable]
public class ApiInitCallbackData : VKPlayWebEventArgsBase
{
    // Пусто.
}

[Serializable]
public class AdsCallbackData : VKPlayWebEventArgsBase
{
    /// <summary>
    /// "adCompleted" - реклама просмотрена;
    /// <br/>
    /// "adDismissed" - реклама пропущена или отсутствует;
    /// <br/>
    /// "adError" - реклама не была показана вследствие возникшей ошибки.
    /// </summary>
    public string type;

    /// <summary>
    /// "UndefinedAdError" - ошибка показа рекламы;
    /// <br/>
    /// "AdblockDetectedAdError" - обнаружен блокировщик рекламы;
    /// <br/>
    /// "WaterfallConfigLoadFailed" - ошибка показа рекламы.
    /// </summary>
    public string code;
}

[Serializable]
public class VKPlayWebAdsConfig
{
    /// <summary>
    /// "admanSource,admanagerSource" ИЛИ
    /// <br/>
    /// "admanSource" ИЛИ
    /// <br/>
    /// "admanagerSource" ИЛИ
    /// <br/>
    /// ничего (null), будет выбран лучший источник рекламы.
    /// </summary>
    public string sources;

    public bool interstitial;
}

[Serializable]
public class UserSocialFriendsCallbackDataFriend
{
    public string nick;
    public int online;
    public string slug;
    public string avatar;
}

[Serializable]
public class UserSocialFriendsCallbackData : VKPlayWebEventArgsBase
{
    public UserSocialFriendsCallbackDataFriend[] friends;
}

[Serializable]
public class UserFriendsCallbackDataFriend
{
    public int uid;
    public string nick;
    public string slug;
    public string avatar;
}

[Serializable]
public class UserFriendsCallbackData : VKPlayWebEventArgsBase
{
    public UserFriendsCallbackDataFriend[] friends;
}

[Serializable]
public class VKPlayWebMerchantParamBase
{
    public int amount;
    public string description;
    public string currency;
    // наследуйте от этого класса чтобы добавить доп. опции...
}

[Serializable]
public class VKPlayPaymentFrameArgs
{
    public string scenario;
    public VKPlayWebMerchantParamBase merchant_param;
}

[Serializable]
public class VKPlayPaymentFrameItemArgs
{
    public string ids;
    public object merchant_param;

    public VKPlayPaymentFrameItemArgs(int[] idsIntArray)
    {
        var s = "";
        for (int i = 0, l = idsIntArray.Length; i < l; ++i)
        {
            s += idsIntArray[i].ToString();
            if (i < l - 1)
            {
                s += ",";
            }
        }

        ids = s;
    }
}

public class VKPlayWeb : MonoBehaviour
{
    private static GameObject instance_;
    private static VKPlayWeb componentInstance_;

    public static VKPlayWeb instance
    {
        get
        {
            if (instance_ == null && componentInstance_ == null)
            {
                instance_ = new GameObject("VKPlayWeb");
                componentInstance_ = instance_.AddComponent<VKPlayWeb>();
            }

            return componentInstance_;
        }
    }

    void Awake()
    {
        var i = instance;
        if (i != null && i != this)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
    }

    // functions
    [DllImport("__Internal")]
    private static extern void VKPlayWeb_Init(int gmrIdInt, string gameObjectName);
    public void init(int gmrIdInt) => VKPlayWeb_Init(gmrIdInt, gameObject.name);

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_GetLoginStatus();
    public void getLoginStatus() => VKPlayWeb_GetLoginStatus();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_RegisterUser();
    public void registerUser() => VKPlayWeb_RegisterUser();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_GetAuthToken();
    public void getAuthToken() => VKPlayWeb_GetAuthToken();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_AuthUser();
    public void authUser() => VKPlayWeb_AuthUser();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_UserProfile();
    public void userProfile() => VKPlayWeb_UserProfile();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_UserInfo();
    public void userInfo() => VKPlayWeb_UserInfo();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_ReloadWindow();
    public void reloadWindow() => VKPlayWeb_ReloadWindow();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_ShowAds(string adsConfigJsonString);
    public void showAds(VKPlayWebAdsConfig adsConfig) => VKPlayWeb_ShowAds(JsonUtility.ToJson(adsConfig));

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_UserFriends();
    public void userFriends() => VKPlayWeb_UserFriends();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_UserSocialFriends();
    public void userSocialFriends() => VKPlayWeb_UserSocialFriends();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_GetGameInventoryItems();
    public void getGameInventoryItems() => VKPlayWeb_GetGameInventoryItems();

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_PaymentFrame(string argsJsonString);
    public void paymentFrame(VKPlayPaymentFrameArgs args) => VKPlayWeb_PaymentFrame(JsonUtility.ToJson(args));

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_PaymentFrameItem(string argsJsonString);
    public void paymentFrameItem(VKPlayPaymentFrameItemArgs args) => VKPlayWeb_PaymentFrameItem(JsonUtility.ToJson(args));

    [DllImport("__Internal")]
    private static extern void VKPlayWeb_PaymentFrameUrl(string argsJsonString);
    public void paymentFrameUrl(VKPlayPaymentFrameArgs args) => VKPlayWeb_PaymentFrameUrl(JsonUtility.ToJson(args));

    // callbacks
    public event EventHandler<ApiInitCallbackData> apiInitCallback;
    public void ThrowApiInit(string a) =>
        apiInitCallback?.Invoke(this, JsonUtility.FromJson<ApiInitCallbackData>(a));

    public event EventHandler<GetLoginStatusCallbackData> getLoginStatusCallback;
    public void ThrowGetLoginStatus(string a) =>
        getLoginStatusCallback?.Invoke(this, JsonUtility.FromJson<GetLoginStatusCallbackData>(a));

    public event EventHandler<UserInfoCallbackData> userInfoCallback;
    public void ThrowUserInfo(string a) =>
        userInfoCallback?.Invoke(this, JsonUtility.FromJson<UserInfoCallbackData>(a));

    public event EventHandler<UserProfileCallbackData> userProfileCallback;
    public void ThrowUserProfile(string a) =>
        userProfileCallback?.Invoke(this, JsonUtility.FromJson<UserProfileCallbackData>(a));

    public event EventHandler<UserInfoCallbackData> registerUserCallback;
    public void ThrowRegisterUser(string a) =>
        registerUserCallback?.Invoke(this, JsonUtility.FromJson<UserInfoCallbackData>(a));

    public event EventHandler<PaymentFrameUrlCallbackData> paymentFrameUrlCallback;
    public void ThrowPaymentFrameUrl(string a) =>
        paymentFrameUrlCallback?.Invoke(this, new PaymentFrameUrlCallbackData() { status = "ok", url = a });

    public event EventHandler<GetAuthTokenCallbackData> getAuthTokenCallback;
    public void ThrowGetAuthToken(string a) =>
        getAuthTokenCallback?.Invoke(this, JsonUtility.FromJson<GetAuthTokenCallbackData>(a));

    public event EventHandler<PaymentReceivedCallbackData> paymentReceivedCallback;
    public void ThrowPaymentReceived(string a) =>
        paymentReceivedCallback?.Invoke(this, JsonUtility.FromJson<PaymentReceivedCallbackData>(a));

    public event EventHandler<PaymentWindowClosedCallbackData> paymentWindowClosedCallback;
    public void ThrowPaymentWindowClosed() =>
        paymentWindowClosedCallback?.Invoke(this, new PaymentWindowClosedCallbackData { status = "ok" });

    public event EventHandler<ConfirmWindowClosedCallbackData> confirmWindowClosedCallback;
    public void ThrowConfirmWindowClosed() =>
        confirmWindowClosedCallback?.Invoke(this, new ConfirmWindowClosedCallbackData { status = "ok" });

    public event EventHandler<AdsCallbackData> adsCallback;
    public void ThrowAds(string a) =>
        adsCallback?.Invoke(this, JsonUtility.FromJson<AdsCallbackData>(a));

    public event EventHandler<UserFriendsCallbackData> userFriendsCallback;
    public void ThrowUserFriends(string a) =>
        userFriendsCallback?.Invoke(this, JsonUtility.FromJson<UserFriendsCallbackData>(a));

    public event EventHandler<UserSocialFriendsCallbackData> userSocialFriendsCallback;
    public void ThrowUserSocialFriends(string a) =>
        userSocialFriendsCallback?.Invoke(this, JsonUtility.FromJson<UserSocialFriendsCallbackData>(a));
}
