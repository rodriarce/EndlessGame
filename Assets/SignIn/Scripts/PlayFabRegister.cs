using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class PlayFabRegister : MonoBehaviour
{
    [Header("PlayFab TitleId")]
    public string titleId;

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
    [Header("Panels")]
    public GameObject registerPanel;
    public GameObject loginPanel;
    public GameObject succesLogin;
    public GameObject panelErrorRegister;
    public GameObject panelErrorLogin;
    public GameObject succesRegister;
    private bool isWithPlayerPref;
    public string nameScene;
    
    



    private void Awake()
    {
        
    }
    private void Start()
    {
        LoginStartUser();

    }

    // Update is called once per frame
    private void Update()
    {

    }
    public void LoginStartUser()
    {
        isWithPlayerPref = true;

        if (isWithPlayerPref)
        {
            if ((PlayerPrefs.HasKey("Email")) && (PlayerPrefs.HasKey("Password")))
            {
                registerPanel.SetActive(false);
                LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
                
                request.Username = PlayerPrefs.GetString("Email");
                request.Password = PlayerPrefs.GetString("Password");              
                
                emailLogin.text = PlayerPrefs.GetString("Email");// Set UserName And Password Text
                passwordLogin.text = PlayerPrefs.GetString("Password");
                PlayFabClientAPI.LoginWithPlayFab(request, OnLoginResult, OnLoginError);


            }
            
        }


    }
    private void OnLoginResult(LoginResult result)
    {
        succesLogin.SetActive(true);
        succesLogin.GetComponentInChildren<TextMeshProUGUI>().text = "Succes Login";
        Debug.Log("Login Succes");
        

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
        succesRegister.GetComponentInChildren<TextMeshProUGUI>().text = "Succes Sign In";

       
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
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
        request.Username = emailLogin.text;
        userName = emailLogin.text;
        request.Password = passwordLogin.text;
        passwordText = passwordLogin.text;
        PlayFabClientAPI.LoginWithPlayFab(request, OnResultClick, ErrorLogin);
    }

    private void OnResultClick(LoginResult result)
    {
       

        if (isWithPlayerPref)
        {
            PlayerPrefs.SetString("Email", userName);
            PlayerPrefs.SetString("Password", passwordText);
        }
        
        succesLogin.SetActive(true);
        succesLogin.GetComponentInChildren<TextMeshProUGUI>().text = "Succes Login";
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


    public void LoadScene(string nameScene)
    {
        if (nameScene == "")
        {
            return;
        }
        else
        {
            SceneManager.LoadScene(nameScene);
        }

    }

}
