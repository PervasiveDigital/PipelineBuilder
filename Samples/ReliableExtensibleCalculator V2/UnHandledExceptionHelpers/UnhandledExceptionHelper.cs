using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.AddIn.Hosting;
using System.Runtime.Serialization.Formatters.Binary;

namespace ExtensibleCalculator
{
    
    public class UnhandledExceptionHelper 
    {
        private static string s_cachePath = "unreliableTokens.tokens";
       
        public static void LogUnhandledExceptions(object addin)
        {
            AddInController controller = AddInController.GetAddInController(addin);
            AddInToken token = controller.Token;
            AppDomain domain = controller.AppDomain;
            RemoteExceptionHelper helper = (RemoteExceptionHelper)domain.CreateInstanceFromAndUnwrap(typeof(RemoteExceptionHelper).Assembly.Location, typeof(RemoteExceptionHelper).FullName);
            helper.Init(token);
        }
      
        internal static void AddTokenToUnreliableList(AddInToken token)
        {
            List<AddInToken> tokens = GetUnreliableTokens();
            tokens.Add(token);
            WriteUnreliableTokens(tokens);           
        }

        public static List<AddInToken> GetUnreliableTokens()
        {
            BinaryFormatter f = new BinaryFormatter();
            if (File.Exists(s_cachePath))
            {
                return (List<AddInToken>)f.Deserialize(File.OpenRead(s_cachePath));
            }
            else
            {
                return new List<AddInToken>();
            }
        }

        static void WriteUnreliableTokens(List<AddInToken> tokens)
        {
            BinaryFormatter f = new BinaryFormatter();
            f.Serialize(File.OpenWrite(s_cachePath), tokens);
        }


    }

    internal class RemoteExceptionHelper : MarshalByRefObject
    {
        AddInToken _token;
      
        internal void Init(AddInToken token)
        {
            _token = token;
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            UnhandledExceptionHelper.AddTokenToUnreliableList(_token);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }

   
}
