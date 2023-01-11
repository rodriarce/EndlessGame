using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;




public class PlayFabRegister : MonoBehaviour
{
    [Header("PlayFab TitleId")]
    private string titleId = "60751";

    [Header("Input Register")]
    public TMP_InputField userNameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPassword;
    public TMP_InputField emailInput;
    
    [Header("input Login")]
    public TMP_InputField emailLogin;
    public TMP_InputField passwordLogin;
    public Toggle toggleOption;
    private string userName;
    private string passwordText;
    private string emailText;
    [Header("Text Error")]
    public TextMeshProUGUI textErrorRegister;
    public TextMeshProUGUI textErrorLogin;
    public TextMeshProUGUI textSuccesLogin;
    [Header("Panels")]
    public GameObject registerPanel;
    public GameObject loginPanel;
    public GameObject succesLogin;
    public GameObject panelErrorRegister;
    public GameObject panelErrorLogin;
    public GameObject succesRegister;
    private bool isWithPlayerPref;
    public string nameScene;
    public Button buttonLogin;
    
    



    private void Awake()
    {
        
    }
    private void Start()
    {
        buttonLogin.interactable = false;
        if (PlayerPrefs.HasKey("Email"))
        {
           
            LoginStartUser();
        }
        else
        {
            succesLogin.SetActive(false);
        }

        
    }

    // Update is called once per frame
    private void Update()
    {

    }
    public void LoginStartUser()
    {
        isWithPlayerPref = true;

      
            if ((PlayerPrefs.HasKey("Email")) && (PlayerPrefs.HasKey("Password")))
            {
                textSuccesLogin.text = "Connecting..";
                registerPanel.SetActive(false);
                buttonLogin.interactable = false;
                LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();                
                
                request.Username = PlayerPrefs.GetString("Email");
                request.Password = PlayerPrefs.GetString("Password");              
                
                emailLogin.text = PlayerPrefs.GetString("Email");// Set UserName And Password Text
                passwordLogin.text = PlayerPrefs.GetString("Password");
                PlayFabClientAPI.LoginWithPlayFab(request, OnLoginResult, OnLoginError);


            }           
       


    }
    public void LoadScene()
    {
        SceneManager.LoadScene("GamePlay");
    }
    private void OnLoginResult(LoginResult result)
    {
        succesLogin.SetActive(true);
        //succesLogin.GetComponentInChildren<TextMeshProUGUI>().text = "Succes Login";
       
        Debug.Log("Login Succes");
        GetPlayerStats();

    }


    public void GetPlayerStats()
    {
        GetPlayerStatisticsRequest request = new GetPlayerStatisticsRequest();
        request.StatisticNames = new List<string>() { "Points" };
        PlayFabClientAPI.GetPlayerStatistics(request, OnResultGetStats, error => { Debug.Log("Error Get Player Stats"); });
        
        //LoadScene();

    }

    private void OnResultGetStats(GetPlayerStatisticsResult result)
    {
        if (result.Statistics.Count > 0)
        {
            foreach (var stat in result.Statistics)
            {
                if (stat.StatisticName == "Points")
                {
                    DataUser.amountPoints = stat.Value;
                }
            }
        }
        //LoadScene();
        textSuccesLogin.text = "Ready!";
        buttonLogin.interactable = true;
        
    }



    private void OnLoginError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }


    public void RegisterUser()
    {
             
             

        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
        request.TitleId = titleId;
        request.Username = userNameInput.text;
        request.Password = passwordInput.text;
        request.RequireBothUsernameAndEmail = false;
        request.DisplayName = userNameInput.text;
        userName = userNameInput.text;// Set Variable Names And Password
        passwordText = passwordInput.text;
        emailText = emailInput.text;
              
        PlayFabClientAPI.RegisterPlayFabUser(request, SuccesRegister, ErrorRegister);
        
    }
    private void SuccesRegister(RegisterPlayFabUserResult result)
    {

        Debug.Log("Player Register");
        if (isWithPlayerPref)
        {
            
            PlayerPrefs.SetString("Password", passwordText);
            PlayerPrefs.SetString("UserName", emailText);

        }
        succesRegister.SetActive(true);
        //succesRegister.GetComponentInChildren<TextMeshProUGUI>().text = "Succes Sign In";

       
    }



    private void ErrorRegister(PlayFabError error)
    {
        
        textErrorRegister.text = "";
        panelErrorRegister.SetActive(true);
        var newError = error.ErrorDetails;

        if (newError == null)
        {
            textErrorRegister.text = "Empty user name or password";
        }
        foreach (var myKey in newError)
        {
            Debug.Log(myKey.Key);

            switch (myKey.Key)
            {
                case "Username":
                    myKey.Value.ForEach(detail =>
                    {

                        textErrorRegister.text += detail + "\n";
                        Debug.Log(detail);
                    });
                    return;
                case "Password":
                    myKey.Value.ForEach(detail =>
                    {

                        textErrorRegister.text += detail + "\n";
                        Debug.Log(detail);
                    });
                    return;
                case "Email":
                    myKey.Value.ForEach(detail =>
                    {

                        textErrorRegister.text += detail + "\n";
                        Debug.Log(detail);
                    });
                    return;

            }
                 

        }
    }

    public void OnClickLogin()
    {
        succesLogin.SetActive(true);
        textSuccesLogin.text = "Connecting..";
        buttonLogin.interactable = false;
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
        request.Username = emailLogin.text;
        userName = emailLogin.text;
        request.Password = passwordLogin.text;
        passwordText = passwordLogin.text;
        PlayFabClientAPI.LoginWithPlayFab(request, OnResultClick, ErrorLogin);
    }

    private void OnResultClick(LoginResult result)
    {
       

    PlayerPrefs.SetString("Email", userName);
    PlayerPrefs.SetString("Password", passwordText);


        //succesLogin.SetActive(true);
        //succesLogin.GetComponentInChildren<TextMeshProUGUI>().text = "Succes Login";
        GetPlayerStats();
        //buttonLogin.interactable = true;
        Debug.Log("Login Succes");
    }

    public void LogOut()
    {
        PlayerPrefs.DeleteAll();
        emailLogin.text = "";
        passwordLogin.text = "";
        succesLogin.SetActive(true);
        succesLogin.GetComponentInChildren<TextMeshProUGUI>().text = "Log Out Succes";
    }


    private void ErrorLogin(PlayFabError error)
    {

        textErrorLogin.text = "";
        panelErrorLogin.SetActive(true);
        if (error.ErrorMessage == "User not found")
        {
            textErrorLogin.text = "Wrong User Name Or Password";
            return;
        }
        
        var newError = error.ErrorDetails;
        if (newError == null)
        {
            textErrorLogin.text = "Wrong User Name or Password";
            return;
        }
        foreach (var myKey in newError)
        {
             myKey.Value.ForEach(detail =>
            {
                textErrorLogin.text += detail + "\n";
                Debug.Log(detail);

            });

        }


    }
   

}
