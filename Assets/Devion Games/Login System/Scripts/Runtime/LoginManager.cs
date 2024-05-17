using DevionGames.LoginSystem.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;

namespace DevionGames.LoginSystem
{
    public class LoginManager : MonoBehaviour
    {
		private static LoginManager m_Current;
		UserDatabaseManager userDatabaseManager;

		/// <summary>
		/// The LoginManager singleton object. This object is set inside Awake()
		/// </summary>
		public static LoginManager current
		{
			get
			{
				Assert.IsNotNull(m_Current, "Requires Login Manager.Create one from Tools > Devion Games > Login System > Create Login Manager!");
				return m_Current;
			}
		}

		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		private void Awake()
		{
			if (LoginManager.m_Current != null)
			{
				if (LoginManager.DefaultSettings.debug)
					Debug.Log("Multiple LoginManager in scene...this is not supported. Destroying instance!");
				Destroy(gameObject);
				return;
			}
			else
			{
				LoginManager.m_Current = this;
				if(LoginManager.DefaultSettings.debug)
					Debug.Log("LoginManager initialized.");

			}
		}

        private void Start()
        {
			if (LoginManager.DefaultSettings.skipLogin)
			{
				if (LoginManager.DefaultSettings.debug)
					Debug.Log("Login System is disabled...Loading "+ LoginManager.DefaultSettings.sceneToLoad+" scene.");
				UnityEngine.SceneManagement.SceneManager.LoadScene(LoginManager.DefaultSettings.sceneToLoad);
			}
			userDatabaseManager = UserDatabaseManager.Instance;
		}


        [SerializeField]
		private LoginConfigurations m_Configurations = null;

		/// <summary>
		/// Gets the login configurations. Configurate it inside the editor.
		/// </summary>
		/// <value>The database.</value>
		public static LoginConfigurations Configurations
		{
			get
			{
				if (LoginManager.current != null)
				{
					Assert.IsNotNull(LoginManager.current.m_Configurations, "Please assign Login Configurations to the Login Manager!");
					return LoginManager.current.m_Configurations;
				}
				return null;
			}
		}


		private static Default m_DefaultSettings;
		public static Default DefaultSettings
		{
			get
			{
				if (m_DefaultSettings == null)
				{
					m_DefaultSettings = GetSetting<Default>();
				}
				return m_DefaultSettings;
			}
		}

		private static UI m_UI;
		public static UI UI
		{
			get
			{
				if (m_UI == null)
				{
					m_UI = GetSetting<UI>();
				}
				return m_UI;
			}
		}

		private static Notifications m_Notifications;
		public static Notifications Notifications
		{
			get
			{
				if (m_Notifications == null)
				{
					m_Notifications = GetSetting<Notifications>();
				}
				return m_Notifications;
			}
		}

		private static Server m_Server;
		public static Server Server
		{
			get
			{
				if (m_Server == null)
				{
					m_Server = GetSetting<Server>();
				}
				return m_Server;
			}
		}

		private static T GetSetting<T>() where T : Configuration.Settings
		{
			if (LoginManager.Configurations != null)
			{
				return (T)LoginManager.Configurations.settings.Where(x => x.GetType() == typeof(T)).FirstOrDefault();
			}
			return default(T);
		}



		public static void CreateAccount(string username, string password, string email)
			{
				if (LoginManager.current != null)
				{
					LoginManager.current.StartCoroutine(LoginManager.current.CreateAccountInternal(username, password));
				}
			}



		private IEnumerator CreateAccountInternal(string id, string password)
		{
			
			yield return StartCoroutine(userDatabaseManager.SaveNewUser(id, password));

			bool saveSuccessful = userDatabaseManager.CurrentSaveStatus;

			if (saveSuccessful)
			{
				Debug.Log("User saved successfully!");
				userDatabaseManager.setCurrentUser(id);
				EventHandler.Execute("OnLogin");

			}
			else
			{
				Debug.LogError("Failed to save user!");
			}
		}



		/// <summary>
		/// Logins the account.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="password">Password.</param>
		public static void LoginAccount(string username, string password)
		{
			if (LoginManager.current != null)
			{
				LoginManager.current.StartCoroutine(LoginManager.current.LoginAccountInternal(username, password));
			}
		}
		

		private IEnumerator LoginAccountInternal(string username, string password)
		{
			if (LoginManager.current != null)
			{
                User user = userDatabaseManager.ConnectUser(username, password);
				if (user != null)
				{
					PlayerPrefs.SetString(LoginManager.Server.accountKey, username);
					if (LoginManager.DefaultSettings.debug)
						Debug.Log("[LoginAccount] Login was successful!");
					userDatabaseManager.setCurrentUser(username);
					EventHandler.Execute("OnLogin");
					yield break;
				}
				else
				{
					if (LoginManager.DefaultSettings.debug){
						Debug.Log("[LoginAccount] Failed to login.");
					}
					EventHandler.Execute("OnFailedToLogin");
				}
			}
			yield return null;
		}

		/// <summary>
		/// Recovers the password.
		/// </summary>
		/// <param name="email">Email.</param>
		public static void RecoverPassword(string email)
		{
			if (LoginManager.DefaultSettings.debug)
				Debug.Log("[RecoverPassword] Fonctionality not available yet");
		}

		/// <summary>
		/// Resets the password.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="password">Password.</param>
		public static void ResetPassword(string username, string password)
		{
			if (LoginManager.DefaultSettings.debug){
				Debug.Log("[ResetPassword] Fonctionality not available yet");
			}
		}

		/// <summary>
		/// Validates the email.
		/// </summary>
		/// <returns><c>true</c>, if email was validated, <c>false</c> otherwise.</returns>
		/// <param name="email">Email.</param>
		public static bool ValidateEmail(string email)
		{
			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
			System.Text.RegularExpressions.Match match = regex.Match(email);
			if (match.Success)
			{
				if (LoginManager.DefaultSettings.debug)
					Debug.Log("Email validation was successfull for email: " + email + "!");
			}
			else
			{
				if (LoginManager.DefaultSettings.debug)
					Debug.Log("Email validation failed for email: " + email + "!");
			}

			return match.Success;
		}
	}
}