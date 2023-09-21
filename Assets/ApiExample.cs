using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApiExample : MonoBehaviour
{
    private VKPlayWeb api;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("before everything");

        api = VKPlayWeb.instance;

        var gmrId = 1; // <<< заменить на свой!!!!

        api.apiInitCallback += (sender, args) =>
        {
            Debug.Log(args);

            if (args.status == "ok")
            {
                api.getLoginStatus();
            }
        };

        api.getLoginStatusCallback += (sender, args) =>
        {
            Debug.Log(args);
            
            if (args.loginStatus == 0)
            {
                api.authUser();
            }
            else if (args.loginStatus == 1)
            {
                api.registerUser();
            }
            else if (args.loginStatus == 2)
            {
                api.showAds(new VKPlayWebAdsConfig() { interstitial = false });
            }
        };

        Debug.Log("before api init");

        api.init(gmrId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
