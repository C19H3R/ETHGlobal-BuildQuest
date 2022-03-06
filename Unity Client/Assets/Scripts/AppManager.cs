using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//Moralis
using MoralisWeb3ApiSdk;
using Moralis.Platform.Objects;

//WalletConnect
using WalletConnectSharp.Core.Models;
using WalletConnectSharp.Unity;

public class AppManager : MonoBehaviour
{
    public MoralisController moralisController;
    public WalletConnect walletConnect;
    public GameObject AuthenticationButton;
    public GameObject qrMenu;

    public GameObject connectPanel;
    public GameObject HomePagePanel;
    public TextMeshProUGUI walletAddress;
    // Start is called before the first frame update
    async void Start()
    {
        connectPanel.SetActive(true);
        HomePagePanel.SetActive(false);
        qrMenu.SetActive(false);


        if (moralisController != null)
        {
            await moralisController.Initialize();
        }
        else
        {
            Debug.LogError("Moralis controller not found");
        }
        if (!MoralisInterface.IsLoggedIn())
        {
            AuthenticationButton.SetActive(true);
        }

    }
    public void Play()
    {
        AuthenticationButton.SetActive(false);

        // If the user is still logged in just show game.
        if (MoralisInterface.IsLoggedIn())
        {
            Debug.Log("User is already logged in to Moralis.");
            UserLoggedInHandler();
        }
        // User is not logged in, depending on build target, begin wallect connection.
        else
        {
            Debug.Log("User is not logged in.");
            qrMenu.SetActive(true);
        }
    }

    public async void WalletConnectHandler(WCSessionData data)
    {
        Debug.Log("Wallet connection recieved");
        Debug.Log(data);
        string address = data.accounts[0].ToLower();
        string appId = MoralisInterface.GetClient().ApplicationId;
        long serverTime = 0;
        walletAddress.text = address;
        // Retrieve server time from Moralis Server for message signature
        Dictionary<string, object> serverTimeResponse = await MoralisInterface.GetClient().Cloud.RunAsync<Dictionary<string, object>>("getServerTime", new Dictionary<string, object>());

        if (serverTimeResponse == null || !serverTimeResponse.ContainsKey("dateTime") ||
            !long.TryParse(serverTimeResponse["dateTime"].ToString(), out serverTime))
        {
            Debug.Log("Failed to retrieve server time from Moralis Server!");
        }

        Debug.Log($"Sending sign request for {address} ...");

        string signMessage = $"Moralis Authentication\n\nId: {appId}:{serverTime}";
        string response = await walletConnect.Session.EthPersonalSign(address, signMessage);

        Debug.Log($"Signature {response} for {address} was returned.");

        // Create moralis auth data from message signing response.
        Dictionary<string, object> authData = new Dictionary<string, object> { { "id", address }, { "signature", response }, { "data", signMessage } };

        Debug.Log("Logging in user.");

        // Attempt to login user.
        MoralisUser user = await MoralisInterface.LogInAsync(authData);

        if (user != null)
        {
            Debug.Log($"User {user.username} logged in successfully. ");
            //infoText.text = "Logged in successfully!";
        }
        else
        {
            Debug.Log("User login failed.");
            //infoText.text = "Login failed";
        }

        UserLoggedInHandler();
    }
    public async void Quit()
    {
        Debug.Log("QUIT");

        // Disconnect wallet subscription.
        await walletConnect.Session.Disconnect();
        // CLear out the session so it is re-establish on sign-in.
        walletConnect.CLearSession();
        // Logout the Moralis User.
        await MoralisInterface.LogOutAsync();
        // Close out the application.

    }




    /// <summary>
    /// Must be referenced by the WalletConnect Game object
    /// </summary>
    /// <param name="session"></param>
    public void WalletConnectSessionEstablished(WalletConnectUnitySession session)
    {
        InitializeWeb3();
    }

    /// <summary>
    /// Must be referenced by the WalletConnect Game object
    /// </summary>
    public void WalletConnectConnected()
    {
        InitializeWeb3();
    }

    public void GoToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    private void InitializeWeb3()
    {
        MoralisInterface.SetupWeb3();
    }

    private async void UserLoggedInHandler()
    {
        var user = await MoralisInterface.GetUserAsync();

        if (user != null)
        {
            connectPanel.SetActive(false);
            HomePagePanel.SetActive(true);
        }
    }

}
