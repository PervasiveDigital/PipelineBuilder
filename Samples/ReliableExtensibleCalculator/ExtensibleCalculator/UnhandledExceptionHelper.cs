using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.AddIn.Hosting;
using Calculators.Extensibility;
using System.Runtime.Serialization.Formatters.Binary;

namespace ExtensibleCalculator
{
    
    internal class UnhandledExceptionHelper : MarshalByRefObject
    {
        private static string s_cachePath = "unreliableTokens.tokens";
        private static Dictionary<AppDomain, AddInToken> s_domains;
        private static bool s_initialized = false;



        public UnhandledExceptionHelper(Calculator calc)
        {
            if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
            {
                throw new InvalidOperationException("This implementation can only be used in the default AppDomain");
            }

            if (!s_initialized)
            {
                s_domains = new Dictionary<AppDomain, AddInToken>();
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                
            }
            AddInController controller = AddInController.GetAddInController(calc);
            s_domains.Add(controller.AppDomain, controller.Token);              

        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            AppDomain source = (AppDomain)sender;
            AddInToken token;
            if (s_domains.TryGetValue(source,out token))
            {
                AddTokenToUnreliableList(token);    
            }
          
            
        }

      
        void AddTokenToUnreliableList(AddInToken token)
        {
            List<AddInToken> tokens = GetUnreliableTokens();
            tokens.Add(token);
            WriteUnreliableTokens(tokens);           
        }

        internal static List<AddInToken> GetUnreliableTokens()
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

   
}
